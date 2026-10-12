using Microsoft.VisualStudio.TestTools.UnitTesting;
using SW.Services.Stamp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Test_SW.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SW.Entities;
using System.Net;
using System.Threading;

namespace Test_SW.Services.StampV4XML_Test
{
    [TestClass]
    public class StampV4XML_Test
    {
        [TestMethod]
        [Ignore] //Debido a intermitencia en el servicio de storage.
        public void Stamp_Test_StampV4XMLV4_SameCustomID_byTokenAsync()
        {
            string CustomId = Guid.NewGuid().ToString();
            var build = new BuildSettings();
            StampV4XML stamp = new StampV4XML(build.Url, build.UrlApi, build.Token);
            var xml = GetXml(build);
            var response = (StampResponseV2)stamp.TimbrarV2(xml, null, CustomId);
            Assert.IsTrue(response.data != null, "El resultado data viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");

            xml = GetXml(build);
            System.Threading.Thread.Sleep(5000);
            response = (StampResponseV2)stamp.TimbrarV2(xml, null, CustomId);
            Assert.IsTrue(response.status == "error" && response.message == "CFDI3307 - Timbre duplicado. El customId proporcionado está duplicado.");
        }
        [Ignore]//Problema cadena SW Tools
        [TestMethod]
        public void Stamp_Test_StampV4XMLV4_DifCustomID_byTokenAsync()
        {
            var build = new BuildSettings();
            StampV4XML stamp = new StampV4XML(build.Url, build.UrlApi, build.Token);
            var xml = GetXml(build);
            string CustomId = Guid.NewGuid().ToString();
            var response = (StampResponseV2)stamp.TimbrarV2(xml, null, CustomId);
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio.");
            CustomId = Guid.NewGuid().ToString();
            response = (StampResponseV2)stamp.TimbrarV2(xml, null, CustomId);
            Assert.IsTrue(response.status == "error" && response.message == "307. El comprobante contiene un timbre previo.");
        }
        [Ignore]//Problema cadena SW Tools
        [TestMethod]
        public void Stamp_Test_StampV4XMLV4_SameCustomID_byToken_NoExistURLXML()
        {
            string CustomId = Guid.NewGuid().ToString();
            var build = new BuildSettings();
            StampV4XML stamp = new StampV4XML(build.Url, build.UrlApi, build.Token);
            var xml = GetXml(build);
            var response = (StampResponseV2)stamp.TimbrarV2(xml, null, CustomId);
            Assert.IsTrue(response.data != null, "El resultado data viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");

            xml = GetXml(build);
            response = (StampResponseV2)stamp.TimbrarV2(xml, null, CustomId);
            Assert.IsTrue(response.status == "error" && response.message.StartsWith("CFDI3307") && response.messageDetail.Contains("No esta disponible el URL de descarga del XML"), response.message + " | " + response.messageDetail);
        }
        
        private string GetXml(BuildSettings build)
        {
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/cfdi40.xml"));
            xml = SignTools.SigXml(xml, Convert.FromBase64String(build.Pfx), build.PfxPassword);
            return xml;
        }

        // Pruebas locales (sin red): recuperacion del XML ante timbre duplicado.
        private const string Xml = "<cfdi:Comprobante xmlns:cfdi=\"http://www.sat.gob.mx/cfd/4\"/>";
        private const string PrimaryCfdi = "<cfdi:Comprobante xmlns:cfdi=\"http://www.sat.gob.mx/cfd/4\" Origen=\"SW\"/>";
        private const string RecoveredCfdi = "<cfdi:Comprobante xmlns:cfdi=\"http://www.sat.gob.mx/cfd/4\" Origen=\"Storage\"/>";
        private const string Uuid = "11111111-2222-3333-4444-555555555555";
        private const string DuplicateMessage = "CFDI3307 - Timbre duplicado. El customId proporcionado está duplicado.";
        private const string PrimaryDetail = "detalle SW original | diagnostico SW adicional";
        private const string RecoveryPrefix = "No fue posible recuperar el XML ya timbrado: ";
        private const string MissingUrl = "No esta disponible el URL de descarga del XML, intente más tarde";

        private enum RecoveryCase
        {
            Storage500,
            StorageSinRecords,
            UuidNoGuid,
            UrlApiNull,
            UrlApiVacia,
            UrlApiEspacios,
            Descarga403,
            Descarga400,
            DescargaVacia,
            TfdSinUuid,
            TfdMalformado,
            RecuperacionExitosa,
            DetalleSwNull,
            DetalleSwVacio
        }

