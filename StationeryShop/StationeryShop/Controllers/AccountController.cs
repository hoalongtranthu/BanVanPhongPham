using StationeryShop.Models;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;
using StationeryShop.Security;

namespace StationeryShop.Controllers
{
	public class AccountController : Controller
	{
		private DBContext db = new DBContext();
		// GET: Account
		[Authenticate]
		public ActionResult Index()
		{
			var user = db.customers.Find(Session["userId"]);
			return View(user);
		}
		[Authenticate]
		public ActionResult Logout()
		{
			Session["userId"] = null;
			return Redirect("/");
		}
		[Authenticate]
		public ActionResult Orders()
		{
			long id = (long)Session["userId"];
			var orders = db.orders.Include(o => o.order_details).Where(o => o.cus_id == id).ToList();
			return View(orders);
		}
		[Authenticate]
		[HttpPost]
		public ActionResult UpdateInfo(customer c)
		{
			var user = db.customers.Find(Session["userId"]);
			user.name = c.name;
			user.address = c.address;
			user.tel = c.tel;
			db.SaveChanges();
			ViewBag.success = true;
			return View("Index",user);
		}
		[Authenticate]
		public ActionResult ChangePass()
		{
			return View();
		}
		[Authenticate]
		[HttpPost]
		public ActionResult UpdatePass(FormCollection f)
		{
			var oldPass = f["old-pass"];
			var newPass = f["new-pass"];
			var user = db.customers.Find(Session["userId"]);
			if (oldPass != user.password)
			{
				ViewBag.error = "Mật khẩu cũ không chính xác!";
			} else
			{
				user.password = newPass;
				db.SaveChanges();
				ViewBag.success = true;
			}
			return View("ChangePass");
		}
		[ChildActionOnly]
		public ActionResult AccountMenu()
		{
			var user = db.customers.Find(Session["userId"]);
			return PartialView(user);
		}
		public ActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public ActionResult DoLogin(FormCollection f)
		{
			string email = f["email"], pass = f["password"];
			var user = db.customers.Where(u => u.email == email && u.password == pass).FirstOrDefault();
			if (user == null)
			{
				return Json(new { success=false},JsonRequestBehavior.AllowGet);
			}
			Session["userId"] = user.id;
			return Json(new { success = true }, JsonRequestBehavior.AllowGet);
		}
		public ActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public ActionResult DoRegister(customer c)
		{
			var user = db.customers.Where(u => u.email == c.email).FirstOrDefault();
			if (user != null)
			{
				return Json(new { success=false},JsonRequestBehavior.AllowGet);
			}
			db.customers.Add(c);
			db.SaveChanges();
			return Json(new { success = true}, JsonRequestBehavior.AllowGet);
		}
	}
}