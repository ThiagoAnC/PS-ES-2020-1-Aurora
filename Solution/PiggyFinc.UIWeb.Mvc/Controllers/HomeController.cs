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

        #region PrivateMethods
        private void PrepareDashboard()
        {
            ViewBag.TopTitle = "Dashboard";

            var transactions = ReadTransactionsFromUser();

            ViewBag.Incomes = transactions.Where(x => x.TransactionType == EnumTransaction.Income).ToList();
            ViewBag.Expenses = transactions.Where(x => x.TransactionType == EnumTransaction.Expense).ToList();
            ViewBag.transactions = transactions;

            ViewBag.IncomesValue = GetTotalTransactionsValues(ViewBag.Incomes);
            ViewBag.ExpenseValue = GetTotalTransactionsValues(ViewBag.Expenses);
            ViewBag.Total = ViewBag.IncomesValue - ViewBag.ExpenseValue;
        }

        private IList<DtoTransaction> ReadTransactionsFromUser()
        {
            using (var repository = new RepositoryTransaction())
            {
                return repository.Read();
            }
        }

        private bool TransactionIsDefined(DtoTransaction dto)
        {
            return dto.TransactionType != EnumTransaction.blank;
        }

        private void TransactionDefine(DtoTransaction dto)
        {
            throw new NotImplementedException();
        }

        private decimal GetTotalTransactionsValues(IList<DtoTransaction> transactions)
        {
            decimal result = 0;

            foreach(var transaction in transactions)
            {
                result += transaction.Value;
            }

            return result;
        }


        #endregion
    }
}