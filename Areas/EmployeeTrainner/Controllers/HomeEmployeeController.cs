using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB_MANGE_COURCE.Areas.EmployeeTrainner.Controllers
{
    public class HomeEmployeeController : Controller
    {
        // GET: EmployeeTrainner/Employee
        public ActionResult Index()
        {
            return View();
        }
    }
}