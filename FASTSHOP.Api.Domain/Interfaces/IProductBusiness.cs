using FASTSHOP.Api.Domain.Models;
using System;
using System.Collections.Generic;

namespace FASTSHOP.Api.Domain.Interfaces
{
    public interface IProductBusiness : IDisposable
    {
        List<Product> Get();
        Product GetById(string Code);
        bool Insert(Product product);
        bool Update(Product product);
        bool Delete(string Code);
    }
}
