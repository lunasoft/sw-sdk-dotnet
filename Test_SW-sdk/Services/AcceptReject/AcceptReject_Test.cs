using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using SW.Helpers;
using Test_SW.Helpers;
using SW.Services.AcceptReject;

namespace Test_SW.Services.AcceptReject_Test
{
    [TestClass]
    public class AcceptReject_Test
    {
        [TestMethod]
        public void ValidateParameters()
        {
            var resultExpect = "Son necesarios el .Cer y el .Key en formato B64";
            var build = new BuildSettings();
            AcceptReject acceptReject = new AcceptReject(build.Url, build.User, build.Password);
            var response = acceptReject.AcceptByCSD(build.Cer, build.Key, build.Rfc, build.CerPassword, new AceptacionRechazoItem[] { new AceptacionRechazoItem() { uuid = ""} });
            Assert.IsTrue(response.messageDetail.Contains((string)resultExpect));
        }
        [TestMethod]
        public void AcceptRejectByRfcUuid()
        {
            var build = new BuildSettings();
            AcceptReject acceptReject = new AcceptReject(build.Url, build.User, build.Password);
            var response = acceptReject.AcceptByRfcUuid(build.Rfc, "01724196-ac5a-4735-b621-e3b42bcbb459", EnumAcceptReject.Aceptacion);
            Assert.IsTrue(response.message != null);
        }
        [TestMethod]
        public void AcceptRejectByCSD()
        {
            var build = new BuildSettings();
            AcceptReject acceptReject = new AcceptReject(build.Url, build.User, build.Password);
            var response = acceptReject.AcceptByCSD(build.Cer, build.Key, build.Rfc, build.CerPassword, new AceptacionRechazoItem[] { new AceptacionRechazoItem() { uuid = "01724196-ac5a-4735-b621-e3b42bcbb459", action = EnumAcceptReject.Aceptacion } });
            Assert.IsTrue(response.message != null, response.message);
        }
        [TestMethod]
        public void AcceptRejectByPfx()
        {
            var build = new BuildSettings();
            AcceptReject acceptReject = new AcceptReject(build.Url, build.User, build.Password);
            var response = acceptReject.AcceptByPFX(build.Pfx, build.Rfc, build.CerPassword, new AceptacionRechazoItem[] { new AceptacionRechazoItem() { uuid = "01724196-ac5a-4735-b621-e3b42bcbb459", action = EnumAcceptReject.Aceptacion } });
            Assert.IsTrue(response.message != null, response.message);
        }
        [TestMethod]
        public void AcceptRejectByXml()
        {
            var build = new BuildSettings();
            AcceptReject acceptReject = new AcceptReject(build.Url, build.User, build.Password);
            var response = acceptReject.AcceptByXML(build.Acuse, EnumAcceptReject.Aceptacion);
            Assert.IsTrue(response.message != null, response.message);
        }
    }
}
