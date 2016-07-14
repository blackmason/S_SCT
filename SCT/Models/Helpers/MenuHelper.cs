using SCT.Models.Domains;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SCT.Models.Helpers
{
    public class MenuHelper : BaseDataAccessHelper
    {
        /* 전체메뉴 가져오기 */
        public List<Menu> GetAllMenus()
        {
            string sql = "SELECT CODE, P_CODE, NAME, URL, ROLE, ENABLED, MODIFIED, CREATED FROM MENUS";

            SetConnectionString();
            Menu menus;
            List<Menu> menuList = new List<Menu>();
            using (connection = new SqlConnection(connectionString))
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    menus = new Menu();
                    menus.Code = reader[0].ToString();
                    menus.ParentCode = reader[1].ToString();
                    menus.Name = reader[2].ToString();
                    menus.Url = reader[3].ToString();
                    menus.Role = reader[4].ToString();
                    menus.Enabled = reader[5].ToString();
                    menus.Modified = reader[6].ToString();
                    menus.Created = reader[7].ToString();
                    menuList.Add(menus);
                }
                connection.Close();
            }
            return menuList;
        }

        public Menu GetMenus(string code)
        {
            string sql = string.Format("SELECT CODE, P_CODE, NAME, URL, ROLE, ENABLED FROM MENUS WHERE CODE = '{0}'", code);
            
            Menu menu = null;
            SetConnectionString();
            using(connection = new SqlConnection(connectionString))
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    menu = new Menu();
                    menu.Code = reader[0].ToString();
                    menu.ParentCode = reader[1].ToString();
                    menu.Name = reader[2].ToString();
                    menu.Url = reader[3].ToString();
                    menu.Role = reader[4].ToString();
                    menu.Enabled = reader[5].ToString();
                }
                connection.Close();
            }

            return menu;
        }

        public void AddMenu(string code, string parentCode, string name, string url)
        {
            throw new NotImplementedException();
        }

        public void UpdateMenu(string code)
        {
            throw new NotImplementedException();
        }

        public void DeleteMenu(string code)
        {
            throw new NotImplementedException();
        }
    }
}