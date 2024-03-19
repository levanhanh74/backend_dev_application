using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
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
                    return RedirectToAction("Index", "HomeEmployee", new
                    {
                        area
                    = "EmployeeTrainner"
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
                    return RedirectToAction("Index", "HomeTeacher", new
                    {
                        area
                    = "Teacher"
                    });
                }
            }
            else if (students != null && VerifyPassword(students.password, password))
            {
                // lấy thông tin vai trò của người dùng từ cơ sở dữ liệu
                var role = db.Roles.FirstOrDefault(r => r.ro_id == roles);

                // kiểm tra xem người dùng có vai trò được chỉ định không
                if (role != null && students.ro_id == role.ro_id)
                {
                    // xác thực thành công, chuyển hướng đến trang homeadmin
                    return RedirectToAction("HomeStudent");
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
            return View();
        }
    }
}