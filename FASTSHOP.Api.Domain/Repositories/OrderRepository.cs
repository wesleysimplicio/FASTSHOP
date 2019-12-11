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
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoClient _mongoClient;
        private readonly string collection = "orders";
        private readonly ILogger _logger;

        public OrderRepository(MongoClient mongoClient, ILogger<OrderRepository> logger)
        {
            this._mongoClient = mongoClient;
            this._logger = logger;
        }

        public List<Order> Get(Order order)
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(order.Code))
                sb.AppendLine("\"Code\":\"" + order.Code + "\",");
            if (!string.IsNullOrEmpty(order.ClientId))
                sb.AppendLine("\"ClientId\":\"" + order.ClientId + "\",");
            if (order.Status != null && order.Status != 0)
                sb.AppendLine("\"Status\":" + (int)order.Status + ",");
            if (!string.IsNullOrEmpty(order.Client))
                sb.AppendLine("\"Client\":/" + order.Client + "/i,");

            if (order.CreateAt.HasValue)
            {
                var dateQuery = new StringBuilder();

                if (order.CreateAt.HasValue)
                    dateQuery.AppendLine($"$gte:ISODate(\"{order.CreateAt.Value.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz")}\"),");

                dateQuery.Remove(dateQuery.Length - 2, 1);

                sb.AppendLine(String.Concat("CreateAt:{", dateQuery.ToString(), "},"));
            }

            if (sb.ToString() == string.Empty)
                return _mongoClient.FindAll<Order>(collection);
            else
            {
                sb.Remove(sb.Length - 3, 2);
                sb.AppendLine("}");
                return _mongoClient.Find<Order>(collection, "{" + sb.ToString());
            }
        }

        public Order GetById(string Code)
        {
            return this._mongoClient.FindOne<Order>(collection, (x => x.Code == Code));
        }
        public void Insert(Order order)
        {
            this._mongoClient.Insert<Order>(collection, order);
        }

        public long Update(Order order)
        {
            return this._mongoClient.Replace<Order>(collection, (collection => order.Code == order.Code), order);
        }

        public long Delete(string Code)
        {
            return this._mongoClient.Delete<Order>(collection, (collection => collection.Code == Code));
        }

        public void Dispose()
        {
        }
    }
}
