using PiggyFinc.UIWeb.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PiggyFinc.UIWeb.Mvc.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Entry()
        {
            var dto = new User
            {
                Email = Request.Form["User"],
                Password = Request.Form["Pass"]
            };

            var UserFromRepository = SearchUser(dto.Email);

            if (UserFromRepository.Password != dto.Password )
            {
                return RedirectToAction("Index");
            }

            CrieCookie();

            return RedirectToAction("Index", "Home", null);
        }

        public ActionResult Register()
        {
            return RedirectToAction("index", "Register", null);
        }

        private User SearchUser(string userToSearch)
        {
            User user = RepositorySearch(userToSearch);

            if(user != null)
            {
                return user;
            }

            return null;
        }

        /// <summary>
        /// Apagar quando tiver persistencia pronta
        /// </summary>
        /// <param name="userToSearch"></param>
        /// <returns></returns>
        private User RepositorySearch(string userToSearch)
        {
            return new User
            {
                Email = "sa@pig.com",
                Password = "admin"
            };
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