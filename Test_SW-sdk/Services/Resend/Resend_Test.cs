using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test_SW.Helpers;

namespace Test_SW.Services.Resend
{
    [TestClass]
    public class Resend_Test
    {
        [TestMethod]
        public void UT_ResendEmail_Null()
        {
            var build = new BuildSettings();
            SW.Services.Resend.Resend resend = new SW.Services.Resend.Resend(build.UrlApi, build.Url, build.User, build.Password);
            var responseResend = resend.ResendEmail(Guid.Parse("b711186a-8452-4206-9fec-1b14baad281e"), null);
            Assert.IsTrue(responseResend.status == "error");
            Assert.IsTrue(responseResend.message == "El listado contiene mas de 5 correos o el formato es incorrecto");
        }
        [TestMethod]
        public void UT_ResendEmail_Empty()
        {
            var build = new BuildSettings();
            string[] email = {" "};
            SW.Services.Resend.Resend resend = new SW.Services.Resend.Resend(build.UrlApi, build.Url, build.User, build.Password);
            var responseResend = resend.ResendEmail(Guid.Parse("b711186a-8452-4206-9fec-1b14baad281e"), email);
            Assert.IsTrue(responseResend.status == "error");
            Assert.IsTrue(responseResend.message == "El listado contiene mas de 5 correos o el formato es incorrecto");
        }
        [TestMethod]
        public void UT_ResendEmail_Token_success()
        {
            var build = new BuildSettings();
            string[] email = {"prueba@test.com"};
            SW.Services.Resend.Resend resend = new SW.Services.Resend.Resend(build.UrlApi, build.Token);
            var responseResend = resend.ResendEmail(Guid.Parse("23a3788a-3ac1-4b53-bb7b-b64839e6c09b"), email);
            Assert.IsTrue(responseResend.status == "success");
            Assert.IsTrue(responseResend.data == "email sent ok" && 
                responseResend.message == "OK" && 
                responseResend.messageDetail == "OK");
        }
        [TestMethod]
        public void UT_ResendEmail_User_success()
        {
            var build = new BuildSettings();
            string[] email = {"prueba@test.com"};
            SW.Services.Resend.Resend resend = new SW.Services.Resend.Resend( build.UrlApi, build.Url, build.User, build.Password);
            var responseResend = resend.ResendEmail(Guid.Parse("23a3788a-3ac1-4b53-bb7b-b64839e6c09b"), email);
            Assert.IsTrue(responseResend.status == "success");
            Assert.IsTrue(responseResend.data == "email sent ok" && 
                responseResend.message == "OK" && 
                responseResend.messageDetail == "OK");
        }
        [TestMethod]
        public void UT_ResendEmail_Max_success()
        {
            var build = new BuildSettings();
            string[] email = {
                "prueba@test.com",
                "prueba@test.com",
                "prueba@test.com",
                "prueba@test.com",
                "prueba@test.com",
            };
            SW.Services.Resend.Resend resend = new SW.Services.Resend.Resend(build.UrlApi, build.Url, build.User, build.Password);
            var responseResend = resend.ResendEmail(Guid.Parse("23a3788a-3ac1-4b53-bb7b-b64839e6c09b"), email);
            Assert.IsTrue(responseResend.status == "success");
            Assert.IsTrue(responseResend.data == "email sent ok" &&
                responseResend.message == "OK" &&
                responseResend.messageDetail == "OK");
        }
        [TestMethod]
        public void UT_Resend_InvalidEmail_Error()
        {
            var build = new BuildSettings();
            string[] email = {"mail.com"};
            SW.Services.Resend.Resend resend = new SW.Services.Resend.Resend(build.UrlApi, build.Url, build.User, build.Password);
            var responseResend = resend.ResendEmail(Guid.Parse("b711186a-8452-4206-9fec-1b14baad281e"), email);
            Assert.IsTrue(responseResend.status == "error");
            Assert.IsTrue(responseResend.message == "El listado contiene mas de 5 correos o el formato es incorrecto");
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
            SW.Services.Resend.Resend resend = new SW.Services.Resend.Resend(build.UrlApi, build.Url, build.User, build.Password);
            var responseResend = resend.ResendEmail(Guid.Parse("b711186a-8452-4206-9fec-1b14baad281e"), email);
            Assert.IsTrue(responseResend.status == "error");
            Assert.IsTrue(responseResend.message == "El listado contiene mas de 5 correos o el formato es incorrecto");
        }
    }
}
