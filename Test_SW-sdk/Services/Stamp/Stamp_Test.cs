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
        private BuildSettings Build;
        private object resultExpect;
        private string xml;
        private void GetEnviromentVariables()
        {
            if (Environment.GetEnvironmentVariable("sw-sdk-url") != null)
            {
                Build.Url = Environment.GetEnvironmentVariable("sw-sdk-url");
            }
            if (Environment.GetEnvironmentVariable("sw-sdk-user") != null)
            {
                Build.User = Environment.GetEnvironmentVariable("sw-sdk-user");
            }
            if (Environment.GetEnvironmentVariable("sw-sdk-password") != null)
            {
                Build.Password = Environment.GetEnvironmentVariable("sw-sdk-password");
            }
            if (Environment.GetEnvironmentVariable("sw-sdk-token") != null)
            {
                Build.Token = Environment.GetEnvironmentVariable("sw-sdk-token");
            }
        }
        [TestMethod]
        public void StampXMLV1()
        {
            Build = new BuildSettings();
            GetEnviromentVariables();
            Stamp stamp = new Stamp(Build.Url, Build.User, Build.Password);
            xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml"));
            var response = (StampResponseV1)stamp.TimbrarV1(xml);
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio.");
        }
        [TestMethod]
        public void StampXMLV1byToken()
        {
            Build = new BuildSettings();
            GetEnviromentVariables();
            Stamp stamp = new Stamp(Build.Url, Build.Token);
            xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml"));
            var response = (StampResponseV1)stamp.TimbrarV1(xml);
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio.");
        }
        [TestMethod]
        public void StampXMLV1Base64()
        {
            Build = new BuildSettings();
            GetEnviromentVariables();
            Stamp stamp = new Stamp(Build.Url, Build.User, Build.Password);
            var xmlBase = Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml")));
            xml = Convert.ToBase64String(xmlBase);
            var response = (StampResponseV1)stamp.TimbrarV1(xml, true);
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio.");
        }
        [TestMethod]
        public void StampXMLV1Base64byToken()
        {
            Build = new BuildSettings();
            GetEnviromentVariables();
            Stamp stamp = new Stamp(Build.Url, Build.Token);
            var xmlBase = Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml")));
            xml = Convert.ToBase64String(xmlBase);
            var response = (StampResponseV1)stamp.TimbrarV1(xml, true);
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio.");
        }
        [TestMethod]
        public void StampXMLV2()
        {
            Build = new BuildSettings();
            GetEnviromentVariables();
            Stamp stamp = new Stamp(Build.Url, Build.User, Build.Password);
            xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml"));
            var response = (StampResponseV2)stamp.TimbrarV2(xml);
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.tfd viene vacio.");
        }
        [TestMethod]
        public void StampXMLV2byToken()
        {
            Build = new BuildSettings();
            GetEnviromentVariables();
            Stamp stamp = new Stamp(Build.Url, Build.Token);
            xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml"));
            var response = (StampResponseV2)stamp.TimbrarV2(xml);
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.tfd viene vacio.");
        }
        [TestMethod]
        public void StampXMLV2Base64()
        {
            Build = new BuildSettings();
            GetEnviromentVariables();
            Stamp stamp = new Stamp(Build.Url, Build.User, Build.Password);
            var xmlBase = Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml")));
            xml = Convert.ToBase64String(xmlBase);
            var response = (StampResponseV2)stamp.TimbrarV2(xml, true);
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.tfd viene vacio.");
        }
        [TestMethod]
        public void StampXMLV2Base64byToken()
        {
            Build = new BuildSettings();
            GetEnviromentVariables();
            Stamp stamp = new Stamp(Build.Url, Build.Token);
            var xmlBase = Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml")));
            xml = Convert.ToBase64String(xmlBase);
            var response = (StampResponseV2)stamp.TimbrarV2(xml, true);
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.tfd viene vacio.");
        }
        [TestMethod]
        public void StampXMLV3byToken()
        {
            Build = new BuildSettings();
            GetEnviromentVariables();
            Stamp stamp = new Stamp(Build.Url, Build.Token);
            xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml"));
            var response = (StampResponseV3)stamp.TimbrarV3(xml);
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.tfd viene vacio.");
        }
        [TestMethod]
        public void StampXMLV3Base64byToken()
        {
            Build = new BuildSettings();
            GetEnviromentVariables();
            Stamp stamp = new Stamp(Build.Url, Build.Token);
            var xmlBase = Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml")));
            xml = Convert.ToBase64String(xmlBase);
            var response = (StampResponseV3)stamp.TimbrarV3(xml, true);
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
        }
        [TestMethod]
        public void StampXMLV4byToken()
        {
            Build = new BuildSettings();
            GetEnviromentVariables();
            Stamp stamp = new Stamp(Build.Url, Build.Token);
            xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml"));
            var response = (StampResponseV4)stamp.TimbrarV4(xml);
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.cadenaOriginalSAT), "El resultado data.cadenaOriginalSAT viene vacio.");
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.noCertificadoSAT), "El resultado data.noCertificadoSAT viene vacio.");
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.noCertificadoCFDI), "El resultado data.noCertificadoCFDI viene vacio.");
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.uuid), "El resultado data.uuid viene vacio.");
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.selloSAT), "El resultado data.selloSAT viene vacio.");
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.selloCFDI), "El resultado data.selloCFDI viene vacio.");
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.fechaTimbrado), "El resultado data.fechaTimbrado viene vacio.");
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.qrCode), "El resultado data.qrCode viene vacio.");
        }
        [TestMethod]
        public void StampXMLV4Base64byToken()
        {
            Build = new BuildSettings();
            GetEnviromentVariables();
            Stamp stamp = new Stamp(Build.Url, Build.Token);
            var xmlBase = Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml")));
            xml = Convert.ToBase64String(xmlBase);
            var response = (StampResponseV4)stamp.TimbrarV4(xml, true);
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.cadenaOriginalSAT), "El resultado data.cadenaOriginalSAT viene vacio.");
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.noCertificadoSAT), "El resultado data.noCertificadoSAT viene vacio.");
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.noCertificadoCFDI), "El resultado data.noCertificadoCFDI viene vacio.");
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.uuid), "El resultado data.uuid viene vacio.");
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.selloSAT), "El resultado data.selloSAT viene vacio.");
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.selloCFDI), "El resultado data.selloCFDI viene vacio.");
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.fechaTimbrado), "El resultado data.fechaTimbrado viene vacio.");
            Assert.IsTrue(response.message.Contains("72 horas") || !string.IsNullOrEmpty(response.data.qrCode), "El resultado data.qrCode viene vacio.");
        }
        [TestMethod]
        public void ValidateServerError()
        {
            resultExpect = "404";
            Build = new BuildSettings();
            Stamp stamp = new Stamp(Build.Url + "/ot", Build.Token);
            xml = File.ReadAllText("Resources/File.xml");
            var response = stamp.TimbrarV1(xml);
            Assert.AreEqual(response.message, (string)resultExpect, (string)resultExpect);
        }
        [TestMethod]
        public void ValidateFormatToken()
        {
            resultExpect = "Token Mal Formado";
            Build = new BuildSettings();
            Stamp stamp = new Stamp(Build.Url, Build.Token + ".");
            xml = File.ReadAllText("Resources/file.xml");
            var response = stamp.TimbrarV1(xml);
            Assert.AreEqual(response.message, (string)resultExpect, (string)resultExpect);
        }
        [TestMethod]
        public void ValidateExistToken()
        {
            resultExpect = "Falta Capturar Token";
            Build = new BuildSettings();
            Stamp stamp = new Stamp(Build.Url, "");
            xml = File.ReadAllText("Resources/file.xml");
            var response = stamp.TimbrarV1(xml);
            Assert.AreEqual(response.message, (string)resultExpect, (string)resultExpect);
        }
        [TestMethod]
        public void ValidateNotIsEmptyXML()
        {
            resultExpect = "T301. La estructura del comprobante es incorrecta.";
            Build = new BuildSettings();
            Stamp stamp = new Stamp(Build.Url, Build.Token);
            xml = File.ReadAllText("Resources/EmptyXML.xml");
            var response = stamp.TimbrarV1(xml);
            Assert.AreEqual(response.message, (string)resultExpect, (string)resultExpect);
        }
        [TestMethod]
        public void ValidateSpecialCharactersFromXML()
        {
            resultExpect = "T301. La estructura del comprobante es incorrecta.";
            Build = new BuildSettings();
            Stamp stamp = new Stamp(Build.Url, Build.Token);
            xml = File.ReadAllText("Resources/SpecialCharacters.xml");
            var response = stamp.TimbrarV1(xml);
            Assert.IsTrue(response.message.Contains((string)resultExpect), (string)resultExpect);
        }
        [TestMethod]
        public void ValidateIsUTF8FromXML()
        {
            resultExpect = "T301. La estructura del comprobante es incorrecta.";
            Build = new BuildSettings();
            Stamp stamp = new Stamp(Build.Url, Build.Token);
            xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/fileANSI.xml"));
            var response = stamp.TimbrarV1(xml);
            Assert.AreEqual(response.message, (string)resultExpect, (string)resultExpect);
        }
        [TestMethod]
        public void MultipleStampXMLV1byToken()
        {
            Build = new BuildSettings();
            GetEnviromentVariables();
            resultExpect = false;
            int iterations = 10;
            Stamp stamp = new Stamp(Build.Url, Build.Token);
            List<StampResponseV1> listXmlResult = new List<StampResponseV1>();
            for (int i = 0; i < iterations; i++)
            {
                string xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml"));
                var response = (StampResponseV1)stamp.TimbrarV1(xml);
                listXmlResult.Add(response);
            }
            if (listXmlResult != null)
                resultExpect = listXmlResult.FindAll(w => w.status == ResponseType.success.ToString() || w.message.Contains("72 horas")).Count == iterations;

            Assert.IsTrue((bool)resultExpect);
        }
    }
}
