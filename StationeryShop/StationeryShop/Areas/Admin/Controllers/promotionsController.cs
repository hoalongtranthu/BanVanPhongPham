using PagedList;
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
	public class promotionsController : Controller
	{
		private DBContext db = new DBContext();

		// GET: Admin/promotions
		public ActionResult Index(string name, string id, int? page)
		{
			int pageSize = 10;
			int pageNumber = (page ?? 1);
			var promotion = db.promotions.AsQueryable();
			if (!string.IsNullOrEmpty(id))
			{
				long pid = long.Parse(id);
				promotion = promotion.Where(p => p.id == pid);
				ViewBag.id = id.ToString();
			}
			if (!string.IsNullOrEmpty(name))
			{
				promotion = promotion.Where(p => p.name.Contains(name));
				ViewBag.name = name;
			}
			if (promotion.ToList().Count == 0)
			{
				ViewBag.msg = "Không tìm thấy khuyến mãi!";
				promotion = db.promotions.AsQueryable();
				promotion = promotion.OrderBy(p => p.id);
				return View(promotion.ToPagedList(pageNumber, pageSize));
			}
			promotion = promotion.OrderBy(p => p.id);
			return View(promotion.ToPagedList(pageNumber, pageSize));
		}

		// GET: Admin/promotions/Details/5
		public ActionResult Details(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			promotion promotion = db.promotions.Find(id);
			if (promotion == null)
			{
				return HttpNotFound();
			}
			return View(promotion);
		}

		// GET: Admin/promotions/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Admin/promotions/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		[ValidateInput(false)]
		public ActionResult Create([Bind(Include = "id,name,from_date,to_date,description,content,image")] promotion promotion)
		{
			if (ModelState.IsValid)
			{
				db.promotions.Add(promotion);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(promotion);
		}

		// GET: Admin/promotions/Edit/5
		public ActionResult Edit(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			promotion promotion = db.promotions.Find(id);
			if (promotion == null)
			{
				return HttpNotFound();
			}
			return View(promotion);
		}

		// POST: Admin/promotions/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "id,name,from_date,to_date,description,content,image")] promotion promotion)
		{
			if (ModelState.IsValid)
			{
				db.Entry(promotion).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(promotion);
		}

		// GET: Admin/promotions/Delete/5
		public ActionResult Delete(long? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			promotion promotion = db.promotions.Find(id);
			if (promotion == null)
			{
				return HttpNotFound();
			}
			return View(promotion);
		}

		// POST: Admin/promotions/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(long id)
		{
			db.promotion_details.RemoveRange(db.promotion_details.Where(x => x.prom_id == id));
			promotion promotion = db.promotions.Find(id);
			db.promotions.Remove(promotion);
			db.SaveChanges();
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
