using StationeryShop.Models;
using System.Web.Mvc;
using System.Linq;
using StationeryShop.Security;

namespace StationeryShop.Controllers
{
	public class CartController : Controller
	{
		private DBContext db = new DBContext();
		[Authenticate]
		public ActionResult Index()
		{
			var id = (long)Session["userId"];
			var carts = db.carts.Include("product").Where(c => c.cus_id == id).ToList();
			ViewBag.total = carts.Sum(c => c.total);
			return View(carts);
		}
		[ChildActionOnly]
		public ActionResult CartCount()
		{
			var id = (long)Session["userId"];
			var count = db.carts.Where(c => c.cus_id == id).Count();
			return PartialView("_CartCount",count);
		}
		[Authenticate]
		public ActionResult Add(long id)
		{
			var cusId = (long)Session["userId"];
			var cart = db.carts.Find(cusId, id);
			if (cart == null)
			{
				cart = new cart { cus_id = cusId, prod_id = id,quantity=1 };
				db.carts.Add(cart);
			} else
			{
				cart.quantity++;
			}
			db.SaveChanges();
			return Redirect("/Cart");
		}
		[Authenticate]
		[HttpPost]
		public void ChangeQuantity(long id,FormCollection f)
		{
			var newQtt = int.Parse(f["qtt"]);
			var cusId = (long)Session["userId"];
			var cart = db.carts.Find(cusId, id);
			cart.quantity = newQtt;
			db.SaveChanges();
		}
		[Authenticate]
		public ActionResult Delete(long id)
		{
			var cusId = (long)Session["userId"];
			var cart = db.carts.Find(cusId, id);
			db.carts.Remove(cart);
			db.SaveChanges();
			return Redirect("/Cart");
		}
	}
}