using System;
using System.Web.Mvc;
using StationeryShop.Models;
using System.Linq;
using StationeryShop.Security;

namespace StationeryShop.Areas.Admin.Controllers
{
	[IsAdmin]
	public class DashboardController : Controller
	{
		private DBContext db = new DBContext();

		// GET: Admin/Dashboard
		public ActionResult Index()
		{
			ViewBag.orderCount = db.orders.Count();
			ViewBag.customerCount = db.customers.Count();
			ViewBag.productCount = db.products.Count();
			ViewBag.totalSale = db.orders.Sum(o => o.order_details.Sum(od => od.price * od.quantity));
			ViewBag.newOrders = db.orders.OrderByDescending(o => o.order_date).Take(5).ToList();
			ViewBag.newCustomers = db.customers.OrderByDescending(c => c.id).Take(5).ToList();
			return View();
		}

		public ActionResult SaleData(int? year, int? month)
		{
			int SumFn(order o) => o.order_details.Sum(od => od.price * od.quantity);
			if (year.HasValue && month.HasValue)
			{
				var dayCount = DateTime.DaysInMonth(year.Value,month.Value);
				var saleData = (from o in db.orders.AsEnumerable()
						where o.order_date.Year == year && o.order_date.Month == month
						group o by o.order_date
						into day
						select new {day = day.Key.Day, sale = day.Sum(SumFn)})
					.ToList();
				var dailySaleData = Enumerable.Repeat(0, dayCount).ToArray();
				saleData.ForEach(sd => dailySaleData[sd.day - 1] = sd.sale);
				return Json(new {dayCount, data = dailySaleData}, JsonRequestBehavior.AllowGet);
			}
			if (year.HasValue)
			{
				var saleData = (from o in db.orders.AsEnumerable()
					where o.order_date.Year == year
					group o by o.order_date.Month
					into m
					select new {month = m.Key, sale = m.Sum(SumFn)}).ToList();
				var monthlySaleData = Enumerable.Repeat(0, 12).ToArray();
				saleData.ForEach(sd => monthlySaleData[sd.month-1] = sd.sale);
				return Json(new {data = monthlySaleData},JsonRequestBehavior.AllowGet);
			}
			else
			{
				var saleData = (from o in db.orders.AsEnumerable()
					group o by o.order_date.Year
					into y
					select new
					{
						year = y.Key, sale = y.Sum(SumFn)
					}).ToList();
				var minY = saleData.Min(d => d.year);
				var maxY = saleData.Max(d => d.year);
				var yearCount = maxY - minY + 1;
				var yearlySaleData = Enumerable.Repeat(0, yearCount).ToArray();
				saleData.ForEach(sd=>yearlySaleData[sd.year-minY]=sd.sale);
				return Json(new {years = Enumerable.Range(minY, yearCount).ToArray(), data = yearlySaleData},JsonRequestBehavior.AllowGet);
			}
		}
	}
}