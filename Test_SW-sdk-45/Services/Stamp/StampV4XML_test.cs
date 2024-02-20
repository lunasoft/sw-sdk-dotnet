using System;
using System.IO;
using System.Text;
using SW.Services.Stamp;
using Test_SW.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Test_SW.Services.Stamp_Test
{
    [TestClass]
    public class StampV4XML_Test
    {
        [TestMethod]
        [Ignore] //Debido a intermitencia en el servicio de storage.
        public void Stamp_Test_StampV4XMLV2_SameCustomID_byToken_Ok()
        {
            string CustomId = Guid.NewGuid().ToString();
            var build = new BuildSettings();
            StampV4XML stamp = new StampV4XML(build.Url, build.UrlApi, build.Token);
            var xml = GetXml(build, "cfdi40.xml");
            var response = (StampResponseV2) stamp.TimbrarV2(xml, null, CustomId);
            Assert.IsTrue(response.data != null, "El resultado data viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
            System.Threading.Thread.Sleep(5000);
            xml = GetXml(build, "cfdi40.xml");
            response = (StampResponseV2) stamp.TimbrarV2(xml, null, CustomId);
            Assert.IsTrue(response.status == "error" && response.message == "CFDI3307 - Timbre duplicado. El customId proporcionado está duplicado.");
        }
        [TestMethod]
        [Ignore] //Debido a intermitencia en el servicio de storage.
        public void Stamp_Test_StampV4XMLV4_SameCustomID_byToken_Ok()
        {
            string CustomId = Guid.NewGuid().ToString();
            var build = new BuildSettings();
            StampV4XML stamp = new StampV4XML(build.Url, build.UrlApi, build.Token);
            var xml = GetXml(build, "cfdi40.xml");
            var response = stamp.TimbrarV4(xml, null, CustomId);
            Assert.IsTrue(response.data != null, "El resultado data viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
            System.Threading.Thread.Sleep(5000);
            xml = GetXml(build, "cfdi40.xml");
            response = stamp.TimbrarV4(xml, null, CustomId);
            ValidateResponseV4(response);
            Assert.IsTrue(response.status == "error" && response.message == "CFDI3307 - Timbre duplicado. El customId proporcionado está duplicado.");
        }
        [TestMethod]
        public void Stamp_Test_StampV4XMLV2_SameCustomID_byToken_NoExistURLXML()
        {
                string CustomId = Guid.NewGuid().ToString();
                var build = new BuildSettings();
                StampV4XML stamp = new StampV4XML(build.Url, build.UrlApi, build.Token);
                var xml = GetXml(build, "cfdi40.xml");
                var response = (StampResponseV2)stamp.TimbrarV2(xml, null, CustomId);
                Assert.IsTrue(response.data != null, "El resultado data viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
                xml = GetXml(build, "cfdi40.xml");
                response = (StampResponseV2)stamp.TimbrarV2(xml, null, CustomId);
                Assert.IsTrue(response.status == "error" && response.message == "No es posible obtener el url para descargar el XML");
        }
        [TestMethod]
        public void Stamp_Test_StampV4XMLV4_SameCustomID_byToken_NoExistURLXML()
        {
            string CustomId = Guid.NewGuid().ToString();
            var build = new BuildSettings();
            StampV4XML stamp = new StampV4XML(build.Url, build.UrlApi, build.Token);
            var xml = GetXml(build, "cfdi40.xml");
            var response = stamp.TimbrarV4(xml, null, CustomId);
            Assert.IsTrue(response.data != null, "El resultado data viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
            xml = GetXml(build, "cfdi40.xml");
            response = stamp.TimbrarV4(xml, null, CustomId);
            Assert.IsTrue(response.status == "error" && response.message == "No es posible obtener el url para descargar el XML");
        }
        [TestMethod]
        public void Stamp_Test_StampV4XMLV2_DifCustomID_byToken()
        {
            string CustomIdfirstRequest = Guid.NewGuid().ToString();
            string CustomIdSecondRequest = Guid.NewGuid().ToString();
            var build = new BuildSettings();
            BaseStampV4XML stamp = new StampV4XML(build.Url, build.UrlApi, build.Token);
            var xml = GetXml(build, "cfdi40.xml");
            var response = (StampResponseV2) stamp.TimbrarV2(xml, null, CustomIdfirstRequest);
            Assert.IsTrue(response.data != null, "El resultado data viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
            response = (StampResponseV2) stamp.TimbrarV2(xml, null, CustomIdSecondRequest);
            Assert.IsTrue(response.status == "error" && response.message == "307. El comprobante contiene un timbre previo.");
        }
        [TestMethod]
        public void Stamp_Test_StampV4XMLV4_DifCustomID_byToken()
        {
            string CustomIdfirstRequest = Guid.NewGuid().ToString();
            string CustomIdSecondRequest = Guid.NewGuid().ToString();
            var build = new BuildSettings();
            BaseStampV4XML stamp = new StampV4XML(build.Url, build.UrlApi, build.Token);
            var xml = GetXml(build, "cfdi40.xml");
            var response = stamp.TimbrarV4(xml, null, CustomIdfirstRequest);
            Assert.IsTrue(response.data != null, "El resultado data viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
            System.Threading.Thread.Sleep(5000);
            response = stamp.TimbrarV4(xml, null, CustomIdSecondRequest);
            ValidateResponseV4(response);
            Assert.IsTrue(response.status == "error" && response.message == "307. El comprobante contiene un timbre previo.");
        }
        [TestMethod]
        public void Stamp_Test_StampV4XMLV2_InvalidDate_byToken_Error()
        {
            string CustomIdfirstRequest = Guid.NewGuid().ToString();
            var build = new BuildSettings();
            BaseStampV4XML stamp = new StampV4XML(build.Url, build.UrlApi, build.Token);
            var xml = GetXml(build, "cfdi40.xml", false);
            var response = (StampResponseV2)stamp.TimbrarV2(xml, null, CustomIdfirstRequest);
            Assert.IsTrue(response.status == "error");
            Assert.IsTrue(response.message == "401 - El rango de la fecha de generación no debe de ser mayor a 72 horas para la emisión del timbre.");
            Assert.IsTrue(response.data is null);
        }
        [TestMethod]
        public void Stamp_Test_StampV4XMLV4_InvalidDate_byToken_Error()
        {
            string CustomIdfirstRequest = Guid.NewGuid().ToString();
            var build = new BuildSettings();
            BaseStampV4XML stamp = new StampV4XML(build.Url, build.UrlApi, build.Token);
            var xml = GetXml(build, "cfdi40.xml", false);
            var response = (StampResponseV4)stamp.TimbrarV4(xml, null, CustomIdfirstRequest);
            Assert.IsTrue(response.status == "error");
            Assert.IsTrue(response.message == "401 - El rango de la fecha de generación no debe de ser mayor a 72 horas para la emisión del timbre.");
            Assert.IsTrue(response.data is null);
        }
        [TestMethod]
        public void Stamp_Test_StampV4XMLV2_InvalidCfdi_byToken_Error()
        {
            string CustomIdfirstRequest = Guid.NewGuid().ToString();
            var build = new BuildSettings();
            BaseStampV4XML stamp = new StampV4XML(build.Url, build.UrlApi, build.Token);
            var xml = GetXml(build, "error.xml");
            var response = (StampResponseV2)stamp.TimbrarV2(xml, null, CustomIdfirstRequest);
            Assert.IsTrue(response.status == "error");
            Assert.IsTrue(response.data is null);
        }
        [TestMethod]
        public void Stamp_Test_StampV4XMLV4_InvalidCfdi_byToken_Error()
        {
            string CustomIdfirstRequest = Guid.NewGuid().ToString();
            var build = new BuildSettings();
            BaseStampV4XML stamp = new StampV4XML(build.Url, build.UrlApi, build.Token);
            var xml = GetXml(build, "error.xml");
            var response = (StampResponseV4)stamp.TimbrarV4(xml, null, CustomIdfirstRequest);
            Assert.IsTrue(response.status == "error");
            Assert.IsTrue(response.data is null);
        }
        private string GetXml(BuildSettings build, string fileName, bool setDate = true)
        {
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes(String.Format("Resources/{0}", fileName)));
            xml = SignTools.SigXml(xml, Convert.FromBase64String(build.Pfx), build.PfxPassword, setDate);
            return xml;
        }
        private static void ValidateResponseV4(StampResponseV4 response)
        {
            Assert.IsTrue(!String.IsNullOrEmpty(response.data.cadenaOriginalSAT));
            Assert.IsTrue(!String.IsNullOrEmpty(response.data.cfdi));
            Assert.IsTrue(!String.IsNullOrEmpty(response.data.fechaTimbrado));
            Assert.IsTrue(!String.IsNullOrEmpty(response.data.noCertificadoCFDI));
            Assert.IsTrue(!String.IsNullOrEmpty(response.data.noCertificadoSAT));
            Assert.IsTrue(!String.IsNullOrEmpty(response.data.qrCode));
            Assert.IsTrue(!String.IsNullOrEmpty(response.data.selloCFDI));
            Assert.IsTrue(!String.IsNullOrEmpty(response.data.selloSAT));
            Assert.IsTrue(!String.IsNullOrEmpty(response.data.uuid));
        }
    }
}
