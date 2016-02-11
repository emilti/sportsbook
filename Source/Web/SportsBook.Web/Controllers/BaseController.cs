namespace SportsBook.Web.Controllers
{
    using System.Web.Mvc;

    using SportsBook.Services.Web;

    public abstract class BaseController : Controller
    {
        public ICacheService Cache { get; set; }
    }
}
