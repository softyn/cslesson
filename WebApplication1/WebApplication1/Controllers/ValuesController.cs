using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

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

        public ValuesController()
        {
            lista = new List<string>();
            lista.Add("aaaaa");
            lista.Add("bbbbb");
            lista.Add("ccccc");
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
        public List<string> Get(string a, string b, string c)
        {
            lista.Add(a);
            lista.Add(b);
            lista.Add(c);
            return lista;
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
        // POST api/values
        [HttpPost("list,{a},{b},{c}")]
        public List<string> Post(string a, string b, string c)
        {
            lista.Add(a);
            lista.Add(b);
            lista.Add(c);
            return lista;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpPatch("{a},{b}")]
        public List<string> Patch(string a, string b)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Contains(a))
                    lista[i] = b;
            }
            return lista;
        }

        // DELETE api/values/5
        [HttpDelete("{a}")]
        public List<string> Delete(string a)
        {
            lista.Remove(a);
            return lista;
        }
    }
}
