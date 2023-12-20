using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Stamp;
using Test_SW.Helpers;
using System.Xml;
using System.Linq;

namespace Test_SW_sdk_45.Services.Stamp
{
    [TestClass]
    public class StampV4_Test_45
    {
        [TestMethod]
        public void Stamp_Test_45_StampV4_V1_some_emails()
        {
            Random rnd = new Random();
            var build = new BuildSettings();
            StampV4 stamp = new StampV4(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV1)stamp.TimbrarV1(xml, "some@email.com,some2@email.com,some3@email.com,some4@email.com,some5@email.com");
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampV4_V1_email_customId()
        {
            Random rnd = new Random();
            var build = new BuildSettings();
            StampV4 stamp = new StampV4(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV1)stamp.TimbrarV1(xml, "some@email.com", rnd.Next().ToString());
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampV4_V1_customId()
        {
            Random rnd = new Random();
            var build = new BuildSettings();
            StampV4 stamp = new StampV4(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV1)stamp.TimbrarV1(xml, null, rnd.Next().ToString());
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampV4_V1_email()
        {
            var build = new BuildSettings();
            StampV4 stamp = new StampV4(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV1)stamp.TimbrarV1(xml, "some@email.com");
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampV4_V1()
        {
            var build = new BuildSettings();
            StampV4 stamp = new StampV4(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV1)stamp.TimbrarV1(xml);
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampV4_V2_email_customId()
        {
            Random rnd = new Random();
            var build = new BuildSettings();
            StampV4 stamp = new StampV4(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV2)stamp.TimbrarV2(xml, "some@email.com", rnd.Next().ToString());
            Assert.IsTrue(response.data != null, "El resultado data viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampV4_V2_some_emails()
        {
            Random rnd = new Random();
            var build = new BuildSettings();
            StampV4 stamp = new StampV4(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV2)stamp.TimbrarV2(xml, "some@email.com,some2@email.com,some3@email.com,some4@email.com,some5@email.com");
            Assert.IsTrue(response.data != null, "El resultado data viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampV4_V2_customId()
        {
            Random rnd = new Random();
            var build = new BuildSettings();
            StampV4 stamp = new StampV4(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV2)stamp.TimbrarV2(xml, null, rnd.Next().ToString());
            Assert.IsTrue(response.data != null, "El resultado data viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampV4_V2_email()
        {
            var build = new BuildSettings();
            StampV4 stamp = new StampV4(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV2)stamp.TimbrarV2(xml, "some@email.com");
            Assert.IsTrue(response.data != null, "El resultado data viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampV4_V2()
        {
            var build = new BuildSettings();
            StampV4 stamp = new StampV4(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV2)stamp.TimbrarV2(xml);
            Assert.IsTrue(response.data != null, "El resultado data viene vacio.");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio");
            Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampV4_V3_some_emails()
        {
            Random rnd = new Random();
            var build = new BuildSettings();
            StampV4 stamp = new StampV4(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV3)stamp.TimbrarV3(xml, "some@email.com,some2@email.com,some3@email.com,some4@email.com,some5@email.com");
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampV4_V3_email_customId()
        {
            Random rnd = new Random();
            var build = new BuildSettings();
            StampV4 stamp = new StampV4(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV3)stamp.TimbrarV3(xml, "some@email.com", rnd.Next().ToString());
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampV4_V3_customId()
        {
            Random rnd = new Random();
            var build = new BuildSettings();
            StampV4 stamp = new StampV4(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV3)stamp.TimbrarV3(xml, null, rnd.Next().ToString());
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampV4_V3_email()
        {
            var build = new BuildSettings();
            StampV4 stamp = new StampV4(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV3)stamp.TimbrarV3(xml, "some@email.com");
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampV4_V3()
        {
            var build = new BuildSettings();
            StampV4 stamp = new StampV4(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV3)stamp.TimbrarV3(xml);
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
        }
        [TestMethod]
        public void Stamp_Test_45_StampV4_V4_some_emails()
        {
            Random rnd = new Random();
            var build = new BuildSettings();
            StampV4 stamp = new StampV4(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV4)stamp.TimbrarV4(xml, "some@email.com,some2@email.com,some3@email.com,some4@email.com,some5@email.com");
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
        public void Stamp_Test_45_StampV4_V4_email_customId()
        {
            Random rnd = new Random();
            var build = new BuildSettings();
            StampV4 stamp = new StampV4(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV4)stamp.TimbrarV4(xml, "some@email.com", rnd.Next().ToString());
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
        public void Stamp_Test_45_StampV4_V4_customId()
        {
            Random rnd = new Random();
            var build = new BuildSettings();
            StampV4 stamp = new StampV4(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV4)stamp.TimbrarV4(xml, null, rnd.Next().ToString());
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
        public void Stamp_Test_45_StampV4_V4_email()
        {
            var build = new BuildSettings();
            StampV4 stamp = new StampV4(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV4)stamp.TimbrarV4(xml, "some@email.com");
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
        public void Stamp_Test_45_StampV4_V4()
        {
            var build = new BuildSettings();
            StampV4 stamp = new StampV4(build.Url, build.User, build.Password);
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
        public void Stamp_Test_StampV4XMLV1_HashedCustomId_IdDuplicado_Error()
        {
            var build = new BuildSettings();
            StampV4 stamp = new StampV4(build.Url, build.User, build.Password);
            var customId = Guid.NewGuid().ToString();
            customId = string.Concat(Enumerable.Repeat(customId, 4));
            var xml = GetXml(build);
            var response = (StampResponseV1)stamp.TimbrarV1(xml, null, customId);
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio.");
            xml = GetXml(build);
            response = (StampResponseV1)stamp.TimbrarV1(xml, null, customId);
            Assert.IsTrue(response.status == "error");
            Assert.IsTrue(response.message == "CFDI3307 - Timbre duplicado. El customId proporcionado está duplicado.");
        }
        [TestMethod]
        public void Stamp_Test_StampV4XMLV1_InvalidCustomId_Error()
        {
            var build = new BuildSettings();
            StampV4 stamp = new StampV4(build.Url, build.User, build.Password);
            var customId = Guid.NewGuid().ToString();
            customId = string.Concat(Enumerable.Repeat(customId, 10));
            var xml = GetXml(build);
            var response = (StampResponseV1)stamp.TimbrarV1(xml, null, customId);
            Assert.IsTrue(response.status == "error");
            Assert.IsTrue(response.message == "El CustomId no es válido o es mayor a 100 cracteres.");
        }
        [TestMethod]
        public void Stamp_Test_StampV4XMLV1_InvalidCustomId_Empty()
        {
            var build = new BuildSettings();
            StampV4 stamp = new StampV4(build.Url, build.User, build.Password);
            var customId = Guid.NewGuid().ToString();
            customId = "";
            var xml = GetXml(build);
            var response = (StampResponseV1)stamp.TimbrarV1(xml, null, customId);
            Assert.IsTrue(response.status == "error");
            Assert.IsTrue(response.message == "El CustomId viene vacío.");
        }
        private string GetXml(BuildSettings build)
        {
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/cfdi40.xml"));
            xml = SignTools.SigXml(xml, Convert.FromBase64String(build.Pfx), build.PfxPassword);
            return xml;
        }
    }
}
