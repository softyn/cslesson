using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GameAPI.DTO;
using GameAPI.DataAccess;
using Newtonsoft.Json.Linq;

namespace GameAPI.Controllers
{
    public enum NewsWriteStatus //do czego to tworzyliśmy?
    {
        Written = 56,
        Deleted = 78,
        Empty = 1000
    }

    [Route("api/[controller]")]
    public class NewsValuesController : Controller
    {
        private INewsDataSource _adapterNews; //do czego to tworzyliśmy?
        public NewsValuesController(INewsDataSource newsDataSource)
        {
            _adapterNews = newsDataSource;
            //todo:
            // - pozostale metody
            // - obsluga bledow
            // - testy
            // - UI
        }

            // GET api/news
            [HttpGet()]
            public ActionResult<IEnumerable<News>> Get()
            {                
                var getNews = new NewsMysqlData();
                return getNews.LoadNews(out bool success).ToList();                     
            }

            // GET api/news/5
            [HttpGet("{id}")]
            public ActionResult<IEnumerable<News>> Get(int id)
            {
                var getNewsById = new NewsMysqlData();
                return getNewsById.LoadNewsById(id, out bool success).ToList();
            }

            // POST api/news
            [HttpPost]
            public void Post([FromBody] JObject data)
            {
                News news = new News();
                data.ToObject<News>();
                news.Title = data["title"].ToString();
                news.Text = data["text"].ToString();
                news.Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var addNews = new DataAccess.NewsMysqlData();
                addNews.SaveNews(news, out bool success);
            }

            // PUT api/news/5
            [HttpPut("{id}")]
            public void Put(int id, [FromBody] string value)
            {
            }

            // DELETE api/news/5
            [HttpDelete("{id}")]
            public void Delete(int id)
            {
                var addNews = new DataAccess.NewsMysqlData();
                addNews.DeleteNews(id, out bool success);
            }
        }
    }