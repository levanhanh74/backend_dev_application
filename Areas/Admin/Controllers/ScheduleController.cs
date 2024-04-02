using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_MANGE_COURCE.Models;
using WebGrease.Css.Ast.Selectors;

namespace WEB_MANGE_COURCE.Areas.Admin.Controllers
{
    public class ScheduleController : Controller
    {
        // GET: Admin/Schudle
        private ma_scschedulesEntities1 db = new ma_scschedulesEntities1();
        public ActionResult schedule()
        {
            var user = Session["user"];
            if (user != null)
            {
                if (user is WEB_MANGE_COURCE.Models.Admin admin && admin.ro_id == 1) // list user admin 
                {
                    var courseList = db.Courses.ToList();
                    var teacherList = db.Teachers.ToList();
                    var classList = db.Classes.ToList();
                    var studentList = db.Students.ToList();

                    var courseItems = courseList.Select(c => new SelectListItem
                    {
                        Value = c.course_id.ToString(),
                        Text = c.title
                    });
                    var teacherItems = teacherList.Select(t => new SelectListItem
                    {
                        Value = t.teacher_id.ToString(),
                        Text = t.username
                    });
                    var classItems = classList.Select(cl => new SelectListItem
                    {
                        Value = cl.class_id.ToString(),
                        Text = cl.class_name
                    });
                    var studentItems = studentList.Select(s => new SelectListItem
                    {
                        Value = s.student_id.ToString(),
                        Text = s.username
                    });

                    // Kết hợp danh sách người dùng từ các bảng lại với nhau
                    ViewBag.course_id = new SelectList(courseItems, "Value", "Text");
                    ViewBag.teacher_id = new SelectList(teacherItems, "Value", "Text");
                    ViewBag.class_id = new SelectList(classItems, "Value", "Text");
                    ViewBag.student_id = new SelectList(studentItems, "Value", "Text");

                    // Pass the model to the view
                    return View();
                }
                else if (user is WEB_MANGE_COURCE.Models.Employee employ && employ.ro_id == 3)
                {
                    var courseList = db.Courses.ToList();
                    var teacherList = db.Teachers.ToList();
                    var classList = db.Classes.ToList();
                    var studentList = db.Students.ToList();

                    var courseItems = courseList.Select(c => new SelectListItem
                    {
                        Value = c.course_id.ToString(),
                        Text = c.title
                    });
                    var teacherItems = teacherList.Select(t => new SelectListItem
                    {
                        Value = t.teacher_id.ToString(),
                        Text = t.username
                    });
                    var classItems = classList.Select(cl => new SelectListItem
                    {
                        Value = cl.class_id.ToString(),
                        Text = cl.class_name
                    });
                    var studentItems = studentList.Select(s => new SelectListItem
                    {
                        Value = s.student_id.ToString(),
                        Text = s.username
                    });

                    // Kết hợp danh sách người dùng từ các bảng lại với nhau
                    ViewBag.course_id = new SelectList(courseItems, "Value", "Text");
                    ViewBag.teacher_id = new SelectList(teacherItems, "Value", "Text");
                    ViewBag.class_id = new SelectList(classItems, "Value", "Text");
                    ViewBag.student_id = new SelectList(studentItems, "Value", "Text");

                    // Pass the model to the view
                    return View();
                }
                else
                {
                    return RedirectToAction("Error", "Error", new { area = "Admin" });
                }
            }
            return RedirectToAction("Login", "HomeStudent", new { area = "" });
          
        }
        [HttpPost]
        public ActionResult schedule(Schedule data)
        {
            var user = Session["user"];
            if (user != null)
            {
                //Admin can add schedule for student with teach
                if (user is WEB_MANGE_COURCE.Models.Admin admin && admin.ro_id == 1) {
                    if (ModelState.IsValid) // Check if the model state is valid
                    {
                        // Save the schedule data to the database
                        db.Schedules.Add(data);
                        db.SaveChanges();

                        // Redirect to a success page or display a success message
                        return RedirectToAction("listschedule", "Schedule", new { area = "Admin" });
                    }
                    else
                    {
                        // Model state is not valid, return the view with validation errors
                        return View(data);
                    }
                }
                // employ can add schedule for student with teach
                else if (user is WEB_MANGE_COURCE.Models.Employee employ && employ.ro_id == 3)
                {
                    if (ModelState.IsValid) // Check if the model state is valid
                    {
                        // Save the schedule data to the database
                        db.Schedules.Add(data);
                        db.SaveChanges();

                        // Redirect to a success page or display a success message
                        return RedirectToAction("listschedule", "Schedule", new { area = "Admin" });
                    }
                    else
                    {
                        // Model state is not valid, return the view with validation errors
                        return View(data);
                    }
                }
                // Error not access
                else
                {
                    return RedirectToAction("Error", "Error", new { area = "Admin" });
                }
            }
                    return View();
        }
        public ActionResult listschedule()
        {
            var user = Session["user"];
            if (user != null)
            {
                if (user is WEB_MANGE_COURCE.Models.Admin admin && admin.ro_id == 1) // list user admin 
                {
                    var schedules = db.Schedules.ToList();

                    // Pass schedules to the view
                    return View(schedules);
                }
                 else if (user is WEB_MANGE_COURCE.Models.Employee employ && employ.ro_id == 3)
                {
                    var schedules = db.Schedules.ToList();

                    // Pass schedules to the view
                    return View(schedules);
                }
                else
                {
                    return RedirectToAction("Error", "Error", new { area = "Admin" });
                }
            }
              return RedirectToAction("Login", "HomeStudent", new {area=""});
        }
    }
}