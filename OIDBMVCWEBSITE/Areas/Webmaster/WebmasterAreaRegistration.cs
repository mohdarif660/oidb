using System.Web.Mvc;

namespace OIDBMVCWEBSITE.Areas.Webmaster
{
    public class WebmasterAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Webmaster";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Webmaster_default",
                "Webmaster/{controller}/{action}/{id}",
                new { controller = "Users", action = "Index", id = UrlParameter.Optional }


            );
        }
    }
}
