using System.Web;
using System.Web.Optimization;

namespace StationeryShop
{
	public class BundleConfig
	{
		// For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/Content/js").Include(
									"~/Content/js/bootstrap.min.js"));
			bundles.Add(new StyleBundle("~/Content/css").Include(
								"~/Content/css/bootstrap.min.css",
								"~/Content/css/style.css"));
		}
	}
}
