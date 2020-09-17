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

            CrieCookie();

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

        private void CrieCookie()
        {
            var token = Guid.NewGuid().ToString();

            var cokkie = new HttpCookie("Autentication");
            cokkie.Expires = DateTime.Today.AddDays(1);
            cokkie.Value = token;

            Response.Cookies.Add(cokkie);
            Session["autentication"] = token;
        }

    }
}