using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PiggyFinc.UIWeb.Mvc.Models.Transaction
{
    public class RepositoryTransaction : IDisposable
    {
        private SqlConnection connection;

        public RepositoryTransaction()
        {
            string strConn = "data source=LAPTOP-JGJDC4IB\\SQLEXPRESS;initial catalog=PIG_BASE;integrated security=True;";

            connection = new SqlConnection(strConn);

            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }

        public void Create(DtoTransaction transaction)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"INSERT INTO TRANSACTIONS VALUES 
                               (@TRANSACTIONNAME, @TRANSACTIONSCATEGORY
                                @TRANSACTIONDATE, @TRANSACTIONSVALUE
                                @TRANSACTIONSTYPE, @ID
                                @USR)";

            cmd.Parameters.AddWithValue("@TRANSACTIONNAME", transaction.Name);
            cmd.Parameters.AddWithValue("@TRANSACTIONSCATEGORY", transaction.Category);
            cmd.Parameters.AddWithValue("@TRANSACTIONDATE", transaction.Date);
            cmd.Parameters.AddWithValue("@TRANSACTIONSVALUE", transaction.Value);
            cmd.Parameters.AddWithValue("@TRANSACTIONSTYPE", (int)transaction.TransactionType);
            cmd.Parameters.AddWithValue("@ID", transaction.Id);
            cmd.Parameters.AddWithValue("@USR", transaction.User.Email);

            cmd.ExecuteNonQuery();
        }

        public List<DtoTransaction> Read()
        {
            List<DtoTransaction> lista = new List<DtoTransaction>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM TRANSACTIONS";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                DtoTransaction transaction = new DtoTransaction();
                transaction.Name = (string)reader["TRANSACTIONNAME"];
                transaction.Category = (string)reader["TRANSACTIONSCATEGORY"];
                transaction.Date = (DateTime)reader["TRANSACTIONDATE"];
                transaction.Value = (decimal)reader["TRANSACTIONSVALUE"];
                transaction.TransactionType = (EnumTransaction)reader["TRANSACTIONSTYPE"];
                transaction.Id = (Guid)reader["ID"];
                transaction.User.Email = (string)reader["USR, transaction"];

                lista.Add(transaction);
            }

            return lista;
        }

        public void Update(DtoTransaction transaction)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE TRANSACTIONS SET 
                                TRANSACTIONNAME=@TRANSACTIONNAME 
                                TRANSACTIONSCATEGORY=@TRANSACTIONSCATEGORY
                                TRANSACTIONDATE=@TRANSACTIONDATE
                                TRANSACTIONSVALUE=@TRANSACTIONSVALUE
                                TRANSACTIONSTYPE=@TRANSACTIONSTYPE
                                WHERE ID=@ID";

            cmd.Parameters.AddWithValue("@TRANSACTIONNAME", transaction.Name);
            cmd.Parameters.AddWithValue("@TRANSACTIONSCATEGORY", transaction.Category);
            cmd.Parameters.AddWithValue("@TRANSACTIONDATE", transaction.Date);
            cmd.Parameters.AddWithValue("@TRANSACTIONSVALUE", transaction.Value);
            cmd.Parameters.AddWithValue("@TRANSACTIONSTYPE", (int)transaction.TransactionType);
            cmd.Parameters.AddWithValue("@ID", transaction.Id);
            cmd.Parameters.AddWithValue("@USR", transaction.User.Email);

            cmd.ExecuteNonQuery();
        }

        public void Delete(DtoTransaction transaction)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"DELETE FROM TRANSACTIONS WHERE ID=@ID";

            cmd.Parameters.AddWithValue("@ID", transaction.Id);

            cmd.ExecuteNonQuery();
        }
    }
}