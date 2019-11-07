using System.Collections.Generic;
using GameAPI.DTO;

namespace GameAPI.DataAccess
{
    public interface IProductsDataSource
    {
        IEnumerable<Products> LoadProducts(out bool success);
        IEnumerable<Products> LoadProductsById(int id, out bool success);
        bool SaveProduct(Products product, out bool success);
        bool DeleteProduct(int id, out bool success);
        bool EditProduct(Products product, out bool success);
    }
}