using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PiggyFinc.UIWeb.Mvc.Models.Common
{
    public class Repository : IDisposable
    {
        protected SqlConnection connection { get; }

        private const string SERVERNAME = "LAPTOP-JGJDC4IB\\SQLEXPRESS";
        private const string CATALOG = "PIG_BASE";

        public Repository()
        {
            string strConn = $"data source={SERVERNAME};initial catalog={CATALOG};integrated security=True;";

            connection = new SqlConnection(strConn);

            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}