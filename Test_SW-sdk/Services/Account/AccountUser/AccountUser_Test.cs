using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using SW.Services.Account.AccountUser;
using SW.Helpers;
using Test_SW.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using SW.Entities;

namespace Test_SW.Services.AccountUser_Test
{
    [TestClass]
    public class AccountUser_Test
    {
        //GetUserByEmail
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
            AccountUser infoUser = new AccountUser(build.Url, build.UrlApi, build.User, build.Password);
            var response = infoUser.GetUserByEmail("emailfake@test.com");
            Assert.IsTrue(response.status == "success", response.messageDetail);
            Assert.IsTrue(!response.data.Any(), "El listado user no está vacío.");
        }
        [TestMethod]
        public void GetUserByEmailErrorV2()
        {
            var build = new BuildSettings();
            AccountUser infoUser = new AccountUser(build.UrlApi, build.Token);
            var response = infoUser.GetUserByEmail("emailfake@test.com");
            Assert.IsTrue(response.status == "success", response.messageDetail);
            Assert.IsTrue(!response.data.Any(), "El listado user no está vacío.");
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
        //GetAllUser
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
        //CreateUser
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
        public void CreateUserAuthSuccessV2()
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
        public void CreateUserErrorEmail()
        {
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.Url, build.UrlApi, build.User, build.Password);
            var response = user.CreateUser(new AccountUserRequest()
            {
                name = "Prueba UT Hijo dotnet",
                taxId = "XAXX010101000",
                email = "",
                stamps = 1,
                isUnlimited = false,
                password = $"_{build.Password}",
                notificationEmail = $"user_{build.User}",
                phone = "0000000000"
            });
            Assert.IsNotNull(response);
        }
        //UpdateUSer
        [TestMethod]
        public void UpdateUserSuccessV2()
        {
            string expectMessage = "No es posible actualizar, los datos enviados son identicos a los actuales";
            var build = new BuildSettings();
            Guid idUser = Guid.Parse("106f4d82-575e-436c-a923-8c85517f2ca9");
            AccountUser userUpdate = new AccountUser(build.UrlApi, build.Token);
            var responseUpdate = userUpdate.UpdateUser(idUser,"Nombre Actualizado", "AAAA000101010", null, "1234567890", false);
            Assert.IsTrue(responseUpdate.status.Equals("success") || (responseUpdate.status.Equals("error") && responseUpdate.message.Equals(expectMessage)));
        }

