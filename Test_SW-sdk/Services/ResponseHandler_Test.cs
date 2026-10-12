using Microsoft.VisualStudio.TestTools.UnitTesting;
using SW.Entities;
using SW.Helpers;
using SW.Services.Stamp;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Test_SW.Services.ResponseHandler_Test
{
    /// <summary>
    /// Valida que el SDK siempre devuelva lo que SW responde, sin sustituirlo por
    /// StatusDescription ("OK"). Usa un servidor local que simula respuestas de SW.
    /// </summary>
    [TestClass]
    public class ResponseHandler_Test
    {
        private const string Xml = "<cfdi:Comprobante xmlns:cfdi=\"http://www.sat.gob.mx/cfd/4\"/>";
        private const string EmptyBodyDetail = "El servicio respondio con un cuerpo vacio.";
        private const string IncompleteHttp200Message =
            "El servicio respondio HTTP 200 pero no fue posible leer la respuesta completa. Verifique si la operacion fue realizada antes de reintentar.";

        private static StampResponseV4 Send(string prefix)
        {
            var request = (HttpWebRequest)WebRequest.Create(RequestHelper.NormalizeBaseUrl(prefix) + "respuesta");
            request.Timeout = 5000;
            request.ReadWriteTimeout = 5000;
            request.Proxy = null;
            RequestHelper.SetupProxy(null, 0, ref request);
            return new StampResponseHandlerV4().GetResponse(request);
        }

        [TestMethod]
        public void Http2xxConJsonDeSW_ConservaStatusMessageYData()
        {
            const string full = "{\"status\":\"success\",\"message\":\"Mensaje SW\",\"messageDetail\":\"Detalle SW\","
                + "\"data\":{\"uuid\":\"local-1\",\"cfdi\":\"<x/>\",\"fechaTimbrado\":\"2026-01-02T03:04:05.1230000-06:00\"}}";
            var response = Run(200, full);
            AssertResponse("completo", response, 200, "success", "Mensaje SW", "Detalle SW", false);
            Assert.AreEqual("local-1", response.data.uuid);
            Assert.AreEqual("<x/>", response.data.cfdi);
            Assert.AreEqual("2026-01-02T03:04:05.1230000-06:00", response.data.fechaTimbrado);

            // Una propiedad conocida basta aunque SW no envie status ni mensajes.
            response = Run(200, "{\"data\":{\"uuid\":\"local-2\"}}");
            AssertResponse("solo data", response, 200, null, null, null, false);
            Assert.AreEqual("local-2", response.data.uuid);
        }

        [TestMethod]
        public void Http4xxYHttp5xxConJsonDeSW_ConservaLoQueDijoSW()
        {
            var response = Run(500, "{\"message\":\"Error interno SW\",\"messageDetail\":\"detalle SW\",\"status\":\"error\"}");
            AssertResponse("500", response, 500, "error", "Error interno SW", "detalle SW", false);

            // Sin messageDetail de SW, el cuerpo recibido queda como detalle.
            const string notFound = "{\"message\":\"Recurso no encontrado SW\",\"status\":\"error\"}";
            response = Run(404, notFound);
            AssertResponse("404", response, 404, "error", "Recurso no encontrado SW", notFound, false);

            // Cuerpos de error mayores a 64 KB no se pierden.
            string detail = new string('x', 70000);
            response = Run(500, "{\"status\":\"error\",\"messageDetail\":\"" + detail + "\",\"message\":\"Mensaje SW al final\"}");
            AssertResponse("mayor a 64 KB", response, 500, "error", "Mensaje SW al final", detail, false);

            // Una redireccion no se sigue: se devuelve lo que SW dijo en el 302.
            int hits;
            response = ServeMany(2, ctx =>
            {
                if (ctx.Request.Url.AbsolutePath == "/respuesta")
                {
                    ctx.Response.RedirectLocation = "/destino";
                    Reply(ctx, 302, "{\"status\":\"error\",\"message\":\"Redireccion SW\",\"messageDetail\":\"Detalle SW\"}");
                }
                else
                    Reply(ctx, 200, "{\"status\":\"success\",\"message\":\"Se siguio la redireccion\"}");
            }, Send, out hits);
            Assert.AreEqual(1, hits, "El SDK siguio la redireccion.");
            AssertResponse("302", response, 302, "error", "Redireccion SW", "Detalle SW", false);
        }

        [TestMethod]
        [TestCategory("RawTcp")]
        public void SinJsonDeSW_DevuelveErrorConCausaYCuerpoEnMessageDetail()
        {
            var response = Run(502, "<html>Bad gateway proxy</html>", "text/html");
            AssertResponse("502 html", response, 502, "error", "HTTP 502 Bad Gateway", "<html>Bad gateway proxy</html>", false);

            response = Run(500, "Servicio caido", "text/plain");
            AssertResponse("500 texto", response, 500, "error", "HTTP 500 Internal Server Error", "Servicio caido", false);

            response = Run(500, "");
            AssertResponse("500 vacio", response, 500, "error", "HTTP 500 Internal Server Error", EmptyBodyDetail, true);

            response = Run(500, "{\"error\":\"texto\"}");
            AssertResponse("500 JSON sin campos conocidos", response, 500, "error", "HTTP 500 Internal Server Error", "{\"error\":\"texto\"}", false);

            response = Run(500, "{\"status\":\"success\",");
            AssertResponse("500 JSON truncado", response, 500, "error", "HTTP 500 Internal Server Error", "{\"status\":\"success\",", false);

            // data conocida pero sin status: el SDK marca error y conserva data y cuerpo.
            const string onlyData = "{\"data\":{\"uuid\":\"local-2\"}}";
            response = Run(500, onlyData);
            AssertResponse("500 solo data", response, 500, "error", "HTTP 500 Internal Server Error", onlyData, false);
            Assert.AreEqual("local-2", response.data.uuid);

            // Conexion cortada a mitad del cuerpo de un 200: se informa la causa y el cuerpo parcial.
            const string part = "{\"data\":{\"cfdi\":\"<cfdi:Compro";
            response = ServeRaw("HTTP/1.1 200 OK\r\nContent-Type: application/json\r\n"
                + "Content-Length: 5000\r\nConnection: close\r\n\r\n" + part,
                prefix => new StampV4(prefix, "aaa.bbb.ccc").TimbrarV4(Xml));
            Assert.AreEqual(200, response.HttpStatusCode);
            Assert.AreEqual("error", response.status);
            Assert.AreEqual(IncompleteHttp200Message, response.message);
            StringAssert.Contains(response.messageDetail, part);
            Assert.AreNotEqual("OK", response.messageDetail);
            Assert.IsFalse(response.HadEmptyBody);
        }

        [TestMethod]
        public void Http2xxNoInterpretable_AvisaQueSeVerifiqueSiLaOperacionFueRealizada()
        {
            string deep = "{\"status\":\"success\",\"extra\":" + new string('[', 129) + "0" + new string(']', 129) + "}";
            var cases = new[]
            {
                new[] { "cuerpo vacio", "", "application/json" },
                new[] { "html", "<html>Gateway page</html>", "text/html" },
                new[] { "objeto vacio", "{}", "application/json" },
                new[] { "campos desconocidos", "{\"data\":{\"error\":\"Rechazo SW\"}}", "application/json" },
                new[] { "tipos incompatibles", "{\"status\":{},\"message\":[],\"messageDetail\":{}}", "application/json" },
                new[] { "JSON truncado", "{\"status\":\"success\",", "application/json" },
                new[] { "contenido adicional", "{\"status\":\"success\"} {}", "application/json" },
                new[] { "arreglo", "[]", "application/json" },
                new[] { "JSON muy profundo", deep, "application/json" }
            };
            foreach (string[] c in cases)
            {
                var response = Run(200, c[1], c[2]);
                AssertResponse(c[0], response, 200, "error", UnrecognizedMessage(200), c[1].Length == 0 ? EmptyBodyDetail : c[1], c[1].Length == 0);
                Assert.IsNull(response.data, c[0]);
                StringAssert.Contains(response.message, "Verifique si la operacion fue realizada", c[0]);
            }

            // 204 sin cuerpo fuera de AccountUser tampoco se reporta como exito.
            var noContent = Serve(ctx =>
            {
                ctx.Request.InputStream.Close();
                ctx.Response.StatusCode = 204;
                ctx.Response.Close();
            }, prefix => new StampV4(prefix, "aaa.bbb.ccc").TimbrarV4(Xml));
            AssertResponse("204", noContent, 204, "error", UnrecognizedMessage(204), EmptyBodyDetail, true);
            Assert.IsNull(noContent.data);

            // data incompatible: se conservan los campos de SW y se anexa el diagnostico.
            const string badData = "{\"status\":\"success\",\"message\":\"Mensaje SW\",\"messageDetail\":\"Detalle SW\",\"data\":[]}";
            var partial = Run(200, badData);
            AssertResponse("data incompatible", partial, 200, "success", "Mensaje SW",
                "Detalle SW | Campos de la respuesta no interpretados: data | Respuesta recibida: " + badData, false);
            Assert.IsNull(partial.data);
        }

        [TestMethod]
        public void RespuestaConCharsetOGzip_ConservaLoQueRespondeSW()
        {
            // UTF-8 sin charset declarado: se conservan los acentos.
            var response = Run(500, "{\"message\":\"Año canción\",\"messageDetail\":\"d\",\"status\":\"error\"}", "text/json");
            AssertResponse("utf8 sin charset", response, 500, "error", "Año canción", "d", false);

            // Respuesta comprimida con gzip.
            response = Serve(ctx => ReplyGzip(ctx, 400,
                "{\"status\":\"error\",\"message\":\"Mensaje SW\",\"messageDetail\":\"Detalle SW\"}"), Send);
            AssertResponse("gzip", response, 400, "error", "Mensaje SW", "Detalle SW", false);
        }

        private static string UnrecognizedMessage(int code)
        {
            return "El servicio respondio HTTP " + code
                + " con un formato que el SDK no reconoce. Verifique si la operacion fue realizada antes de reintentar.";
        }

        private static void AssertResponse(string scenario, StampResponseV4 response, int code, string status, string message, string detail, bool emptyBody)
        {
            Assert.IsNotNull(response, scenario + ": el SDK devolvio null.");
            Assert.AreEqual(code, response.HttpStatusCode, scenario + ": HttpStatusCode");
            Assert.AreEqual(status, response.status, scenario + ": status");
            Assert.AreEqual(message, response.message, scenario + ": message");
            Assert.AreEqual(detail, response.messageDetail, scenario + ": messageDetail");
            Assert.AreEqual(emptyBody, response.HadEmptyBody, scenario + ": HadEmptyBody");
        }

        private delegate void ServerBehavior(HttpListenerContext context);

        private static StampResponseV4 Run(int code, string body, string contentType = "application/json; charset=utf-8")
        {
            return Serve(ctx => Reply(ctx, code, body, contentType), Send);
        }

        private static T Serve<T>(ServerBehavior behavior, Func<string, T> call)
        {
            int hits;
            T result = ServeMany(1, behavior, call, out hits);
            Assert.AreEqual(1, hits, "El servidor local debe recibir exactamente una peticion.");
            return result;
        }

        /// <summary>
        /// Levanta un servidor local que atiende hasta maxRequests peticiones con el mismo comportamiento.
        /// Verifica que la peticion llego, propaga errores del servidor y espera su cierre.
        /// </summary>
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
            int stopping = 0;
            Thread server = new Thread(() =>
            {
                try
                {
                    while (served < maxRequests)
                    {
                        HttpListenerContext ctx = listener.GetContext();
                        served++;
                        behavior(ctx);
                    }
                }
                catch (HttpListenerException ex)
                {
                    // El listener se cierra al terminar la prueba; antes de eso es un fallo real.
                    if (Thread.VolatileRead(ref stopping) == 0)
                        serverError = ex;
                }
                catch (ObjectDisposedException)
                {
                }
                catch (Exception ex)
                {
                    serverError = ex;
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
                Interlocked.Exchange(ref stopping, 1);
                listener.Close();
                stopped = server.Join(5000);
            }
            Assert.IsTrue(stopped, "El servidor local no termino.");
            hits = served;
            Assert.IsTrue(served >= 1, "La peticion nunca llego al servidor local.");
            Assert.IsNull(serverError, "El servidor local fallo: " + serverError);
            return result;
        }

        /// <summary>
        /// Atiende una sola conexion y cierra con FIN despues de leer toda la peticion.
        /// La respuesta se envia literalmente para simular un cuerpo mas corto que Content-Length.
        /// </summary>
        private static T ServeRaw<T>(string rawResponse, Func<string, T> call)
        {
            TcpListener listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            string prefix = "http://127.0.0.1:" + ((IPEndPoint)listener.LocalEndpoint).Port + "/";
            int hits = 0;
            Exception serverError = null;
            Thread server = new Thread(() =>
            {
                try
                {
                    using (TcpClient client = listener.AcceptTcpClient())
                    {
                        // No se aceptan conexiones adicionales ni reintentos.
                        listener.Stop();
                        client.ReceiveTimeout = 5000;
                        client.SendTimeout = 5000;
                        NetworkStream stream = client.GetStream();
                        ReadRequest(stream);
                        hits++;
                        byte[] bytes = Encoding.UTF8.GetBytes(rawResponse);
                        stream.Write(bytes, 0, bytes.Length);
                        client.Close();
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
                listener.Stop();
                server.Join(5000);
                Assert.IsFalse(server.IsAlive, "El servidor TCP no termino despues de Join(5000).");
            }
            Assert.IsNull(serverError, "El servidor TCP fallo: " + serverError);
            Assert.AreEqual(1, hits, "El servidor TCP debe recibir una peticion completa.");
            return result;
        }

        private static void ReadRequest(NetworkStream stream)
        {
            StringBuilder headers = new StringBuilder();
            while (true)
            {
                int value = stream.ReadByte();
                Assert.AreNotEqual(-1, value, "La conexion termino antes de recibir los encabezados.");
                headers.Append((char)value);
                int length = headers.Length;
                Assert.IsTrue(length <= 65536, "Los encabezados exceden el limite de la prueba.");
                if (length >= 4 && headers[length - 4] == '\r' && headers[length - 3] == '\n'
                    && headers[length - 2] == '\r' && headers[length - 1] == '\n')
                    break;
            }

            int contentLength = 0;
            bool expectContinue = false;
            string[] lines = headers.ToString().Split(new[] { "\r\n" }, StringSplitOptions.None);
            for (int index = 1; index < lines.Length; index++)
            {
                int separator = lines[index].IndexOf(':');
                if (separator < 0)
                    continue;
                string name = lines[index].Substring(0, separator).Trim();
                string value = lines[index].Substring(separator + 1).Trim();
                Assert.IsFalse(string.Equals(name, "Transfer-Encoding", StringComparison.OrdinalIgnoreCase),
                    "El servidor TCP de prueba requiere Content-Length, no Transfer-Encoding.");
                if (string.Equals(name, "Content-Length", StringComparison.OrdinalIgnoreCase))
                {
                    Assert.IsTrue(int.TryParse(value, out contentLength) && contentLength >= 0,
                        "Content-Length de la peticion no es valido.");
                }
                if (string.Equals(name, "Expect", StringComparison.OrdinalIgnoreCase))
                    expectContinue = string.Equals(value, "100-continue", StringComparison.OrdinalIgnoreCase);
            }
            if (expectContinue)
            {
                byte[] interim = Encoding.ASCII.GetBytes("HTTP/1.1 100 Continue\r\n\r\n");
                stream.Write(interim, 0, interim.Length);
            }

            byte[] buffer = new byte[4096];
            while (contentLength > 0)
            {
                int read = stream.Read(buffer, 0, Math.Min(buffer.Length, contentLength));
                Assert.IsTrue(read > 0, "La conexion termino antes de recibir todo el cuerpo de la peticion.");
                contentLength -= read;
            }
        }

        private static int GetFreePort()
        {
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            return port;
        }

        private static void Reply(HttpListenerContext ctx, int code, string body, string contentType = "application/json; charset=utf-8")
        {
            byte[] bytes = Encoding.UTF8.GetBytes(body);
            ctx.Request.InputStream.Close();
            ctx.Response.StatusCode = code;
            ctx.Response.ContentType = contentType;
            ctx.Response.ContentLength64 = bytes.Length;
            ctx.Response.OutputStream.Write(bytes, 0, bytes.Length);
            ctx.Response.Close();
        }

        private static void ReplyGzip(HttpListenerContext context, int code, string body)
        {
            byte[] raw = Encoding.UTF8.GetBytes(body);
            byte[] bytes;
            using (var ms = new System.IO.MemoryStream())
            {
                using (var gz = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Compress))
                    gz.Write(raw, 0, raw.Length);
                bytes = ms.ToArray();
            }
            context.Request.InputStream.Close();
            context.Response.StatusCode = code;
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.AddHeader("Content-Encoding", "gzip");
            context.Response.ContentLength64 = bytes.Length;
            context.Response.OutputStream.Write(bytes, 0, bytes.Length);
            context.Response.Close();
        }
    }
}
