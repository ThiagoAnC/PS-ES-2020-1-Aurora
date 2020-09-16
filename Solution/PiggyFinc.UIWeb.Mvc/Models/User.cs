using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PiggyFinc.UIWeb.Mvc.Models
{
    public class User
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public List<Financial> Entries { get; set; }

        public List<Financial> Outings { get; set; }
    }
}