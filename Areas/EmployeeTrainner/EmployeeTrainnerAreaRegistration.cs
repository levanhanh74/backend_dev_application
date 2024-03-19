using System.Web.Mvc;

namespace WEB_MANGE_COURCE.Areas.EmployeeTrainner
{
    public class EmployeeTrainnerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "EmployeeTrainner";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "EmployeeTrainner_default",
                "EmployeeTrainner/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}