        [TestMethod]
        [Timeout(60000)]
        public void TimbrarV2_DuplicadoCustomId_ConservaRespuestaDeSWYAnexaDiagnostico()
        {
            // Cada escenario (storage, descarga, UUID, urlApi) conserva el HTTP, mensaje, data y TFD de SW.
            foreach (RecoveryCase scenario in Enum.GetValues(typeof(RecoveryCase)))
            {
                try
                {
                    Verify(scenario);
                }
                catch (AssertFailedException ex)
                {
                    throw new AssertFailedException("Escenario " + scenario + ": " + ex.Message, ex);
                }
            }
        }

        private static void Verify(RecoveryCase scenario)
        {
            int expectedHits = 1;
            string uuid = Uuid;
            string primaryDetail = PrimaryDetail;
            string diagnostic;
            switch (scenario)
            {
                case RecoveryCase.Storage500:
                    expectedHits = 2;
                    diagnostic = MissingUrl + " | Storage: Storage no disponible | detalle Storage";
                    break;
                case RecoveryCase.StorageSinRecords:
                    expectedHits = 2;
                    diagnostic = MissingUrl;
                    break;
                case RecoveryCase.UuidNoGuid:
                    uuid = "uuid-no-guid";
                    diagnostic = "El UUID del TFD no es un Guid valido.";
                    break;
                case RecoveryCase.UrlApiNull:
                case RecoveryCase.UrlApiVacia:
                case RecoveryCase.UrlApiEspacios:
                    diagnostic = "urlApi no esta configurada para consultar Storage.";
                    break;
                case RecoveryCase.Descarga403:
                case RecoveryCase.Descarga400:
                    expectedHits = 3;
                    diagnostic = "No fue posible descargar el XML ya timbrado: No fue posible descargar el XML (HTTP "
                        + (scenario == RecoveryCase.Descarga403 ? "403" : "400") + ") | Enlace expirado";
                    break;
                case RecoveryCase.DescargaVacia:
                    expectedHits = 3;
                    diagnostic = "La descarga del XML esta vacia.";
                    break;
                case RecoveryCase.TfdSinUuid:
                case RecoveryCase.TfdMalformado:
                    uuid = null;
                    diagnostic = "No es posible obtener el UUID";
                    break;
                case RecoveryCase.RecuperacionExitosa:
                    expectedHits = 3;
                    diagnostic = null;
                    break;
                case RecoveryCase.DetalleSwNull:
                case RecoveryCase.DetalleSwVacio:
                    expectedHits = 2;
                    primaryDetail = scenario == RecoveryCase.DetalleSwNull ? null : "";
                    diagnostic = MissingUrl;
                    break;
                default:
                    throw new InvalidOperationException("Caso de prueba desconocido.");
            }
            string tfd = "<tfd:TimbreFiscalDigital xmlns:tfd=\"http://www.sat.gob.mx/TimbreFiscalDigital\" UUID=\"" + uuid + "\"/>";
            if (scenario == RecoveryCase.TfdSinUuid)
                tfd = "<tfd:TimbreFiscalDigital xmlns:tfd=\"http://www.sat.gob.mx/TimbreFiscalDigital\"/>";
            if (scenario == RecoveryCase.TfdMalformado)
                tfd = "<tfd:TimbreFiscalDigital";
            string primaryJson = JsonConvert.SerializeObject(new
            {
                status = "error",
                message = DuplicateMessage,
                messageDetail = primaryDetail,
                data = new { tfd = tfd, cfdi = PrimaryCfdi }
            });

            int hits;
            Response response = ServeMany(ctx =>
            {
                if (ctx.Request.HttpMethod == "POST")
                {
                    Assert.AreEqual("/v4/cfdi33/stamp/v2/", ctx.Request.Url.AbsolutePath);
                    Assert.AreEqual("custom-id-local", ctx.Request.Headers["customId"]);
                    Reply(ctx, (scenario == RecoveryCase.DetalleSwNull || scenario == RecoveryCase.DetalleSwVacio) ? 200 : 400, primaryJson);
                    return;
                }
                Assert.IsTrue(expectedHits > 1, "No debio intentarse la recuperacion.");
                Assert.AreEqual("GET", ctx.Request.HttpMethod);
                if (ctx.Request.Url.AbsolutePath == "/datawarehouse/v1/live/" + Uuid)
                {
                    if (scenario == RecoveryCase.Storage500)
                        Reply(ctx, 500, "{\"status\":\"error\",\"message\":\"Storage no disponible\",\"messageDetail\":\"detalle Storage\"}");
                    else if (expectedHits == 2)
                        Reply(ctx, 200, "{\"status\":\"success\",\"data\":{\"records\":[]}}");
                    else
                        Reply(ctx, 200, "{\"status\":\"success\",\"data\":{\"records\":[{\"urlXml\":\""
                            + ctx.Request.Url.GetLeftPart(UriPartial.Authority) + "/descarga/xml\"}]}}");
                    return;
                }
                Assert.AreEqual("/descarga/xml", ctx.Request.Url.AbsolutePath);
                Assert.AreEqual(3, expectedHits, "No debio intentarse la descarga.");
                if (scenario == RecoveryCase.RecuperacionExitosa)
                    Reply(ctx, 200, RecoveredCfdi, "text/xml");
                else if (scenario == RecoveryCase.DescargaVacia)
                    Reply(ctx, 200, "", "text/xml");
                else
                    Reply(ctx, scenario == RecoveryCase.Descarga403 ? 403 : 400, "Enlace expirado", "text/plain");
            }, prefix =>
            {
                string apiUrl = prefix;
                if (scenario == RecoveryCase.UrlApiNull)
                    apiUrl = null;
                else if (scenario == RecoveryCase.UrlApiVacia)
                    apiUrl = "";
                else if (scenario == RecoveryCase.UrlApiEspacios)
                    apiUrl = "   ";
                var stamp = new StampV4XML(prefix, apiUrl, "aaa.bbb.ccc");
                return (Response)stamp.TimbrarV2(Xml, null, "custom-id-local");
            }, out hits);

            Assert.AreEqual(expectedHits, hits, "Cantidad inesperada de peticiones; no debe reintentarse el timbrado.");
            Assert.IsNotNull(response, "El SDK devolvio null.");
            Assert.AreEqual("error", response.status);
            Assert.AreEqual(DuplicateMessage, response.message);
            string expectedDetail = diagnostic == null ? primaryDetail
                : (string.IsNullOrEmpty(primaryDetail) ? "" : primaryDetail + " | ") + RecoveryPrefix + diagnostic;
            Assert.AreEqual(expectedDetail, response.messageDetail);
            Assert.AreEqual((scenario == RecoveryCase.DetalleSwNull || scenario == RecoveryCase.DetalleSwVacio) ? 200 : 400, response.HttpStatusCode, "Debe conservarse el HTTP de la respuesta primaria.");
            Assert.IsFalse(response.HadEmptyBody);
            JObject data = JObject.FromObject(response)["data"] as JObject;
            Assert.IsNotNull(data, "Se perdieron los datos de SW.");
            Assert.AreEqual(tfd, (string)data["tfd"], "Se perdio o altero el TFD original.");
            Assert.AreEqual(scenario == RecoveryCase.RecuperacionExitosa ? RecoveredCfdi : PrimaryCfdi, (string)data["cfdi"]);
        }

        private delegate void ServerBehavior(HttpListenerContext context);

        // Atiende hasta cerrar el cliente para contar tambien las peticiones inesperadas.
        private static T ServeMany<T>(ServerBehavior behavior, Func<string, T> call, out int hits)
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
                    while (true)
                    {
                        HttpListenerContext ctx = listener.GetContext();
                        Interlocked.Increment(ref served);
                        try
                        {
                            behavior(ctx);
                        }
                        catch (Exception ex)
                        {
                            serverError = ex;
                            ctx.Response.Abort();
                            break;
                        }
                    }
                }
                catch (HttpListenerException)
                {
                    // El listener se cierra cuando termina la llamada al SDK.
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

        private static void Reply(HttpListenerContext ctx, int code, string body, string contentType = "application/json")
        {
            byte[] bytes = Encoding.UTF8.GetBytes(body);
            ctx.Request.InputStream.Close();
            ctx.Response.StatusCode = code;
            ctx.Response.ContentType = contentType + "; charset=utf-8";
            ctx.Response.ContentLength64 = bytes.Length;
            ctx.Response.OutputStream.Write(bytes, 0, bytes.Length);
            ctx.Response.OutputStream.Close();
        }
    }
}
