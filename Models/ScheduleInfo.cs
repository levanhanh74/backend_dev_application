using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_MANGE_COURCE.Models
{
    public class ScheduleInfo
    {
        public Schedule Schedule { get; set; }
        public Teacher Teacher { get; set; }
        public Student Student { get; set; }
        public Cours Course { get; set; }
        public Class Class { get; set; }
    }
}