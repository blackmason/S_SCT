using SCT.Models.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SCT.Models.Helpers
{
    public class AuthorizeHelper : BaseDataAccessHelper
    {
        public List<User> LoginCheck(string id, string password)
        {
            string sql = "USP_LOGIN";

            List<User> userList;
            SetConnectionString();
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", id);
                command.Parameters.AddWithValue("@PASSWORD", password);
                reader = command.ExecuteReader();

                User user;
                userList = new List<User>();
                if (reader.Read())
                {
                    user = new User();
                    user.Name = reader[0].ToString();
                    user.Enabled = reader[1].ToString();
                    userList.Add(user);
                }
                connection.Close();
            }

            return userList;
        }
    }
}