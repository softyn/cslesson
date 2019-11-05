using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DataAccess;
using WebApplication1.DTO;

namespace WebApplication1.Controllers
{

    public enum WriteStatus
    {
        Written=56,
        Deleted=78,
        Empty=1000
    }

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {


        private List<string> lista;
        private IDataSource adapter;
        bool doneOk;
        public ValuesController(IDataSource dataSource)
        {
            adapter = dataSource;


            /*lista = new List<string>();
            lista.Add("aaaaa");
            lista.Add("bbbbb");
            lista.Add("ccccc");*/
        }

        
        // GET api/values
        [HttpGet()]
        public JsonResult Get()
        {
            Response.Headers.Add("Access-Control-Allow-Origin","*");
            lista = adapter.LoadNames(out doneOk);
            if (doneOk == true)
            {
                return Json(new { ReturnedData = lista });
            }
            else
            {
                var res = new JsonResult(new { Error = "Error saving data" });
                res.StatusCode = 500;
                return res;
            }
            
        }

        // GET api/values
        [HttpGet("sum,{a},{b}")]
        public int Get(int a, int b)
        {
            return a+b;
        }
        //1. /sum (a,b)=> a+b
        //2. echo a => echo a (string)
        //3. a,b,c => List(a,b,c)
        //4. 0..4(enum) => text

        [HttpGet("text,{text}")]
        public string Getsdfsdfds(string text)
        {
            return "echo "+text;
        }

        // GET api/values/5
      
           

            

        
        [HttpGet("list,{a},{b},{c}")]
        public JsonResult Get(string a, string b, string c)
        {
            lista = adapter.LoadNames(out doneOk);
            if (doneOk == true)
            {
                var templista = new List<string>();
                templista.Add(a);
                templista.Add(b);
                templista.Add(c);
                adapter.SaveNames(templista);
                lista.AddRange(templista);
                return Json(new { ReturnedData = lista });
            }
            else
            {
                return Json(new { Error = "Error saving data" });
            }




        }
        [HttpGet("list2,{a},{b},{c}")]
        
        public JsonResult Get2(string a, string b, string c)
        {
            lista = adapter.LoadNames(out doneOk);
            if (doneOk == true)
            {
                var templista = new List<string>();
                templista.Add(a);
                templista.Add(b);
                templista.Add(c);
                if (adapter.SaveNames(templista) == true)
                {
                    lista.AddRange(templista);
                    return Json(new { ReturnedData = lista });
                }
                else
                {
                    var res = new JsonResult(new {Error = "Error saving data"});
                    res.StatusCode = 500;
                    return res;

                }
            }
            else
            {
                var res = new JsonResult(new { Error = "Error loading data" });
                res.StatusCode = 500;
                return res;
            }
            

            
            //return lista;

        }

        [HttpGet("enum,{enumName}")]
        public int Getaaa(string enumName)
        {
            WriteStatus writeStatus= Enum.Parse<WriteStatus>(enumName);
            return (int)writeStatus;
        }

        [HttpGet("enum2,{enumNumber}")]
        public string Getaaa(int enumNumber)
        {
            WriteStatus writeStatus =(WriteStatus) enumNumber;
            return writeStatus.ToString();
        }
        //// POST api/values
        //[HttpPost("list,{a},{b},{c}")]
        //public List<string> Post(string a, string b, string c)
        //{
        //    lista.Add(a);
        //    lista.Add(b);
        //    lista.Add(c);
        //    return lista;
        //}

        // PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //    var e = 1;
        //}
        [HttpPost()]
        public void Post([FromBody]string value2)
        {
            var e = 1;
        }
        [HttpPost("sendObj")]
        public void Post([FromBody]User user)
        {
            var e = 1;
        }
        [HttpPatch("{a},{b}")]
        public ActionResult Patch(string a, string b)
        {
            lista = adapter.LoadNames(out doneOk);
            if (doneOk == true)
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    if (lista[i].Contains(a))
                        lista[i] = b;
                }
                return Json(new { ReturnedData = lista });
            }
            else
            {
                return StatusCode(500, Json(new { Error = "Error loading data" }));
            }
            
           
        }

        // DELETE api/values/5
        [HttpDelete("{a}")]
        public ActionResult Delete(string a)
        {
            lista = adapter.LoadNames(out doneOk);
            if (doneOk == true)
            {
                lista.Remove(a);
                return Json(new { ReturnedData = lista });
            }
            else
            {
                return StatusCode(500, Json(new { Error = "Error loading data" }));
            }
            
            
        }
    }
}
