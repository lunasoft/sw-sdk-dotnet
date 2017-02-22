using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SW.Services.Authentication;
using Test_SW.Helpers;

namespace Test_SW.Services.Authentication_Test
{
    [TestClass]
    public class Authentication_Test
    {
        private BuildSettings Build;
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
        }
        [TestMethod]
        public void ValidateAuthentication()
        {
            Build = new BuildSettings();
            GetEnviromentVariables();
            Authentication auth = new Authentication(Build.Url, Build.User, Build.Password);
            var response = auth.GetToken();
            Assert.IsTrue(!string.IsNullOrEmpty(response.Data.token));
        }
        [TestMethod]
        public void ValidateExistUser()
        {
            Build = new BuildSettings();
            resultExpect = "Falta Capturar Usuario";
            Authentication auth = new Authentication(Build.Url, "", Build.Password);
            var response = auth.GetToken();
            Assert.AreEqual(response.Message, (string)resultExpect, (string)resultExpect);
        }
        [TestMethod]
        public void ValidateExistPassword()
        {
            Build = new BuildSettings();
            resultExpect = "Falta Capturar Contraseña";
            Authentication auth = new Authentication(Build.Url, Build.User, "");
            var response = auth.GetToken();
            Assert.AreEqual(response.Message, (string)resultExpect, (string)resultExpect);
        }
        [TestMethod]
        public void ValidateExistUrl()
        {
            Build = new BuildSettings();
            resultExpect = "Falta Capturar URL";
            Authentication auth = new Authentication("", Build.User, Build.Password);
            var response = auth.GetToken();
            Assert.AreEqual(response.Message, (string)resultExpect, (string)resultExpect);
        }
    }
}
