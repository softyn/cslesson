using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DTO
{
    public class UserWithWehicles : User
    {
        public List<IWehicle> Wehicles { get; set; }

        public UserWithWehicles(IWehicle wehicle) : base()
        {
            Wehicles = new List<IWehicle>();
            Wehicles.Add(wehicle);
        }

        public UserWithWehicles(string name, string super2) : base(name, super2)
        {
            Wehicles = new List<IWehicle>();
        }

       
    }
}
