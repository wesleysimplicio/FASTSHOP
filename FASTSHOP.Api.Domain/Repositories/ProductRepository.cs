using FASTSHOP.Api.Domain.Interfaces;
using FASTSHOP.Api.Domain.Models;
using FASTSHOP.Api.Domain.Mongo;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FASTSHOP.Api.Domain.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoClient _mongoProduct;
        private readonly string collection = "products";
        private readonly ILogger _logger;

        public ProductRepository(MongoClient mongoProduct, ILogger<ProductRepository> logger)
        {
            this._mongoProduct = mongoProduct;
            this._logger = logger;
        }

        public List<Product> Get()
        {
            var result =  this._mongoProduct.FindAll<Product>(collection);
            return result;
        }

        public Product GetById(string Code)
        {
            return this._mongoProduct.FindOne<Product>(collection, (x => x.Code == Code));
        }
        public void Insert(Product product)
        {
            this._mongoProduct.Insert<Product>(collection, product);
        }

        public long Update(Product product)
        {
            return this._mongoProduct.Replace<Product>(collection, (collection => product.Code == product.Code), product);
        }

        public long Delete(string Code)
        {
            return this._mongoProduct.Delete<Product>(collection, (collection => collection.Code == Code));
        }

        public void Dispose()
        {
        }
    }
}
