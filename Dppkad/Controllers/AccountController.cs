using Dppkad.Common;
using Dppkad.DAL;
using Dppkad.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Dppkad.Controllers
{
    /// <summary>
    /// controller for aunthentication
    /// </summary>
    public class AccountController : Controller
    {
        
        private IAccountService service;

        public AccountController(IAccountService service)
        {
            this.service = service;
        }

        //Display Login Screen
        [AllowAnonymous]
        public ActionResult Login()
        {
            ViewBag.Success = TempData["Success"];
            ViewBag.Error = TempData["Error"];
            return this.View();
        }

        //Handle Authentication When Login Form Submitted
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UserDppkadInfo model, string returnUrl)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            string accountId = model.UserName;
            string password = model.UserPassword;

            var result = service.Login(accountId, password);
            if (result != null)
            {
                CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cultureInfo.TextInfo;
                SimpleSessionPersister.Username = result.UserName;
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                TempData["Error"] = "The username or password you entered did not match our records. Please double-check and try again.";
                return RedirectToAction("Login", "Account");
            }
        }

        /// <summary>
        /// Handle Logout Activity.
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult LogOff()
        {
            Session.RemoveAll();
            return this.RedirectToAction("Login", "Account");
        }
    }
}