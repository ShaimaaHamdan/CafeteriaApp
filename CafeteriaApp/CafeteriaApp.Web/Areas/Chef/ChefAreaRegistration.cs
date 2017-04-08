using System.Web.Mvc;

namespace CafeteriaApp.Web.Areas.Chef
{
    public class ChefAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Chef";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Chef_default",
                "Chef/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}