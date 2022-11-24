using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_SW.Helpers;
using SW.Services.Resend;

namespace Resend_Test.Services.Resend_Tests
{
    [TestClass]
    public class ResendUnit
    {
        [TestMethod]
        public void UT_ResendEmail_Null()
        {
            var build = new BuildSettings();
            Resend resend = new Resend(build.Url, build.UrlApi, build.User, build.Password);
            var resendResult = resend.ResendEmail(Guid.Parse("b711186a-8452-4206-9fec-1b14baad281e"), null);
            Assert.IsTrue(resendResult.status == "error");
        }
        [TestMethod]
        public void UT_ResendEmail_Empty()
        {
            var build = new BuildSettings();
            string[] email = {
                " "
            };
            Resend resend = new Resend(build.Url, build.UrlApi, build.User, build.Password);
            var resendResult = resend.ResendEmail(Guid.Parse("b711186a-8452-4206-9fec-1b14baad281e"), email);
            Assert.IsTrue(resendResult.status == "error");
        }
        [TestMethod]
        public void UT_ResendEmail_Token_success()
        {
            var build = new BuildSettings();
            string[] email = {
                "fernando.carrillo@sw.com.mx"
            };
            Resend resend = new Resend(build.UrlApi, build.Token);
            var resendResult = resend.ResendEmail(Guid.Parse("b711186a-8452-4206-9fec-1b14baad281e"), email);
            Assert.IsTrue(resendResult.status == "success");
        }
        [TestMethod]
        public void UT_ResendEmail_User_success()
        {
            var build = new BuildSettings();
            string[] email = {
                "fernando.carrillo@sw.com.mx"
            };
            Resend resend = new Resend(build.Url, build.UrlApi, build.User, build.Password);
            var resendResult = resend.ResendEmail(Guid.Parse("b711186a-8452-4206-9fec-1b14baad281e"), email);
            Assert.IsTrue(resendResult.status == "success");
        }
        [TestMethod]
        public void UT_ResendEmail_Max_success()
        {
            var build = new BuildSettings();
            string[] email = {
                "fernando.carrillo@sw.com.mx",
                "carrillo.fernando.01@gmail.com",
                "fernando.carrillo@sw.com.mx",
                "fernando.carrillo@sw.com.mx",
                "fernando.carrillo@sw.com.mx",
            };
            Resend resend = new Resend(build.Url, build.UrlApi, build.User, build.Password);
            var resendResult = resend.ResendEmail(Guid.Parse("b711186a-8452-4206-9fec-1b14baad281e"), email);
            Assert.IsTrue(resendResult.status == "success");
        }
        [TestMethod]
        public void UT_Resend_InvalidEmail_Error()
        {
            var build = new BuildSettings();
            string[] email = {
                "@mail.com"
            };
            Resend resend = new Resend(build.Url, build.UrlApi, build.User, build.Password);
            var resendResult = resend.ResendEmail(Guid.Parse("b711186a-8452-4206-9fec-1b14baad281e"), email);
            Assert.IsTrue(resendResult.status == "error");
        }
        [TestMethod]
        public void UT_Resend_MaxEmail_Error()
        {
            var build = new BuildSettings();
            string[] email = {
                "prueba@test.com",
                "prueba@test.com",
                "prueba@test.com",
                "prueba@test.com",
                "prueba@test.com",
                "prueba@test.com"
            };
            Resend resend = new Resend(build.Url, build.UrlApi, build.User, build.Password);
            var resendResult = resend.ResendEmail(Guid.Parse("b711186a-8452-4206-9fec-1b14baad281e"), email);
            Assert.IsTrue(resendResult.status == "error");
        }
    }
}
