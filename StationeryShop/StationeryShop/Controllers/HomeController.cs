using StationeryShop.Models;
using StationeryShop.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StationeryShop.Controllers
{
	public class HomeController : Controller
	{
		private DBContext db = new DBContext();
		public ActionResult Index()
		{
			ViewBag.proProds = db.products.ToList().Where(p => p.Discount > 0).OrderByDescending(p=>p.Discount).Take(6).ToList();
			ViewBag.topProds = db.products.OrderByDescending(p => p.order_details.Sum(od => od.quantity)).Take(6).ToList();
			ViewBag.newProds = db.products.OrderByDescending(p => p.id).Take(6).ToList();
			return View();
		}
		public ActionResult About()
		{
			return View();
		}

		public ActionResult Contact()
		{
			return View();
		}
		public ActionResult Support()
		{
			return View();
		}
	}
}