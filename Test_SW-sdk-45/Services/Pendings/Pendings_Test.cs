using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using SW.Helpers;
using Test_SW.Helpers;
using SW.Services.Pendings;

namespace Test_SW.Services.Pendings
{
    [TestClass]
    public class Pendings_Test
    {
        [TestMethod]
        public void ValidateParameters()
        {
            var resultExpect = "La fecha de emisión no esta dentro de la vigencia del CSD del Emisor";
            var build = new BuildSettings();
            Pending pendientes = new Pending(build.Url, build.User, build.Password);
            var response = pendientes.PendingsByRfc("Test");
            Assert.IsTrue(response.message.Contains((string)resultExpect));
        }
        [TestMethod]
        public void RelationsByRfcUuid()
        {
            var build = new BuildSettings();
            Pending pendientes = new Pending(build.Url, build.User, build.Password);
            PendingsResponse response = pendientes.PendingsByRfc(build.Rfc);
            Assert.IsTrue(response.status == "success");
        }
    }
}
