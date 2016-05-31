using SCT.Models.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SCT.Models.Helpers
{
    public class BoardHelper : BaseDataAccessHelper
    {
        /*
         * GetAllContents
         * GetContents
         * AddContents
         * UpdateContents
         * DeleteContents
         */

        public List<Board> GetAllContents()
        {
            string sql = "SELECT NO, CATEGORY, FIXED, SUBJECT, CREATED FROM COMM_NOTICE ORDER BY NO DESC";

            /*
                SELECT
	                NO
	                , CATEGORY
	                , FIXED
	                , SUBJECT
	                , (	CASE
			                WHEN DATEDIFF(D, CREATED, GETDATE()) >= 1 THEN CONVERT(CHAR(10), CREATED, 120)
			                ELSE LEFT(CONVERT(CHAR(12), CREATED, 114), 5)
		                END	) AS CREATED
                FROM
	                COMM_NOTICE
             */

            SetConnectionString();
            List<Board> bbsList = new List<Board>();
            Board bbs;
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    bbs = new Board();
                    bbs.No = reader[0].ToString();
                    bbs.Category = reader[1].ToString();
                    bbs.Fixed = reader[2].ToString();
                    bbs.Subject = reader[3].ToString();
                    bbs.Created = reader[4].ToString();
                    bbsList.Add(bbs);
                }

                connection.Close();
            }

            return bbsList;
        }

        public int AddContents(string subject, string contents)
        {
            string revContents = contents.Replace("'", "''");
            
            string sql = string.Format("INSERT INTO COMM_NOTICE (SUBJECT, CONTENTS) VALUES ('{0}','{1}')", subject, revContents);

            int line;
            SetConnectionString();
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                line = command.ExecuteNonQuery();
                connection.Close();
            }

            return line;
        }

        public Board GetContents(int? id)
        {
            string sql = string.Format("SELECT SUBJECT, CONTENTS, LEFT(CONVERT(CHAR(19), CREATED, 120), 16) FROM COMM_NOTICE WHERE NO = {0}", id);

            SetConnectionString();
            Board bbs = new Board();
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    bbs.Subject = reader[0].ToString();
                    bbs.Contents = reader[1].ToString();
                    bbs.Created = reader[2].ToString();
                }

                connection.Close();
            }

            return bbs;
        }
    }
}