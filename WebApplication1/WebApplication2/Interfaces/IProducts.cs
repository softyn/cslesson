using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameAPI.Interfaces
{
    interface IProducts
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        float Price { get; set; }
    }
}
