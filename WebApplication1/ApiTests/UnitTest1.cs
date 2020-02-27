using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication1.Controllers;
using WebApplication1.DataAccess;
using Xunit;

namespace ApiTests
{
    public class myMoq : IDataSource
    {
        public List<string> LoadNames(out bool success)
        {
            success = true;
            return new List<string>() { "aaaa", "bbbb" };
        }

        public bool SaveNames(List<string> list)
        {
            return true;
        }

        public bool DeleteNames(int Id)
        {
            return true;
        }
    }

    public class UnitTest1
    {
        //[Fact]
        //public void Get_CheckProperOutput()
        //{
        //    //Setup
        //    var moq = new Moq.Mock<IDataSource>();
        //    bool succes;
        //    moq.Setup(x => x.LoadNames(out succes)).Returns(new List<string>(){"aaaa","bbbb"});
        //    moq.Setup(x => x.SaveNames(Moq.It.IsAny<List<string>>())).Returns(true);
        //    //var myMockObjet = new myMoq();
        //    ValuesController controller = new ValuesController(moq.Object);

        //    //Do
        //    var result = controller.Get("a","b","c").ToString();
        //    //
        //    //Assert
        //    var expected = Json(new { ReturnedData = "aaaa", "bbbb", "a", "b", "c" }
        //    Assert.Equal(expected, result);
        //}

        [Fact]
        public void Get_CheckProperOutput2()
        {
            //Setup
            var moq = new Moq.Mock<IDataSource>();
            bool succes;
            moq.Setup(x => x.LoadNames(out succes)).Returns(new List<string>() { "aaaa", "bbbb" });
            moq.Setup(x => x.SaveNames(Moq.It.IsAny<List<string>>())).Returns(true);
            //var myMockObjet = new myMoq();
            ValuesController controller = new ValuesController(moq.Object);

            //Do
            var result = controller.Get(1, 2);
            //
            //Assert
           
            Assert.Equal(3, result);
        }

        [Fact]
        public void Get_CheckProperOutput3()
        {
            //Setup
            var moq = new Moq.Mock<IDataSource>();
            bool succes = true;
            moq.Setup(x => x.LoadNames(out succes)).Returns(new List<string>() { "1aaaa", "2bbbb" });
            moq.Setup(x => x.SaveNames(Moq.It.IsAny<List<string>>())).Returns(true);
            //var myMockObjet = new myMoq();
            ValuesController controller = new ValuesController(moq.Object);

            //Do
            JsonResult result = controller.Get("1", "2","3");
            //
            //Assert
            string jsonstring = JsonConvert.SerializeObject(result.Value);
            Assert.Equal("{\"ReturnedData\":[\"1aaaa\",\"2bbbb\",\"1\",\"2\",\"3\"]}", jsonstring);
        }

        [Fact]
        public void Get_CheckProperOutput4()
        {
            //Setup
            var moq = new Moq.Mock<IDataSource>();
            bool succes = true;
            moq.Setup(x => x.LoadNames(out succes)).Returns(new List<string>() { "1aaaa", "2bbbb" });
            moq.Setup(x => x.SaveNames(Moq.It.IsAny<List<string>>())).Returns(true);
            //var myMockObjet = new myMoq();
            ValuesController controller = new ValuesController(moq.Object);

            //Do
            JsonResult result = controller.Get2("1", "2", "3");
            //
            //Assert
            string jsonstring = JsonConvert.SerializeObject(result.Value);
            Assert.Equal("{\"ReturnedData\":[\"1aaaa\",\"2bbbb\",\"1\",\"2\",\"3\"]}", jsonstring);
        }

        [Fact]
        public void Get_CheckProperOutput5()
        {
            //Setup
            var moq = new Moq.Mock<IDataSource>();
            bool succes = false;
            moq.Setup(x => x.LoadNames(out succes)).Returns(new List<string>() { "1aaaa", "2bbbb" });
            moq.Setup(x => x.SaveNames(Moq.It.IsAny<List<string>>())).Returns(true);
            //var myMockObjet = new myMoq();
            ValuesController controller = new ValuesController(moq.Object);

            //Do
            JsonResult result = controller.Get2("1", "2", "3");
            //
            //Assert
            string jsonstring = JsonConvert.SerializeObject(result.Value);
            Assert.Equal("{\"Error\":\"Error loading data\"}", jsonstring);
        }

    }
}
