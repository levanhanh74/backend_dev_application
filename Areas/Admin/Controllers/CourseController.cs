using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_MANGE_COURCE.Models;

namespace WEB_MANGE_COURCE.Areas.Admin.Controllers
{
    public class CourseController : Controller
    {
        private ma_scschedulesEntities1 db = new ma_scschedulesEntities1();
        // GET: Admin/Course
        public ActionResult course()
        {
            var user = Session["user"];
            if (user != null)
            {
                if (user is WEB_MANGE_COURCE.Models.Admin admin && admin.ro_id == 1)
                {
                    var categories = db.Categories.ToList();
                    ViewBag.CategoryList = new SelectList(categories, "category_id", "cate_name");
                    return View();
                }else 
                {
                    return RedirectToAction("Error", "Error", new { area = "" });
                }
            }
            return RedirectToAction("Login", "HomeStudent", new { area = "" });
              
        }
        [HttpPost]
        public ActionResult course(Cours course)
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
                        db.Courses.Add(course);
                        db.SaveChanges();

                        // Sau khi thêm thành công, chuyển hướng về trang danh sách danh mục
                        return RedirectToAction("course", "Course");
                    }
                    // Nếu dữ liệu không hợp lệ, hiển thị lại biểu mẫu với thông báo lỗi
                    return View(course);
                }
                else
                {
                    return RedirectToAction("Error", "Error", new { area = "" });
                }
            }
            return RedirectToAction("Login", "HomeStudent", new { area = "" });
                 
        }
        public ActionResult list()
        {
            var user = Session["user"];
            if (user != null)
            {
                if (user is WEB_MANGE_COURCE.Models.Admin admin && admin.ro_id == 1)
                {
                    var list = db.Courses.ToList();
                    return View(list);
                }
                else if(user is WEB_MANGE_COURCE.Models.Employee employ && employ.ro_id == 2)
                {
                    var list = db.Courses.ToList();
                    return View(list);
                }
            }
            return RedirectToAction("Login", "HomeStudent", new { area = "" });
                  
        }
    }
}