        [TestMethod]
        public void UpdateUserAuthSuccessV2()
        {
            string expectMessage = "No es posible actualizar, los datos enviados son identicos a los actuales";
            Guid idUser = Guid.Parse("2c6a91f6-2b14-4e61-b528-2becd26d6c33");
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.Url, build.UrlApi, build.User, build.Password);
            var responseUpdate = user.UpdateUser(idUser, "Nombre Actualizado", "AAAA000101010", null, "1234567890", false);
            Assert.IsTrue(responseUpdate.status.Equals("success") || (responseUpdate.status.Equals("error") && responseUpdate.message.Equals(expectMessage)));
        }
        [TestMethod]
        public void UpdateUserAuthErrorV2()
        {
            Guid idUser = Guid.Parse("106f4d82-575e-436c-a923-8c85517f2ca9");
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.Url, build.UrlApi, build.User, build.Password);
            var response = user.UpdateUser(idUser, "Nombre Actualizado", "AAAA000101010", null, "1234567890", false);
            Assert.IsTrue(response.status.Equals("error"), response.messageDetail);
        }
        [TestMethod]
        public void UpdateUserErrorV2()
        {
            Guid idUser = Guid.Parse("106f4d82-575e-436c-a923-8c85517f2ca9");
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.UrlApi, build.Token);
            var response = user.UpdateUser(idUser, "Nombre Actualizado", "AAAA000101010", null, "1234567890", false);
            Assert.IsTrue(response.status.Equals("error"), response.messageDetail);
        }
        //Delete
        [Ignore]
        public void DeleteUserSuccessV2()
        {
            Guid idUser = Guid.Parse("b451c159-5997-4e2d-b7bc-9a4c331c090c");
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.UrlApi, build.Token);
            var response = user.DeleteUser(idUser);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [Ignore]
        public void DeleteUserErrorV2()
        {
            Guid idUserFake = Guid.Parse("fbed157d-1949-4531-8058-0a8ee0209d36");
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.UrlApi, build.Token);
            var response = user.DeleteUser(idUserFake);
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }
        [Ignore]
        public void DeleteUserAuthSuccessV2()
        {
            Guid idUser = Guid.Parse("fbed157d-1949-4351-8058-0a8ee0201d36");
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.Url, build.UrlApi, build.User, build.Password);
            var response = user.DeleteUser(idUser);
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [Ignore]
        public void DeleteUserAuthErrorV2()
        {
            Guid idUserFake = Guid.Parse("fbed157d-1949-4531-8058-0a8ee0209d36");
            var build = new BuildSettings();
            AccountUser user = new AccountUser(build.UrlApi, build.Token);
            var response = user.DeleteUser(idUserFake);
            Assert.IsTrue(response.status == "error", response.messageDetail);
        }

        // Pruebas locales (sin red): respuesta de UpdateUser y DeleteUser.
        private const string Token = "aaa.bbb.ccc";
        private const string SwMessage = "Mensaje original SW";
        private const string SwDetail = "Detalle original SW";
        private static readonly Guid UserId = new Guid("11111111-1111-1111-1111-111111111111");

        [TestMethod]
        public void DeleteYUpdateUser_RespetaHttpYMensajesDeSW()
        {
            string swJson = JsonConvert.SerializeObject(new { status = "error", message = SwMessage, messageDetail = SwDetail });

            // 204 sin cuerpo es exito al eliminar.
            var response = Serve(ctx =>
            {
                ctx.Request.InputStream.Close();
                ctx.Response.StatusCode = 204;
                ctx.Response.Close();
            }, prefix => new AccountUser(prefix, Token).DeleteUser(UserId));
            AssertResponse(response, "success", "Usuario eliminado con exito", null);
            Assert.AreEqual(204, response.HttpStatusCode);
            Assert.IsTrue(response.HadEmptyBody);

            // 404 sin cuerpo: se conserva el mensaje historico.
            response = Serve(ctx => Reply(ctx, 404, ""), prefix => new AccountUser(prefix, Token).DeleteUser(UserId));
            AssertResponse(response, "error", "error al cancelar", "El usuario no se encuentra o aun tiene timbres");
            Assert.AreEqual(404, response.HttpStatusCode);

            // 404 con JSON de SW: se devuelve lo que dijo SW.
            response = Serve(ctx => Reply(ctx, 404, swJson), prefix => new AccountUser(prefix, Token).DeleteUser(UserId));
            AssertResponse(response, "error", SwMessage, SwDetail);
            Assert.AreEqual(404, response.HttpStatusCode);
            Assert.IsFalse(response.HadEmptyBody);

            // UpdateUser: 500 con JSON de SW, 500 sin cuerpo y status propio de SW en un 200.
            response = Serve(ctx => Reply(ctx, 500, swJson), prefix => new AccountUser(prefix, Token).UpdateUser(UserId, "Nombre"));
            AssertResponse(response, "error", SwMessage, SwDetail);
            Assert.AreEqual(500, response.HttpStatusCode);

            response = Serve(ctx => Reply(ctx, 500, ""), prefix => new AccountUser(prefix, Token).UpdateUser(UserId, "Nombre"));
            AssertResponse(response, "error", "Error al Actualizar", "Error al generar la petición.");
            Assert.AreEqual(500, response.HttpStatusCode);

            response = Serve(ctx => Reply(ctx, 200, swJson.Replace("\"error\"", "\"400\"")),
                prefix => new AccountUser(prefix, Token).UpdateUser(UserId, "Nombre"));
            AssertResponse(response, "400", SwMessage, SwDetail);
        }

        private static void AssertResponse(Response response, string status, string message, string detail)
        {
            Assert.IsNotNull(response);
            Assert.AreEqual(status, response.status);
            Assert.AreEqual(message, response.message);
            Assert.AreEqual(detail, response.messageDetail);
        }

        private delegate void ServerBehavior(HttpListenerContext context);

        private static T Serve<T>(ServerBehavior behavior, Func<string, T> call)
        {
            int hits;
            T response = ServeMany(2, behavior, call, out hits);
            Assert.AreEqual(1, hits);
            return response;
        }

        // Mantener el listener abierto hasta terminar la llamada permite contar tambien peticiones sobrantes.
        private static T ServeMany<T>(int maxRequests, ServerBehavior behavior, Func<string, T> call, out int hits)
        {
            HttpListener listener = null;
            string prefix = null;
            for (int attempt = 0; attempt < 5 && listener == null; attempt++)
            {
                prefix = "http://localhost:" + GetFreePort() + "/";
                HttpListener candidate = new HttpListener();
                candidate.Prefixes.Add(prefix);
                try
                {
                    candidate.Start();
                    listener = candidate;
                }
                catch (HttpListenerException)
                {
                    candidate.Close();
                }
            }
            Assert.IsNotNull(listener, "No fue posible reservar un puerto local.");
            int served = 0;
            Exception serverError = null;
            Thread server = new Thread(() =>
            {
                while (served < maxRequests)
                {
                    HttpListenerContext context;
                    try
                    {
                        context = listener.GetContext();
                    }
                    catch (HttpListenerException)
                    {
                        return;
                    }
                    catch (ObjectDisposedException)
                    {
                        return;
                    }
                    try
                    {
                        served++;
                        behavior(context);
                    }
                    catch (Exception ex)
                    {
                        serverError = ex;
                        context.Response.Abort();
                        return;
                    }
                }
            });
            server.IsBackground = true;
            server.Start();
            T result;
            bool stopped;
            try
            {
                result = call(prefix);
            }
            finally
            {
                listener.Close();
                stopped = server.Join(5000);
            }
            hits = served;
            Assert.IsTrue(stopped, "El servidor local no termino.");
            Assert.IsTrue(served >= 1, "La peticion nunca llego al servidor local.");
            Assert.IsNull(serverError, "El servidor local fallo: " + serverError);
            return result;
        }

        private static int GetFreePort()
        {
            var listener = new System.Net.Sockets.TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            int port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        private static void Reply(HttpListenerContext context, int code, string body)
        {
            context.Request.InputStream.Close();
            byte[] bytes = Encoding.UTF8.GetBytes(body);
            context.Response.StatusCode = code;
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.ContentLength64 = bytes.Length;
            context.Response.OutputStream.Write(bytes, 0, bytes.Length);
            context.Response.Close();
        }
    }
}
