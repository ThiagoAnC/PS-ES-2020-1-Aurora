using PiggyFinc.UIWeb.Mvc.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PiggyFinc.UIWeb.Mvc.Controllers
{
    public class RegisterController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewUser()
        {
            if(Request.Form["Pass"] != Request.Form["Pass-Replic"])
            {
                return RedirectToAction("index", "Register", null);
            }

            var user = new DtoUser
            {
                Email = Request.Form["User"],
                Pass = Request.Form["Pass"]
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

        private void RegisterNew(DtoUser user)
        {
            using (RepositoryUser model = new RepositoryUser())
            {
                model.Create(user);
            }
        }

        private bool AlreadyRegistered(DtoUser user)
        {
            var lista = new List<DtoUser>();

            using (RepositoryUser model = new RepositoryUser())
            {
                lista = model.Read();
            }

            foreach (var registro in lista)
            {
                if (user.Email == registro.Email)
                {
                    return true;
                }
            }

            return false;
        }
    }
}