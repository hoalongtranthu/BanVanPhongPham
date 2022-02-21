using PagedList;
using StationeryShop.Models;
using StationeryShop.Security;
using System;
using System.Linq;
using System.Web.Mvc;
namespace StationeryShop.Areas.Admin.Controllers
{
	[IsAdmin]
	public class ProductController : Controller
	{
		DBContext db = new DBContext();
		// GET: Admin/Product
		public ActionResult Index(string name, string id, int? page)
		{
			int pageSize = 10;
			int pageNumber = (page ?? 1);
			var product = db.products.AsQueryable();
			if (!string.IsNullOrEmpty(id))
			{
				long pid = long.Parse(id);
				product = product.Where(p => p.id == pid);
				ViewBag.id = id.ToString();
			}
			if (!string.IsNullOrEmpty(name))
			{
				product = product.Where(p => p.name.Contains(name));
				ViewBag.name = name;
			}
			if (product.ToList().Count == 0)
			{
				ViewBag.msg = "Không tìm thấy sản phẩm!";
				product = db.products.AsQueryable();
				product = product.OrderBy(p => p.id);
				return View(product.ToPagedList(pageNumber, pageSize));
			}
			product = product.OrderBy(p => p.id);
			return View(product.ToPagedList(pageNumber, pageSize));
		}
		public ActionResult Detail(long id)
		{
			var detail = db.products.FirstOrDefault(d => d.id == id);
			return View(detail);
		}
		public ActionResult Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Create(FormCollection f)
		{
			try
			{
				product p = new product();
				var image = Request.Files["avt"];
				if (image != null && image.ContentLength > 0)
				{
					string fileName = System.IO.Path.GetFileName(image.FileName);
					string imgpath = Server.MapPath("~/Content/images/" + fileName);
					image.SaveAs(imgpath);
					p.image = imgpath;
				}
				p.cat_id = long.Parse(f["cat_id"]);
				p.name = f["name"];
				p.origin = f["origin"];
				p.price = int.Parse(f["price"]);
				p.amount = int.Parse(f["amount"]);
				p.unit = f["unit"];
				p.description = f["description"];
				if (db.products.Find(p.id) == null)
				{
					db.products.Add(p);
					db.SaveChanges();
					ViewBag.Error = "";
					return RedirectToAction("Index");
				}
				else
				{
					ViewBag.Error = "Sản phẩm đã tồn tại";
					return View(p);
				}
			}
			catch (Exception e)
			{
				ViewBag.Error = "Lỗi" + e.Message;
				return View("Create");
			}
		}
		public ActionResult Edit(long id)
		{
			var product = db.products.FirstOrDefault(p => p.id == id);
			return View(product);
		}
		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Edit(product p)
		{
			try
			{
				var image = Request.Files["avt"];
				if (image != null && image.ContentLength > 0)
				{
					string fileName = System.IO.Path.GetFileName(image.FileName);
					string imagepath = Server.MapPath("~/Content/images/" + fileName);
					image.SaveAs(imagepath);
					p.image = imagepath;
				}
				db.Entry(p).State = System.Data.Entity.EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			catch (Exception e)
			{
				ViewBag.Error = "lỗi nhập dữ liệu" + e.Message;
				return View(p);
			}
		}
		public ActionResult Delete(long id)
		{
			var pro = db.promotion_details.Where(p => p.prod_id == id).ToList();
			db.promotion_details.RemoveRange(pro);
			db.SaveChanges();
			var cart = db.carts.Where(c => c.prod_id == id).ToList();
			db.carts.RemoveRange(cart);
			db.SaveChanges();
			var od = db.order_details.Where(o => o.prod_id == id).ToList();
			db.order_details.RemoveRange(od);
			db.SaveChanges();
			db.products.Remove(db.products.Find(id));
			db.SaveChanges();
			return RedirectToAction("Index");
		}

	}
}