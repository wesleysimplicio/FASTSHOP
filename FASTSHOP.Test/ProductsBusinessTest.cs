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
    public class ProductsBusinessTest
    {

        [TestMethod]
        public void Post()
        {
            var mockProductR = new Mock<IProductRepository>();

            ProductBusiness productBusiness = new ProductBusiness(mockProductR.Object);
            var product = new Product()
            {
                Code = "3255d0c982054e1d8d1301ec31039ea3",
                Name = "Mouse sem fio",
                Status = StatusEnum.InStock,
                Price = Decimal.Parse("10.90")
            };
            var result = productBusiness.Insert(product);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetById()
        {
            var mockProductR = new Mock<IProductRepository>();
            ProductBusiness productBusiness = new ProductBusiness(mockProductR.Object);
            var product = new Product()
            {
                Code = "3255d0c982054e1d8d1301ec31039ea3",
                Name = "Mouse sem fio",
                Status = StatusEnum.InStock,
                Price = Decimal.Parse("10.90")
            };
            mockProductR.Setup(p => p.GetById(product.Code)).Returns(product);

            var result = productBusiness.GetById(product.Code);
            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void Get()
        {
            var mockProductR = new Mock<IProductRepository>();
            ProductBusiness productBusiness = new ProductBusiness(mockProductR.Object);
            var product = new List<Product>();
            product.Add(new Product()
            {
                Code = "3255d0c982054e1d8d1301ec31039ea3",
                Name = "Mouse sem fio",
                Status = StatusEnum.InStock,
                Price = Decimal.Parse("10.90")
            });
            mockProductR.Setup(p => p.Get()).Returns(product);

            var result = productBusiness.Get();
            Assert.IsTrue(result.Count > 0);
        }


        [TestMethod]
        public void Put()
        {
            var mockProductR = new Mock<IProductRepository>();

            ProductBusiness productBusiness = new ProductBusiness(mockProductR.Object);
            var product = new Product()
            {
                Code = "3255d0c982054e1d8d1301ec31039ea3",
                Name = "Mouse sem fio",
                Status = StatusEnum.InStock,
                Price = Decimal.Parse("10.99"),
            };
            mockProductR.Setup(p => p.GetById(product.Code)).Returns(product);
            mockProductR.Setup(p => p.Update(product)).Returns(1);
            
            var result = productBusiness.Update(product);
            Assert.IsTrue(result);
        }
    }
}
