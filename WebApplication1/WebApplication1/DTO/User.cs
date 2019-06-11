using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DTO
{
    public class User
    {
        public User()
        {
            Super2 = "111114";

        }
        public User(string name, string super2)
        {
            Name = name;
            Super2 = super2;
        }
        public string Name { get; set; }
        public int Age { get; set; }



        public int Super { get; set; }
        public string Super2 { get; set; }
        public DateTime Super3 { get; set; }

    }


   

    

   
}
