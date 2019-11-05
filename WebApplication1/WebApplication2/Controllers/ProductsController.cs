using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GameAPI.DataAccess;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameAPI.Controllers
{
    public enum ProductsWriteStatus //do czego to tworzyliśmy?
    {
        Written = 56,
        Deleted = 78,
        Empty = 1000
    }

    [Route("api/[controller]")]
    public class ProductsValuesController : Controller
    {
        private IProductsDataSource _adapterProducts; //do czego to tworzyliśmy?
        private bool doneOk;
        public ProductsValuesController(IProductsDataSource productsDataSource)
        {
            _adapterProducts = productsDataSource;
            //todo:
            // - pozostale metody
            // - obsluga bledow
            // - testy
            // - UI
        }

        // GET: api/products
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            ProductsMysqlData getProducts = new DataAccess.ProductsMysqlData();
            return getProducts.LoadProducts(out bool success);
        }

        // GET api/products/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<string>> Get(int id)
        {
            ProductsMysqlData getProducts = new DataAccess.ProductsMysqlData();
            return getProducts.LoadProductsById(id, out bool success);
        }

            // POST api/products
            [HttpPost]
            public void Post([FromBody] string value)
            {
            }

            // PUT api/products/5
            [HttpPut("{id}")]
            public void Put(int id, [FromBody] string value)
            {
            }

            // DELETE api/products/5
            [HttpDelete("{id}")]
            public bool Delete(int id)
            {
                ProductsMysqlData getProducts = new DataAccess.ProductsMysqlData();
                return getProducts.DeleteProduct(id, out bool success);
        }
        
    }
}