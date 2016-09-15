using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dppkad.DAL;
using Dppkad.Models;
using System.Linq.Expressions;

namespace Dppkad.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IAdminService _service;

        public AdminController(IAdminService service)
        {
            _service = service;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}