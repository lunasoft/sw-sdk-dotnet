using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using SW.Services.Account.AccountUser;
using SW.Helpers;
using Test_SW.Helpers;

namespace Test_SW.Services.AccountUser_Test
{
    [TestClass]
    public class AccountUser_Test
    {
        [TestMethod]
        public void GetUserByTokenAuthSuccess()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi, build.User, build.Password);
            var response = infoUser.GetUserByToken();
            Assert.IsNotNull(response.data);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByTokenSuccess()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, build.Token);
            var response = infoUser.GetUserByToken();
            Assert.IsNotNull(response.data);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByTokenAuthError()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi, build.User, "fakepassword");
            var response = infoUser.GetUserByToken();
            Assert.IsNull(response.data);
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByTokenError()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, "T2jendl...");
            var response = infoUser.GetUserByToken();
            Assert.IsNull(response.data);
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        [TestMethod]
        public void GetAllUserAuthSuccess()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi, build.User, build.Password);
            var response = infoUser.GetAllUsers();
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void GetAllUserSuccess()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, build.Token);
            var response = infoUser.GetAllUsers();
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void GetAllUserAuthError()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi, build.User, "fakepassword");
            var response = infoUser.GetAllUsers();
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        [TestMethod]
        public void GetAllUserError()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, "T2jendl...");
            var response = infoUser.GetAllUsers();
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByIdAuthSuccess()
        {
            Guid idUser = Guid.Parse("77d1df67-10ef-4b4d-a07a-99dd4ab7f4f4");
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi, build.User, build.Password);
            var response = infoUser.GetUserById(idUser);
            Assert.IsNotNull(response.data);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByIdSuccess()
        {
            Guid idUser = Guid.Parse("77d1df67-10ef-4b4d-a07a-99dd4ab7f4f4");
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, build.Token);
            var response = infoUser.GetUserById(idUser);
            Assert.IsNotNull(response.data);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByIdAuthError()
        {
            Guid idUserFake = Guid.Parse("77d1df67-10ef-7d4d-a07a-99dd4ab7f4f4");
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi, build.User, build.Password);
            var response = infoUser.GetUserById(idUserFake);
            Assert.IsNull(response.data);
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByIdError()
        {
            Guid idUserFake = Guid.Parse("77d1df67-10ef-7d4d-a07a-99dd4ab7f4f4");
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, build.Token);
            var response = infoUser.GetUserById(idUserFake);
            Assert.IsNull(response.data);
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByIdNotFound()
        {
            Guid idUser = Guid.Parse("fbed157d-1949-4351-8058-0a8ee0201d36");
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, build.Token);
            var response = infoUser.GetUserById(idUser);
            Assert.IsNull(response.data);
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        [TestMethod]
        public void CreateUserSuccess()
        {
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.UrlApi, build.Token);
            var response = user.CreateUser(new AccountUserRequest()
            {
                email = $"user_{build.User}",
                password = $"_{build.Password}",
                name = "Pruebas User",
                rfc = "XAXX010101000",
                profileType = SW.Helpers.AccountUserProfile.Hijo,
                stamps = 1,
                unlimited = false
            });
            Assert.IsNotNull(response);
            Assert.IsTrue(response.status.Equals("success") || (response.status.Equals("error") && response.message.Contains("AU1001")), response.messageDetail);
        }
        [TestMethod]
        public void CreateUserAuthSuccess()
        {
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.Url, build.UrlApi, build.User, build.Password);
            var response = user.CreateUser(new AccountUserRequest()
            {
                email = $"user_{build.User}",
                password = $"_{build.Password}",
                name = "Pruebas User",
                rfc = "XAXX010101000",
                profileType = SW.Helpers.AccountUserProfile.Hijo,
                stamps = 1,
                unlimited = false
            });
            Assert.IsNotNull(response);
            Assert.IsTrue(response.status.Equals("success") || (response.status.Equals("error") && response.message.Contains("AU1001")), response.messageDetail);
        }
        [TestMethod]
        public void CreateUserAuthError()
        {
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.Url, build.UrlApi, build.User, "fakepassword");
            var response = user.CreateUser(new AccountUserRequest()
            {
                email = $"user_{build.User}",
                password = $"_{build.Password}",
                name = "Pruebas User",
                rfc = "XAXX010101000",
                profileType = SW.Helpers.AccountUserProfile.Hijo,
                stamps = 1,
                unlimited = false
            });
            Assert.IsNotNull(response);
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        [TestMethod]
        public void CreateUserPasswordError()
        {
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.UrlApi, build.Token);
            var response = user.CreateUser(new AccountUserRequest()
            {
                email = $"user_{build.User}",
                password = $"_{build.Password}",
                name = "Pruebas User",
                rfc = "XAXX010101000",
                profileType = SW.Helpers.AccountUserProfile.Hijo,
                stamps = 1,
                unlimited = false
            });
            Assert.IsNotNull(response);
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }

        [TestMethod]
        public void CreateUserNoDealerError()
        {
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.Url, build.UrlApi, build.User, build.Password);
            var response = user.CreateUser(new AccountUserRequest()
            {
                email = $"user_{build.User}",
                password = $"_{build.Password}",
                name = "Pruebas User",
                rfc = "XAXX010101000",
                profileType = SW.Helpers.AccountUserProfile.Hijo,
                stamps = 1,
                unlimited = false
            });
            Assert.IsTrue(response.status == "error", response.messageDetail= "Profile del token no se encuentra en los permitidos para crear usuarios (Admin, Dealer). Perfil del token: User");
        }
        [TestMethod]
        public void UpdateUserSuccess()
        {
            var build = new BuildSettings();
            Guid idUser = Guid.Parse("106f4d82-575e-436c-a923-8c85517f2ca9");
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi, build.User, build.Password);
            var response = infoUser.GetUserById(idUser);
            string tokenUser = response.data.tokenAccess;
            AccountUser user = new AccountUser(build.UrlApi,tokenUser);
            var responseUpdate = user.UpdateUser(idUser, "AAAA000101010", "Pruebas Update", false, true);
            Assert.IsTrue(responseUpdate.status.Equals("success"), responseUpdate.messageDetail);
        }

        [TestMethod]
        public void UpdateUserAuthSuccess()
        {
            Guid idUser = Guid.Parse("2c6a91f6-2b14-4e61-b528-2becd26d6c33");
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.Url, build.UrlApi, $"user_{build.User}", $"_{build.Password}");
            var response = user.UpdateUser(idUser, "AAAA000101010", "Pruebas Update", false, true);
            Assert.IsTrue(response.status.Equals("success"), response.messageDetail);
        }
        [TestMethod]
        public void UpdateUserAuthError()
        {
            Guid idUser = Guid.Parse("106f4d82-575e-436c-a923-8c85517f2ca9");

            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.Url, build.UrlApi, build.User, build.Password);
            var response = user.UpdateUser(idUser, "AAAA000101010", "Pruebas Update", false, true);
            Assert.IsTrue(response.status.Equals("error"), response.messageDetail);
        }
        [TestMethod]
        public void UpdateUserError()
        {
            Guid idUser = Guid.Parse("4ec7dbb9-8957-4c90-8eb3-91bfeeb1b24b");

            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.UrlApi, build.Token);
            var response = user.UpdateUser(idUser, "AAAA000101010", "Pruebas Update", false, true);
            Assert.IsTrue(response.status.Equals("error"), response.messageDetail);
        }

        [Ignore]
        public void DeleteUserSuccess()
        {
            Guid idUser = Guid.Parse("fbed157d-1949-4351-8058-0a8ee0201d36");
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.UrlApi, build.Token);
            var response = user.DeleteUser(idUser);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [Ignore]
        public void DeleteUserError()
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
        [Ignore]
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
