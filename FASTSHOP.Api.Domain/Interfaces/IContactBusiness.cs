using FASTSHOP.Api.Domain.Models;
using System;
using System.Collections.Generic;

namespace FASTSHOP.Api.Domain.Interfaces
{
    public interface IContactBusiness : IDisposable
    {
        List<Contact> Get();
        Contact GetById(string Code);
        bool Insert(Contact contact);
        bool Update(Contact contact);
        bool Delete(string Code);
    }
}
