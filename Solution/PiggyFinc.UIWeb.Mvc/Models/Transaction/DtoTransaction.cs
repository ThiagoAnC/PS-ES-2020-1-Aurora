using PiggyFinc.UIWeb.Mvc.Models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PiggyFinc.UIWeb.Mvc.Models.Transaction
{
    public class DtoTransaction
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Category { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public decimal Value { get; set; }
        public EnumTransaction TransactionType { get; set; }
        public Guid Id { get; set; }
        public string User { get; set; }

        public DtoTransaction(){
            this.TransactionType = EnumTransaction.blank;
        }
    }

    public enum EnumTransaction
    {
        Expense,
        Income,
        blank
    }
}