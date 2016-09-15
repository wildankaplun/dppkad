using Dppkad.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Dppkad.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!string.IsNullOrEmpty(Session["UserId"] as string))
            {
                filterContext.HttpContext.User =
                    new CustomPrincipal(
                        new CustomIdentity(Session["UserName"] as string));

                base.OnAuthorization(filterContext);
            }

            if (string.IsNullOrEmpty(Session["UserId"] as string))
            {
                var url = new UrlHelper(filterContext.RequestContext);
                var LoginUrl = url.Action("Login", "Account", null);
                filterContext.Result = new RedirectResult(LoginUrl);
            }
        }
    }
}