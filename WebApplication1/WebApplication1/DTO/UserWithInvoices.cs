using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Interfaces;

namespace WebApplication1.DTO
{
    public class UserWithInvoices : User
    {
        public List<IInvoice> Invoices { get; set; }
        public UserWithInvoices(List<IInvoice> invoices) : base()
        {
            Invoices = invoices;
        }

        public int InvoicesSum()
        {
            int sum = 0;
            foreach (var invoice in Invoices)
            {
                sum = sum + invoice.Sum;
            }

            return sum;
        } 
    }
}
