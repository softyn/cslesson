using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameAPI.DTO;
using GameAPI.DataAccess;

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
        private List<string> lista;
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
            public ActionResult<IEnumerable<string>> Get()
            {                
                DataAccess.NewsMysqlData getNews = new DataAccess.NewsMysqlData();
                return getNews.LoadNews(out bool success);                     
            }

            // GET api/news/5
            [HttpGet("{id}")]
            public ActionResult<IEnumerable<string>> Get(int id)
            {
                DataAccess.NewsMysqlData getNewsById = new DataAccess.NewsMysqlData();
                return getNewsById.LoadNewsById(id, out bool success);
            }

            // POST api/news
            [HttpPost]
            public void Post([FromBody] string title, string text)
            {
                News news = new News();
                news.Title = title;
                news.Text = text;
                news.Date = DateTime.Now;
                DataAccess.NewsMysqlData addNews = new DataAccess.NewsMysqlData();
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
                DataAccess.NewsMysqlData addNews = new DataAccess.NewsMysqlData();
                addNews.DeleteNews(id, out bool success);
            }
        }
    }
