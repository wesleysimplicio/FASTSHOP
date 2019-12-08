using FASTSHOP.Api.Domain.Models;
using System;
using System.Collections.Generic;

namespace FASTSHOP.Api.Domain.Interfaces
{
    public interface IClientRepository : IDisposable
    {
        List<Client> Get();
        Client GetById(string Id);
        Client GetByDocument(long? Document);
        void Insert(Client client);
        long Update(Client client);
        long Delete(long? Document);
    }
}
