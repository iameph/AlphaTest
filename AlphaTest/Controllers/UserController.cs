using System;
using System.Linq;
using System.Web.Mvc;
using AlphaTest.Models;

namespace AlphaTest.Controllers
{
    public class UserController : Controller
    {
        // GET
        public ActionResult Index()
        {
            if (CurrentUser == null)
                return RedirectToAction("Index", "Account");

            var model = new UserQueriesModel();
            model.Queries = CurrentUser.Queries;
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateQueryModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var db = new MyContext())
                    {
                        var q = new Query()
                        {
                            UserId = CurrentUser.Id,
                            Category = model.Category,
                            QueryDate = DateTime.Now,
                            State = QueryState.New,
                            Text = model.Text
                        };

                        db.Queries.Add(q);
                        db.SaveChanges();
                        return View("CreateSuccess");
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("","Не удалось создать заявку");
                }

                return View("Create", model);
            }

            ModelState.AddModelError("Text","Некорректный текст заявки");
            return View("Create", model);
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
                    db.Entry(CurrentUser).Collection(c => c.Queries).Load();
                    if (CurrentUser.IsAdmin)
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