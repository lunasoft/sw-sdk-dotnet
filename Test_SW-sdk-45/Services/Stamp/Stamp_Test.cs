using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Stamp;
using Test_SW.Helpers;
using System.Xml;

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
        public void Stamp_Test_45_ValidateServerError()
        {
            var resultExpect = "404";
            var build = new BuildSettings();
            Stamp stamp = new Stamp(build.Url + "/ot", build.Token);
            var xml = File.ReadAllText("Resources/Cfdi40.xml");
            var response = stamp.TimbrarV1(xml);
            Assert.AreEqual(response.message, (string)resultExpect, (string)resultExpect);
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
        private string GetXml(BuildSettings build)
        {
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/cfdi40.xml"));
            xml = SignTools.SigXml(xml, Convert.FromBase64String(build.Pfx), build.PfxPassword);
            return xml;
        }
    }
}
