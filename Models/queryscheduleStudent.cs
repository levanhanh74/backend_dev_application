using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_MANGE_COURCE.Models
{
    public class queryscheduleStudent
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime Date { get; set; } // Thêm thuộc tính Date
                                           // Các thuộc tính khác nếu cần

    }
}