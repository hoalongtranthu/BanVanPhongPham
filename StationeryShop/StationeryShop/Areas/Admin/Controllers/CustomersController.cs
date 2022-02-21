using StationeryShop.Models;
using StationeryShop.Security;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace StationeryShop.Areas.Admin.Controllers
{
	[IsAdmin]
	public class CustomersController : Controller
	{
		private DBContext db = new DBContext();

		// GET: Admin/Customers
		public ActionResult Index(string name, string id)
		{
			var customer = db.customers.AsQueryable();
			if (!string.IsNullOrEmpty(id))
			{
				long cid = long.Parse(id);
				customer = customer.Where(p => p.id == cid);
				ViewBag.id = id.ToString();
			}
			if (!string.IsNullOrEmpty(name))
			{
				customer = customer.Where(p => p.name.Contains(name));
				ViewBag.name = name;
			}
			if (customer.ToList().Count == 0)
			{
				ViewBag.msg = "Không tìm thấy sản phẩm!";
				customer = db.customers.AsQueryable();
				customer = customer.OrderBy(p => p.id);
				return View(customer);
			}
			customer = customer.OrderBy(p => p.id);
			return View(customer);
		}

		// GET: Admin/Customers/Details/5
		public ActionResult Details(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			customer customer = db.customers.Find(id);
			if (customer == null)
			{
				return HttpNotFound();
			}
			return View(customer);
		}

		// GET: Admin/Customers/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Admin/Customers/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "id,email,password,name,tel,address")] customer customer)
		{
			if (ModelState.IsValid)
			{
				db.customers.Add(customer);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(customer);
		}

		// GET: Admin/Customers/Edit/5
		public ActionResult Edit(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			customer customer = db.customers.Find(id);
			if (customer == null)
			{
				return HttpNotFound();
			}
			return View(customer);
		}

		// POST: Admin/Customers/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "id,email,password,name,tel,address")] customer customer)
		{
			if (ModelState.IsValid)
			{
				db.Entry(customer).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(customer);
		}

		// GET: Admin/Customers/Delete/5
		//public ActionResult Delete(long? id)
		//{
		//    if (id == null)
		//    {
		//        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		//    }
		//    customer customer = db.customers.Find(id);
		//    if (customer == null)
		//    {
		//        return HttpNotFound();
		//    }
		//    return View(customer);
		//}
		public ActionResult Delete(long id)
		{
			db.customers.Remove(db.customers.Find(id));
			db.SaveChanges();
			return RedirectToAction("Index");
		}


		// POST: Admin/Customers/Delete/5
		//[HttpPost, ActionName("Delete")]
		//[ValidateAntiForgeryToken]
		//public ActionResult DeleteConfirmed(long id)
		//{
		//    customer customer = db.customers.Find(id);
		//    db.customers.Remove(customer);
		//    db.SaveChanges();
		//    return RedirectToAction("Index");
		//}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
