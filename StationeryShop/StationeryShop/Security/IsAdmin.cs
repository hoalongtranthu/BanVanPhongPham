using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace StationeryShop.Security
{
	public class IsAdmin : ActionFilterAttribute, IAuthenticationFilter
	{
		public void OnAuthentication(AuthenticationContext filterContext)
		{
			if (filterContext.HttpContext.Session["admin"] == null)
			{
				filterContext.Result = new HttpUnauthorizedResult();
			}
		}

		public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
		{
			if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
			{
				filterContext.Result = new RedirectResult("/Admin/AdminAccount/Login");
			}
		}
	}
}