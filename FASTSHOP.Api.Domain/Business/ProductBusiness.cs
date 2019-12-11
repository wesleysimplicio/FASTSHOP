using FASTSHOP.Api.Domain.Enums;
using FASTSHOP.Api.Domain.Interfaces;
using FASTSHOP.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FASTSHOP.Api.Domain.Business
{
    public class ProductBusiness : IProductBusiness
    {
        private readonly IProductRepository _productRepository;

        public ProductBusiness(
           IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public List<Product> Get()
        {
            return _productRepository.Get();
        }

        public Product GetById(string Code)
        {
            return _productRepository.GetById(Code);
        }

        public bool Insert(Product product)
        {
            product.Code = Guid.NewGuid().ToString("N");
            product.CreateAt = DateTime.Now;
            product.Status = StatusEnum.InStock;
            _productRepository.Insert(product);
            return true;
        }

        public bool Update(Product product)
        {
            var prod = GetById(product.Code);
            if (prod == null) throw new Exception("Este Produto não existe");

            product.Id = null;
            product.Code = prod.Code;
            product.CreateAt = prod.CreateAt;
            product.UpdateAt = DateTime.Now;
            return _productRepository.Update(product) > 0;
        }

        public bool Delete(string Code)
        {
            if (Code == null) return false;

            var result = _productRepository.GetById(Code);
            if (result == null) throw new Exception("Este Produto não existe");

            return _productRepository.Delete(Code) > 0;
        }

        public void Dispose()
        {
        }
    }

}
