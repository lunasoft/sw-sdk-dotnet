using Microsoft.VisualStudio.TestTools.UnitTesting;
using SW.Services.Issue;
using SW.Services.Stamp;
using SW.Tools.Services.Fiscal;
using System;
using System.IO;
using System.Text;
using System.Xml;
using Test_SW.Helpers;

namespace Test_SW_sdk_45.Services.Issue
{
    [TestClass]
    public class IssueV4_Test
    {
        [TestMethod]
        public void IssueV4Emailbytoken()
        {
            var build = new BuildSettings();
            IssueV4 issue = new IssueV4(build.Url, build.Token);
            var xml = GetXml(build);
            var response = (StampResponseV4)issue.TimbrarV4(xml, "fernando.carrillo@sw.com.mx,prueba@test.com");
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
        public void IssueV4IvalidEmailbytoken()
        {
            var build = new BuildSettings();
            IssueV4 issue = new IssueV4(build.Url, build.Token);
            var xml = GetXml(build);
            var response = (StampResponseV4)issue.TimbrarV4(xml, "prueba@");
            Assert.IsTrue(response.status == "error");
            Assert.IsTrue(!string.IsNullOrEmpty(response.message), "El listado contiene más de 5 correos o el formato es incorrecto.");
        }

        [TestMethod]
        public void IssueV4CustomIdbytoken()
        {
            var build = new BuildSettings();
            IssueV4 issue = new IssueV4(build.Url, build.Token);
            var xml = GetXml(build);
            Random rnd = new Random();
            var CustomId = rnd.Next().ToString();
            var response = (StampResponseV4)issue.TimbrarV4(xml, null, CustomId);
            Assert.IsTrue(response.data != null, "El resultado data viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
            xml = GetXml(build);
            response = (StampResponseV4)issue.TimbrarV4(xml, null, CustomId);
            Assert.IsTrue(response.status == "error");
            Assert.IsTrue(!string.IsNullOrEmpty(response.message), "CFDI3307 - Timbre duplicado. El customId proporcionado está duplicado.");
        }

        [TestMethod]
        public void IssueV4NullCustomIdbytoken()
        {
            var build = new BuildSettings();
            IssueV4 issue = new IssueV4(build.Url, build.Token);
            var xml = GetXml(build);
            var response = (StampResponseV4)issue.TimbrarV4(xml, "fernando.carrillo@sw.com.mx,prueba@test.com", "");
            Assert.IsTrue(response.status == "error");
            Assert.IsTrue(!string.IsNullOrEmpty(response.message), "El CustomId viene vacío.");
        }

        [TestMethod]
        public void IssueV4EmailCustomIdbytoken()
        {
            var build = new BuildSettings();
            IssueV4 issue = new IssueV4(build.Url, build.Token);
            var xml = GetXml(build);
            Random rnd = new Random();
            var CustomId = rnd.Next().ToString();
            var response = (StampResponseV4)issue.TimbrarV4(xml, "fernando.carrillo@sw.com.mx,prueba@test.com", CustomId);
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

        static Random randomNumber = new Random(1);
        private string GetXml(BuildSettings build)
        {
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/fileIssue40.xml"));
            xml = Fiscal.RemoverCaracteresInvalidosXml(xml);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            doc.DocumentElement.SetAttribute("Fecha", DateTime.Now.AddHours(-12).ToString("s"));
            doc.DocumentElement.SetAttribute("Folio", DateTime.Now.Ticks.ToString() + randomNumber.Next(100));
            xml = doc.OuterXml;
            return xml;
        }
    }
}
