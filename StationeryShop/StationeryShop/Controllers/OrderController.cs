using StationeryShop.Models;
using StationeryShop.Security;
using System;
using System.Linq;
using System.Web.Mvc;

namespace StationeryShop.Controllers
{
	public class OrderController : Controller
	{
		private DBContext db = new DBContext();
		[Authenticate]
		public ActionResult Address()
		{
			var id = (long)Session["userId"];
			var user = db.customers.Find(id);
			order o = new order {address=user.address,name=user.name,tel=user.tel };
			return View(o);
		}
		[HttpPost]
		[Authenticate]
		public ActionResult AddAddress(order o)
		{
			var id = (long)Session["userId"];
			var carts = db.carts.Where(c => c.cus_id == id).ToList();
			ViewBag.carts = carts;
			ViewBag.total = carts.Sum(c => c.total);
			TempData["order"] = o;
			return View("Payment", o);
		}
		[HttpPost]
		[Authenticate]
		public ActionResult Finish(FormCollection f)
		{
			order o = (order)TempData["order"];
			var id = (long)Session["userId"];
			var carts = db.carts.Where(c => c.cus_id == id).ToList();
			var newOrder = new order
			{
				address = o.address,
				name = o.name,
				tel = o.tel,
				cus_id = id,
				notes = f["notes"],
				order_date = DateTime.Now,
				status = order.Status.PENDING,
				order_details = carts.Select(c => new order_details { prod_id = c.prod_id, price = c.product.PromoPrice, quantity = c.quantity }).ToList()
			};
			db.orders.Add(newOrder);
			db.SaveChanges();
			db.carts.RemoveRange(db.carts.Where(c => c.cus_id == id));
			db.SaveChanges();
			return Redirect("/Account/Orders");
		}
	}
}