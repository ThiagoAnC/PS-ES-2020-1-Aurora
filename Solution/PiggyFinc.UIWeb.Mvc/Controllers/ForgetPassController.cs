using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PiggyFinc.UIWeb.Mvc.Controllers
{
    public class ForgetPassController : Controller
    {
        // GET: ForgetPass
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Forget()
        {
            string Email = Request.Form["User"];

            //contata suporte

            return RedirectToAction("index", "Login", null);
        }
    }
}