using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_MANGE_COURCE.Models;

namespace WEB_MANGE_COURCE.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        private ma_scschedulesEntities2 db = new ma_scschedulesEntities2();
        public ActionResult category()
        {
            var user = Session["user"];

           if(user!= null)
            {
                 if (user is WEB_MANGE_COURCE.Models.Admin useradmin && useradmin.ro_id == 1)
            {
                return View();
            }else if (user is WEB_MANGE_COURCE.Models.Employee useremploy && useremploy.ro_id == 2)
            {
                return View();
            }else if(user is WEB_MANGE_COURCE.Models.Admin teacher && teacher.ro_id == 3) {
                return RedirectToAction("Index", "HomeTeacher", new { area = "Teacher" });
            } else
            {
                return RedirectToAction("HomeStudent", "HomeStudent", new { area = "" });
            }
            }
            else
            {
                return RedirectToAction("login", "HomeStudent", new { area = "" });
            }
        }

        [HttpPost]
        public ActionResult category(Category category)
        {
            var user = Session["user"];
            if (user != null)
            {
                if (user is WEB_MANGE_COURCE.Models.Admin useradmin && useradmin.ro_id == 1)
                {
                    if (ModelState.IsValid)
                    {
                        // Thực hiện lưu danh mục vào cơ sở dữ liệu
                        // Ví dụ:
                        db.Categories.Add(category);
                        db.SaveChanges();

                        // Sau khi thêm thành công, chuyển hướng về trang danh sách danh mục
                        return RedirectToAction("Index", "Category");
                    }

                    // Nếu dữ liệu không hợp lệ, hiển thị lại biểu mẫu với thông báo lỗi
                    return View(category);
                }
                else if (user is WEB_MANGE_COURCE.Models.Employee useremploy && useremploy.ro_id == 2)
                {
                    if (ModelState.IsValid)
                    {
                        // Thực hiện lưu danh mục vào cơ sở dữ liệu
                        // Ví dụ:
                        db.Categories.Add(category);
                        db.SaveChanges();

                        // Sau khi thêm thành công, chuyển hướng về trang danh sách danh mục
                        return RedirectToAction("Index", "Category");
                    }

                    // Nếu dữ liệu không hợp lệ, hiển thị lại biểu mẫu với thông báo lỗi
                    return View(category);
                } else
                {
                    return RedirectToAction("Error", "Error", new { area = "Admin" });
                }
            }
             return RedirectToAction("login", "HomeStudent", new { area = "" });
        }
        public ActionResult list()
        {
            var user = Session["user"];
            if (user is WEB_MANGE_COURCE.Models.Admin useradmin && useradmin.ro_id == 1)
            {
                var listCate = db.Categories.ToList();
                return View(listCate);
            }
            else if (user is WEB_MANGE_COURCE.Models.Employee useremploy && useremploy.ro_id == 2)
            {
                var listCate = db.Categories.ToList();
                return View(listCate);
            }
            else if (user is WEB_MANGE_COURCE.Models.Admin teacher && teacher.ro_id == 3)
            {
                return RedirectToAction("Index", "Teacher", new { area = "" });
            }
            else
            {
                return RedirectToAction("HomeStudent", "HomeStudent", new { area = "" });
            }  
        }
    }
}