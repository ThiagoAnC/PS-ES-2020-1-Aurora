using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiggyFinc.UIWeb.Mvc.Models
{
    public class Financial
    {
        string Name { get; set; }

        string Category { get; set; }

        DateTime PurchaseTime { get; set; }

        double Value { get; set; }
    }
}