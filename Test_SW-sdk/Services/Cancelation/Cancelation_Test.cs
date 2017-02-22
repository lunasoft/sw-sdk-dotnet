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
        [Ignore]
        public void CancelationV1()
        {
            Build = new BuildSettings();
            GetEnviromentVariables();
            Cancelation cancelation = new Cancelation(Build.Url, Build.User, Build.Password);
            var response = cancelation.Cancelar(CancelationTypes.v1, Build.Cer, Build.Key, Build.CerPassword, Build.UUIDs);
            Trace.WriteLine(response.Data);
            Assert.IsTrue(!string.IsNullOrEmpty(response.Data.Acuse));
        }
        [TestMethod]
        public void ValidateExistUUIDs()
        {
            resultExpect = "Faltan especificar los UUIDs a Cancelar";
            Build = new BuildSettings();
            Build.UUIDs = new string[0];
            Cancelation cancelation = new Cancelation(Build.Url, Build.User, Build.Password);
            var response = cancelation.Cancelar(CancelationTypes.v1, Build.Cer, Build.Key, Build.CerPassword, Build.UUIDs);
            Assert.AreEqual(response.Message, (string)resultExpect, (string)resultExpect);
        }
        [TestMethod]
        public void ValidateExistCer()
        {
            resultExpect = "Falta Capturar el Certificado";
            Build = new BuildSettings();
            Cancelation cancelation = new Cancelation(Build.Url, Build.User, Build.Password);
            var response = cancelation.Cancelar(CancelationTypes.v1, "", Build.Key, Build.CerPassword, Build.UUIDs);
            Assert.AreEqual(response.Message, (string)resultExpect, (string)resultExpect);
        }
        [TestMethod]
        public void ValidateExistKey()
        {
            resultExpect = "Falta Capturar Key del Certificado";
            Build = new BuildSettings();
            Cancelation cancelation = new Cancelation(Build.Url, Build.User, Build.Password);
            var response = cancelation.Cancelar(CancelationTypes.v1, Build.Cer, "" , Build.CerPassword, Build.UUIDs);
            Assert.AreEqual(response.Message, (string)resultExpect, (string)resultExpect);
        }
        [TestMethod]
        public void ValidateExistPassword()
        {
            resultExpect = "Falta Capturar Contraseña del Certificado";
            Build = new BuildSettings();
            Cancelation cancelation = new Cancelation(Build.Url, Build.User, Build.Password);
            var response = cancelation.Cancelar(CancelationTypes.v1, Build.Cer, Build.Key, "", Build.UUIDs);
            Assert.AreEqual(response.Message, (string)resultExpect, (string)resultExpect);
        }
        [TestMethod]
        public void ValidateIsBase64Cer()
        {
            resultExpect = "Tu Certificado no es Base64";
            Build = new BuildSettings();
            Cancelation cancelation = new Cancelation(Build.Url, Build.User, Build.Password);
            var response = cancelation.Cancelar(CancelationTypes.v1, "541", Build.Key, Build.CerPassword, Build.UUIDs);
            Assert.AreEqual(response.Message, (string)resultExpect, (string)resultExpect);
        }
        [TestMethod]
        public void ValidateIsBase64Key()
        {
            resultExpect = "Tu Key no es Base64";
            Build = new BuildSettings();
            Cancelation cancelation = new Cancelation(Build.Url, Build.User, Build.Password);
            var response = cancelation.Cancelar(CancelationTypes.v1, Build.Cer, "123", Build.CerPassword, Build.UUIDs);
            Assert.AreEqual(response.Message, (string)resultExpect, (string)resultExpect);
        }
    }
}
