using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using SW.Services.Account;
using SW.Helpers;
using Test_SW.Helpers;

namespace Test_SW.Services.Account_Test
{
    [TestClass]
    public class Account_Test_45
    {
        
        [TestMethod]
        public void Account_Test_45_ConsultaDeSaldoByUser()
        {
            var build = new BuildSettings();
            BalanceAccount account = new BalanceAccount(build.Url, build.User, build.Password);
            var response = account.ConsultarSaldo();
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void Account_Test_45_ConsultaDeSaldoByToken()
        {
            var build = new BuildSettings();
            BalanceAccount account = new BalanceAccount(build.Url, build.Token);
            var response = account.ConsultarSaldo();
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
    }
}
