using FASTSHOP.Api.Domain.Models;
using System;
using System.Collections.Generic;

namespace FASTSHOP.Api.Domain.Interfaces
{
    public interface IOrderBusiness : IDisposable
    {
        List<Order> Get(Order order);
        Order GetById(string code);
        bool Insert(Order order);
        bool Update(Order order);
        bool Delete(string code);
    }
}
