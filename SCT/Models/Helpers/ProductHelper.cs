using SCT.Models.Domains;
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

        public List<Product> GetAllProducts()
        {
            string sql = "SELECT SEQ, PRODUCT_CD, MODEL_NM, PRODUCT_NM, UNIT, PRICE, CONTENTS, ENABLED, MODIFIED, CREATED FROM PROD_PRODUCTS ORDER BY SEQ DESC";

            List<Product> list;
            SetConnectionString();
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                reader = command.ExecuteReader();

                list = new List<Product>();
                while (reader.Read())
                {
                    Product product = new Product();
                    product.Seq = reader["SEQ"].ToString();
                    product.ProductCode = reader["PRODUCT_CD"].ToString();
                    product.ModelName = reader["MODEL_NM"].ToString();
                    product.ProductName = reader["PRODUCT_NM"].ToString();
                    product.Unit = reader["UNIT"].ToString();
                    product.Price = reader["PRICE"].ToString();
                    product.Enabled = reader["ENABLED"].ToString();
                    product.Modified = reader["MODIFIED"].ToString();
                    product.Contents = reader["CONTENTS"].ToString();
                    product.Created = reader["CREATED"].ToString();
                    list.Add(product);
                }

                connection.Close();
            }

            return list;

        }

    }
}