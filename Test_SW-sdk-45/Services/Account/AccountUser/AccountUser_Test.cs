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
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi, build.UserDealer, build.PasswordDealer);
            var response = infoUser.GetUserByToken();
            Assert.IsNotNull(response.data);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByTokenSuccess()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, build.TokenDealer);
            var response = infoUser.GetUserByToken();
            Assert.IsNotNull(response.data);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByTokenAuthError()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi, build.UserDealer, "fakepassword");
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
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi, build.UserDealer, build.PasswordDealer);
            var response = infoUser.GetAllUsers();
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void GetAllUserSuccess()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, build.TokenDealer);
            var response = infoUser.GetAllUsers();
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void GetAllUserAuthError()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi, build.UserDealer, "fakepassword");
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
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi, build.UserDealer, build.PasswordDealer);
            var response = infoUser.GetUserById(idUser);
            Assert.IsNotNull(response.data);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByIdSuccess()
        {
            Guid idUser = Guid.Parse("77d1df67-10ef-4b4d-a07a-99dd4ab7f4f4");
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, build.TokenDealer);
            var response = infoUser.GetUserById(idUser);
            Assert.IsNotNull(response.data);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByIdAuthError()
        {
            Guid idUserFake = Guid.Parse("77d1df67-10ef-7d4d-a07a-99dd4ab7f4f4");
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi, build.UserDealer, build.PasswordDealer);
            var response = infoUser.GetUserById(idUserFake);
            Assert.IsNull(response.data);
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByIdError()
        {
            Guid idUserFake = Guid.Parse("77d1df67-10ef-7d4d-a07a-99dd4ab7f4f4");
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, build.TokenDealer);
            var response = infoUser.GetUserById(idUserFake);
            Assert.IsNull(response.data);
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByIdNotFound()
        {
            Guid idUser = Guid.Parse("fbed157d-1949-4351-8058-0a8ee0201d36");
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, build.TokenDealer);
            var response = infoUser.GetUserById(idUser);
            Assert.IsNull(response.data);
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        [TestMethod]
        public void CreateUserSuccess()
        {
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.UrlApi, build.TokenDealer);
            var response = user.CreateUser(new AccountUserRequest()
            {
                email = $"hijo_{build.User}",
                password = "SwpassTest1!",
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
            AccountUser user = new AccountUser(build.Url, build.UrlApi, build.UserDealer, build.PasswordDealer);
            var response = user.CreateUser(new AccountUserRequest()
            {
                email = $"cuenta_hijo_{build.User}",
                password = "SwpassTest1!",
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
            AccountUser user = new AccountUser(build.Url, build.UrlApi, build.UserDealer, "fakepassword");
            var response = user.CreateUser(new AccountUserRequest()
            {
                email = $"hijo_{build.User}",
                password = "SwpassTest1!",
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
            AccountUser user = new AccountUser(build.UrlApi, build.TokenDealer);
            var response = user.CreateUser(new AccountUserRequest()
            {
                email = $"hijo_{build.User}",
                password = "SwpassTest",
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
            AccountUser user = new AccountUser(build.Url, build.UrlApi, build.UserDealer, build.PasswordDealer);
            var response = user.CreateUser(new AccountUserRequest()
            {
                email = $"hijo_{build.User}",
                password = "SwpassTest1!",
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
            Guid idUser = Guid.Parse("106f4d82-575e-436c-a923-8c85517f2ca9");
            String tokenUserUpdate = "T2lYQ0t4L0RHVkR4dHZ5Nkk1VHNEakZ3Y0J4Nk9GODZuRyt4cE1wVm5tbXB3YVZxTHdOdHAwVXY2NTdJb1hkREtXTzE3dk9pMmdMdkFDR2xFWFVPUXpTUm9mTG1ySXdZbFNja3FRa0RlYURqbzdzdlI2UUx1WGJiKzViUWY2dnZGbFloUDJ6RjhFTGF4M1BySnJ4cHF0YjUvbmRyWWpjTkVLN3ppd3RxL0dJPQ.T2lYQ0t4L0RHVkR4dHZ5Nkk1VHNEakZ3Y0J4Nk9GODZuRyt4cE1wVm5tbFlVcU92YUJTZWlHU3pER1kySnlXRTF4alNUS0ZWcUlVS0NhelhqaXdnWTRncklVSWVvZlFZMWNyUjVxYUFxMWFxcStUL1IzdGpHRTJqdS9Zakw2UGRod1g3Y1FGcGVpS1Z5OVZoMEZRTDBkdHZCbUFsYjZTbS9MQWh0V0dtQTcwNVQ1VldhSDN0ZFBJL0ZaN3FnM1RqcGU2UENQMjVuSzRhVUtGblcwb01BTzhWWXcwQTR6aGg0V2QyRDE2MFZ3dW5vWG5QZXNRekxUeVdGMHJwekRuQUJBeDgvTTRxb0g4YWlaQW1DR2xydlBnRFdzYmpXYkZ1RDhCbGtteTJhRXZSVDRvamJZclN0NkFCNGN2V1lXYUtQQnBZMGt2WTJkNWVJM043TzFVL09PNjU4cXVuOXZQYmVZZkVBKytRemp0Vm0wSUgycVdla1dMbU5oTGVEeHZ5SHVqU2JrdHR0NlVLOXM5Wjh1S3hDY00yMmRMQTN2bm1jbzJpcDRWK1RVamFGQ1FwYTlnVlBXMXBGSFZrUUErSTFqbnAvRkUyQUwwRXRNWm1weFlMRCs0Znk2OStFd2hBK2Y4dUEyekg2VVF4TitvNlJpQmVhRDg3MkZJM3BuT0gwUElzMW1DYmJmcE5iUGxrVnNySmRnPT0.voHhJQQkVfPC8swHKekH0giMAQfnny2YsXypG6cIqqo";
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.UrlApi, tokenUserUpdate);
            var response = user.UpdateUser(idUser, "AAAA000101010", "Pruebas Update", false, true);
            Assert.IsTrue(response.status.Equals("success"), response.messageDetail);
        }

        [TestMethod]
        public void UpdateUserAuthSuccess()
        {
            Guid idUser = Guid.Parse("106f4d82-575e-436c-a923-8c85517f2ca9");
            String userUpdate = "cuenta_hijo_pruebas_ut@sw.com.mx";
            String passUpdate = "SwpassTest1!";
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.Url, build.UrlApi,userUpdate, passUpdate);
            var response = user.UpdateUser(idUser, "AAAA000101010", "Pruebas Update", false, true);
            Assert.IsTrue(response.status.Equals("success"), response.messageDetail);
        }
        [TestMethod]
        public void UpdateUserAuthError()
        {
            Guid idUser = Guid.Parse("106f4d82-575e-436c-a923-8c85517f2ca9");

            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.Url, build.UrlApi, build.UserDealer, build.PasswordDealer);
            var response = user.UpdateUser(idUser, "AAAA000101010", "Pruebas Update", false, true);
            Assert.IsTrue(response.status.Equals("error"), response.messageDetail);
        }
        [TestMethod]
        public void UpdateUserError()
        {
            Guid idUser = Guid.Parse("4ec7dbb9-8957-4c90-8eb3-91bfeeb1b24b");

            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.UrlApi, build.TokenDealer);
            var response = user.UpdateUser(idUser, "AAAA000101010", "Pruebas Update", false, true);
            Assert.IsTrue(response.status.Equals("error"), response.messageDetail);
        }

        [Ignore]
        public void DeleteUserSuccess()
        {
            Guid idUser = Guid.Parse("fbed157d-1949-4351-8058-0a8ee0201d36");
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.UrlApi, build.TokenDealer);
            var response = user.DeleteUser(idUser);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [Ignore]
        public void DeleteUserError()
        {
            Guid idUserFake = Guid.Parse("fbed157d-1949-4531-8058-0a8ee0209d36");
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.UrlApi, build.TokenDealer);
            var response = user.DeleteUser(idUserFake);
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        [Ignore]
        public void DeleteUserAuthSuccess()
        {
            Guid idUser = Guid.Parse("fbed157d-1949-4351-8058-0a8ee0201d36");
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.Url, build.UrlApi, build.UserDealer, build.PasswordDealer);
            var response = user.DeleteUser(idUser);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [Ignore]
        public void DeleteUserAuthError()
        {
            Guid idUserFake = Guid.Parse("fbed157d-1949-4531-8058-0a8ee0209d36");
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.UrlApi, build.TokenDealer);
            var response = user.DeleteUser(idUserFake);
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
    }
}
