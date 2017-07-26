using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using SW.Services.Account;
using SW.Helpers;
using Test_SW.Helpers;

namespace Test_SW.Services.Account_Test
{
    [TestClass]
    public class Account_Test
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
        public void ConsultaDeSaldoByUser()
        {
            Build = new BuildSettings();
            GetEnviromentVariables();
            BalanceAccount account = new BalanceAccount(Build.Url, Build.User, Build.Password);
            var response = account.ConsultarSaldo();
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void ConsultaDeSaldoByToken()
        {
            Build = new BuildSettings();
            GetEnviromentVariables();
            BalanceAccount account = new BalanceAccount(Build.Url, Build.Token);
            var response = account.ConsultarSaldo();
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
    }
}
