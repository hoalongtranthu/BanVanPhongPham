using StationeryShop.Models;
using System.Linq;
using System.Web.Mvc;

namespace StationeryShop.Areas.Admin.Controllers
{
	public class AdminAccountController : Controller
	{
		private DBContext db = new DBContext();
		// GET: Admin/AdminAccount
		[HttpGet]
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(string username, string password)
		{
			var usr = username;
			var pw = password;
			var acc = db.admins.SingleOrDefault(x => x.username == usr && x.password == pw);
			if (acc != null)
			{
				Session["admin"] = acc;
				return RedirectToAction("Index", "Dashboard");
			}
			else if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
			{
				ViewBag.Message = "Chưa điền tên đăng nhập hoặc mật khẩu !";
			}
			else
			{
				ViewBag.Message = "Sai tên đăng nhập hoặc mật khẩu !";
			}
			return View();
		}
		public ActionResult Logout()
		{
			Session["admin"] = null;
			return RedirectToAction("Login", "AdminAccount");
		}
	}
}