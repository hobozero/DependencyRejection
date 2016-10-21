using System.Web;
using System.Web.Mvc;

namespace CCi.Shared.Admin.AppCatalog
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
