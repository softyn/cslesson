using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DTO;
using WebApplication1.Enums;
using WebApplication1.Interfaces;

namespace WebApplication1.Tests
{
    public class Tests1
    {

        public void TestUser()
        {
            User user = new User("dsfds","3424234");
            
            var uwc = new UserWithWehicles("aaa","dsfsdf");
            var count = uwc.Wehicles.Count();
            var uwc2 = new UserWithWehicles(new Car());
            bool check=false;
            int age= 1;

            List<IInvoice> InvoiceList = new List<IInvoice>();
            InvoiceList.Add(new invoice(6000,"Zabka"));
            InvoiceList.Add(new CreditInvoice());
            UserWithInvoices ui = new UserWithInvoices(InvoiceList);

            UserWithBike u = new UserWithBike();
            Console.WriteLine(u.Type);
            UserWithBike uu = new UserWithBike(Bajka.red);

            if (age > 1)
            {
                Console.Write("sdfsdfdsf");
            }

            if (user!=null && user.Name!=null)
            {
                Console.Write("sdfsdfdsf");
            }else
                Console.Write("sdfsdfdsf111");

        }
    }
}
