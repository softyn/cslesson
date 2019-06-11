using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Enums;

namespace WebApplication1.DTO
{
    public class UserWithBike : User
    {
        public Bajka Type { get; set; }

        public UserWithBike()
        {
            Type = Bajka.black;
        }

        public UserWithBike(Bajka type)
        {
            Type = type;
        }
    }
}
