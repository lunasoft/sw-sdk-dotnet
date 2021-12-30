using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using SW.Services.Cancelation;
using SW.Helpers;
using Test_SW.Helpers;

namespace Test_SW.Services.Cancelation_Test
{
    [TestClass]
    public class Cancelation_Test
    {
        private const string uuid = "575f7182-58e2-417d-a20d-fe037f7fa049";
        [TestMethod]
        public void CancelationByCSD()
        {
            var build = new BuildSettings();
            Cancelation cancelation = new Cancelation(build.Url, build.User, build.Password);
            var response = cancelation.CancelarByCSD(build.Cer, build.Key, build.Rfc, build.CerPassword, uuid, "02");
            Assert.IsTrue(response.data.acuse != null && response.status == "success");
        }
        [TestMethod]
        public void CancelationByPFX()
        {
            var build = new BuildSettings();
            Cancelation cancelation = new Cancelation(build.Url, build.User, build.Password);
            CancelationResponse response = cancelation.CancelarByPFX(build.Pfx, build.Rfc, build.CerPassword, uuid, "02");
            Assert.IsTrue(response.data.acuse != null && response.status == "success");
        }
        
        [TestMethod]
        public void CancelationByXML()
        {
            var build = new BuildSettings();
            Cancelation cancelation = new Cancelation(build.Url, build.User, build.Password);
            var response = cancelation.CancelarByXML(build.Acuse);
            Assert.IsTrue(response.data.acuse != null && response.status == "success");

        }
        [TestMethod]
        public void CancelationByUuid()
        {
            var build = new BuildSettings();
            Cancelation cancelation = new Cancelation(build.Url, build.User, build.Password);
            var response = cancelation.CancelarByRfcUuid(build.Rfc, uuid, "02");
            Assert.IsTrue(response.data.acuse != null && response.status == "success");
        }
        [TestMethod]
        public void ValidateParameters()
        {
            var resultExpect = "Son necesarios el .Cer y el .Key en formato B64";
            var build = new BuildSettings();
            Cancelation cancelation = new Cancelation(build.Url, build.User, build.Password);
            var response = cancelation.CancelarByCSD(build.Cer, build.Key, build.Rfc, build.CerPassword, "", "02", "");
            Assert.IsTrue(response.messageDetail.Contains((string)resultExpect));
        }
    }
}
