using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AlphaTest.Models;

namespace AlphaTest.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            if (CurrentUser != null)
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (CurrentUser != null)
                return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                try
                {
                    using (var db = new MyContext())
                    {
                        var user = db.Users.FirstOrDefault(u => u.Login == model.Login && u.Password == model.Password);
                        if (user != null)
                        {
                            Response.SetCookie(new HttpCookie("Auth",user.Id.ToString()){Expires = DateTime.Now+TimeSpan.FromHours(2)});
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("Password","Неверный логин или пароль");
                            return View(model);
                        }
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "Не удалось выполнить вход");
                    return View(model);
                }
            }

            ModelState.AddModelError("", "Логин или пароль некорректны");
            return View(model);
        }

        public ActionResult Exit()
        {
            Response.Cookies.Set(new HttpCookie("Auth")
            {
                Expires = DateTime.Now.AddDays( -1 )
            });
            return Redirect("/Home/Index");
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (CurrentUser != null)
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (CurrentUser != null)
                return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    using (var db = new MyContext())
                    {
                        var user = new User()
                        {
                            Login = model.Login,
                            Name = model.Name,
                            Password = model.Password,
                            IsAdmin = model.IsAdmin
                        };

                        if (db.Users.Any(u => u.Login == model.Login))
                        {
                            ModelState.AddModelError("Login", "Пользователь с таким логином уже существует");
                            throw new Exception();
                        }

                        db.Users.Add(user);
                        db.SaveChanges();

                        Response.SetCookie(new HttpCookie("Auth", user.Id.ToString()) { Expires = DateTime.Now + TimeSpan.FromHours(2) });
                        ViewBag.User = user;
                    }

                    ViewBag.Text = "Новый пользователь успешно зарегистрирован";

                    return View("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "Не удалось зарегистрировать нового пользователя.");
                }
            }
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