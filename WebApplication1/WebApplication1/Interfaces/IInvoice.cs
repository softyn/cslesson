using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Interfaces
{
    public interface IInvoice
    {
        int Sum { get; set; }
        string Customer { get; set; }
    }
}
