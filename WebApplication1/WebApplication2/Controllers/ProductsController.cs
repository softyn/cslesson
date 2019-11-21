using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GameAPI.DataAccess;
using GameAPI.DTO;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameAPI.Controllers
{
    [Route("api/[controller]")]
    public class ProductsValuesController : Controller
    {
        public IProductsDataSource AdapterProducts { get; }

        public ProductsValuesController(IProductsDataSource productsDataSource)
        {
            AdapterProducts = productsDataSource;
            //todo:
            // - pozostale metody
            // - obsluga bledow
            // - testy
            // - UI
        }

        // GET: api/products
        [HttpGet]
        public ActionResult<IEnumerable<Products>> Get()
        {
            var getProducts = new ProductsMysqlData();
            var list = getProducts.LoadProducts(out bool success).ToList();

            if (!success)
            {
                return BadRequest(new { Message = "Error getting data." });
            }

            return list;
        }

        // GET api/products/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Products>> Get(int id)
        {
            var getProducts = new ProductsMysqlData();
            var list = getProducts.LoadProductsById(id, out bool success).ToList();

            if (!success)
            {
                return BadRequest(new {Message = "Error getting data"});
            }

            if (list.Count == 0)
            {
                return NotFound(new {Message = $"Item with id {id} not found"});
            }
        }

        // POST api/products
        [HttpPost]
        public ActionResult Post([FromBody] JObject data)
        {
            Products product = new Products();
            data.ToObject<News>();
            product.Name = data["name"].ToString();
            product.Description = data["description"].ToString();
            product.Price = float.Parse(data["price"].ToString());
            var addProduct = new ProductsMysqlData();
            var add = addProduct.SaveProduct(product, out bool success);

            if (!success)
            {
                return BadRequest(new { Message = "Error with adding data" });
            }

            return Ok(new {Message = $"Product {product.Name} added."});
        }

        // PATCH api/products/5
        [HttpPatch("{id}")]
        public ActionResult Patch(int id, [FromBody] JObject data)
        {
            Products product = new Products();
            data.ToObject<News>();
            product.Id = id;
            var getProductById = new ProductsMysqlData();
            var productOld = getProductById.LoadProductsById(id, out bool LoadSuccess).ToList();

            product.Name = data["name"].ToString().Length == 0 ? productOld.ElementAt(0).Name : data["name"].ToString();

            product.Description = data["description"].ToString().Length == 0 ? productOld.ElementAt(0).Description : data["description"].ToString();

            product.Price = data["price"].ToString().Length == 0 ? productOld.ElementAt(0).Price : float.Parse(data["price"].ToString());

            var addProduct = new ProductsMysqlData();
            var add = addProduct.EditProduct(product, out bool success);

            if (!success)
            {
                return BadRequest(new {Message = "Error with editing item."});
            }

            return Ok("Product edited.");

        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var getProducts = new ProductsMysqlData();
            var delete = getProducts.DeleteProduct(id, out bool success);
            if (!success)
            {
                return BadRequest(new { Message = "Error with deleting item." });
            }

            return Ok("Product deleted.");
        }

    }
}