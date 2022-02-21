using System.Web.Mvc;

namespace StationeryShop.Areas.Admin
{
	public class AdminAreaRegistration : AreaRegistration
	{
		public override string AreaName
		{
			get
			{
				return "Admin";
			}
		}

		public override void RegisterArea(AreaRegistrationContext context)
		{
			context.MapRoute(
					"Admin_default",
					"Admin/{controller}/{action}/{id}",
					new { action = "Index",controller="Dashboard", id = UrlParameter.Optional },
					new string[] { "StationeryShop.Areas.Admin.Controllers" }
			);
		}
	}
}