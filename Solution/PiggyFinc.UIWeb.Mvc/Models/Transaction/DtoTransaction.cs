using PiggyFinc.UIWeb.Mvc.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiggyFinc.UIWeb.Mvc.Models.Transaction
{
    public class DtoTransaction
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public EnumTransaction TransactionType { get; set; }
        public Guid Id { get; set; }
        public DtoUser User { get; set; }
    }

    public enum EnumTransaction
    {
        Expense,
        Income
    }
}