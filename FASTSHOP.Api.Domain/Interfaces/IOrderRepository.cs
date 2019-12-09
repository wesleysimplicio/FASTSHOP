using FASTSHOP.Api.Domain.Models;
using System;
using System.Collections.Generic;

namespace FASTSHOP.Api.Domain.Interfaces
{
    public interface IOrderRepository : IDisposable
    {
        List<Order> Get(Order order);
        Order GetById(string code);
        void Insert(Order order);
        long Update(Order order);
        long Delete(string code);
    }
}
