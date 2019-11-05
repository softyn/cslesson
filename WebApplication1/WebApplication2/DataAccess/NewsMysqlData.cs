using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MySql.Data.MySqlClient;
using Dapper;
using GameAPI.DTO;

namespace GameAPI.DataAccess
{
    public interface INewsDataSource
    {
        List<string> LoadNews(out bool success);
        List<string> LoadNewsById(int id, out bool success);
        bool SaveNews(News news, out bool success);
        bool DeleteNews(int id, out bool success);
    }

    public class NewsMysqlData :INewsDataSource
    {
        private const string ConnectionString = "Server=localhost;Port=3306;Database=game;Uid=root;Pwd=root";
        private readonly MySqlConnection _conn = new MySqlConnection(ConnectionString);

        public List<string> LoadNews(out bool success)
        {
            success = true;
            var myNameList = new List<string>();
            try
            {            
                var amount = _conn.Query<int>("select Count(*) from news").Single();
                for (var id = 1; id <= amount; id++)
                {
                    var news = GetNewsById(id);
                    myNameList.Add(news.Id.ToString());
                    myNameList.Add(news.Title);
                    myNameList.Add(news.Text);
                    myNameList.Add(news.Date.ToString(CultureInfo.CurrentCulture));
                }
                success = true;                
            }
            catch (Exception)
            {
                success = false;

            }          
            return myNameList;
        }

        public List<string> LoadNewsById(int id, out bool success)
        {
            success = true;
            var myNameList = new List<string>();
            try
            {
                var news = GetNewsById(id);
                myNameList.Add(news.Id.ToString());
                myNameList.Add(news.Title);
                myNameList.Add(news.Text);
                myNameList.Add(news.Date.ToString(CultureInfo.CurrentCulture));                
                success = true;
            }
            catch (Exception)
            {
                success = false;

            }
            return myNameList;
        }

        public bool SaveNews(News news, out bool success)
        {
            var title = news.Title;
            var text = news.Text;
            var date = news.Date;
            success = true;
            try
            {
                //INSERT INTO `game`.`news` (`title`, `text`, `date`) VALUES ('Title3', 'News3 News3', '2019-11-05 11:00:00');
                //var query = "INSERT INTO `game`.`news` (`title`, `text`, `date`) VALUES('{news.Title}', '{news.Text}', '{news.Date}')";
                var addNews = _conn.Query<News>("INSERT INTO `game`.`news` (`title`, `text`, `date`) VALUES('{news.Title}', '{news.Text}', '{news.Date}')").Single();
                _conn.Close();

                //var list = _conn.Query<string>("insert into Name(Name) values (@Name)", new {Name = name}).ToList();

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
                var news = _conn.Query<News>("DELETE FROM news WHERE id = " + id).Single();
                _conn.Close();
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }

        private News GetNewsById(int id)
        {
            var news = _conn.Query<News>("select id,title,text,date from news where id = " + id).Single();
            _conn.Close();
            return news;
        }
    }
}
