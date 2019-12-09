using FASTSHOP.Api.Domain.Models;
using System;
using System.Collections.Generic;

namespace FASTSHOP.Api.Domain.Interfaces
{
    public interface IProductRepository : IDisposable
    {
        List<Product> Get();
        Product GetById(string code);
        void Insert(Product product);
        long Update(Product product);
        long Delete(string code);
    }
}
