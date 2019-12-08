using FASTSHOP.Api.Domain.Models;
using System;
using System.Collections.Generic;

namespace FASTSHOP.Api.Domain.Interfaces
{
    public interface IClientBusiness : IDisposable
    {
        List<Client> Get();
        Client GetById(string Id);
        Client GetByDocument(long? Document);
        bool Insert(Client client);
        bool Update(Client client);
        bool Delete(long? Document);
    }
}
