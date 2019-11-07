﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using GameAPI.DataAccess;
using GameAPI.DTO;
using Newtonsoft.Json.Linq;

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
        public ActionResult<IEnumerable<Products>> Get()
        {
            var getProducts = new ProductsMysqlData();
            return getProducts.LoadProducts(out bool success).ToList();
        }

        // GET api/products/5
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Products>> Get(int id)
        {
            var getProducts = new ProductsMysqlData();
            return getProducts.LoadProductsById(id, out bool success).ToList();
        }

        // POST api/products
        [HttpPost]
        public void Post([FromBody] JObject data)
        {
            Products product = new Products();
            data.ToObject<News>();
            product.Name = data["name"].ToString();
            product.Description = data["description"].ToString();
            product.Price = float.Parse(data["price"].ToString());
            var addProduct = new ProductsMysqlData();
            addProduct.SaveProduct(product, out bool success);
        }

        // PATCH api/products/5
        [HttpPatch("{id}")]
        public void Patch(int id, [FromBody] JObject data)
        {
            Products product = new Products();
            data.ToObject<News>();
            product.Id = id;
            var getProductById = new ProductsMysqlData();
            var productOld = getProductById.LoadProductsById(id, out bool LoadSuccess).ToList();

            if (data["name"].ToString().Length == 0)
            {
                product.Name = productOld.ElementAt(0).Name;
            }
            else product.Name = data["name"].ToString();

            if (data["description"].ToString().Length == 0)
            {
                product.Description = productOld.ElementAt(0).Description;
            }
            else product.Description= data["description"].ToString();

            if (data["price"].ToString().Length == 0)
            {
                product.Price = productOld.ElementAt(0).Price;
            }
            else product.Price = float.Parse(data["price"].ToString());

            var addProduct = new DataAccess.ProductsMysqlData();
            addProduct.EditProduct(product, out bool success);
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var getProducts = new ProductsMysqlData();
            return getProducts.DeleteProduct(id, out bool success);
        }

    }
}