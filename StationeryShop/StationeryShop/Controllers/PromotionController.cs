using StationeryShop.Models;
using System.Web.Mvc;
using System.Linq;

namespace StationeryShop.Controllers
{
	public class PromotionController : Controller
	{
		private DBContext db = new DBContext();
		public ActionResult All()
		{
			var pros = db.promotions.OrderByDescending(p => p.to_date).ToList();
			return View(pros);
		}
		public ActionResult Index(long id)
		{
			var pro = db.promotions.Find(id);
			return View(pro);
		}
		[ChildActionOnly]
		public ActionResult NewPromotions()
		{
			var pros = db.promotions.OrderByDescending(p => p.to_date).Take(5).ToList();
			return PartialView("_NewPromotions",pros);
		}
	}
}