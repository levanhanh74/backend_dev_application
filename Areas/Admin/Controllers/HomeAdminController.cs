using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WEB_MANGE_COURCE.Models;

namespace WEB_MANGE_COURCE.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        private ma_scschedulesEntities1 db = new ma_scschedulesEntities1();
        public ActionResult Index()
        {
            var user = Session["user"];
           
           if(user != null) // have user 
            {
                if (user is WEB_MANGE_COURCE.Models.Employee useremploy && useremploy.ro_id == 3)
                {
                    return View();
                }
                else if (user is WEB_MANGE_COURCE.Models.Admin admin && admin.ro_id == 1)
                {
                    return View();
                }
                else if (user is WEB_MANGE_COURCE.Models.Student student && student.ro_id == 5)
                {
                    return RedirectToAction("Error", "Error", new { area = "Admin" });
                }
                else if (user is WEB_MANGE_COURCE.Models.Teacher teacher && teacher.ro_id == 4)
                {
                    return RedirectToAction("Error", "Error", new { area = "Admin" });
                }
                else
                {
                    return View();
                }
            }
            else // Not isset user
            {
                return RedirectToAction("Login", "HomeStudent");
            }
        }
        public ActionResult createAdd()
        {
            List<Role> Role = db.Roles.ToList();  // get All role user 
            return View(Role);
        }

        public ActionResult profileAdmin()
        {
            var user  = Session["user"] ;
            if(user != null)
            {
                if(user is WEB_MANGE_COURCE.Models.Admin admin && admin.ro_id == 1)
                {
                    return View();
                }else if(user is WEB_MANGE_COURCE.Models.Employee employ && employ.ro_id == 3)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Error", "Error", new {area=""});
                }
            }
            return RedirectToAction("Login", "HomeStudent", new { area = "" });
        }
        [HttpPost]
        public ActionResult profileAdmin(string username, string password, HttpPostedFileBase image) {
            var user  = Session["user"];
            if(user != null)
            {
                if(user is WEB_MANGE_COURCE.Models.Admin admin && admin.ro_id == 1)
                {
                    // Upload 
                    if (image != null && image.ContentLength > 0)
                    {
                        var userID = db.Admins.Find(admin.ad_id);
                        if(userID   != null)
                        {
                            var fileName = Path.GetFileName(image.FileName);
                            var path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                            // Tạo thư mục nếu chưa tồn tại
                            Directory.CreateDirectory(Server.MapPath("~/Uploads/"));
                            // Lưu hình ảnh
                            image.SaveAs(path);
                            userID.username = username;
                            userID.password = password;
                            userID.image = fileName;
                            Session["user"] = userID;
                            db.Entry(userID).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        return HttpNotFound();
                    }
                    return View(username, password, image);
                    
                }else if (user is WEB_MANGE_COURCE.Models.Employee employ && employ.ro_id == 3)
                {
                    if (image != null && image.ContentLength > 0)
                    {
                        var userID = db.Employees.Find(employ.emp_id);
                        if (userID != null)
                        {
                            var fileName = Path.GetFileName(image.FileName);
                            var path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                            // Tạo thư mục nếu chưa tồn tại
                            Directory.CreateDirectory(Server.MapPath("~/Uploads/"));
                            // Lưu hình ảnh
                            image.SaveAs(path);
                            userID.username = username;
                            userID.password = password;
                            userID.image = fileName;
                            Session["user"] = userID;
                            db.Entry(userID).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        return HttpNotFound();

                    }
                    return View(username, password, image);
                }
                else
                {
                    return RedirectToAction("Error", "Error", new { area = "" });
                }
            }
            return RedirectToAction("Login", "HomeStudent", new { area = "" });
        }
            
        public ActionResult Logout()
        {
            var user = Session["user"];
            if (user != null)
            {
                if (user is WEB_MANGE_COURCE.Models.Admin admin && admin.ro_id == 1)
                {
                    // Xóa Session user
                    Session.Remove("user");
                    // Đăng xuất Forms Authentication
                    FormsAuthentication.SignOut();
                }
                else if (user is WEB_MANGE_COURCE.Models.Employee empoly && empoly.ro_id == 3)
                {
                    // Xóa Session user
                    Session.Remove("user");
                    // Đăng xuất Forms Authentication
                    FormsAuthentication.SignOut();

                }
            }
                // Kiểm tra xem Session tồn tại không
            // Chuyển hướng người dùng đến trang đăng nhập
            return RedirectToAction("Login", "HomeStudent", new { area = ""});
           

        }

    }
}