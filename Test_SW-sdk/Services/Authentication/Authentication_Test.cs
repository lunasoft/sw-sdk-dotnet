using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SW.Services.Authentication;
using Test_SW.Helpers;

namespace Test_SW.Services.Authentication_Test
{
    [TestClass]
    public class Authentication_Test
    {
        [TestMethod]
        public void ValidateAuthenticationV2()
        {
            var build = new BuildSettings();
            Authentication auth = new Authentication(build.Url, build.User, build.Password);
            var response = auth.GetToken();
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.token));
        }
        [TestMethod]
        public void ValidateExistUserV2()
        {
            var build = new BuildSettings();
            var resultExpect = "Falta Capturar Usuario";
            Authentication auth = new Authentication(build.Url, "", build.Password);
            var response = auth.GetToken();
            Assert.AreEqual(response.message, (string)resultExpect, (string)resultExpect);
        }
        [TestMethod]
        public void ValidateExistPasswordV2()
        {
            var build = new BuildSettings();
            var resultExpect = "Falta Capturar Contraseña";
            Authentication auth = new Authentication(build.Url, build.User, "");
            var response = auth.GetToken();
            Assert.AreEqual(response.message, (string)resultExpect, (string)resultExpect);
        }
        [TestMethod]
        public void ValidateExistUrlV2()
        {
            var build = new BuildSettings();
            var resultExpect = "Falta Capturar URL";
            Authentication auth = new Authentication("", build.User, build.Password);
            var response = auth.GetToken();
            Assert.AreEqual(response.message, (string)resultExpect, (string)resultExpect);
        }
    }
}
