using StationeryShop.Models;
using StationeryShop.Security;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace StationeryShop.Areas.Admin.Controllers
{
	[IsAdmin]
	public class CategoryController : Controller
	{
		private DBContext db = new DBContext();

		// GET: Admin/Category
		public ActionResult Index()
		{
			ViewBag.res = true;
			return View(db.categories.ToList());
		}

		// GET: Admin/Category/Details/5
		public ActionResult Details(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			category category = db.categories.Find(id);
			if (category == null)
			{
				return HttpNotFound();
			}
			return View(category);
		}

		// GET: Admin/Category/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Admin/Category/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		[ValidateInput(false)]
		public ActionResult Create([Bind(Include = "id,name,description")] category category)
		{
			if (ModelState.IsValid)
			{
				db.categories.Add(category);
				db.SaveChanges();
				ViewBag.res = true;
				return RedirectToAction("Index");
			}

			return View(category);
		}

		// GET: Admin/Category/Edit/5
		public ActionResult Edit(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			category category = db.categories.Find(id);
			if (category == null)
			{
				return HttpNotFound();
			}
			return View(category);
		}

		// POST: Admin/Category/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		[ValidateInput(false)]
		public ActionResult Edit([Bind(Include = "id,name,description")] category category)
		{
			if (ModelState.IsValid)
			{

				db.Entry(category).State = EntityState.Modified;
				db.SaveChanges();
				ViewBag.res = true;
				return RedirectToAction("Index");
			}
			return View(category);
		}

		// GET: Admin/Category/Delete/5
		public ActionResult Delete(long id)
		{
			category category = db.categories.Find(id);
			if (category == null)
			{
				return HttpNotFound();
			}
			if (category.products.Count == 0)
			{
				db.categories.Remove(category);
				db.SaveChanges();
				ViewBag.res = true;
				return RedirectToAction("Index");
			}
				ViewBag.res = false;
				return RedirectToAction("Index");

			
		}
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
