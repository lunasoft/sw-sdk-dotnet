using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using SW.Services.Cancelation;
using SW.Helpers;
using Test_SW.Helpers;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.IO;

namespace Test_SW.Services.Cancelation_Test_45
{
    [TestClass]
    public class Cancelation_Test_45
    {
        private const string uuid = "e61b1d91-b4f5-4af7-b39d-8731ba06a23c";
        [TestMethod]
        [Ignore]
        public void Cancelation_Test_45_CancelationByCSD()
        {
            var build = new BuildSettings();
            Cancelation cancelation = new Cancelation(build.Url, build.User, build.Password);
            var response = cancelation.CancelarByCSD(build.Cer, build.Key, build.Rfc, build.CerPassword, uuid, "02");
            Assert.IsTrue(response.data.acuse != null && response.status == "success");
        }
        [TestMethod]
        [Ignore]
        public void Cancelation_Test_45_CancelationByRfcUuid()
        {
            var build = new BuildSettings();
            Cancelation cancelation = new Cancelation(build.Url, build.User, build.Password);
            var response = cancelation.CancelarByRfcUuid(build.Rfc, uuid, "02");
            Assert.IsTrue(response.data.acuse != null && response.status == "success");
        }

        [TestMethod]
        [Ignore]
        public void Cancelation_Test_45_CancelationByPFX()
        {
            var build = new BuildSettings();
            Cancelation cancelation = new Cancelation(build.Url, build.User, build.Password);
            CancelationResponse response = cancelation.CancelarByPFX(build.Pfx, build.Rfc, build.CerPassword, uuid, "02");
            Assert.IsTrue(response != null && response.status == "success");
        }

        [TestMethod]
        [Ignore]
        public void Cancelation_Test_45_CancelationByXML()
        {
            var build = new BuildSettings();
            Cancelation cancelation = new Cancelation(build.Url, build.User, build.Password);
            var response = cancelation.CancelarByXML(build.CancelacionXML);
            Assert.IsTrue(response != null && response.status == "success");
        }
        [TestMethod]
        public void Cancelation_Test_45_ValidateParameters()
        {
            var resultExpect = "Son necesarios el .Cer y el .Key en formato B64";
            var build = new BuildSettings();
            Cancelation cancelation = new Cancelation(build.Url, build.User, build.Password);
            var response = cancelation.CancelarByCSD("", build.Key, build.Rfc, build.CerPassword, Guid.NewGuid().ToString(), "02");
            Assert.IsTrue(response.messageDetail.Contains((string)resultExpect));
        }
    }
}
