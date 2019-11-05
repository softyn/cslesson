using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MySql.Data.MySqlClient;
using Dapper;
using GameAPI.DTO;

namespace GameAPI.DataAccess
{
    public interface IProductsDataSource
    {
        List<string> LoadProducts(out bool success);
        List<string> LoadProductsById(int id, out bool success);
        bool DeleteProduct(int id, out bool success);
    }
    public class ProductsMysqlData :IProductsDataSource
    {
        private const string ConnectionString = "Server=localhost;Port=3306;Database=game;Uid=root;Pwd=root";
        private readonly MySqlConnection _conn = new MySqlConnection(ConnectionString);

        public List<string> LoadProducts(out bool success)
        {
            success = true;
            var myNameList = new List<string>();
            try
            {
                var amount = _conn.Query<int>("select Count(*) from products").Single();
                for (var id = 1; id <= amount; id++)
                {
                    var products = GetProductById(id);
                    myNameList.Add(products.Id.ToString());
                    myNameList.Add(products.Name);
                    myNameList.Add(products.Description);
                    myNameList.Add(products.Price.ToString(CultureInfo.InvariantCulture));
                }
                success = true;
            }
            catch (Exception)
            {
                success = false;

            }
            return myNameList;
        }

        public List<string> LoadProductsById(int id, out bool success)
        {
            success = true;
            var myNameList = new List<string>();
            try
            {
                var products = GetProductById(id);
                myNameList.Add(products.Id.ToString());
                myNameList.Add(products.Name);
                myNameList.Add(products.Description);
                myNameList.Add(products.Price.ToString(CultureInfo.InvariantCulture));

                success = true;
            }
            catch (Exception)
            {
                success = false;

            }
            return myNameList;
        }

        public bool DeleteProduct(int id, out bool success)
        {
            success = true;
            try
            {
                var news = _conn.Query<News>("DELETE FROM products WHERE id = " + id).Single();
                _conn.Close();
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }

        private Products GetProductById(int id)
        {
            var product = _conn.Query<Products>("select id,name,description,price from products where id = " + id).Single();
            _conn.Close();
            return product;
        }
    }
}
