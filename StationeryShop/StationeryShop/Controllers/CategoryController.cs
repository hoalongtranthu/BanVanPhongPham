using StationeryShop.Models;
using System.Web.Mvc;
using System.Linq;
using PagedList;

namespace StationeryShop.Controllers
{
	public class CategoryController : Controller
	{
		private DBContext db = new DBContext();
		[ChildActionOnly]
		public ActionResult List()
		{
			return PartialView("_Categories",db.categories.ToList());
		}
		public ActionResult Index(long id,int? page)
		{
			var cat = db.categories.Include("products.promotion_details.promotion").FirstOrDefault(c => c.id == id);
			ViewBag.cat = cat;
			return View("Index",cat.products.ToPagedList(page??1,15));
		}
	}
}