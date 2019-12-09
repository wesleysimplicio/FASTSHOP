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
    public class ClientsBusinessTest
    {

        [TestMethod]
        public void Post()
        {
            var mockClientR = new Mock<IClientRepository>();

            ClientBusiness clientBusiness = new ClientBusiness(mockClientR.Object);
            var client = new Client()
            {
                Code = "3255d0c982054e1d8d1301ec31039ea3",
                Name = "Wesley Simplicio",
                Document = 41343850835,
                Email = "wesleysimplicio@live.com",
                Status = StatusEnum.Active,
                CreateAt = new DateTime(2019, 12, 08)
            };
            var result = clientBusiness.Insert(client);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetByDocument()
        {
            var mockClientR = new Mock<IClientRepository>();
            ClientBusiness clientBusiness = new ClientBusiness(mockClientR.Object);
            var client = new Client()
            {
                Code = "3255d0c982054e1d8d1301ec31039ea3",
                Name = "Wesley Simplicio",
                Document = 41343850835,
                Email = "wesleysimplicio@live.com",
                Status = StatusEnum.Active,
                CreateAt = new DateTime(2019, 12, 08)
            };
            mockClientR.Setup(p => p.GetByDocument(client.Document)).Returns(client);

            var result = clientBusiness.GetByDocument(41343850835);
            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void Get()
        {
            var mockClientR = new Mock<IClientRepository>();
            ClientBusiness clientBusiness = new ClientBusiness(mockClientR.Object);
            var client = new List<Client>();
            client.Add(new Client()
            {
                Code = "3255d0c982054e1d8d1301ec31039ea3",
                Name = "Wesley Simplicio",
                Document = 41343850835,
                Email = "wesleysimplicio@live.com",
                Status = StatusEnum.Active,
                CreateAt = new DateTime(2019, 12, 08)
            });
            mockClientR.Setup(p => p.Get()).Returns(client);

            var result = clientBusiness.Get();
            Assert.IsTrue(result.Count > 0);
        }


        [TestMethod]
        public void Put()
        {
            var mockClientR = new Mock<IClientRepository>();

            ClientBusiness clientBusiness = new ClientBusiness(mockClientR.Object);
            var client = new Client()
            {
                Code = "3255d0c982054e1d8d1301ec31039ea3",
                Name = "Wesley Simplicio",
                Document = 41343850835,
                Email = "wesleysimplicio@live.com",
                Status = StatusEnum.Active,
                CreateAt = new DateTime(2019, 12, 08)
            };
            mockClientR.Setup(p => p.GetByDocument(client.Document)).Returns(client);
            mockClientR.Setup(p => p.Update(client)).Returns(1);
            var result = clientBusiness.Update(client);
            Assert.IsTrue(result);
        }
    }
}
