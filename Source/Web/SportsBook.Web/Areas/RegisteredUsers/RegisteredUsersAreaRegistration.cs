using System.Web.Mvc;

namespace SportsBook.Web.Areas.RegisteredUsers
{
    public class RegisteredUsersAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "RegisteredUsers";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "RegisteredUsers_default",
                "RegisteredUsers/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}