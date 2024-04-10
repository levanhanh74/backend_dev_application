using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WEB_MANGE_COURCE.Models;
using System.Data;

namespace WEB_MANGE_COURCE.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public static string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }
        private ma_scschedulesEntities2 db = new ma_scschedulesEntities2();

        public ActionResult Index()
        {
            return View();
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
                else if (user is WEB_MANGE_COURCE.Models.Employee empoly && empoly.ro_id == 2)
                {
                    // Xóa Session user
                    Session.Remove("user");
                    // Đăng xuất Forms Authentication
                    FormsAuthentication.SignOut();

                }
                else if (user is WEB_MANGE_COURCE.Models.Teacher techer && techer.ro_id == 3)
                {
                    Session.Remove("user");
                    // Đăng xuất Forms Authentication
                    FormsAuthentication.SignOut();
                }
            }
            // Kiểm tra xem Session tồn tại không
            // Chuyển hướng người dùng đến trang đăng nhập
            return RedirectToAction("Login", "HomeStudent", new { area = "" });
        }

        public ActionResult profile()
        {
            var user = Session["user"];
            if (user != null)
            {
                if (user is WEB_MANGE_COURCE.Models.Admin admin && admin.ro_id == 1)
                {
                    return View();
                }
                else if (user is WEB_MANGE_COURCE.Models.Employee employ && employ.ro_id == 2)
                {
                    return View();
                }
                else if (user is WEB_MANGE_COURCE.Models.Teacher teacher && teacher.ro_id == 3)
                {
                    return View();
                }
                else if (user is WEB_MANGE_COURCE.Models.Student student && student.ro_id == 4)
                {
                    return RedirectToAction("HomeStudent", "HomeStudent", new { area = "" });
                }
            }
            return RedirectToAction("Login", "HomeStudent", new { area = "" });
        }
        [HttpPost]
        public ActionResult profile(string username, string password, HttpPostedFileBase image)
        {
            var user = Session["user"];
            if (user != null)
            {
                if (user is WEB_MANGE_COURCE.Models.Admin admin && admin.ro_id == 1)
                {
                    //Upload
                    if (image != null && image.ContentLength > 0)
                    {

                        var userID = db.Admins.Find(admin.ad_id);
                        if (userID != null)
                        {
                            var fileName = Path.GetFileName(image.FileName);
                            var path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                            // Tạo thư mục nếu chưa tồn tại
                            Directory.CreateDirectory(Server.MapPath("~/Uploads/"));
                            // Lưu hình ảnh
                            image.SaveAs(path);
                            userID.username = username;
                            userID.password = GetMd5Hash(password);
                            userID.image = fileName;
                            Session["user"] = userID;
                            db.Entry(userID).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index", "Admin", new { area = "Admin" });
                        }
                        return HttpNotFound();
                    }
                    return View(username, password, image);
                }
                else if (user is WEB_MANGE_COURCE.Models.Employee employ && employ.ro_id == 2)
                {
                    // Upload 
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
                            userID.password = GetMd5Hash(password);
                            userID.image = fileName;
                            Session["user"] = userID;
                            db.Entry(userID).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index", "Admin", new { area = "Admin" });
                        }
                    }
                    return View(username, password, image);
                }
                else if (user is WEB_MANGE_COURCE.Models.Teacher teacher && teacher.ro_id == 3)
                {
                   
                        // Upload 
                        if (image != null && image.ContentLength > 0)
                        {
                            var userID = db.Teachers.Find(teacher.teacher_id);
                            if (userID != null)
                            {
                                var fileName = Path.GetFileName(image.FileName);
                                var path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                                // Tạo thư mục nếu chưa tồn tại
                                Directory.CreateDirectory(Server.MapPath("~/Uploads/"));
                                // Lưu hình ảnh
                                image.SaveAs(path);
                                userID.username = username;
                                userID.password = GetMd5Hash(password);
                                userID.image = fileName;
                                Session["user"] = userID;
                                db.Entry(userID).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                                return RedirectToAction("Index", "Teacher");
                            }
                            return HttpNotFound();
                    }
                        return View(username, password, image);
                }
                else if (user is WEB_MANGE_COURCE.Models.Student student && student.ro_id == 4)
                {
                    return RedirectToAction("Error", "Error", new { area = "" });
                }
            }
            return RedirectToAction("Login", "HomeStudent", new { area = "" });
        }


        public ActionResult schedule()
        {
            var user = Session["user"];
            if (user != null)
            {
                if (user is WEB_MANGE_COURCE.Models.Admin admin && admin.ro_id == 1)
                {
                    var schedule = db.Schedules.ToList();
                    return View();
                }
                else if (user is WEB_MANGE_COURCE.Models.Employee employ && employ.ro_id == 2)
                {
                    var schedule = db.Schedules.ToList();
                    return View();
                }
                else if (user is WEB_MANGE_COURCE.Models.Teacher teacher && teacher.ro_id == 3)
                {
                    var query = (from Schedule in db.Schedules
                                 join Class in db.Classes on Schedule.class_id equals Class.class_id
                                 join Course in db.Courses on Schedule.course_id equals Course.course_id
                                 join Teacher in db.Teachers on Schedule.teacher_id equals Teacher.teacher_id
                                 join Student in db.Students on Schedule.student_id equals Student.student_id
                                 select new ScheduleInfo
                                 {
                                     Schedule = Schedule,
                                     Teacher = Teacher,
                                     Student = Student,
                                     Course = Course,
                                     Class = Class
                                 }).ToList();
                    return View(query);
                }
                else if (user is WEB_MANGE_COURCE.Models.Student student && student.ro_id == 4)
                {

                    return RedirectToAction("HomeStudent", "HomeStudent", new { area = "" });
                 
                }
            }
            return RedirectToAction("Login", "HomeStudent", new { area = "" });
        }
    }
}