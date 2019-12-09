using FASTSHOP.Api.Controllers;
using FASTSHOP.Api.Domain.Enums;
using FASTSHOP.Api.Domain.Interfaces;
using FASTSHOP.Api.Domain.Models;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using FASTSHOP.Api.Domain.Business;
using System.Collections.Generic;

namespace FASTSHOP.Test
{
    [TestClass]
    public class OrdersBusinessTest
    {

        [TestMethod]
        public void Post()
        {
            var mockOrder = new Mock<IOrderRepository>();

            OrderBusiness orderBusiness = new OrderBusiness(mockOrder.Object);
            var product = new Product()
            {
                Code = "3255d0c982054e1d8d1301ec31039ea4",
                Name = "Mouse sem fio",
                Status = StatusEnum.InStock,
                Price = Decimal.Parse("10.90")
            };
            var order = new Order()
            {
                Code = "3255d0c982054e1d8d1301ec31039ea3",
                ClientId = "5d36795888eef21b24abf8c8",
                Status = StatusEnum.Processing
            };
            order.Products.Add(product);

            var result = orderBusiness.Insert(order);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetById()
        {
            var mockOrder = new Mock<IOrderRepository>();
            OrderBusiness OrderBusiness = new OrderBusiness(mockOrder.Object);
            var order = new Order()
            {
                Code = "3255d0c982054e1d8d1301ec31039ea3",
                ClientId = "5d36795888eef21b24abf8c8",
                Status = StatusEnum.Processing
            };
            mockOrder.Setup(p => p.GetById(order.Code)).Returns(order);

            var result = OrderBusiness.GetById(order.Code);
            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void Get()
        {
            var mockOrder = new Mock<IOrderRepository>();
            OrderBusiness OrderBusiness = new OrderBusiness(mockOrder.Object);
            var order = new List<Order>();
            order.Add(new Order()
            {
                Code = "3255d0c982054e1d8d1301ec31039ea3",
                ClientId = "5d36795888eef21b24abf8c8",
                Status = StatusEnum.Processing
            });
            var filter = new Order() { Status = StatusEnum.Processing };
            mockOrder.Setup(p => p.Get(filter)).Returns(order);

            var result = OrderBusiness.Get(filter);
            Assert.IsTrue(result.Count > 0);
        }


        [TestMethod]
        public void Put()
        {
            var mockOrder = new Mock<IOrderRepository>();

            OrderBusiness OrderBusiness = new OrderBusiness(mockOrder.Object);
            var order = new Order()
            {
                Code = "3255d0c982054e1d8d1301ec31039ea3",
                ClientId = "5d36795888eef21b24abf8c8",
                Status = StatusEnum.Processing
            };
            mockOrder.Setup(p => p.GetById(order.Code)).Returns(order);
            mockOrder.Setup(p => p.Update(order)).Returns(1);

            var result = OrderBusiness.Update(order);
            Assert.IsTrue(result);
        }
    }
}
