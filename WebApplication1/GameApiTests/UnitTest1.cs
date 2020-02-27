using System;
using GameAPI.DataAccess;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using GameAPI.DTO;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace GameApiTests
{
    public class myMoq : INewsDataSource
    {
        bool INewsDataSource.DeleteNews(int id, out bool success)
        {
            success = true;
            return true;
        }

        bool INewsDataSource.EditNews(News news, out bool success)
        {
            success = true;
            return true;
        }

        IEnumerable<News> INewsDataSource.LoadNews(out bool success, out string errorMessage)
        {
            success = true;
            errorMessage = "";
            var result = new List<News>();            
            return result;
        }

        IEnumerable<News> INewsDataSource.LoadNewsById(int id, out bool success)
        {
            success = true;
            var result = new List<News>();
            return result;
        }

        bool INewsDataSource.SaveNews(News news, out bool success)
        {
            success = true;
            return true;
        }
    }

    public class UnitTest1
    {
        [Fact]
        public void Get_CheckProperOutput()
        {
            //Setup
            var moq = new Moq.Mock<INewsDataSource>();
            bool success=true;
            var err1 = "";
            GameAPI.DTO.News news_test = new GameAPI.DTO.News();
            news_test.Text = null;
            moq.Setup(x => x.LoadNews(out success, out err1)).Returns(new List<GameAPI.DTO.News>() {news_test});
            GameAPI.Controllers.NewsValuesController controller = new GameAPI.Controllers.NewsValuesController(moq.Object);

            //Do
            var result = controller.Get();
            //Assert

            Xunit.Assert.Equal(news_test.Text, result.Value.First().Text);
        }

        [Fact]
        public void Get_CheckProperOutputById()
        {
            //Setup
            var moq = new Moq.Mock<INewsDataSource>();
            bool success=true;
            var err1 = "";
            GameAPI.DTO.News news_test = new GameAPI.DTO.News();
            news_test.Text = "test1";
            news_test.Id = 2;
            moq.Setup(x => x.LoadNewsById(news_test.Id, out success)).Returns(new List<GameAPI.DTO.News>() { news_test });
            //var myMockObjet = new myMoq();
            GameAPI.Controllers.NewsValuesController controller = new GameAPI.Controllers.NewsValuesController(moq.Object);

            //Do
            var result = controller.Get(news_test.Id);
            //
            //Assert

//            Xunit.Assert.Equal(news_test.Text, result.Value.First().Text);
            Xunit.Assert.Single(result.Value);
        }

        [Fact]
        public void Post_AddNews()
        {
            //Setup
            var moq = new Moq.Mock<INewsDataSource>();
            bool success = true;
            var err1 = "";
            GameAPI.DTO.News news_test = new GameAPI.DTO.News();
            news_test.Text = null;
            moq.Setup(x => x.SaveNews((Moq.It.IsAny<News>()), out success)).Returns(success);
            GameAPI.Controllers.NewsValuesController controller = new GameAPI.Controllers.NewsValuesController(moq.Object);
            JObject ExampleNews = new JObject();
            DateTime newsDate = DateTime.Now;
            ExampleNews.Add("title","news123");
            ExampleNews.Add("text","news123 text");
            ExampleNews.Add("date", newsDate);

            //Do
            var result = controller.Post(ExampleNews);
            
            //Assert

            Xunit.Assert.Equal("{ Message = News successfully added. }", result.Value.ToString());
        }

        [Fact]
        public void Post_AddNews_Fails()
        {
            //Setup
            var moq = new Moq.Mock<INewsDataSource>();
            bool success = false;
            var err1 = "";
            GameAPI.DTO.News news_test = new GameAPI.DTO.News();
            news_test.Text = null;
            moq.Setup(x => x.SaveNews((Moq.It.IsAny<News>()), out success)).Returns(success);
            GameAPI.Controllers.NewsValuesController controller = new GameAPI.Controllers.NewsValuesController(moq.Object);
            JObject ExampleNews = new JObject();
            DateTime newsDate = DateTime.Now;
            ExampleNews.Add("title", "news123");
            ExampleNews.Add("text", "news123 text");
            ExampleNews.Add("date", newsDate);

            //Do
            var result = controller.Post(ExampleNews);

            //Assert

            Xunit.Assert.Equal("{ Error = Error adding data. }", result.Value.ToString());
        }

    }
}
