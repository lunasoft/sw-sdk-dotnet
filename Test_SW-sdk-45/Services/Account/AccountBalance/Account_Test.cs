﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using SW.Services.Account.AccountBalance;
using SW.Helpers;
using Test_SW.Helpers;

namespace Test_SW.Services.Account_Test
{
    [TestClass]
    public class Account_Test_45
    {
        
        [TestMethod]
        public void Account_Test_45_ConsultaDeSaldoByUserV2()
        {
            var build = new BuildSettings();
            AccountBalance account = new AccountBalance(build.Url, build.UrlApi, build.User, build.Password);
            var response = account.ConsultarSaldo();
            Assert.IsNotNull(response.data);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void Account_Test_45_ConsultaDeSaldoByTokenV2()
        {
            var build = new BuildSettings();
            AccountBalance account = new AccountBalance(build.UrlApi, build.Token);
            var response = account.ConsultarSaldo();
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [Ignore]
        public void AsignarTimbresByUserV2()
        {
            var build = new BuildSettings();
            var timbres = 2;
            Guid idUser = Guid.Parse("32501CF2-DC62-4370-B47D-25024C44E131");
            AccountBalance account = new AccountBalance(build.Url, build.UrlApi, build.User, build.Password);
            var response = account.AgregarTimbres(idUser, timbres, "Timbres agregados");
            Assert.IsNotNull(response.data);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [Ignore]
        public void AsignarTimbresByTokenV2()
        {
            var build = new BuildSettings();
            var timbres = 2;
            Guid idUser = Guid.Parse("32501CF2-DC62-4370-B47D-25024C44E131");
            AccountBalance account = new AccountBalance(build.UrlApi, build.Token);
            var response = account.AgregarTimbres(idUser, timbres, "Timbres agregados");
            Assert.IsNotNull(response.data);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [Ignore]
        public void EliminarTimbresByUserV2()
        {
            var build = new BuildSettings();
            var timbres = 2;
            Guid idUser = Guid.Parse("32501CF2-DC62-4370-B47D-25024C44E131");
            AccountBalance account = new AccountBalance(build.Url, build.UrlApi, build.User, build.Password);
            var response = account.EliminarTimbres(idUser, timbres, "Timbres removidos");
            Assert.IsNotNull(response.data);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [Ignore]
        public void EliminarTimbresByToken()
        {
            var build = new BuildSettings();
            var timbres = 2;
            Guid idUser = Guid.Parse("32501CF2-DC62-4370-B47D-25024C44E131");
            AccountBalance account = new AccountBalance(build.UrlApi, build.Token);
            var response = account.EliminarTimbres(idUser, timbres, "Timbres removidos");
            Assert.IsNotNull(response.data);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void AsignarTimbresEmptyUrlApi()
        {
            var build = new BuildSettings();
            Guid idUser = Guid.Parse("32501CF2-DC62-4370-B47D-25024C44E131");
            AccountBalance account = new AccountBalance(build.Url, "", build.User, build.Password);
            var response = account.AgregarTimbres(idUser, 2, "Timbres agregados");
            Assert.IsTrue(response.status == "error");
            Assert.IsTrue(response.message == "Falta Capturar URL Api");
        }
        [TestMethod]
        public void AsignarTimbresEmptyUrl()
        {
            var build = new BuildSettings();
            Guid idUser = Guid.Parse("32501CF2-DC62-4370-B47D-25024C44E131");
            AccountBalance account = new AccountBalance("", build.Token);
            var response = account.AgregarTimbres(idUser, 2, "Timbres agregados");
            Assert.IsTrue(response.status == "error");
            Assert.IsTrue(response.message == "Falta Capturar URL");
        }
        [TestMethod]
        public void EliminarTimbresWrongUser()
        {
            var build = new BuildSettings();
            Guid idUser = Guid.Parse("32501CF2-DC62-0000-0000-25024C44E131");
            AccountBalance account = new AccountBalance(build.Url, build.UrlApi, build.User, build.Password);
            var response = account.EliminarTimbres(idUser, 2, "Timbres removidos");
            Assert.IsTrue(response.status == "error");
            Assert.IsTrue(response.message == "El usuario no fue encontrado.");
        }
    }
}
