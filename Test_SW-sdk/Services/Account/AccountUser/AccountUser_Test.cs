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
        string tokenDealer = "T2lYQ0t4L0RHVkR4dHZ5Nkk1VHNEakZ3Y0J4Nk9GODZuRyt4cE1wVm5tbXB3YVZxTHdOdHAwVXY2NTdJb1hkREtXTzE3dk9pMmdMdkFDR2xFWFVPUXpTUm9mTG1ySXdZbFNja3FRa0RlYURqbzdzdlI2UUx1WGJiKzViUWY2dnZGbFloUDJ6RjhFTGF4M1BySnJ4cHF0YjUvbmRyWWpjTkVLN3ppd3RxL0dJPQ.T2lYQ0t4L0RHVkR4dHZ5Nkk1VHNEakZ3Y0J4Nk9GODZuRyt4cE1wVm5tbFlVcU92YUJTZWlHU3pER1kySnlXRTF4alNUS0ZWcUlVS0NhelhqaXdnWTRncklVSWVvZlFZMWNyUjVxYUFxMWFxcStUL1IzdGpHRTJqdS9Zakw2UGQyZmtSaFI1RGQ2dEtBYXNWem1rTHo4cHhpS3lWa0s1MG5wZUYxVHo2MWRqcjdtSnJLcit3bS8zTXAyaHB5by9zdWJkNXlpRUE1TjBQdENVMTNzaG9CcE9RaWhNRlIvbFV0elhKMnZPN2xkQzA1dnRPMnhjYTUvODlBS2lOaEVvWDlCVklKMWtXelJMMmlmd1lqTzFKNU4rTDJOb2ptUDFNNmNqMERXd1F2T2lpVVdGWFA4M1EySEh6b0E3TVg5RFFTQ3JzT0M4TlA4RzczempTWTF5RmpDWksrc2JjM0I1b1JwR1dObVhGb0RvNmF1YWNEWlBOM0w1YnZIUSt3YnNpREhrTzA2dkpCZGlkNmRRN1RaYlQ2NGtabUVoK2w3RVRRS0NEUE9UZ0dHWlhWQVV3Zy9lY2NvM2YxOWJUblJpS0ExVm5EK2NFYkp1dW9BRUtCa1FCQkN2cUhiMFlDS2JEWm50N3gvT2dMZ1FOMC9YcFNwSDJEby9ROHNTRzk5SVE.2qKwZRx2qt65pxqBuPA6SrHg11t8c5ZBVqtSZTTP00c";
        string userDealer= "marifer.mares@sw.com.mx";
        string passwordDealer= "M4rifer+SW2";

        [TestMethod]
        public void GetUserByTokenAuthSuccess()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi, build.User, build.Password);
            var response = infoUser.GetUserByToken();
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByTokenSuccess()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, build.Token);
            var response = infoUser.GetUserByToken();
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByTokenAuthError()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi, build.User, "fakepassword");
            var response = infoUser.GetUserByToken();
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByTokenError()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, "T2jendl...");
            var response = infoUser.GetUserByToken();
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
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByIdSuccess()
        {
            Guid idUser = Guid.Parse("77d1df67-10ef-4b4d-a07a-99dd4ab7f4f4");
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, build.Token);
            var response = infoUser.GetUserById(idUser);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByIdAuthError()
        {
            Guid idUserFake = Guid.Parse("77d1df67-10ef-7d4d-a07a-99dd4ab7f4f4");
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi, build.User, build.Password);
            var response = infoUser.GetUserById(idUserFake);
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByIdError()
        {
            Guid idUserFake = Guid.Parse("77d1df67-10ef-7d4d-a07a-99dd4ab7f4f4");
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, build.Token);
            var response = infoUser.GetUserById(idUserFake);
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        [TestMethod]
        public void GetUserByIdNotFound()
        {
            Guid idUser = Guid.Parse("fbed157d-1949-4351-8058-0a8ee0201d36");
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, build.Token);
            var response = infoUser.GetUserById(idUser);
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        [TestMethod]
        public void CreateUserSuccess()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, tokenDealer);
            var response = infoUser.CreateUser(new AccountUserRequest()
            {
                email = $"hijo_{build.User}",
                password = "SwpassTest1!",
                name = "Pruebas User",
                rfc = "XAXX010101000",
                profileType = SW.Helpers.AccountUserProfile.Hijo,
                stamps = 1,
                unlimited = false
            });
            Assert.IsTrue(response.status.Equals("success") || (response.status.Equals("error") && response.message.Contains("AU1001")), response.messageDetail);
        }
        [TestMethod]
        public void CreateUserAuthSuccess()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi, userDealer, passwordDealer);
            var response = infoUser.CreateUser(new AccountUserRequest()
            {
                email = $"hijo_{build.User}",
                password = "SwpassTest1!",
                name = "Pruebas User",
                rfc = "XAXX010101000",
                profileType = SW.Helpers.AccountUserProfile.Hijo,
                stamps = 1,
                unlimited = false
            });
            Assert.IsTrue(response.status.Equals("success") || (response.status.Equals("error") && response.message.Contains("AU1001")), response.messageDetail);
        }
        [TestMethod]
        public void CreateUserAuthError()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi, build.User, "fakepassword");
            var response = infoUser.CreateUser(new AccountUserRequest()
            {
                email = $"hijo_{build.User}",
                password = "SwpassTest1!",
                name = "Pruebas User",
                rfc = "XAXX010101000",
                profileType = SW.Helpers.AccountUserProfile.Hijo,
                stamps = 1,
                unlimited = false
            });
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        [TestMethod]
        public void CreateUserPasswordError()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, tokenDealer);
            var response = infoUser.CreateUser(new AccountUserRequest()
            {
                email = $"hijo_{build.User}",
                password = "SwpassTest",
                name = "Pruebas User",
                rfc = "XAXX010101000",
                profileType = SW.Helpers.AccountUserProfile.Hijo,
                stamps = 1,
                unlimited = false
            });
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }

        [TestMethod]
        public void CreateUserNoDealerError()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi, build.User, build.Password);
            var response = infoUser.CreateUser(new AccountUserRequest()
            {
                email = $"hijo_{build.User}",
                password = "SwpassTest1!",
                name = "Pruebas User",
                rfc = "XAXX010101000",
                profileType = SW.Helpers.AccountUserProfile.Hijo,
                stamps = 1,
                unlimited = false
            });
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        [TestMethod]
        public void UpdateUserSuccess()
        {
            Guid idUser = Guid.Parse("4ec7dbb9-8457-4c90-8eb3-91bfeeb1b24b");
            
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, "T2lYQ0t4L0RHVkR4dHZ5Nkk1VHNEakZ3Y0J4Nk9GODZuRyt4cE1wVm5tbXB3YVZxTHdOdHAwVXY2NTdJb1hkREtXTzE3dk9pMmdMdkFDR2xFWFVPUXpTUm9mTG1ySXdZbFNja3FRa0RlYURqbzdzdlI2UUx1WGJiKzViUWY2dnZGbFloUDJ6RjhFTGF4M1BySnJ4cHF0YjUvbmRyWWpjTkVLN3ppd3RxL0dJPQ.T2lYQ0t4L0RHVkR4dHZ5Nkk1VHNEakZ3Y0J4Nk9GODZuRyt4cE1wVm5tbFlVcU92YUJTZWlHU3pER1kySnlXRTF4alNUS0ZWcUlVS0NhelhqaXdnWTRncklVSWVvZlFZMWNyUjVxYUFxMWFxcStUL1IzdGpHRTJqdS9Zakw2UGRMTy9DVFlZNXArQnJxTUw5SGkwM0tXNDhDT0w5RXQ5V1VjRm9US24veVcybm12aDVZTUNWc0lzRi9BM0taS2laRUZaTmxUNXgrUDlRMUdVb3htZXZXMTlhRWM2TFJNNXZBOFFLbFdBbEhDTGYzK1k3ZFB0SWFId2J6ajQ2UVJCVjQzRXBHcTRWbnRsZkxyWlh4Y2c4WWVxWlovd3plWEw4eVJISVFlQ3ZzTnFLVFVHb1p4NEVSRXNLdFJENVFHWkpPclZFYURneGRNbUwycGQwaXNjY2dUdC9SeUdsSHdBQ3paNkNEUHFGWlJQKzJHeXVpSHI0WTF3Z2RVTjVJSmFEZ09EWFVic2lnUHVoVUp0RjFweE9VOEU2ZG5oekxad1MxMnN3eWFObGQ1aXVXRWZWcWc1RUdsMytHNWlYNTRxdFc4S294cGljNVQzbEpXL0tDUEd6ZHhURW9sOE5MVHJGZzZlazBUdWdkeFVhdXkrdTFmUDJubEE1OHBzcm1UMms.BecefhoDitho2V5ZiqhA171yAO46NoCvw6wx-pWBVR8");
            var response = infoUser.UpdateUser(idUser,"prueba update","prueba",false, true);
            Assert.IsTrue(response.status.Equals("success"), response.messageDetail);
        }

        [TestMethod]
        public void UpdateUserAuthSuccess()
        {
            Guid idUser = Guid.Parse("4ec7dbb9-8457-4c90-8eb3-91bfeeb1b24b");

            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi,userDealer, passwordDealer);
            var response = infoUser.UpdateUser(idUser, "prueba update", "prueba", false, true);
            Assert.IsTrue(response.status.Equals("success"), response.messageDetail);
        }
        [TestMethod]
        public void UpdateUserAuthError()
        {
            Guid idUser = Guid.Parse("4ec7dbb9-8957-4c90-8eb3-91bfeeb1b24b");

            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi, userDealer, passwordDealer);
            var response = infoUser.UpdateUser(idUser, "prueba update", "prueba", false, true);
            Assert.IsTrue(response.status.Equals("error"), response.messageDetail);
        }
        [TestMethod]
        public void UpdateUserError()
        {
            Guid idUser = Guid.Parse("4ec7dbb9-8957-4c90-8eb3-91bfeeb1b24b");

            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, tokenDealer);
            var response = infoUser.UpdateUser(idUser, "prueba update", "prueba", false, true);
            Assert.IsTrue(response.status.Equals("error"), response.messageDetail);
        }

        [Ignore]
        public void DeleteUserSuccess()
        {
            Guid idUser = Guid.Parse("fbed157d-1949-4351-8058-0a8ee0201d36");
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, build.Token);
            var response = infoUser.DeleteUser(idUser);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [Ignore]
        public void DeleteUserError()
        {
            Guid idUserFake = Guid.Parse("fbed157d-1949-4531-8058-0a8ee0209d36");
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, build.Token);
            var response = infoUser.DeleteUser(idUserFake);
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        [Ignore]
        public void DeleteUserAuthSuccess()
        {
            Guid idUser = Guid.Parse("fbed157d-1949-4351-8058-0a8ee0201d36");
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi, build.User, build.Password);
            var response = infoUser.DeleteUser(idUser);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [Ignore]
        public void DeleteUserAuthError()
        {
            Guid idUserFake = Guid.Parse("fbed157d-1949-4531-8058-0a8ee0209d36");
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, build.Token);
            var response = infoUser.DeleteUser(idUserFake);
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
    }
}
