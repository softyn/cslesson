using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DTO
{
    public class Car: IWehicle,IDisposable
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public int Doors { get; set; }

        public void Dispose()
        {
            Name = null;
            Year = 0;
            Doors = 0;
        }
    }
}
