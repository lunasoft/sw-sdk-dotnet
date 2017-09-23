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
        [TestMethod]
        public void CancelationByCSD()
        {
            var build = new BuildSettings();
            Cancelation cancelation = new Cancelation(build.Url, build.User, build.Password);
            var response = cancelation.CancelarByCSD(build.Cer, build.Key, build.Rfc, build.CerPassword, "01724196-ac5a-4735-b621-e3b42bcbb459");
            Assert.IsTrue(response.Data.Acuse != null && response.status == "success");
        }
        [TestMethod]
        public void CancelationByPFX()
        {
            var build = new BuildSettings();
            Cancelation cancelation = new Cancelation(build.Url, build.User, build.Password);
            var response = cancelation.CancelarByPFX(build.Pfx, build.Rfc, build.CerPassword, "01724196-ac5a-4735-b621-e3b42bcbb459");
            Assert.IsTrue(response.Data.Acuse != null && response.status == "success");
        }
        [TestMethod]
        public void CancelationByXML()
        {
            var build = new BuildSettings();
            Cancelation cancelation = new Cancelation(build.Url, build.User, build.Password);
            var response = cancelation.CancelarByXML(build.Acuse);
            Assert.IsTrue(response.Data.Acuse != null && response.status == "success");
        }
        [TestMethod]
        public void ValidateParameters()
        {
            var resultExpect = "Son necesarios el .Cer y el .Key en formato B64";
            var build = new BuildSettings();
            Cancelation cancelation = new Cancelation(build.Url, build.User, build.Password);
            var response = cancelation.CancelarByCSD(build.Cer, build.Key, build.Rfc, build.CerPassword, "");
            Assert.IsTrue(response.messageDetail.Contains((string)resultExpect));
        }
    }
}
