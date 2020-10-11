using PiggyFinc.UIWeb.Mvc.Models.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PiggyFinc.UIWeb.Mvc.Models.User
{
    public class RepositoryUser : Repository
    {
        public RepositoryUser() : base()
        {
        }

        public void Create(DtoUser user)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"INSERT INTO usr VALUES (@email, @pass)";

            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@pass", user.Pass);

            cmd.ExecuteNonQuery();
        }

        public List<DtoUser> Read()
        {
            List<DtoUser> lista = new List<DtoUser>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM USR";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                DtoUser User = new DtoUser();
                User.Email = (string)reader["Email"];
                User.Pass = (string)reader["Pass"];

                lista.Add(User);
            }

            return lista;
        }

        public void Update(DtoUser user)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE USR SET Pass=@pass WHERE Email=@email";

            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@pass", user.Pass);

            cmd.ExecuteNonQuery();
        }

        public void Delete(DtoUser user)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = @"DELETE FROM USR WHERE Email=@email";

            cmd.Parameters.AddWithValue("@email", user.Email);

            cmd.ExecuteNonQuery();
        }
    }
}