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
            string sql = string.Format("SELECT NAME, ROLE, ENABLED FROM USERS WHERE ID = '{0}' AND PASSWORD = '{1}'", id, password);

            List<User> userList;
            SetConnectionString();
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                reader = command.ExecuteReader();

                User user;
                userList = new List<User>();
                if (reader.Read())
                {
                    user = new User();
                    user.Name = reader[0].ToString();
                    user.Role = reader[1].ToString();
                    user.Enabled = reader[2].ToString();
                    userList.Add(user);
                }
                connection.Close();
            }

            return userList;
        }
    }
}