using PiggyFinc.UIWeb.Mvc.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PiggyFinc.UIWeb.Mvc.Controllers
{
    public class ExpensesController : Controller
    {
        public ActionResult Index()
        {
            PrepareDashboard();
            return View();
        }

        public ActionResult Edit(DtoTransaction dto)
        {
            if (RequestIsLoaded())
            {
                dto.Value = decimal.Parse(Request.Form["value"]);
                dto.Date = DateTime.Today;
                dto.User = Request.Cookies["user"].Value;
                dto.TransactionType = EnumTransaction.Income;

                return RedirectToAction("Save", dto);
            }

            else
            {
                return View();
            }

        }

        public ActionResult Save(DtoTransaction dto)
        {
            if (NewItem(dto))
            {
                dto.Id = Guid.NewGuid();
                using (var repository = new RepositoryTransaction())
                {
                    repository.Create(dto);
                }
            }
            else
            {
                using (var repository = new RepositoryTransaction())
                {
                    repository.Update(dto);
                }

            }

            return RedirectToAction("Index"); ;
        }

        public ActionResult Delete(DtoTransaction dto)
        {
            if (RequestIsLoaded())
            {
                dto.Value = decimal.Parse(Request.Form["value"]);

                using (var repository = new RepositoryTransaction())
                {
                    repository.Delete(dto);
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        #region PrivateMethods
        private void PrepareDashboard()
        {
            ViewBag.TopTitle = "Dashboard";

            var transactions = ReadTransactionsFromUser();

            ViewBag.Expenses = transactions.Where(x => x.TransactionType == EnumTransaction.Expense).ToList();

            ViewBag.ExpensesValue = GetTotalTransactionsValues(ViewBag.Expenses);
        }

        private IList<DtoTransaction> ReadTransactionsFromUser()
        {
            using (var repository = new RepositoryTransaction())
            {
                return repository.Read();
            }
        }

        private bool RequestIsLoaded()
        {
            return !string.IsNullOrEmpty(Request.Form["value"]);
        }

        private bool NewItem(DtoTransaction dto)
        {
            return dto.Id == Guid.Empty;
        }

        private decimal GetTotalTransactionsValues(IList<DtoTransaction> transactions)
        {
            decimal result = 0;

            foreach (var transaction in transactions)
            {
                result += transaction.Value;
            }

            return result;
        }


        #endregion

    }
}