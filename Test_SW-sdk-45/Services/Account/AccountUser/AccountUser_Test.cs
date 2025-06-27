using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using SW.Services.Account.AccountUser;
using SW.Helpers;
using Test_SW.Helpers;
using System.Linq;
using System.Collections.Generic;

namespace Test_SW.Services.AccountUser_Test
{
    [TestClass]
    public class AccountUser_Test
    {
        //
        [TestMethod]
        public void GetUserByEmailAuthSuccessV2()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi, build.User, build.Password);
            var response = infoUser.GetUserByEmail($"userdotnet_{build.User}");
            Assert.IsNotNull(response.data);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByEmailSuccessV2()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, build.Token);
            var response = infoUser.GetUserByEmail($"userdotnet_{build.User}");
            Assert.IsNotNull(response.data);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByEmailAuthErrorV2()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi, build.User, "fakepassword");
            var response = infoUser.GetUserByEmail($"userdotnet_{build.User}");
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByEmailErrorV2()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, "T2jendl...");
            var response = infoUser.GetUserByEmail($"userdotnet_{build.User}");
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        //GetUserByTaxId
        [TestMethod]
        public void GetUserByTaxIdSuccessV2()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, build.Token);
            var response = infoUser.GetUserByTaxId("XAXX010101000");
            Assert.IsNotNull(response.data);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByTaxIdErrorV2()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, build.Token);
            var response = infoUser.GetUserByTaxId("XEXX010101001");
            Assert.IsTrue(response.status == "success", response.messageDetail);
            Assert.IsTrue(!response.data.Any(), "El array user no está vacío.");
        }
        //GetUserById
        [TestMethod]
        public void GetUserByIdSuccessV2()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, build.Token);
            Guid idUser = Guid.Parse("32501CF2-DC62-4370-B47D-25024C44E131");
            var response = infoUser.GetUserById(idUser);
            Assert.IsNotNull(response.data);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByErrorV2()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, build.Token);
            Guid idUser = Guid.Parse("32501CF2-DC62-4370-B47D-25024C44E133");
            var response = infoUser.GetUserById(idUser);
            Assert.IsTrue(response.status == "success", response.messageDetail);
            Assert.IsTrue(!response.data.Any(), "El array user no está vacío.");
        }
        //GetUserByIsActive
        [TestMethod]
        public void GetUserByIsActiveFalseSuccessV2()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, build.Token);
            var response = infoUser.GetUserByIsActive(false);
            Assert.IsNotNull(response.data);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByIsActiveTrueSuccessV2()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, build.Token);
            var response = infoUser.GetUserByIsActive(true);
            Assert.IsNotNull(response.data);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void GetAllUserAuthSuccessV2()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi, build.User, build.Password);
            var response = infoUser.GetAllUsers();
            Assert.IsTrue(response.status == "success", response.messageDetail);
            Assert.IsNotNull(response.data);
            List<AccountUserData> user = response.data;
            foreach (var item in user)
            {
                Console.WriteLine(item.idUser);
                Console.WriteLine(item.idDealer);
                Console.WriteLine(item.taxId);
                Console.WriteLine(item.name);
                Console.WriteLine(item.username);
                Console.WriteLine(item.profile);
                Console.WriteLine(item.accessToken);
            }
        }
        [TestMethod]
        public void GetAllUserSuccessV2()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, build.Token);
            var response = infoUser.GetAllUsers();
            Assert.IsTrue(response.status == "success", response.messageDetail);
            Assert.IsNotNull(response.data);
            List<AccountUserData> user = response.data;
            foreach (var item in user)
            {
                Console.WriteLine(item.idUser);
                Console.WriteLine(item.idDealer);
                Console.WriteLine(item.taxId);
                Console.WriteLine(item.name);
                Console.WriteLine(item.username);
                Console.WriteLine(item.profile);
                Console.WriteLine(item.accessToken);
            }
        }
        [TestMethod]
        public void GetAllUserAuthErrorV2()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi, build.User, "fakepassword");
            var response = infoUser.GetAllUsers();
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        [TestMethod]
        public void GetAllUserErrorV2()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, "T2jendl...");
            var response = infoUser.GetAllUsers();
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        //Create User
        [TestMethod]
        public void CreateUserSuccessV2()
        {
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.UrlApi, build.Token);
            var response = user.CreateUser(new AccountUserRequest()
            {
                name = "Prueba UT Hijo dotnet",
                taxId = "XAXX010101000",
                email = $"userdotnet_{build.User}",
                stamps = 1,
                isUnlimited = false,
                password = $"_{build.Password}",
                notificationEmail = $"user_{build.User}",
                phone = "0000000000"
            });
            Assert.IsNotNull(response);
            Assert.IsTrue(response.status.Equals("success") || (response.status.Equals("error") && response.message.Equals($"El email 'userdotnet_{build.User}' ya esta en uso.")));
        }
        [TestMethod]
        public void CreateUserAuthSuccess()
        {
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.Url, build.UrlApi, build.User, build.Password);
            var response = user.CreateUser(new AccountUserRequest()
            {
                name = "Prueba UT Hijo dotnet",
                taxId = "XAXX010101000",
                email = $"userdotnet_{build.User}",
                stamps = 1,
                isUnlimited = false,
                password = $"_{build.Password}",
                notificationEmail = $"user_{build.User}",
                phone = "0000000000"
            });
            Assert.IsNotNull(response);
            Assert.IsTrue(response.status.Equals("success") || (response.status.Equals("error") && response.message.Equals($"El email 'userdotnet_{build.User}' ya esta en uso.")));
        }
        [TestMethod]
        public void CreateUserAuthErrorV2()
        {
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.UrlApi, "tokenFake");
            var response = user.CreateUser(new AccountUserRequest()
            {
                name = "Prueba UT Hijo dotnet",
                taxId = "XAXX010101000",
                email = $"userdotnet_{build.User}",
                stamps = 1,
                isUnlimited = false,
                password = $"_{build.Password}",
                notificationEmail = $"user_{build.User}",
                phone = "0000000000"
            });
            Assert.IsNotNull(response);
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        [TestMethod]
        public void CreateUserPasswordErrorV2()
        {
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.Url, build.UrlApi, build.User, "fakepassword");
            var response = user.CreateUser(new AccountUserRequest()
            {
                name = "Prueba UT Hijo dotnet",
                taxId = "XAXX010101000",
                email = $"userdotnet_{build.User}",
                stamps = 1,
                isUnlimited = false,
                password = $"_{build.Password}",
                notificationEmail = $"user_{build.User}",
                phone = "0000000000"
            });
            Assert.IsNotNull(response);
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        //Update User
        [TestMethod]
        public void UpdateUserSuccessV2()
        {
            var build = new BuildSettings();
            string expectMessage = "No es posible actualizar, los datos enviados son identicos a los actuales";
            Guid idUser = Guid.Parse("106f4d82-575e-436c-a923-8c85517f2ca9");
            AccountUser user = new AccountUser(build.UrlApi, build.Token);
            var responseUpdate = user.UpdateUser(idUser, "Nombre Actualizado", "AAAA000101010", null, "1234567890", false);
            Assert.IsTrue(responseUpdate.status.Equals("success") || (responseUpdate.status.Equals("error") && responseUpdate.message.Equals(expectMessage)));
        }

        [TestMethod]
        public void UpdateUserAuthSuccessV2()
        {
            string expectMessage = "No es posible actualizar, los datos enviados son identicos a los actuales";
            Guid idUser = Guid.Parse("2c6a91f6-2b14-4e61-b528-2becd26d6c33");
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.Url, build.UrlApi, build.User, build.Password);
            var response = user.UpdateUser(idUser, "Nombre Actualizado", "AAAA000101010", null, "1234567890", false);
            Assert.IsTrue(response.status.Equals("success") || (response.status.Equals("error") && response.message.Equals(expectMessage)));
        }
        [TestMethod]
        public void UpdateUserAuthErrorV2()
        {
            Guid idUser = Guid.Parse("d1defb8a-f7f8-4a70-83f2-989458560cfa");
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.Url, build.UrlApi, build.User, build.Password);
            var response = user.UpdateUser(idUser, "Nombre Actualizado", "AAAA000101010", null, "1234567890", false);
            Assert.IsTrue(response.status.Equals("error"), response.messageDetail);
        }
        [TestMethod]
        public void UpdateUserError()
        {
            Guid idUser = Guid.Parse("d1defb8a-f7f8-4a70-83f2-989458560cfa");
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.UrlApi, build.Token);
            var response = user.UpdateUser(idUser, "Nombre Actualizado", "AAAA000101010", null, "1234567890", false);
            Assert.IsTrue(response.status.Equals("error"), response.messageDetail);
        }
        //Delete User
        [Ignore]
        public void DeleteUserSuccessV2()
        {
            Guid idUser = Guid.Parse("fbed157d-1949-4351-8058-0a8ee0201d36");
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.UrlApi, build.Token);
            var response = user.DeleteUser(idUser);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void DeleteUserErrorV2()
        {
            Guid idUserFake = Guid.Parse("fbed157d-1949-4531-8058-0a8ee0209d36");
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.UrlApi, build.Token);
            var response = user.DeleteUser(idUserFake);
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        [Ignore]
        public void DeleteUserAuthSuccess()
        {
            Guid idUser = Guid.Parse("fbed157d-1949-4351-8058-0a8ee0201d36");
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.Url, build.UrlApi, build.User, build.Password);
            var response = user.DeleteUser(idUser);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void DeleteUserAuthError()
        {
            Guid idUserFake = Guid.Parse("fbed157d-1949-4531-8058-0a8ee0209d36");
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.UrlApi, build.Token);
            var response = user.DeleteUser(idUserFake);
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
    }
}
