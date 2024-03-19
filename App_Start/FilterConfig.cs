using System.Web;
using System.Web.Mvc;

namespace WEB_MANGE_COURCE
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
