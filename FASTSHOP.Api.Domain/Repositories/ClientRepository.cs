﻿using FASTSHOP.Api.Domain.Interfaces;
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
    public class ClientRepository : IClientRepository
    {
        private readonly IMongoClient _mongoClient;
        private readonly string collection = "clients";
        private readonly ILogger _logger;

        public ClientRepository(MongoClient mongoClient, ILogger<ClientRepository> logger)
        {
            this._mongoClient = mongoClient;
            this._logger = logger;
        }

        public List<Client> Get(Client client)
        {
            StringBuilder sb = new StringBuilder();
            if (client.Document != null)
                sb.AppendLine("\"Document\":" + client.Document + ",");
            if (!string.IsNullOrEmpty(client.Name))
                sb.AppendLine("\"Name\":/" + client.Name + "/i,");

            if (sb.ToString() == string.Empty)
                return _mongoClient.FindAll<Client>(collection);
            else
            {
                sb.Remove(sb.Length - 3, 2);
                sb.AppendLine("}");
                return _mongoClient.Find<Client>(collection, "{" + sb.ToString());
            }

        }

        public Client GetById(string Code)
        {
            return this._mongoClient.FindOne<Client>(collection, (x => x.Code == Code));
        }
        public Client GetByDocument(long? Document)
        {
            return this._mongoClient.FindOne<Client>(collection, (x => x.Document == Document));
        }

        public void Insert(Client client)
        {
            this._mongoClient.Insert<Client>(collection, client);
        }

        public long Update(Client client)
        {
            return this._mongoClient.Replace<Client>(collection, (collection => client.Code == client.Code), client);
        }

        public long Delete(long? document)
        {
            return this._mongoClient.Delete<Client>(collection, (collection => collection.Document == document));
        }

        public void Dispose()
        {
        }
    }
}
