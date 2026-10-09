using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Stamp;
using Test_SW.Helpers;
using System.Net;
using System.Xml;
using System.Collections.Concurrent;
using System.Threading;
using Newtonsoft.Json;
using SW.Entities;

namespace Test_SW.Services.Stamp_Test
{
    [TestClass]
    public class Stamp_Test_45
    {
        [TestMethod]
        public void Stamp_Test_45_StampXMLV1()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV1)stamp.TimbrarV1(xml);
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampXMLV1byToken()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = GetXml(build);
            var response = (StampResponseV1)stamp.TimbrarV1(xml);
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampXMLV1Base64()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            xml = Convert.ToBase64String(Encoding.UTF8.GetBytes(xml));
            var response = (StampResponseV1)stamp.TimbrarV1(xml, true);
            Assert.IsTrue(response.status == "success"
               && !string.IsNullOrEmpty(response.data.tfd), response.message);
        }
        [TestMethod]
        public void Stamp_Test_45_StampXMLV1Base64byToken()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = GetXml(build);
            xml = Convert.ToBase64String(Encoding.UTF8.GetBytes(xml));
            var response = (StampResponseV1)stamp.TimbrarV1(xml, true);
            Assert.IsTrue(response.status == "success"
              && !string.IsNullOrEmpty(response.data.tfd), response.message);
        }
        [TestMethod]
        public void Stamp_Test_45_StampXMLV2()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV2)stamp.TimbrarV2(xml);
            Assert.IsTrue(response.status == "success"
               && !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampXMLV2byToken()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = GetXml(build);
            var response = (StampResponseV2)stamp.TimbrarV2(xml);
            Assert.IsTrue(response.status == "success"
               && !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampXMLV2Base64()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            xml = Convert.ToBase64String(Encoding.UTF8.GetBytes(xml));
            var response = (StampResponseV2)stamp.TimbrarV2(xml, true);
            Assert.IsTrue(response.status == "success"
               && !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampXMLV2Base64byToken()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = GetXml(build);
            xml = Convert.ToBase64String(Encoding.UTF8.GetBytes(xml));
            var response = (StampResponseV2)stamp.TimbrarV2(xml, true);
            Assert.IsTrue(response.status == "success"
              && !string.IsNullOrEmpty(response.data.cfdi), response.message);
        }
        [TestMethod]
        public void Stamp_Test_45_StampXMLV3byToken()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = GetXml(build);
            var response = (StampResponseV3)stamp.TimbrarV3(xml);
            Assert.IsTrue(response.status == "success"
               && !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampXMLV3Base64byToken()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = GetXml(build);
            xml = Convert.ToBase64String(Encoding.UTF8.GetBytes(xml));
            var response = (StampResponseV3)stamp.TimbrarV3(xml, true);
            Assert.IsTrue(response.status == "success"
               && !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
        }
       
        [TestMethod]
        public void Stamp_Test_45_StampXMLV4byToken()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = GetXml(build);
            var response = (StampResponseV4)stamp.TimbrarV4(xml);
            Assert.IsTrue(response.data != null, "El resultado data viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cadenaOriginalSAT), "El resultado data.cadenaOriginalSAT viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.noCertificadoSAT), "El resultado data.noCertificadoSAT viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.noCertificadoCFDI), "El resultado data.noCertificadoCFDI viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.uuid), "El resultado data.uuid viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.selloSAT), "El resultado data.selloSAT viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.selloCFDI), "El resultado data.selloCFDI viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.fechaTimbrado), "El resultado data.fechaTimbrado viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.qrCode), "El resultado data.qrCode viene vacio.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampXMLV4Base64byToken()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = GetXml(build);
            xml = Convert.ToBase64String(Encoding.UTF8.GetBytes(xml));
            var response = (StampResponseV4)stamp.TimbrarV4(xml, true);
            Assert.IsTrue(response.data != null, "El resultado data viene vacio." + response.message);
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cadenaOriginalSAT), "El resultado data.cadenaOriginalSAT viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.noCertificadoSAT), "El resultado data.noCertificadoSAT viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.noCertificadoCFDI), "El resultado data.noCertificadoCFDI viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.uuid), "El resultado data.uuid viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.selloSAT), "El resultado data.selloSAT viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.selloCFDI), "El resultado data.selloCFDI viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.fechaTimbrado), "El resultado data.fechaTimbrado viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.qrCode), "El resultado data.qrCode viene vacio.");
        }
        [TestMethod]
        public void Stamp_Test_45_MassStampXMLV4()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.User, build.Password);
            List<string> xmls = new List<string>();
            for (int i = 0; i < 50; i++)
            {
                xmls.Add(GetXml(build));
            }
            var mass_response = stamp.TimbrarV4(xmls.ToArray());
            foreach (var dic in mass_response)
            {
                Assert.IsTrue(dic.Key != null, "El resultado data viene vacio." + dic.Value.message);
                Assert.IsTrue(!string.IsNullOrEmpty(dic.Value.data.cfdi), "El resultado data.cfdi viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(dic.Value.data.cadenaOriginalSAT), "El resultado data.cadenaOriginalSAT viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(dic.Value.data.noCertificadoSAT), "El resultado data.noCertificadoSAT viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(dic.Value.data.noCertificadoCFDI), "El resultado data.noCertificadoCFDI viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(dic.Value.data.uuid), "El resultado data.uuid viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(dic.Value.data.selloSAT), "El resultado data.selloSAT viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(dic.Value.data.selloCFDI), "El resultado data.selloCFDI viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(dic.Value.data.fechaTimbrado), "El resultado data.fechaTimbrado viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(dic.Value.data.qrCode), "El resultado data.qrCode viene vacio.");
            }
        }

        [TestMethod]
        public void Stamp_Test_45_StampXmlV3byToken()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = GetXml(build);
            var response = (StampResponseV3)stamp.TimbrarXmlV3(xml);
            Assert.IsTrue(response.status == "success"
               && !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampXmlV3Base64byToken()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = GetXml(build);
            xml = Convert.ToBase64String(Encoding.UTF8.GetBytes(xml));
            var response = (StampResponseV3)stamp.TimbrarXmlV3(xml, true);
            Assert.IsTrue(response.status == "success"
               && !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampXmlV3_ValidateEmptyXML()
        {
            var resultExpect = "Xml CFDI33 no proporcionado o viene vacio.";
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = File.ReadAllText("Resources/EmptyXML.xml");
            var response = stamp.TimbrarXmlV3(xml);
            Assert.AreEqual(response.message, (string)resultExpect, (string)resultExpect);
        }
        [TestMethod]
        public void Stamp_Test_45_StampXmlV3_ValidateExistToken()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, "");
            var xml = File.ReadAllText("Resources/cfdi40.xml");
            var response = stamp.TimbrarXmlV3(xml);
            Assert.IsTrue(response.message.Contains("El token debe contener 3 partes"));
        }
        [TestMethod]
        public void Stamp_Test_45_ValidateServerError()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url + "/ot", build.Token);
            var xml = File.ReadAllText("Resources/Cfdi40.xml");
            var response = stamp.TimbrarV1(xml);
            Assert.AreEqual(404, response.HttpStatusCode, "Debe recibirse el HTTP 404 del servidor, no un fallo de red.");
            Assert.AreEqual("error", response.status);
            Assert.IsFalse(string.IsNullOrEmpty(response.message), "El SDK debe devolver lo que respondio el servidor.");
            Assert.AreNotEqual("OK", response.messageDetail);
        }
        [TestMethod]
        public void Stamp_Test_45_ValidateFormatToken()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token + ".");
            var xml = File.ReadAllText("Resources/cfdi40.xml");
            var response = stamp.TimbrarV1(xml);
            Assert.IsTrue(response.message.Contains("El token debe contener 3 partes"));
        }
        [TestMethod]
        public void Stamp_Test_45_ValidateExistToken()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, "");
            var xml = File.ReadAllText("Resources/cfdi40.xml");
            var response = stamp.TimbrarV1(xml);
            Assert.IsTrue(response.message.Contains("El token debe contener 3 partes"));
        }
        [TestMethod]
        public void Stamp_Test_45_ValidateEmptyXML()
        {
            var resultExpect = "Xml CFDI33 no proporcionado o viene vacio.";
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = File.ReadAllText("Resources/EmptyXML.xml");
            var response = stamp.TimbrarV1(xml);
            Assert.AreEqual(response.message, (string)resultExpect, (string)resultExpect);
        }
        [TestMethod]
        public void Stamp_Test_45_ValidateSpecialCharactersFromXML()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = File.ReadAllText("Resources/SpecialCharacters40.xml");
            xml = SignTools.SigXml(xml, Convert.FromBase64String(build.Pfx), build.PfxPassword);
            var response = stamp.TimbrarV1(xml);
            Assert.IsTrue(response.status == "success", "Result not expected. Error: " + response.message);
            Assert.IsFalse(string.IsNullOrEmpty(response.data.tfd), "Result not expected. Error: " + response.message);
        }
        [TestMethod]
        public void Stamp_Test_45_ValidateIsUTF8FromXML()
        {
            var resultExpect = "301";
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/cfdi40_ansi.xml"));            
            var response = stamp.TimbrarV1(xml);
            Assert.IsTrue(response.message.Contains(resultExpect), "Result not expected. Error: " + response.message);
        }
        [TestMethod]
        public void Stamp_Test_45_MultipleStampXMLV1byToken()
        {
            var build = new BuildSettings();
            var resultExpect = false;
            int iterations = 10;
            Stamp stamp = new Stamp(build.Url, build.Token);
            List<StampResponseV1> listXmlResult = new List<StampResponseV1>();
            for (int i = 0; i < iterations; i++)
            {
                string xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/cfdi40.xml"));
                xml = SignTools.SigXml(xml, Convert.FromBase64String(build.Pfx), build.PfxPassword);
                var response = (StampResponseV1)stamp.TimbrarV1(xml);
                listXmlResult.Add(response);
            }
            if (listXmlResult != null)
                resultExpect = listXmlResult.FindAll(w => w.status == ResponseType.success.ToString() || w.message.Contains("72 horas")).Count == iterations;

            Assert.IsTrue((bool)resultExpect);
        }
        [TestMethod]
        public void Stamp_Test_45_ValidateResponseWhenServerUnreachable()
        {
            Stamp stamp = new Stamp("http://unreachable.invalid", "T2lYQ0t4L0RH.....");
            var xml = File.ReadAllText("Resources/cfdi40.xml");
            var response = stamp.TimbrarV1(xml);
            Assert.IsNotNull(response, "Response should not be null.");
            Assert.AreEqual("error", response.status, "Status should be 'error'.");
            Assert.IsFalse(string.IsNullOrEmpty(response.message), "Message should not be empty.");
        }
        [TestMethod]
        public void Stamp_Test_45_ValidateResponseHandlerWhenNullWebResponse()
        {
            var handler = new StampResponseHandlerV1();
            var request = (HttpWebRequest)WebRequest.Create("http://192.0.2.1/api/test");
            request.Timeout = 3000;
            var response = handler.GetResponseRequest(request);
            Assert.IsNotNull(response, "Response should not be null.");
            Assert.AreEqual("error", response.status, "Status should be 'error'.");
            Assert.IsFalse(string.IsNullOrEmpty(response.message), "Message should not be empty.");
            Assert.IsFalse(string.IsNullOrEmpty(response.messageDetail), "MessageDetail should not be empty.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampXMLV1CadenaOriginalSAT()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = stamp.TimbrarV1CadenaOriginalSAT(xml);
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cadenaOriginalSAT)
                && response.data.cadenaOriginalSAT.StartsWith("||1.1|"), "El resultado data.cadenaOriginalSAT no es valido.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampXMLV1CadenaOriginalSATbyToken()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = GetXml(build);
            var response = stamp.TimbrarV1CadenaOriginalSAT(xml);
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cadenaOriginalSAT)
                && response.data.cadenaOriginalSAT.StartsWith("||1.1|"), "El resultado data.cadenaOriginalSAT no es valido.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampXMLV1CadenaOriginalSATBase64byToken()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = GetXml(build);
            xml = Convert.ToBase64String(Encoding.UTF8.GetBytes(xml));
            var response = stamp.TimbrarV1CadenaOriginalSAT(xml, true);
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.tfd), response.message);
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cadenaOriginalSAT), "El resultado data.cadenaOriginalSAT viene vacio.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampXMLV1CadenaOriginalSAT_ValidateExistToken()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, "");
            var xml = File.ReadAllText("Resources/cfdi40.xml");
            var response = stamp.TimbrarV1CadenaOriginalSAT(xml);
            Assert.AreEqual("error", response.status);
            Assert.IsTrue(response.message.Contains("El token debe contener 3 partes"));
        }
        [TestMethod]
        public void Stamp_Test_45_CadenaOriginalSAT_FromTfd()
        {
            string tfd = "<tfd:TimbreFiscalDigital xmlns:tfd=\"http://www.sat.gob.mx/TimbreFiscalDigital\" " +
                "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" " +
                "xsi:schemaLocation=\"http://www.sat.gob.mx/TimbreFiscalDigital http://www.sat.gob.mx/sitio_internet/cfd/TimbreFiscalDigital/TimbreFiscalDigitalv11.xsd\" " +
                "Version=\"1.1\" UUID=\"5c9a7d3e-1b2f-4c8a-9e6d-0a1b2c3d4e5f\" FechaTimbrado=\"2024-05-10T12:30:45\" " +
                "RfcProvCertif=\"SPR190613I52\" SelloCFD=\"c2VsbG9DRkQ=\" NoCertificadoSAT=\"30001000000500003456\" SelloSAT=\"c2VsbG9TQVQ=\" />";
            string expected = "||1.1|5c9a7d3e-1b2f-4c8a-9e6d-0a1b2c3d4e5f|2024-05-10T12:30:45|SPR190613I52|c2VsbG9DRkQ=|30001000000500003456||";
            Assert.AreEqual(expected, SW.Helpers.Convertion.CadenaOriginalHelper.GetCadenaOriginalTfd(tfd));
            //Segunda llamada: valida que el XSLT reutilizado produce el mismo resultado
            Assert.AreEqual(expected, SW.Helpers.Convertion.CadenaOriginalHelper.GetCadenaOriginalTfd(tfd));
        }
        private string GetXml(BuildSettings build)
        {
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/cfdi40.xml"));
            xml = SignTools.SigXml(xml, Convert.FromBase64String(build.Pfx), build.PfxPassword);
            return xml;
        }

        // Pruebas locales (sin red): lotes de timbrado.
        private const string Token = "aaa.bbb.ccc";
        private const string Xml = "<cfdi:Comprobante xmlns:cfdi=\"http://www.sat.gob.mx/cfd/4\" Version=\"4.0\"/>";
        private const string SwMessage = "Mensaje original SW";
        private const string SwDetail = "Detalle original SW";

        [TestMethod]
        public void Lote_XmlRepetidosONulos_EnviaCadaXmlUnaSolaVez()
        {
            AssertBatch((prefix, xmls) => new Stamp(prefix, Token).TimbrarV1(xmls), "/cfdi33/stamp/v1/");
            AssertBatch((prefix, xmls) => new Stamp(prefix, Token).TimbrarV2(xmls), "/cfdi33/stamp/v2/");
            AssertBatch((prefix, xmls) => new Stamp(prefix, Token).TimbrarV3(xmls), "/cfdi33/stamp/v3/");
            AssertBatch((prefix, xmls) => new Stamp(prefix, Token).TimbrarV4(xmls), "/cfdi33/stamp/v4/");
            AssertBatch((prefix, xmls) => new StampV4(prefix, Token).TimbrarV1(xmls), "/v4/cfdi33/stamp/v1/");
            AssertBatch((prefix, xmls) => new StampV4(prefix, Token).TimbrarV2(xmls), "/v4/cfdi33/stamp/v2/");
            AssertBatch((prefix, xmls) => new StampV4(prefix, Token).TimbrarV3(xmls), "/v4/cfdi33/stamp/v3/");
            AssertBatch((prefix, xmls) => new StampV4(prefix, Token).TimbrarV4(xmls), "/v4/cfdi33/stamp/v4/");
        }

        private static void AssertBatch<T>(Func<string, string[], ConcurrentDictionary<string, T>> call, string path) where T : Response
        {
            const string otherXml = "<otro/>";
            int hits;
            // XML repetidos: se envia una sola peticion por XML distinto y cada uno conserva su respuesta de SW.
            var response = ServeMany(4, ctx =>
            {
                Assert.AreEqual("POST", ctx.Request.HttpMethod);
                Assert.AreEqual(path, ctx.Request.Url.AbsolutePath);
                string body = new StreamReader(ctx.Request.InputStream).ReadToEnd();
                bool first = body.Contains(Xml);
                Assert.IsTrue(first || body.Contains(otherXml));
                Reply(ctx, 400, SwJson("error", first ? "Respuesta XML 1" : "Respuesta XML 2"));
            }, prefix => call(prefix, new[] { Xml, otherXml, Xml, otherXml }), out hits);
            Assert.AreEqual(2, hits, path + ": cada XML distinto debe enviarse una sola vez.");
            Assert.AreEqual(2, response.Count);
            AssertResponse(response[Xml], "error", "Respuesta XML 1", SwDetail);
            AssertResponse(response[otherXml], "error", "Respuesta XML 2", SwDetail);

            // Nulos y vacios: no se envian y se informan sin afectar al XML valido.
            response = ServeMany(5, ctx => Reply(ctx, 200, SwJson("success", SwMessage)),
                prefix => call(prefix, new[] { Xml, null, "", null, Xml }), out hits);
            Assert.AreEqual(1, hits, path + ": las entradas nulas y vacias no se deben enviar.");
            Assert.AreEqual(4, response.Count);
            AssertResponse(response[Xml], "success", SwMessage, SwDetail);
            AssertResponse(response[""], "error", "El XML no puede estar vacio.", null);
            AssertResponse(response["[XML nulo en indice 1]"], "error", "El XML de la entrada 1 es nulo.", null);
            AssertResponse(response["[XML nulo en indice 3]"], "error", "El XML de la entrada 3 es nulo.", null);
        }

        private static void AssertResponse(Response response, string status, string message, string detail)
        {
            Assert.IsNotNull(response);
            Assert.AreEqual(status, response.status);
            Assert.AreEqual(message, response.message);
            Assert.AreEqual(detail, response.messageDetail);
        }

        private static string SwJson(string status, string message)
        {
            return JsonConvert.SerializeObject(new { status, message, messageDetail = SwDetail });
        }

        private delegate void ServerBehavior(HttpListenerContext context);

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
