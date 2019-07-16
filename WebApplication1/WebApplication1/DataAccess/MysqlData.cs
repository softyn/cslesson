using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using  Dapper;
using WebApplication1.DTO;

namespace WebApplication1.DataAccess
{
    public class MysqlData
    {
        private string connectionString = "Server=localhost;Port=3306;Database=webservice;Uid=root;Pwd=root";
        public void ConnectToDataBase()
        {
           // var config = Configuration.
           // connectionString = 
        }

        public List<string> LoadNames()
        {
            List<string> myNameList;
           using (MySql.Data.MySqlClient.MySqlConnection conn = new MySqlConnection(connectionString))
           {
               myNameList = conn.Query<string>("select Name from name").ToList();
           }

            return myNameList;
        }

        public void SaveNames(List<string> namesList)
        {

        }
    }
}
