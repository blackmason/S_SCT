using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SCT.Models.Helpers
{
    public class AuthorizeHelper : BaseDataAccessHelper
    {
        public string LoginCheck(string id, string password)
        {
            string result = null;
            string sql = string.Format("SELECT NAME, ROLE, ENABLED FROM USERS WHERE ID = '{0}' AND PASSWORD = '{1}'", id, password);

            SetConnectionString();
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string name = reader[0].ToString();
                    string role = reader[1].ToString();
                    string enabled = reader[2].ToString();

                    result = string.Format("{{ name: '{0}', role: '{1}', enabled: '{2}' }}", name, role, enabled); 
                }
                connection.Close();
            }

            return result;
        }
    }
}