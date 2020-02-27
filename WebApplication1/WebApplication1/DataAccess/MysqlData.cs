using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using Dapper;

namespace WebApplication1.DataAccess
{
    public interface IDataSource
    {
        List<string> LoadNames(out bool success);
        bool SaveNames(List<string> namesList);
        bool DeleteNames(int Id);
    }

    public class MysqlData :IDataSource
    {

        public MysqlData()
        {
        }
        private string connectionString = "Server=localhost;Port=3306;Database=webservice;Uid=root;Pwd=root";
        public void ConnectToDataBase()
        {
           // var config = Configuration.
           // connectionString = 
        }

        public List<string> LoadNames(out bool success)
        {
            success = true;
            List<string> myNameList =new List<string>();
            try
            {
                using (MySql.Data.MySqlClient.MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    myNameList = conn.Query<string>("select Name from name").ToList();
                }
            }
            catch (Exception e)
            {
                success = false;

            }
           

            return myNameList;
        }

        public bool SaveNames(List<string> namesList)
        {
           
            bool success = true;
            try
            {
                
                foreach (var name in namesList)
                {
                    using (MySql.Data.MySqlClient.MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Query<string>("insert into Name(Name) values (@Name)", new {Name = name}).ToList();
                    }
                }
            }
            catch (Exception e)
            {
                success = false;
            }

            return success;
        }
        public bool DeleteNames(int Id)
        {

            bool success = true;
            try
            {    
                    using (MySql.Data.MySqlClient.MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Execute("delete from Name where Id=@Id ", new {Id = Id});
                    }
               
            }
            catch (Exception e)
            {
                success = false;
            }

            return success;
        }
    }
}
