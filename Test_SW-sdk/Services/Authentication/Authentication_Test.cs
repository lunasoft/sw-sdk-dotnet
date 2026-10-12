using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SW.Services.Authentication;
using Test_SW.Helpers;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using SW.Entities;
using SW.Helpers;
using SW.Services.Cancelation;
using SW.Services.Csd;
using SW.Services.Stamp;

namespace Test_SW.Services.Authentication_Test
{
    [TestClass]
    public class Authentication_Test
    {
        [TestMethod]
        public void ValidateAuthenticationV2()
        {
            var build = new BuildSettings();
            Authentication auth = new Authentication(build.Url, build.User, build.Password);
            var response = auth.GetToken();
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.token));
        }
        [TestMethod]
        public void ValidateExistUserV2()
        {
            var build = new BuildSettings();
            var resultExpect = "Falta Capturar Usuario";
            Authentication auth = new Authentication(build.Url, "", build.Password);
            var response = auth.GetToken();
            Assert.AreEqual(response.message, (string)resultExpect, (string)resultExpect);
        }
        [TestMethod]
        public void ValidateExistPasswordV2()
        {
            var build = new BuildSettings();
            var resultExpect = "Falta Capturar Contraseña";
            Authentication auth = new Authentication(build.Url, build.User, "");
            var response = auth.GetToken();
            Assert.AreEqual(response.message, (string)resultExpect, (string)resultExpect);
        }
        [TestMethod]
        public void ValidateExistUrlV2()
        {
            var build = new BuildSettings();
            var resultExpect = "Falta Capturar URL";
            Authentication auth = new Authentication("", build.User, build.Password);
            var response = auth.GetToken();
            Assert.AreEqual(response.message, (string)resultExpect, (string)resultExpect);
        }

        // Pruebas locales (sin red): autenticacion y errores durante el envio.
        private const string User = "usuario@example.invalid";
        private const string Password = "clave-local";
        private const string Token = "aaa.bbb.ccc";
        private const string Uuid = "11111111-2222-3333-4444-555555555555";
        private const string Rfc = "AAA010101AAA";
        private const string Xml = "<cfdi:Comprobante xmlns:cfdi=\"http://www.sat.gob.mx/cfd/4\"/>";
        private const string SwError = "{\"status\":\"error\",\"message\":\"Autenticacion rechazada por SW\",\"messageDetail\":\"Detalle original de SW\"}";
        private const string EmptyMessage = "El servicio respondio HTTP 200 con un formato que el SDK no reconoce. Verifique si la operacion fue realizada antes de reintentar.";

        [TestMethod]
        public void FalloDeAutenticacion_NoEnviaOperacionYDevuelveErrorDeSW()
        {
            Func<string, Response>[] services =
            {
                prefix => new Stamp(prefix, User, Password).TimbrarV4(Xml),
                prefix => new Cancelation(prefix, User, Password).CancelarByRfcUuid(Rfc, Uuid, "02"),
                prefix => new CsdUtils(prefix, User, Password).GetListCsd()
            };
            foreach (Func<string, Response> call in services)
            {
                // SW rechaza las credenciales: se devuelve su respuesta y la operacion nunca se envia.
                AssertBlocked(call, 401, SwError, "error", "Autenticacion rechazada por SW", "Detalle original de SW");

                // SW responde 200 pero sin token utilizable.
                foreach (string data in new[] { "", ",\"data\":null", ",\"data\":{}", ",\"data\":{\"token\":\"\"}" })
                {
                    AssertBlocked(call, 200, "{\"status\":\"success\",\"message\":\"Respuesta SW sin token\",\"messageDetail\":\"Detalle SW\"" + data + "}",
                        "error", "La autenticacion respondio sin un token utilizable.", "Respuesta SW sin token | Detalle SW");
                }

                // SW responde 200 con cuerpo vacio.
                Response empty = AssertBlocked(call, 200, "", "error", EmptyMessage, "El servicio respondio con un cuerpo vacio.");
                Assert.IsTrue(empty.HadEmptyBody);
            }
        }

        [TestMethod]
        public void ErrorDuranteEnvio_ConservaLoQueRespondioSW()
        {
            // Sin y con excepcion envolvente: el JSON de SW se conserva.
            AssertSendFailure(401, SwError, false, "error", "Autenticacion rechazada por SW", "Detalle original de SW");
            AssertSendFailure(401, SwError, true, "error", "Autenticacion rechazada por SW", "Detalle original de SW");
            AssertSendFailure(400, "{\"message\":\"Mensaje SW\",\"messageDetail\":\"Detalle SW\"}", false, "error", "Mensaje SW", "Detalle SW");
            // Sin JSON de SW: HTTP y cuerpo recibido como causa.
            AssertSendFailure(503, "Servicio temporalmente no disponible", true, "error", "HTTP 503 Service Unavailable", "Servicio temporalmente no disponible");
            AssertSendFailure(503, "", false, "error", "HTTP 503 Service Unavailable", "El servicio respondio con un cuerpo vacio.");
        }

        private static Response AssertBlocked(Func<string, Response> call, int code, string body, string status, string message, string detail)
        {
            int authHits = 0;
            int operationHits = 0;
            int hits;
            Response response = ServeMany(5, ctx =>
            {
                if (ctx.Request.Url.AbsolutePath == "/v2/security/authenticate")
                {
                    authHits++;
                    Reply(ctx, code, body);
                }
                else
                {
                    operationHits++;
                    Reply(ctx, 400, "{\"status\":\"error\",\"message\":\"Operacion indebidamente enviada\"}");
                }
            }, call, out hits);
            Assert.AreEqual(1, hits);
            Assert.AreEqual(1, authHits);
            Assert.AreEqual(0, operationHits, "No debe enviarse la operacion si falla la autenticacion.");
            AssertFields(response, status, message, detail);
            Assert.AreEqual(code, response.HttpStatusCode);
            return response;
        }

        private static void AssertSendFailure(int code, string body, bool wrapped, string status, string message, string detail)
        {
            int hits;
            Response response = ServeMany(1, ctx => Reply(ctx, code, body),
                prefix => new SendFailureCancelation(prefix, wrapped).CancelarByRfcUuid(Rfc, Uuid, "02"), out hits);
            Assert.AreEqual(1, hits);
            AssertFields(response, status, message, detail);
            Assert.AreEqual(code, response.HttpStatusCode);
            Assert.AreEqual(body.Length == 0, response.HadEmptyBody);
        }

        // Inyecta una WebException HTTP real en la construccion/envio de la peticion.
        private sealed class SendFailureCancelation : Cancelation
        {
            private readonly bool _wrapped;

            internal SendFailureCancelation(string url, bool wrapped) : base(url, Authentication_Test.Token)
            {
                _wrapped = wrapped;
            }

            internal override HttpWebRequest RequestCancelar(string rfc, string uuid, string motivo, string folioSustitucion)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + "rechazo-durante-envio");
                request.Proxy = null;
                request.Timeout = 5000;
                request.ReadWriteTimeout = 5000;
                try
                {
                    using (request.GetResponse())
                        Assert.Fail("El servidor debio rechazar la peticion.");
                }
                catch (WebException ex)
                {
                    Assert.IsInstanceOfType(ex.Response, typeof(HttpWebResponse));
                    if (_wrapped)
                        throw new IOException("Fallo al enviar", new ServicesException("Envoltura", ex));
                    throw;
                }
                return request;
            }
        }

        private static void AssertFields(Response response, string status, string message, string detail)
        {
            Assert.IsNotNull(response);
            Assert.AreEqual(status, response.status);
            Assert.AreEqual(message, response.message);
            Assert.AreEqual(detail, response.messageDetail);
        }

        private delegate void ServerBehavior(HttpListenerContext context);

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
                try
                {
                    while (served < maxRequests)
                    {
                        HttpListenerContext ctx;
                        try
                        {
                            ctx = listener.GetContext();
                        }
                        catch (HttpListenerException)
                        {
                            break;
                        }
                        catch (ObjectDisposedException)
                        {
                            break;
                        }
                        served++;
                        try
                        {
                            behavior(ctx);
                        }
                        finally
                        {
                            ctx.Response.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    serverError = ex;
                }
            });
            server.IsBackground = true;
            server.Start();
            T result;
            try
            {
                result = call(prefix);
            }
            finally
            {
                listener.Close();
                Assert.IsTrue(server.Join(5000), "El servidor local no termino.");
            }
            hits = served;
            Assert.IsTrue(served >= 1, "La peticion nunca llego al servidor local.");
            Assert.IsNull(serverError, "El servidor local fallo: " + serverError);
            return result;
        }

        private static int GetFreePort()
        {
            System.Net.Sockets.TcpListener listener = new System.Net.Sockets.TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            int port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        private static void Reply(HttpListenerContext ctx, int code, string body)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(body);
            ctx.Request.InputStream.Close();
            ctx.Response.StatusCode = code;
            ctx.Response.ContentType = "application/json; charset=utf-8";
            ctx.Response.ContentLength64 = bytes.Length;
            ctx.Response.OutputStream.Write(bytes, 0, bytes.Length);
            ctx.Response.OutputStream.Close();
        }
    }
}
