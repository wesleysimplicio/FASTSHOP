using FASTSHOP.Api.Domain.Interfaces;
using FASTSHOP.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FASTSHOP.Api.Domain.Business
{
    public class ClientBusiness : IClientBusiness
    {
        private readonly IClientRepository _clientRepository;

        public ClientBusiness(
           IClientRepository clientRepository)
        {
            this._clientRepository = clientRepository;
        }
        public List<Client> Get()
        {
            return this._clientRepository.Get();
        }

        public Client GetById(string Id)
        {
            return this._clientRepository.GetById(Id);
        }

        public Client GetByDocument(long? Document)
        {
            return this._clientRepository.GetByDocument(Document);
        }

        public bool Insert(Client client)
        {
            this._clientRepository.Insert(client);
            return true;
        }

        public bool Update(Client client)
        {

            if (string.IsNullOrWhiteSpace(client.Name)
                || string.IsNullOrWhiteSpace(client.Email)
                || client.Document == null)
            {
                return false;
            }

            return this._clientRepository.Update(client) > 0;
        }

        public bool Delete(long? Document)
        {
            if (Document == null)
                return false;

            var dbUser = this._clientRepository.GetByDocument(Document);
            if (dbUser == null)
            {
                throw new Exception("Este cliente não existe");
            }

            return this._clientRepository.Delete(Document) > 0;
        }

        public void Dispose()
        {
        }
    }


}
