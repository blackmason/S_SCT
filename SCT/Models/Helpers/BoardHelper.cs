using SCT.Models.Domains;
using System;
using System.Collections.Generic;
using System.Data;
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

        // 게시물 리스트
        public List<Board> GetAllContents()
        {
            string sql = "NOTICE_LIST_USP";

            SetConnectionString();
            List<Board> bbsList = new List<Board>();
            Board bbs;
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.StoredProcedure;

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    bbs = new Board();
                    bbs.No = reader[0].ToString();
                    bbs.Category = reader[1].ToString();
                    bbs.Fixed = reader[2].ToString();
                    bbs.Subject = reader[3].ToString();
                    bbs.Author = reader[4].ToString();
                    bbs.Created = reader[5].ToString();
                    bbsList.Add(bbs);
                }
                connection.Close();
            }

            return bbsList;
        }

        // 게시물 입력
        public int AddContents(string subject, string contents)
        {
            string revContents = contents.Replace("'", "''");
            
            string sql = string.Format("INSERT INTO COMM_NOTICE (SUBJECT, CONTENTS) VALUES ('{0}','{1}')", subject, revContents);

            int result;
            SetConnectionString();
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                result = command.ExecuteNonQuery();
                connection.Close();
            }

            return result;
        }

        // 게시물 보기
        public Board GetContents(string id)
        {
            //string sql = string.Format("SELECT SUBJECT, CONTENTS, AUTHOR, LEFT(CONVERT(CHAR(19), CREATED, 120), 16), VISIT FROM COMM_NOTICE WHERE NO = {0}", id);
            // 어떤 bbs 타입인지 인자로 구분하고, 프로시저 실행 할 것.
            string sql = "NOTICE_VIEW_USP";

            SetConnectionString();
            Board bbs = new Board();
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@NO", id);
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    bbs.No = reader[0].ToString();
                    bbs.Subject = reader[1].ToString();
                    bbs.Contents = reader[2].ToString();
                    bbs.Author = reader[3].ToString();
                    bbs.Created = reader[4].ToString();
                    bbs.Visit = reader[5].ToString();
                }
                connection.Close();
            }

            return bbs;
        }

        // 게시물 삭제
        public int DeleteContents(string bbsId, string id)
        {
            string tblName = ReturnTblName(bbsId);
            string sql = string.Format("DELETE FROM {0} WHERE NO = {1}", tblName, id);

            int result;
            SetConnectionString();
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                result = command.ExecuteNonQuery();
                connection.Close();
            }

            return result;
        }

        public int EditContents(string bbsId, string id, string subject, string contents)         //업데이트문으로 교체
        {
            string tblName = ReturnTblName(bbsId);
            string sql = string.Format("UPDATE {0} SET SUBJECT = '{1}', CONTENTS = '{2}' WHERE NO = '{3}'", tblName, subject, contents, id);
            //string sql = "NOTICE_USP";

            int result;
            Board bbs = new Board();
            SetConnectionString();
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                result = command.ExecuteNonQuery();
                connection.Close();
            }

            return result;
        }

        public List<Board> SummaryData(string bbsId)
        {
            string tblName = ReturnTblName(bbsId);
            string sql = string.Format("SELECT TOP 5 SUBJECT, CONVERT(CHAR(10), CREATED, 120) FROM {0} ORDER BY CREATED DESC", tblName);

            List<Board> bbsList = new List<Board>();
            Board bbs;
            SetConnectionString();
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    bbs = new Board();
                    bbs.Subject = reader[0].ToString();
                    bbs.Created = reader[1].ToString();
                    bbsList.Add(bbs);
                }
                connection.Close();
            }

            return bbsList;
        }

        public string ReturnTblName(string bbsId)
        {
            string tblName;

            if (" " == bbsId)           // 추후 수정 필요
            {
                tblName = "BBS_NOTICE";
                return tblName;
            }
            else
            {
                return "BBS_NOTICE";
            }
        }

        
    }
}