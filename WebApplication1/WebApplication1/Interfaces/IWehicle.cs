using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DTO
{
    public interface IWehicle
    {
        string Name { get; set; }
        int Year { get; set; }
    }
}
