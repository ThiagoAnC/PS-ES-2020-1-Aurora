using PiggyFinc.UIWeb.Mvc.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PiggyFinc.UIWeb.Mvc.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            if (ExistUserLogged())
            {
                return RedirectToAction("Index", "Home", null);
            }

            return View();
        }

        public ActionResult Entry()
        {
            var dto = new DtoUser
            {
                Email = Request.Form["User"],
                Pass = Request.Form["Pass"]
            };

            var UserFromRepository = SearchUser(dto.Email);

            if (UserFromRepository == null)
            {
                //user not founded
                return RedirectToAction("Index");
            }

            if (UserFromRepository.Pass != dto.Pass )
            {
                //password incorret
                return RedirectToAction("Index");
            }

            RequestCookie(dto.Email);

            return RedirectToAction("Index", "Home", null);
        }

        public ActionResult Register()
        {
            return RedirectToAction("index", "Register", null);
        }

        public ActionResult ForgetPass()
        {
            return RedirectToAction("index", "ForgetPass", null);
        }

        private DtoUser SearchUser(string userToSearch)
        {
            var user = new DtoUser();

            using(RepositoryUser model = new RepositoryUser())
            {
                user = model.Read().FirstOrDefault(registro => registro.Email == userToSearch);
            }

            if (user != null)
            {
                return user;
            }

            return null;
        }

        private void RequestCookie(string email)
        {
            var cokkie = new HttpCookie("user");
            cokkie.Expires = DateTime.Today.AddDays(1);
            cokkie.Value = email;

            Response.Cookies.Add(cokkie);
            Session["user"] = cokkie.Value;
        }

        private bool ExistUserLogged()
        {
            var userFromSession = Session["user"];
            var userFromCookie = Request.Cookies["user"].Value;

            if (userFromSession == userFromCookie)
            {
                return true;
            }

            return false;
        }

    }
}