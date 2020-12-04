using PiggyFinc.UIWeb.Mvc.Models.Common;
using PiggyFinc.UIWeb.Mvc.Models.User;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace PiggyFinc.UIWeb.Mvc.Models.Transaction
{
    public class RepositoryTransaction : Repository
    {
        public void Create(DtoTransaction transaction)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = String.Format(
                "INSERT INTO TRANSACTIONS VALUES('{0}','{1}','{2}',{3},{4},'{5}','{6}')",
                 transaction.Name,
                 transaction.Category,
                 transaction.Date.ToString("yyyy-MM-dd"),
                 transaction.Value,
                 (int)transaction.TransactionType,
                 transaction.Id,
                 transaction.User);

            cmd.ExecuteNonQuery();
        }

        public List<DtoTransaction> Read()
        {
            List<DtoTransaction> lista = new List<DtoTransaction>();
            string userFromCookie = HttpContext.Current.Request.Cookies["user"].Value;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = $"SELECT * FROM TRANSACTIONS WHERE USR = '{userFromCookie}'";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                DtoTransaction transaction = new DtoTransaction();
                transaction.Name = (string)reader["TRANSACTIONNAME"];
                transaction.Category = (string)reader["TRANSACTIONSCATEGORY"];
                transaction.Date = (DateTime)reader["TRANSACTIONDATE"];
                transaction.Value = (decimal)reader["TRANSACTIONSVALUE"];

                transaction.TransactionType = (EnumTransaction)(decimal)reader["TRANSACTIONSTYPE"];

                transaction.Id = Guid.Parse((string)reader["ID"]);
                transaction.User = userFromCookie;

                lista.Add(transaction);
            }

            return lista;
        }

        public void Update(DtoTransaction transaction)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = String.Format(
                "UPDATE TRANSACTIONS SET TRANSACTIONNAME='{0}', TRANSACTIONSCATEGORY='{1}', TRANSACTIONSVALUE='{2}' WHERE ID = '{3}'",
                 transaction.Name,
                 transaction.Category,
                 transaction.Value,
                 transaction.Id);

            cmd.ExecuteNonQuery();
        }

        public void Delete(DtoTransaction transaction)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = string.Format("DELETE FROM TRANSACTIONS WHERE ID='{0}'",transaction.Id);

            cmd.ExecuteNonQuery();
        }
    }
}