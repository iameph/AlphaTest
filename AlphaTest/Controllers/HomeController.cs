using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlphaTest.Models;

namespace AlphaTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (CurrentUser == null)
            {
                return RedirectToAction("Index", "Account");
            }

            if (CurrentUser.IsAdmin)
            {
                return RedirectToAction("Index", "Admin");
            }

            return RedirectToAction("Index", "User");

        }


        protected User CurrentUser { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var login = Request.Cookies["Auth"];
            if (login !=  null)
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
