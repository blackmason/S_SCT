using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SCT.Models.Helpers
{
    public class ProductHelper : BaseDataAccessHelper
    {
        public int AddProduct(string productGb, string modelName, string productName, string contents)
        {
            string sql = "PROD_ADD_USP";
            int result;
            SetConnectionString();
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PROD_GB", productGb);
                command.Parameters.AddWithValue("@MODEL_NM", modelName);
                command.Parameters.AddWithValue("@PROD_NM", productName);
                command.Parameters.AddWithValue("@CONTENTS", contents);
                result = command.ExecuteNonQuery();

                connection.Close();
            }

            return result;
        }

    }
}