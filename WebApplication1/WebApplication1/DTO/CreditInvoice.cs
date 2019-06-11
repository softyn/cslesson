using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Interfaces;

namespace WebApplication1.DTO
{
    public class CreditInvoice:IInvoice
 {
     public int Sum { get; set; }
     public string Customer { get; set; }
     public string BaseInvoiceNumber { get; set; }
    }
}
