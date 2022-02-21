using StationeryShop.Models;
using System.Web.Mvc;
using System.Linq;
using System;
using System.Threading;
using StationeryShop.Security;

namespace StationeryShop.Areas.Admin.Controllers
{
	[IsAdmin]
	public class OrderController : Controller
	{
		private DBContext db = new DBContext();
		// GET: Admin/Order
		public ActionResult Index()
		{
			var orders = db.orders.Include("customer").ToList();
			return View(orders);
		}
		public ActionResult Detail(long id)
		{
			var order = db.orders.Include("customer").Include("order_details.product").FirstOrDefault(o => o.id == id);
			return View(order);
		}
		[HttpPost]
		public ActionResult Search(FormCollection f)
		{
			string id = f["id"], name = f["name"], dateRange = f["date"], status = f["status"];
			var orders = db.orders.Include("customer").Include("order_details.product").AsQueryable();
			if (!string.IsNullOrEmpty(id))
			{
				var pid = long.Parse(id);
				orders=orders.Where(o => o.id == pid);
				ViewBag.sId = pid;
			}
			if (!string.IsNullOrEmpty(name))
			{
				orders = orders.Where(o => o.name.Contains(name));
				ViewBag.sName = name;
			}
			if (!string.IsNullOrEmpty(dateRange))
			{
				var dates = dateRange.Split('-');
				var fromDate = Convert.ToDateTime(dates[0]);
				var toDate = Convert.ToDateTime(dates[1]);
				orders = orders.Where(o => fromDate <= o.order_date && o.order_date <= toDate);
				ViewBag.sDate = dateRange;
			}
			if (!string.IsNullOrEmpty(status))
			{
				orders = orders.Where(o => o.status==status);
				ViewBag.sStatus = status;
			}
			return PartialView("OrderTable",orders.ToList());
		}
		[HttpPost]
		public void UpdateStatus(FormCollection f)
		{
			var status = f["status"];
			var id = long.Parse(f["id"]);
			db.orders.Find(id).status = status;
			db.SaveChanges();
		}
		public ActionResult Delete(long id)
		{
			db.orders.Remove(db.orders.Find(id));
			db.SaveChanges();
			return Redirect("/Admin/Order");
		}
	}
}