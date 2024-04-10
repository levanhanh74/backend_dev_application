    using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WEB_MANGE_COURCE.Models;

namespace WEB_MANGE_COURCE.Areas.Admin.Controllers
{
    public class ManagerUserController : Controller
    {
        private ma_scschedulesEntities2 db = new ma_scschedulesEntities2();


        // Method to hash password
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

        // GET: Admin/ManagerUser
        // only admin access add user 
        public ActionResult user()
        {
            var user = Session["user"];
            if (user != null)
            {
                if (user is WEB_MANGE_COURCE.Models.Admin useradmin && useradmin.ro_id == 1)
                {
                    var role = db.Roles.ToList();
                    return View(role);
                }
                else if (user is WEB_MANGE_COURCE.Models.Employee useremploy && useremploy.ro_id == 2)
                {
                    return RedirectToAction("Error", "Error", new { area = "Admin" });
                }
                else if (user is WEB_MANGE_COURCE.Models.Teacher teacher && teacher.ro_id == 3)
                {
                    return RedirectToAction("Error", "Error", new { area = "Admin" });
                }
                else
                {
                    return RedirectToAction("Error", "Error", new { area = "Admin" });
                }
            }
            else
            {
                return RedirectToAction("login", "HomeStudent", new { area = "" });
            }
         
        }
        [HttpPost]
        // only user admin add user 
        public ActionResult user(string username, string password,string confirmpass,  string image , int ro_id)
        {
            var user = Session["user"];
            // add user due to admin add
            if(user != null)
            {
                if (user is WEB_MANGE_COURCE.Models.Admin admin && admin.ro_id == 1)
                {
                    if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(confirmpass) && ro_id > 0)
                    {
                        if (password == confirmpass)
                        {
                            // add admin
                            if (ro_id == 1)
                            {
                                int id_ad = new Random().Next(1, 1000);
                                db.Admins.Add(new Models.Admin { username = username, password = GetMd5Hash(password), image = image, ro_id = ro_id, ad_id = id_ad });
                                db.SaveChanges();
                                return RedirectToAction("listuser", "ManagerUser");

                            }
                            // add employee
                            else if (ro_id == 2)
                            {
                                int id_em = new Random().Next(1, 1000);
                                db.Employees.Add(new Models.Employee { username = username, password = GetMd5Hash(password), image = image, ro_id = ro_id, emp_id = id_em });
                                db.SaveChanges();
                                return RedirectToAction("listuser", "ManagerUser");
                            }
                            // add teacher
                            else if (ro_id == 3)
                            {
                                int id_te = new Random().Next(1, 1000);
                                db.Teachers.Add(new Models.Teacher { username = username, password = GetMd5Hash(password), image = image, ro_id = ro_id, teacher_id = id_te });
                                db.SaveChanges();
                                return RedirectToAction("listuser", "ManagerUser");
                            }
                            // add student
                            else if (ro_id == 4)
                            {
                                int id_st = new Random().Next(1, 1000);
                                db.Students.Add(new Models.Student { username = username, password = GetMd5Hash(password), image = image, ro_id = ro_id, student_id = id_st });
                                db.SaveChanges();
                                return RedirectToAction("listuser", "ManagerUser");
                            }
                            // error
                            else
                            {
                                return View();
                            }

                        }
                        // truong hop khong dung pass
                        return View();
                    }
                    else
                    { // truong hop null 
                        return View();
                    }
                }
                // employ khong duoc add 
                else if (user is WEB_MANGE_COURCE.Models.Employee employee && employee.ro_id == 2)
                {
                    return RedirectToAction("Index", "HomeAdmin", new { area = "Admin" });
                }
                // teacher khong duoc add
                else if (user is WEB_MANGE_COURCE.Models.Teacher teacher && teacher.ro_id == 3)
                {
                    return RedirectToAction("HomeStudent", "HomeStudent", new { area = "" });
                }
                // student khong duoc add
                else if (user is WEB_MANGE_COURCE.Models.Student student && student.ro_id == 4)
                {
                    return RedirectToAction("HomeStudent", "HomeStudent", new { area = "" });
                }
                else
                {
                    return RedirectToAction("Error", "Error", new { area = "Admin" });
                }
            }
            // not have user 
              return RedirectToAction("Login", "HomeStudent", new { area = "" });
        }
        
        // list user only admin and employ watch
        public ActionResult listuser()
        {
            // Truy xuất danh sách người dùng từ các bảng khác nhau và kết hợp chúng

            var user = Session["user"];
            if(user != null)
            {
                if (user is WEB_MANGE_COURCE.Models.Admin admin && admin.ro_id == 1) // list user admin 
                {
                    var adminList = db.Admins.ToList();
                    var employList = db.Employees.ToList();
                    var teacherList = db.Teachers.ToList();
                    var studentList = db.Students.ToList();
                    // Kết hợp danh sách người dùng từ các bảng lại với nhau
                    var userList = adminList.Select(a => new UserViewModel { Username = a.username, Password = a.password, Role = "Admin" })
                        .Union(employList.Select(e => new UserViewModel { Username = e.username, Password = e.password, Role = "Employee" }))
                        .Union(teacherList.Select(t => new UserViewModel { Username = t.username, Password = t.password, Role = "Teacher" }))
                        .Union(studentList.Select(s => new UserViewModel { Username = s.username, Password = s.password, Role = "Student" }))
                        .ToList();
                    // Truyền danh sách người dùng đã kết hợp đến view để hiển thị
                    return View(userList);
                }// list user employ
                else if (user is WEB_MANGE_COURCE.Models.Employee employ && employ.ro_id == 2)
                {
                    var adminList = db.Admins.ToList();
                    var employList = db.Employees.ToList();
                    var teacherList = db.Teachers.ToList();
                    var studentList = db.Students.ToList();
                    // Kết hợp danh sách người dùng từ các bảng lại với nhau
                    var userList = adminList.Select(a => new UserViewModel { Username = a.username, Password = a.password, Role = "Admin" })
                        .Union(employList.Select(e => new UserViewModel { Username = e.username, Password = e.password, Role = "Employee" }))
                        .Union(teacherList.Select(t => new UserViewModel { Username = t.username, Password = t.password, Role = "Teacher" }))
                        .Union(studentList.Select(s => new UserViewModel { Username = s.username, Password = s.password, Role = "Student" }))
                        .ToList();
                    // Truyền danh sách người dùng đã kết hợp đến view để hiển thị
                    return View(userList);
                } // not list user teacher 
                else if(user is WEB_MANGE_COURCE.Models.Teacher teacher && teacher.ro_id == 3)
                {
                    return RedirectToAction("Error", "Error", new { area = "" });
                } // not list user student 
                else 
                {
                    return RedirectToAction("Error", "Error", new { area = "" });
                }
            }
            else
            {
                return RedirectToAction("Login", "HomeStudent", new { area = "" });
            }
        }

       
    }
}