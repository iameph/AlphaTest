using System.Linq;
using System.Web.Mvc;
using AlphaTest.Models;

namespace AlphaTest.Controllers
{
    public class AdminController : Controller
    {
        // GET
        public ActionResult Index()
        {
            return
            View();
        }

        protected User CurrentUser { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var login = Request.Cookies["Auth"];
            if (login != null)
            {
                using (var db = new MyContext())
                {
                    CurrentUser = db.Users.FirstOrDefault(u => u.Id.ToString() == login.Value);
                }
            }
            else
            {
                CurrentUser = null;
            }

            filterContext.Controller.ViewBag.User = CurrentUser;
        }
    }
}