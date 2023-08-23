using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Stamp;
using Test_SW.Helpers;

namespace Test_SW.Services.Stamp_Test
{
    [TestClass]
    public class Stamp_Test
    {
        [Ignore]//Problema cadena SW Tools
        [TestMethod]
        public void StampXMLV1()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV1)stamp.TimbrarV1(xml);
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio.");
        }
        [Ignore]//Problema cadena SW Tools
        [TestMethod]
        public void StampXMLV1byToken()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = GetXml(build);
            var response = (StampResponseV1)stamp.TimbrarV1(xml);
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio.");
        }
        [Ignore]//Problema cadena SW Tools
        [TestMethod]
        public void StampXMLV1Base64()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            xml = Convert.ToBase64String(Encoding.UTF8.GetBytes(xml));
            var response = (StampResponseV1)stamp.TimbrarV1(xml, true);
            Assert.IsTrue(response.status == "success"
               && !string.IsNullOrEmpty(response.data.tfd), response.message);
        }
        [Ignore]//Problema cadena SW Tools
        [TestMethod]
        public void StampXMLV1Base64byToken()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = GetXml(build);
            xml = Convert.ToBase64String(Encoding.UTF8.GetBytes(xml));
            var response = (StampResponseV1)stamp.TimbrarV1(xml, true);
            Assert.IsTrue(response.status == "success"
              && !string.IsNullOrEmpty(response.data.tfd), response.message);
        }
        [Ignore]//Problema cadena SW Tools
        [TestMethod]
        public void StampXMLV2()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampResponseV2)stamp.TimbrarV2(xml);
            Assert.IsTrue(response.status == "success"
               && !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.tfd viene vacio.");
        }
        [Ignore]//Problema cadena SW Tools
        [TestMethod]
        public void StampXMLV2byToken()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = GetXml(build);
            var response = (StampResponseV2)stamp.TimbrarV2(xml);
            Assert.IsTrue(response.status == "success"
               && !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.tfd viene vacio.");
        }
        [Ignore]//Problema cadena SW Tools
        [TestMethod]
        public void StampXMLV2Base64()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            xml = Convert.ToBase64String(Encoding.UTF8.GetBytes(xml));
            var response = (StampResponseV2)stamp.TimbrarV2(xml, true);
            Assert.IsTrue(response.status == "success"
               && !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.tfd viene vacio.");
        }
        [Ignore]//Problema cadena SW Tools
        [TestMethod]
        public void StampXMLV2Base64byToken()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = GetXml(build);
            xml = Convert.ToBase64String(Encoding.UTF8.GetBytes(xml));
            var response = (StampResponseV2)stamp.TimbrarV2(xml, true);
            Assert.IsTrue(response.status == "success"
              && !string.IsNullOrEmpty(response.data.cfdi), response.message);
        }
        [Ignore]//Problema cadena SW Tools
        [TestMethod]
        public void StampXMLV3byToken()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = GetXml(build);
            var response = (StampResponseV3)stamp.TimbrarV3(xml);
            Assert.IsTrue(response.status == "success"
               && !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.tfd viene vacio.");
        }
        [Ignore]//Problema cadena SW Tools
        [TestMethod]
        public void StampXMLV3Base64byToken()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = GetXml(build);
            xml = Convert.ToBase64String(Encoding.UTF8.GetBytes(xml));
            var response = (StampResponseV3)stamp.TimbrarV3(xml, true);
            Assert.IsTrue(response.status == "success"
               && !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.tfd viene vacio.");
        }
        [Ignore]//Problema cadena SW Tools
        [TestMethod]
        public void StampXMLV4byToken()
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
        [Ignore]//Problema cadena SW Tools
        [TestMethod]
        public void StampXMLV4Base64byToken()
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
        public void ValidateServerError()
        {
            var resultExpect = "404";
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url + "/ot", build.Token);
            var xml = File.ReadAllText("Resources/Cfdi40.xml");
            var response = stamp.TimbrarV1(xml);
            Assert.AreEqual(response.message, (string)resultExpect, (string)resultExpect);
        }
        [TestMethod]
        public void ValidateFormatToken()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token + ".");
            var xml = File.ReadAllText("Resources/cfdi40.xml");
            var response = stamp.TimbrarV1(xml);
            Assert.IsTrue(response.message.Contains("El token debe contener 3 partes"));
        }
        [TestMethod]
        public void ValidateExistToken()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, "");
            var xml = File.ReadAllText("Resources/cfdi40.xml");
            var response = stamp.TimbrarV1(xml);
            Assert.IsTrue(response.message.Contains("El token debe contener 3 partes"));
        }
        [TestMethod]
        public void ValidateEmptyXML()
        {
            var resultExpect = "Xml CFDI33 no proporcionado o viene vacio.";
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = File.ReadAllText("Resources/EmptyXML.xml");
            var response = stamp.TimbrarV1(xml);
            Assert.AreEqual(response.message, (string)resultExpect, (string)resultExpect);
        }
        [Ignore]//Problema cadena SW Tools
        [TestMethod]
        public void ValidateSpecialCharactersFromXML()
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
        public void ValidateIsUTF8FromXML()
        {
            var resultExpect = "301";
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/cfdi40_ansi.xml"));            
            var response = stamp.TimbrarV1(xml);
            Assert.IsTrue(response.message.Contains(resultExpect), "Result not expected. Error: " + response.message);
        }
        [Ignore]//Problema cadena SW Tools
        [TestMethod]
        public void MultipleStampXMLV1byToken()
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
        [Ignore]//Problema cadena SW Tools
        [TestMethod]
        public void StampLargeXMLV4byToken()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = GetXml(build, "Resources/cfdi40_big.xml");
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
        [Ignore]//Problema cadena SW Tools
        [TestMethod]
        public void StampLargeXMLV4Base64ByToken()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = GetXml(build, "Resources/cfdi40_big.xml");
            xml = Convert.ToBase64String(Encoding.UTF8.GetBytes(xml));
            var response = (StampResponseV4)stamp.TimbrarV4(xml, true);
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
        public void StampLargeXMLV4byTokenError()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = GetXml(build, "Resources/cfdi40_big.xml", false);
            var response = (StampResponseV4)stamp.TimbrarV4(xml);
            Assert.IsTrue(response != null, "El resultado viene vacio.");
            Assert.IsTrue(response.status == "error");
        }
        [TestMethod]
        public void StampLargeXMLV4Base64ByTokenError()
        {
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url, build.Token);
            var xml = GetXml(build, "Resources/cfdi40_big.xml", false);
            xml = Convert.ToBase64String(Encoding.UTF8.GetBytes(xml));
            var response = (StampResponseV4)stamp.TimbrarV4(xml, true);
            Assert.IsTrue(response != null, "El resultado viene vacio.");
            Assert.IsTrue(response.status == "error");
        }
        private string GetXml(BuildSettings build, string fileName = null, bool setDate = true)
        {
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes(fileName ?? "Resources/cfdi40.xml"));
            xml = SignTools.SigXml(xml, Convert.FromBase64String(build.Pfx), build.PfxPassword, setDate);
            return xml;
        }
    }
}
