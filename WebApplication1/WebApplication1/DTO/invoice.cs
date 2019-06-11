using WebApplication1.Interfaces;

namespace WebApplication1.DTO
{
    public class invoice : IInvoice
    {
        public int Sum { get; set; }
        public string Customer { get; set; }

        public invoice(int sum, string customer)
        {
            Sum = sum;
            Customer = customer;
        }
    }
}