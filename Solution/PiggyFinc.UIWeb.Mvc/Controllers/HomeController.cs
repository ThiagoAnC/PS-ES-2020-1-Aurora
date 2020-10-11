using PiggyFinc.UIWeb.Mvc.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PiggyFinc.UIWeb.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.TopTitle = "Dashboard";

            using(var repo = new RepositoryTransaction())
            {
                var list = repo.Read();
            }

            return View();
        }

        public ActionResult Transaction()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }
    }
}