﻿using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace ApiCatalogo.Repositories
{
    public interface IProductRepository
    {
        IQueryable<Product> GetProducts();

        Product GetProduct(int id);

        Product Create(Product product);

        bool Update(Product product);

        bool Delete(int id);
    }
}
