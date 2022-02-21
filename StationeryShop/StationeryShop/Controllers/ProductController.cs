using StationeryShop.Models;
using System.Web.Mvc;
using System.Linq;
using System.Text;
using System.Globalization;
using PagedList;

namespace StationeryShop.Controllers
{
	public class ProductController : Controller
	{
		private DBContext db = new DBContext();
		public ActionResult Index(long id)
		{
			var prod = db.products.Find(id);
			ViewBag.relatedProds = db.products.Where(p => p.cat_id == prod.cat_id && p.id != id).Take(6).ToList();
			return View(prod);
		}
		private string normalize(string s)
		{
			s = s.Normalize(NormalizationForm.FormD).ToLower().Trim();
			var sb = new StringBuilder();
			foreach(var c in s)
			{
				if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
				{
					sb.Append(c);
				}
			}
			return sb.ToString().Normalize(NormalizationForm.FormC);
		}
		public ActionResult Search(string q,int? page)
		{
			q = normalize(q??"");
			var prods = db.products.ToList().Where(p => normalize(p.name).Contains(q)).ToPagedList(page ?? 1, 15);
			return View(prods);
		}
	}
}