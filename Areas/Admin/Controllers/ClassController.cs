using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_MANGE_COURCE.Models;

namespace WEB_MANGE_COURCE.Areas.Admin.Controllers
{
    public class ClassController : Controller
    {
        private ma_scschedulesEntities1 db = new ma_scschedulesEntities1();
        // GET: Admin/Class
        public ActionResult Index()
        {
            var user = Session["user"];
            if (user != null)
            {
                if (user is WEB_MANGE_COURCE.Models.Admin admin && admin.ro_id == 1)
                {
                    var teachers = db.Teachers.ToList();
                    ViewBag.Teachers = new SelectList(teachers, "teacher_id", "username");
                    return View();
                }
                else if (user is WEB_MANGE_COURCE.Models.Employee employ && employ.ro_id == 3)
                {
                    var teachers = db.Teachers.ToList();
                    ViewBag.Teachers = new SelectList(teachers, "teacher_id", "username");
                    return View();
                }
                else
                {
                    return RedirectToAction("Error", "Error", new {area="Admin"});
                }
            }
            return RedirectToAction("Login", "HomeStudent", new { area = "" });
                   
        }
        [HttpPost]
        public ActionResult Index(Class data)
        {
            var user = Session["user"];
            if (user != null)
            {
                if (user is WEB_MANGE_COURCE.Models.Admin admin && admin.ro_id == 1)
                {
                    if (ModelState.IsValid)
                    {
                        // Thực hiện lưu danh mục vào cơ sở dữ liệu
                        // Ví dụ:
                        db.Classes.Add(data);
                        db.SaveChanges();

                        // Sau khi thêm thành công, chuyển hướng về trang danh sách danh mục
                        return RedirectToAction("Index", "Class");
                    }

                    // Nếu dữ liệu không hợp lệ, hiển thị lại biểu mẫu với thông báo lỗi
                    return View(data);
                }else if (user is WEB_MANGE_COURCE.Models.Employee employ && employ.ro_id == 3)
                {
                    if (ModelState.IsValid)
                    {
                        // Thực hiện lưu danh mục vào cơ sở dữ liệu
                        // Ví dụ:
                        db.Classes.Add(data);
                        db.SaveChanges();

                        // Sau khi thêm thành công, chuyển hướng về trang danh sách danh mục
                        return RedirectToAction("Index", "Class");
                    }
                    // Nếu dữ liệu không hợp lệ, hiển thị lại biểu mẫu với thông báo lỗi
                    return View(data);
                }
                else
                {
                    return RedirectToAction("Error", "Error", new { area = "Admin" });
                }
            }
            return RedirectToAction("Login", "HomeStudent", new {area=""});
                  
        }
        public ActionResult list()
        {
            var user = Session["user"];
            if (user != null)
            {
                if (user is WEB_MANGE_COURCE.Models.Admin admin && admin.ro_id == 1)
                {
                    var list = db.Classes.ToList();
                    return View(list);
                }
                else if (user is WEB_MANGE_COURCE.Models.Employee employ && employ.ro_id == 3) {
                    var list = db.Classes.ToList();
                    return View(list);
                }
                else
                {
                    return RedirectToAction("Error", "Error", new { area = "Admin" });
                }
            }
            return RedirectToAction("Login", "HomeStudent", new { area = "" });
        }
    }
}