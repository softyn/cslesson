using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GameAPI.DTO;
using GameAPI.DataAccess;
using Newtonsoft.Json.Linq;

namespace GameAPI.Controllers
{
    [Route("api/[controller]")]
    public class NewsValuesController : Controller
    {
        public INewsDataSource AdapterNews { get; }

        public NewsValuesController(INewsDataSource newsDataSource)
        {
            AdapterNews = newsDataSource;
            //todo:
            // - UI
            // - testy

        }

        // GET api/news
        [HttpGet()]
            public ActionResult<IEnumerable<News>> Get()
            {                
                var getNews = new NewsMysqlData();
               
                
                var list =  getNews.LoadNews(out bool success,out string errorMessage).ToList();
                if (!success)
                {
                    return BadRequest(new {Message="Error getting data."});             
                }

                return list;
            }

            // GET api/news/5
            [HttpGet("{id}")]
            public ActionResult<IEnumerable<News>> Get(int id)
            {
                var getNewsById = new NewsMysqlData();
                var list = getNewsById.LoadNewsById(id, out bool success).ToList();
            
                if (!success)
                {
                    return BadRequest(new { Message = "Error getting data." });
                }

                if (list.Count == 0)
                {
                    return NotFound(new {Message = $"{id} doesn't exist."});
                }

                return list;
            }

            // POST api/news
            [HttpPost]
            public ActionResult Post([FromBody] JObject data)
            {
                News news = new News();
                data.ToObject<News>();
                news.Title = data["title"].ToString();
                news.Text = data["text"].ToString();
                news.Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var addNews = new NewsMysqlData();
                var add = addNews.SaveNews(news, out bool success);

                if (!success)
                {
                   return BadRequest(new { Message = "Error adding data." });
                }

            return Ok(new { Message = "News successfully added." });
            }

            // PATCH api/news/5
            [HttpPatch("{id}")]
            public ActionResult Patch(int id, [FromBody] JObject data)
            {
                News news = new News();
                data.ToObject<News>();
                news.Id = id;
                var getNewsById = new NewsMysqlData();                
                var newsOld = getNewsById.LoadNewsById(id, out bool LoadSuccess).ToList();

                news.Title = data["title"].ToString().Length == 0 ? newsOld.ElementAt(0).Title : data["title"].ToString();

                news.Text = data["text"].ToString().Length == 0 ? newsOld.ElementAt(0).Text : data["text"].ToString();

                var addNews = new NewsMysqlData();
                var update = addNews.EditNews(news, out bool success);

                if (!success)
                {
                    return BadRequest(new { Message = "Error editing data." });
                }

                return Ok(new { Message = "News successfully edited." });
            }

            // DELETE api/news/5
            [HttpDelete("{id}")]
            public ActionResult Delete(int id)
            {
                var addNews = new NewsMysqlData();
                var delete = addNews.DeleteNews(id, out bool success);

                if (!success)
                {
                    return BadRequest(new { Message = "Error editing data." });
                }

                return Ok(new { Message = "News successfully edited." });
        }
        }
    }