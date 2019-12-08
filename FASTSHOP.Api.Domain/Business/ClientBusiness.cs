﻿using FASTSHOP.Api.Domain.Interfaces;
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
        public List<Client> Get()
        {
            return _clientRepository.Get();
        }

        public Client GetById(string Id)
        {
            return _clientRepository.GetById(Id);
        }

        public Client GetByDocument(long? Document)
        {
            return _clientRepository.GetByDocument(Document);
        }

        public bool Insert(Client client)
        {
            var findDocument = GetByDocument(client.Document);
            if (findDocument != null)
            {
                throw new Exception("Documento já existe");
            }

            _clientRepository.Insert(client);
            return true;
        }

        public bool Update(Client client)
        {
            return _clientRepository.Update(client) > 0;
        }

        public bool Delete(long? Document)
        {
            if (Document == null)
                return false;

            var dbUser = _clientRepository.GetByDocument(Document);
            if (dbUser == null)
            {
                throw new Exception("Este cliente não existe");
            }

            return _clientRepository.Delete(Document) > 0;
        }


        public void Dispose()
        {
        }
    }


}
