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
            PrepareDashboard();
            return View();
        }

        public ActionResult Transaction()
        {
            return View();
        }

        public ActionResult Edit()
        {
            var transaction = new DtoTransaction
            {
                Category = Request.Form["category"],//pegar da tela de edite o dto de quem vai ser editado e o ID
            };
            return View();
        }

        #region PrivateMethods
        private void PrepareDashboard()
        {
            ViewBag.TopTitle = "Dashboard";

            var transactions = ReadTransactionsFromUser();

            ViewBag.Incomes = transactions.Where(x => x.TransactionType == EnumTransaction.Income).ToList();
            ViewBag.Expenses = transactions.Where(x => x.TransactionType == EnumTransaction.Expense).ToList();
            ViewBag.transactions = transactions;

            ViewBag.IncomesValue = GetTransactionsValues(ViewBag.Incomes);
            ViewBag.ExpenseValue = GetTransactionsValues(ViewBag.Expenses);
            ViewBag.Total = ViewBag.IncomesValue - ViewBag.ExpenseValue;
        }

        private IList<DtoTransaction> ReadTransactionsFromUser()
        {
            using (var repository = new RepositoryTransaction())
            {
                return repository.Read();
            }
        }

        private decimal GetTransactionsValues(IList<DtoTransaction> transactions)
        {
            decimal result = 0;

            foreach(var transaction in transactions)
            {
                result =+ transaction.Value;
            }

            return result;
        }


        #endregion
    }
}