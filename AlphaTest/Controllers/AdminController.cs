using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AlphaTest.Models;

namespace AlphaTest.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            if (CurrentUser == null)
                return RedirectToAction("Index", "Account");

            var model = new AdminQueriesModel();
            using (var db = new MyContext())
            {
                model.Queries = db.Queries.Include(q => q.User).ToList();
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult ChangeState(int id)
        {
            using (var db = new MyContext())
            {
                var model = db.Queries.Find(id);
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeState(Query model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var db = new MyContext())
                    {
                        var query = db.Queries.Find(model.Id);
                        query.State = model.State;
                        db.Entry(query).State = EntityState.Modified;
                        db.SaveChanges();
                        return View("ChangeStateSuccess", model);
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "Не удалось изменить статус заявки");
                }

                return View(model);
            }

            ModelState.AddModelError("State", "Выбран недопустимый статус");
            return View(model);
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
                    if (CurrentUser != null && !CurrentUser.IsAdmin)
                    {
                        filterContext.Result = new HttpUnauthorizedResult();
                        return;
                    }
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