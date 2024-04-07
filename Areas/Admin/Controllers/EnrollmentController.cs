using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_MANGE_COURCE.Models;

namespace WEB_MANGE_COURCE.Areas.Admin.Controllers
{
    public class EnrollmentController : Controller
    {
        private ma_scschedulesEntities1 db = new ma_scschedulesEntities1();
        // GET: Admin/Enrollment

        // Only addmin access errollment
        public ActionResult Index()
        {
            var user = Session["user"];
            if(user != null)
            {
                if(user is WEB_MANGE_COURCE.Models.Admin admin && admin.ro_id == 1)
                {
                    var teachers = db.Teachers.ToList();
                    ViewBag.TeacherList = new SelectList(teachers, "teacher_id", "username"); // Assuming FullName is the property representing teacher's name
                    var courses = db.Courses.ToList();
                    ViewBag.CourseList = new SelectList(courses, "course_id", "title"); // Assuming CourseName is the property representing course's name
                    var students = db.Students.ToList();
                    ViewBag.StudentList = new SelectList(students, "student_id", "username"); // Assuming FullName is the property representing student's 
                    return View();
                }else if (user is WEB_MANGE_COURCE.Models.Employee employ && employ.ro_id == 3)
                {
                    return RedirectToAction("Error", "Error", new {area ="Admin"});
                }else if (user is WEB_MANGE_COURCE.Models.Teacher teacher && teacher.ro_id == 4)
                {
                    return RedirectToAction("Error", "Error", new { area = "Admin" });
                }
                else 
                {
                    return RedirectToAction("Error", "Error", new { area = "Admin" });
                }
            }
            return RedirectToAction("Login", "HomeStudent", new { area = "" });
        }
        [HttpPost]

        // Only addmin add errollment
        public ActionResult Index(Enrollment data)
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
                        db.Enrollments.Add(data);
                        db.SaveChanges();

                        // Sau khi thêm thành công, chuyển hướng về trang danh sách danh mục
                        return RedirectToAction("Index", "Enrollment");
                    }

                    // Nếu dữ liệu không hợp lệ, hiển thị lại biểu mẫu với thông báo lỗi
                    return View(data);
                }
                else if (user is WEB_MANGE_COURCE.Models.Employee employ && employ.ro_id == 3)
                {
                    return RedirectToAction("Error", "Error", new { area = "Admin" });
                }
                else if (user is WEB_MANGE_COURCE.Models.Teacher teacher && teacher.ro_id == 4)
                {
                    return RedirectToAction("Error", "Error", new { area = "Admin" });
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
                    var list = db.Enrollments.ToList();
                    return View(list);
                }
                else
                {
                    return RedirectToAction("Error", "Error", new { area = "" });
                }
            }
            return RedirectToAction("Login", "HomeStudent", new { area = "" });
               
        }
    }
}