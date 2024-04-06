using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WEB_MANGE_COURCE.Models;

namespace WEB_MANGE_COURCE.Controllers
{
    public class HomeStudentController : Controller
    {
        private ma_scschedulesEntities1 db =  new ma_scschedulesEntities1();
        public ActionResult Login()
        {
            List<Role> Role = db.Roles.ToList();  // get All role user 
            return View(Role);
        }
        [HttpPost]
        public ActionResult PostLogin(string username, string password, int roles)
        {
            // Lấy thông tin người dùng từ cơ sở dữ liệu với username được cung cấp
            var admin = db.Admins.FirstOrDefault(a => a.username == username && a.password == password && a.ro_id == roles);
            var employees = db.Employees.FirstOrDefault(a => a.username == username && a.password == password && a.ro_id == roles);
            var teachers = db.Teachers.FirstOrDefault(a => a.username == username && a.password == password && a.ro_id == roles);
            var students = db.Students.FirstOrDefault(a => a.username == username && a.password == password && a.ro_id == roles);



            // Kiểm tra xem người dùng có tồn tại không và mật khẩu có đúng không
            if (admin != null && VerifyPassword(admin.password, password))
            {
                // lấy thông tin vai trò của người dùng từ cơ sở dữ liệu
                var role = db.Roles.FirstOrDefault(r => r.ro_id == roles);

                // kiểm tra xem người dùng có vai trò được chỉ định không
                if (role != null && admin.ro_id == role.ro_id)
                {
                    // xác thực thành công, chuyển hướng đến trang homeadmin
                    Session["user"] = admin;
                    return RedirectToAction("Index", "HomeAdmin", new {area
                    = "Admin"});
                }
            }else if(employees != null && VerifyPassword(employees.password, password))
            {
                // lấy thông tin vai trò của người dùng từ cơ sở dữ liệu
                var role = db.Roles.FirstOrDefault(r => r.ro_id == roles);

                // kiểm tra xem người dùng có vai trò được chỉ định không
                if (role != null && employees.ro_id == role.ro_id)
                {
                    // xác thực thành công, chuyển hướng đến trang homeadmin
                    Session["user"] = employees;
                    return RedirectToAction("Index", "HomeAdmin", new
                    {
                        area
                    = "Admin"
                    });
                }
            }
            else if (teachers != null && VerifyPassword(teachers.password, password))
            {
                // lấy thông tin vai trò của người dùng từ cơ sở dữ liệu
                var role = db.Roles.FirstOrDefault(r => r.ro_id == roles);

                // kiểm tra xem người dùng có vai trò được chỉ định không
                if (role != null && teachers.ro_id == role.ro_id)
                {
                    // xác thực thành công, chuyển hướng đến trang homeadmin
                    Session["user"] = teachers;
                    return RedirectToAction("Index", "Teacher", new {area=""});
                }
            }
            else if (students != null && VerifyPassword(students.password, password))
            {
                // lấy thông tin vai trò của người dùng từ cơ sở dữ liệu
                var role = db.Roles.FirstOrDefault(r => r.ro_id == roles);

                // kiểm tra xem người dùng có vai trò được chỉ định không
                if (role != null && students.ro_id == role.ro_id)
                {

                    Session["user"] = students;
                    // xác thực thành công, chuyển hướng đến trang homeadmin
                    return RedirectToAction("HomeStudent", "HomeStudent", new {area=""});
                }
            }
            // Xác thực không thành công, chuyển hướng đến trang Login
            return RedirectToAction("Login");
        }

        // Hàm để xác thực mật khẩu
        private bool VerifyPassword(string hashedPassword, string inputPassword)
        {
            // Trong trường hợp này, bạn cần sử dụng một phương thức bảo mật như bcrypt để so sánh mật khẩu đã hash và mật khẩu nhập vào
            // Tạm thời, chúng ta sẽ so sánh trực tiếp hai mật khẩu (đây không phải là phương pháp an toàn nhất)
            return hashedPassword == inputPassword;
        }
        public ActionResult HomeStudent()
        {
            var user = Session["user"];
            if (user != null)
            {
                if (user is WEB_MANGE_COURCE.Models.Admin admin && admin.ro_id == 1)
                {
                    return View();
                }
                else if (user is WEB_MANGE_COURCE.Models.Employee employ && employ.ro_id == 3)
                {
                    return View();
                }
                else if (user is WEB_MANGE_COURCE.Models.Teacher  teacher && teacher.ro_id == 4)
                {
                    return RedirectToAction("Index", "Teacher", new {area=""});
                }
                else if (user is WEB_MANGE_COURCE.Models.Student student && student.ro_id == 5)
                {
                    return View();
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
                else if (user is WEB_MANGE_COURCE.Models.Employee employ && employ.ro_id == 3)
                {
                    var schedule = db.Schedules.ToList();
                    return View();
                }
                else if (user is WEB_MANGE_COURCE.Models.Teacher teacher && teacher.ro_id == 4)
                {
                    return RedirectToAction("Index", "Teacher", new {area=""});
                }  else if (user is WEB_MANGE_COURCE.Models.Student student && student.ro_id == 5)
                {

                    var query = (from Schedule in db.Schedules
                                 join Class in db.Classes on Schedule.class_id equals Class.class_id
                                 join Course in db.Courses on Schedule.course_id equals Course.course_id
                                 join Teacher in db.Teachers on Schedule.teacher_id equals Teacher.teacher_id
                                 join Student in db.Students on Schedule.student_id equals Student.student_id
                                 select new ScheduleInfo {
                                    Schedule = Schedule,
                                    Teacher = Teacher,
                                    Student = Student,
                                    Course = Course,
                                    Class = Class
                                 }).ToList();



                    return View(query);
                }
            }
            return RedirectToAction("Login", "HomeStudent", new { area = "" });
        }

        public ActionResult Logout()
        {
            var user = Session["user"];
            if (user != null)
            {
                if (user is WEB_MANGE_COURCE.Models.Admin admin && admin.ro_id == 1 )
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
                else if (user is WEB_MANGE_COURCE.Models.Teacher techer && techer.ro_id == 4)
                {
                    Session.Remove("user");
                    // Đăng xuất Forms Authentication
                    FormsAuthentication.SignOut();
                }
                else if (user is WEB_MANGE_COURCE.Models.Student student  && student.ro_id == 5)
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
                else if (user is WEB_MANGE_COURCE.Models.Employee employ && employ.ro_id == 3)
                {
                    return View();
                }
                else if (user is WEB_MANGE_COURCE.Models.Teacher teacher && teacher.ro_id == 4)
                {
                    return RedirectToAction("Index", "Teacher", new { area = "" });
                }
                else if (user is WEB_MANGE_COURCE.Models.Student student && student.ro_id == 5)
                {
                    return View();
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
                            userID.password = password;
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
                else if (user is WEB_MANGE_COURCE.Models.Employee employ && employ.ro_id == 3)
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
                            userID.password = password;
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
                else if (user is WEB_MANGE_COURCE.Models.Teacher teacher && teacher.ro_id == 4)
                {
                    return RedirectToAction("Error", "Error", new { area = "" });
                }
                else if (user is WEB_MANGE_COURCE.Models.Student student && student.ro_id == 5)
                {
                    if (image != null && image.ContentLength > 0)
                    {
                        var userID = db.Students.Find(student.student_id);
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
                            return RedirectToAction("HomeStudent", "HomeStudent", new { area = "" });
                        }
                        return HttpNotFound();
                    }
                    return View(username, password, image);
                }
            }
            return RedirectToAction("Login", "HomeStudent", new { area = "" });
        }
    }
}