using PiggyFinc.UIWeb.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PiggyFinc.UIWeb.Mvc.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewUser()
        {
            var user = new User
            {
                Email = Request.Form["User"],
                Password = Request.Form["Pass"]
            };

            if (AlreadyRegistered(user))
            {
                //Dispara excecao usuario ja cadastrado
            }
            else
            {
                RegisterNew(user);
            }

            return RedirectToAction("index", "Login", null);
        }

        private void RegisterNew(User user)
        {
            throw new NotImplementedException();
        }

        private bool AlreadyRegistered(User user)
        {
            return false;
        }
    }
}