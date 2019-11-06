using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using Dapper;
using GameAPI.DTO;

namespace GameAPI.DataAccess
{
    public interface IProductsDataSource
    {
        IEnumerable<Products> LoadProducts(out bool success);
        IEnumerable<Products> LoadProductsById(int id, out bool success);
        bool SaveProduct(Products product, out bool success);
        bool DeleteProduct(int id, out bool success);
    }
    public class ProductsMysqlData :IProductsDataSource
    {
        private const string ConnectionString = "Server=localhost;Port=3306;Database=game;Uid=root;Pwd=root";
        private readonly MySqlConnection _conn = new MySqlConnection(ConnectionString);

        public IEnumerable<Products> LoadProducts(out bool success)
        {
            success = true;
            try
            {
                var products = _conn.Query<Products>("select id,name,description,price from products");               
                success = true;
                return products.ToList();
            }
            catch (Exception)
            {
                success = false;

            }
            return Enumerable.Empty<Products>();
        }

        public IEnumerable<Products> LoadProductsById(int id, out bool success)
        {
            success = true;
            try
            {
                var products = _conn.Query<Products>("select id,name,description,price from products where id = " + id);
                return products.ToList();
            }
            catch (Exception)
            {
                success = false;

            }

            return Enumerable.Empty<Products>();
        }

        public bool SaveProduct(Products product, out bool success)
        {
            success = true;
            try
            {
                var query = "INSERT INTO news (`title`, `text`, `date`) VALUES('" + product.Name + "', '" + product.Description + "', '" + product.Price + "')";
                _conn.Query<News>(query);
                _conn.Close();
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }

        public bool DeleteProduct(int id, out bool success)
        {
            success = true;
            try
            {
                _conn.Query<News>("DELETE FROM products WHERE id = " + id);
                _conn.Close();
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }
    }
}
