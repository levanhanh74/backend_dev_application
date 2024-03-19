using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEB_MANGE_COURCE.Areas.Teacher.Controllers
{
    public class HomeTeacherController : Controller
    {
        // GET: Teacher/HomeTeacher
        public ActionResult Index()
        {
            return View();
        }
    }
}