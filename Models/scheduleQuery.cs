using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebGrease;

namespace WEB_MANGE_COURCE.Models
{
    public class scheduleQuery
    {
        public string StudentName { get; set; }
        public string Student_Id { get; set; }
        public string ClassName { get; set; }
        public string Class_Id { get; set; }
        public string CourseName { get; set; }
        public string Course_Id { get; set; }
        public string TeachName { get; set; }
        public string Teacher_Id { get; set; }
        public string Role { get; set; }
        public TimeMeasure Starttime { get; set; }
        public TimeMeasure Endtime { get; set; }
        public string status { get; set; }
        public TimeMeasure dateWeek { get; set; }
    }
}