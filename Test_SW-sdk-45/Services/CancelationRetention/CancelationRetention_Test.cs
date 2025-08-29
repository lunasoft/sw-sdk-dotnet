using Microsoft.VisualStudio.TestTools.UnitTesting;
using SW.Services.CancelationRetention;
using Test_SW.Helpers;

namespace Test_SW_sdk_45.Services.CancelationRetention_Test_45
{
    [TestClass]
    public class CancelationRetention_Test
    {
        [TestMethod]
        public void UT_CancelationRetention_Auth_Test_CancelationUno()
        {
            var build = new BuildSettings();
            CancelationRetention cancelation = new CancelationRetention(build.Url, build.User, build.Password);
            var response = cancelation.CancelarUno(build.CancelacionRetXML);
            Assert.IsTrue(response.data.acuse != null && response.status == "success");
        }
        [TestMethod]
        public void UT_CancelationRetention_Token_Test_CancelationUno()
        {
            var build = new BuildSettings();
            CancelationRetention cancelation = new CancelationRetention(build.Url, build.Token);
            var response = cancelation.CancelarUno(build.CancelacionRetXML);
            Assert.IsTrue(response.data.acuse != null && response.status == "success");
        }

        [TestMethod]
        public void UT_CancelationRetention_Auth_Test_CancelationUnoCsd()
        {
            var build = new BuildSettings();
            var uuid = "1fae5735-ca51-4be4-9180-827c44fdb227";
            CancelationRetention cancelation = new CancelationRetention(build.Url, build.User, build.Password);
            var response = cancelation.CancelarUnoCSD(build.Cer, build.Key, build.Rfc, build.CerPassword, uuid, "02");
            Assert.IsTrue(response.data.acuse != null && response.status == "success");
        }

        [TestMethod]
        public void UT_CancelationRetention_Token_Test_CancelationUnoCsd()
        {
            var build = new BuildSettings();
            var uuid = "1fae5735-ca51-4be4-9180-827c44fdb227";
            CancelationRetention cancelation = new CancelationRetention(build.Url, build.Token);
            var response = cancelation.CancelarUnoCSD(build.Cer, build.Key, build.Rfc, build.CerPassword, uuid, "02");
            Assert.IsTrue(response.data.acuse != null && response.status == "success");
        }

        [TestMethod]
        public void UT_CancelationRetention_Token_Error_Test_CancelationUnoCsd()
        {
            var build = new BuildSettings();
            var uuid = "1fae5735-ca51-4be4-9180-827c44fdb227";
            CancelationRetention cancelation = new CancelationRetention(build.Url, build.Token);
            var response = cancelation.CancelarUnoCSD(build.Cer, build.Key, build.Rfc, build.CerPassword, uuid, "0");
            var resultExpect = "CR1310. Clave de motivo de cancelación no válida";
            var resultExpectMessage = "CACFDI33 - Problemas con el xml.";
            Assert.IsTrue(response.status == "error");
            Assert.IsTrue(response.messageDetail == resultExpect);
            Assert.IsTrue(response.message == resultExpectMessage);
        }

        [TestMethod]
        public void UT_CancelationRetention_Auth_Test_CancelationUnoPfx()
        {
            var build = new BuildSettings();
            var uuid = "1fae5735-ca51-4be4-9180-827c44fdb227";
            CancelationRetention cancelation = new CancelationRetention(build.Url, build.User, build.Password);
            var response = cancelation.CancelarUnoPFX(build.Pfx, build.Rfc, build.PfxPassword, uuid, "02");
            Assert.IsTrue(response.data.acuse != null && response.status == "success");
        }

        [TestMethod]
        public void UT_CancelationRetention_Token_Test_CancelationUnoPfx()
        {
            var build = new BuildSettings();
            var uuid = "1fae5735-ca51-4be4-9180-827c44fdb227";
            CancelationRetention cancelation = new CancelationRetention(build.Url, build.Token);
            var response = cancelation.CancelarUnoPFX(build.Pfx, build.Rfc, build.PfxPassword, uuid, "02");
            Assert.IsTrue(response.data.acuse != null && response.status == "success");
        }

        [TestMethod]
        public void UT_CancelationRetention_Token_Error_Test_CancelationUnoPfx()
        {
            var build = new BuildSettings();
            var uuid = "1fae5735-ca51-4be4-9180-827c44fdb227";
            CancelationRetention cancelation = new CancelationRetention(build.Url, build.Token);
            var response = cancelation.CancelarUnoPFX("", build.Rfc, build.PfxPassword, uuid, "0");
            var resultExpect = "El archivo PFX es requerido.";
            var resultExpectMessage = "CACFDI33 - Problemas con los campos.";
            Assert.IsTrue(response.status == "error");
            Assert.IsTrue(response.messageDetail == resultExpect);
            Assert.IsTrue(response.message == resultExpectMessage);
        }
    }
}
