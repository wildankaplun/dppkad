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
            if (!string.IsNullOrEmpty(SimpleSessionPersister.Username))
            {
                filterContext.HttpContext.User =
                    new CustomPrincipal(
                        new CustomIdentity(SimpleSessionPersister.Username));

                base.OnAuthorization(filterContext);
            }

            if (string.IsNullOrEmpty(SimpleSessionPersister.Username))
            {
                var url = new UrlHelper(filterContext.RequestContext);
                var LoginUrl = url.Action("Login", "Account", null);
                filterContext.Result = new RedirectResult(LoginUrl);
            }
        }
    }
}