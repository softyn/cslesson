using System;
using System.Collections.Generic;
using System.Linq;
using MySql.Data.MySqlClient;
using Dapper;
using GameAPI.DTO;

namespace GameAPI.DataAccess
{
    public interface INewsDataSource
    {
        IEnumerable<News> LoadNews(out bool success);
        IEnumerable<News> LoadNewsById(int id, out bool success);
        bool SaveNews(News news, out bool success);
        bool DeleteNews(int id, out bool success);
    }

    public class NewsMysqlData :INewsDataSource
    {
        private const string ConnectionString = "Server=localhost;Port=3306;Database=game;Uid=root;Pwd=root";
        private readonly MySqlConnection _conn = new MySqlConnection(ConnectionString);

        public IEnumerable<News> LoadNews(out bool success)
        {
            success = true;            
            try
            {
                var news = _conn.Query<News>("select id,title,text,date from news");               
                success = true;
                return news.ToList();
            }
            catch (Exception)
            {
                success = false;

            }

            return Enumerable.Empty<News>();
        }

        public IEnumerable<News> LoadNewsById(int id, out bool success)
        {
            success = true;
            try
            {
                var news = _conn.Query<News>("select id,title,text,date from news where id = " + id);      
                return news.ToList();
            }
            catch (Exception)
            {
                success = false;

            }
            return Enumerable.Empty<News>();
        }

        public bool SaveNews(News news, out bool success)
        {
            success = true;
            try
            {
                var query = "INSERT INTO news (`title`, `text`, `date`) VALUES('" + news.Title + "', '" + news.Text + "', '" + news.Date + "')";
                _conn.Query<News>(query);
                _conn.Close();
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }
        public bool DeleteNews(int id, out bool success)
        {
            success = true;
            try
            {
                _conn.Query<News>("DELETE FROM news WHERE id = " + id).Single();
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
