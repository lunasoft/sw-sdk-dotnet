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
        private BuildSettings Build = new BuildSettings();
        private object resultExpect;
        private void GetEnviromentVariables()
        {
            if (Environment.GetEnvironmentVariable("sw-sdk-url") != null)
            {
                Build.Url = Environment.GetEnvironmentVariable("sw-sdk-url");
            }
            if (Environment.GetEnvironmentVariable("sw-sdk-user") != null)
            {
                Build.User = Environment.GetEnvironmentVariable("sw-sdk-user");
            }
            if (Environment.GetEnvironmentVariable("sw-sdk-password") != null)
            {
                Build.Password = Environment.GetEnvironmentVariable("sw-sdk-password");
            }
            if (Environment.GetEnvironmentVariable("sw-sdk-token") != null)
            {
                Build.Token = Environment.GetEnvironmentVariable("sw-sdk-token");
            }
            if (Environment.GetEnvironmentVariable("sw-sdk-cer") != null)
            {
                Build.Cer = Environment.GetEnvironmentVariable("sw-sdk-cer");
            }
            if (Environment.GetEnvironmentVariable("sw-sdk-key") != null)
            {
                Build.Key = Environment.GetEnvironmentVariable("sw-sdk-key");
            }
            if (Environment.GetEnvironmentVariable("sw-sdk-cer-password") != null)
            {
                Build.CerPassword = Environment.GetEnvironmentVariable("sw-sdk-cer-password");
            }
        }
        [TestMethod]
        public void CancelationByCSD()
        {
            Build = new BuildSettings();
            GetEnviromentVariables();
            Cancelation cancelation = new Cancelation(Build.Url, Build.User, Build.Password);
            var response = cancelation.CancelarByCSD(Build.Cer, Build.Key, Build.Rfc, Build.CerPassword, "01724196-ac5a-4735-b621-e3b42bcbb459");
            Assert.IsTrue(response.Data.Acuse != null && response.status == "success");
        }
        [TestMethod]
        public void CancelationByPFX()
        {
            Build = new BuildSettings();
            GetEnviromentVariables();
            Cancelation cancelation = new Cancelation(Build.Url, Build.User, Build.Password);
            var response = cancelation.CancelarByPFX(Build.Pfx, Build.Rfc, Build.CerPassword, "01724196-ac5a-4735-b621-e3b42bcbb459");
            Assert.IsTrue(response.Data.Acuse != null && response.status == "success");
        }
        [TestMethod]
        public void CancelationByXML()
        {
            Build = new BuildSettings();
            GetEnviromentVariables();
            Cancelation cancelation = new Cancelation(Build.Url, Build.User, Build.Password);
            var response = cancelation.CancelarByXML(Build.Acuse);
            Assert.IsTrue(response.Data.Acuse != null && response.status == "success");
        }
        [TestMethod]
        public void ValidateParameters()
        {
            resultExpect = "Son necesarios el .Cer y el .Key en formato B64";
            Build = new BuildSettings();
            Cancelation cancelation = new Cancelation(Build.Url, Build.User, Build.Password);
            var response = cancelation.CancelarByCSD(Build.Cer, Build.Key, Build.Rfc, Build.CerPassword, "");
            Assert.IsTrue(response.messageDetail.Contains((string)resultExpect));
        }
    }
}
