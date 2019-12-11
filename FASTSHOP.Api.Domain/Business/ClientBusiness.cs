using FASTSHOP.Api.Domain.Enums;
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
            _clientRepository = clientRepository;
        }
        public List<Client> Get(Client client)
        {
            return _clientRepository.Get(client);
        }

        public Client GetById(string Code)
        {
            return _clientRepository.GetById(Code);
        }

        public Client GetByDocument(long? Document)
        {
            return _clientRepository.GetByDocument(Document);
        }

        public bool Insert(Client client)
        {
            var findDocument = GetByDocument(client.Document);
            if (findDocument != null) throw new Exception("CPF já existe");

            client.Code = Guid.NewGuid().ToString("N");
            client.CreateAt = DateTime.Now;
            _clientRepository.Insert(client);

            return true;
        }

        public bool Update(Client client)
        {
            var cliResult = GetById(client.Code);
            if (cliResult == null) throw new Exception("Cliente não existe");

            client.Id = null;
            client.Code = cliResult.Code;
            client.CreateAt = cliResult.CreateAt;
            client.UpdateAt = DateTime.Now;
            return _clientRepository.Update(client) > 0;
        }

        public bool Delete(long? Document)
        {
            if (Document == null) return false;

            var result = _clientRepository.GetByDocument(Document);
            if (result == null) throw new Exception("Este cliente não existe");

            return _clientRepository.Delete(Document) > 0;
        }

        public void Dispose()
        {
        }
    }

}
