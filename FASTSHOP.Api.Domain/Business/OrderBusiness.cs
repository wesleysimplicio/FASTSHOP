using FASTSHOP.Api.Domain.Enums;
using FASTSHOP.Api.Domain.Interfaces;
using FASTSHOP.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FASTSHOP.Api.Domain.Business
{
    public class OrderBusiness : IOrderBusiness
    {
        private readonly IOrderRepository _orderRepository;

        public OrderBusiness(
           IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public List<Order> Get(Order order)
        {
            return _orderRepository.Get(order);
        }

        public Order GetById(string Code)
        {
            return _orderRepository.GetById(Code);
        }

        public bool Insert(Order order)
        {
            order.Code = Guid.NewGuid().ToString("N");
            order.CreateAt = DateTime.Now;
            order.Status = StatusEnum.Processing;
            _orderRepository.Insert(order);
            return true;
        }

        public bool Update(Order order)
        {
            var prod = GetById(order.Code);
            if (prod == null) throw new Exception("Este Produto não existe");

            order.Code = prod.Code;
            order.CreateAt = prod.CreateAt;
            order.UpdateAt = DateTime.Now;
            return _orderRepository.Update(order) > 0;
        }

        public bool Delete(string Code)
        {
            if (Code == null) return false;

            var result = _orderRepository.GetById(Code);
            if (result == null) throw new Exception("Este Produto não existe");

            return _orderRepository.Delete(Code) > 0;
        }

        public void Dispose()
        {
        }
    }

}
