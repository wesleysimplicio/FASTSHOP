using FASTSHOP.Api.Domain.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FASTSHOP.Test
{
    [TestClass]
    public class ClientsTest
    {
        private readonly IClientBusiness _clientBusiness;

        [TestMethod]
        public void GetByDocument()
        {
            var result = this._clientBusiness.GetByDocument(41343850835);
            Assert.IsTrue(result != null);

        }
    }
}
