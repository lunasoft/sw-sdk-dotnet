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
        [Ignore]
        public void StampXMLV1()
        {
            Build = new BuildSettings();
            GetEnviromentVariables();
            Stamp stamp = new Stamp(Build.Url, Build.User, Build.Password);
            xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml"));
            var response = stamp.Timbrar(xml, StampTypes.v1);
            Assert.IsTrue(!string.IsNullOrEmpty(response.Data.tfd));
        }
        [TestMethod]
        [Ignore]
        public void StampXMLV1byToken()
        {
            Build = new BuildSettings();
            GetEnviromentVariables();
            Stamp stamp = new Stamp(Build.Url, Build.Token);
            xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml"));
            var response = stamp.Timbrar(xml, StampTypes.v1);
            Assert.IsTrue(!string.IsNullOrEmpty(response.Data.tfd));
        }
        [TestMethod]
        [Ignore]
        public void StampXMLV2()
        {
            Build = new BuildSettings();
            GetEnviromentVariables();
            Stamp stamp = new Stamp(Build.Url, Build.User, Build.Password);
            xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/fileBase64.xml"));
            var response = stamp.TimbrarBase64(xml, StampTypes.v1);
            Assert.IsTrue(!string.IsNullOrEmpty(response.Data.cfdi));
        }
        [TestMethod]
        [Ignore]
        public void StampXMLV2byToken()
        {
            Build = new BuildSettings();
            GetEnviromentVariables();
            Stamp stamp = new Stamp(Build.Url, Build.Token);
            xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/fileBase64.xml"));
            var response = stamp.TimbrarBase64(xml, StampTypes.v1);
            Assert.IsTrue(!string.IsNullOrEmpty(response.Data.cfdi));
        }
        [TestMethod]
        public void ValidateServerError()
        {
            resultExpect = "Url Invalida";
            Build = new BuildSettings();
            Stamp stamp = new Stamp(Build.Url + "/ot", Build.Token);
            xml = File.ReadAllText("Resources/File.xml");
            var response = stamp.Timbrar(xml, StampTypes.v1);
            Assert.AreEqual(response.Message, (string)resultExpect, (string)resultExpect);
        }
        [TestMethod]
        public void ValidateFormatToken()
        {
            resultExpect = "Token Mal Formado";
            Build = new BuildSettings();
            Stamp stamp = new Stamp(Build.Url, Build.Token + ".");
            xml = File.ReadAllText("Resources/file.xml");
            var response = stamp.Timbrar(xml, StampTypes.v1);
            Assert.AreEqual(response.Message, (string)resultExpect, (string)resultExpect);
        }
        [TestMethod]
        public void ValidateExistToken()
        {
            resultExpect = "Falta Capturar Token";
            Build = new BuildSettings();
            Stamp stamp = new Stamp(Build.Url, "");
            xml = File.ReadAllText("Resources/file.xml");
            var response = stamp.Timbrar(xml, StampTypes.v1);
            Assert.AreEqual(response.Message, (string)resultExpect, (string)resultExpect);
        }
        [TestMethod]
        public void ValidateNotIsEmptyXML()
        {
            resultExpect = "Tu XML esta vacio";
            Build = new BuildSettings();
            Stamp stamp = new Stamp(Build.Url, Build.Token);
            xml = File.ReadAllText("Resources/EmptyXML.xml");
            var response = stamp.Timbrar(xml, StampTypes.v1);
            Assert.AreEqual(response.Message, (string)resultExpect, (string)resultExpect);
        }
        [TestMethod]
        public void ValidateSpecialCharactersFromXML()
        {
            resultExpect = "Tu XML no tiene codificacion UTF-8";
            Build = new BuildSettings();
            Stamp stamp = new Stamp(Build.Url, Build.Token);
            xml = File.ReadAllText("Resources/SpecialCharacters.xml");
            var response = stamp.Timbrar(xml, StampTypes.v1);
            Assert.IsTrue(response.Message.Contains((string)resultExpect), (string)resultExpect);
        }
        [TestMethod]
        public void ValidateIsUTF8FromXML()
        {
            resultExpect = "Tu XML no tiene codificacion UTF-8";
            Build = new BuildSettings();
            Stamp stamp = new Stamp(Build.Url, Build.Token);
            xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/fileANSI.xml"));
            var response = stamp.Timbrar(xml, StampTypes.v1);
            Assert.AreEqual(response.Message, (string)resultExpect, (string)resultExpect);
        }
        [TestMethod]
        [Ignore]
        public void MultipleStampXMLV1byToken()
        {
            Build = new BuildSettings();
            GetEnviromentVariables();
            resultExpect = false;
            int iterations = 10;
            Stamp stamp = new Stamp(Build.Url, Build.Token);
            List<StampResponse> listXmlResult = new List<StampResponse>();
            for (int i = 0; i < iterations; i++)
            {
                string xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml"));
                var response = stamp.Timbrar(xml, StampTypes.v1);
                listXmlResult.Add(response);
            }
            if (listXmlResult != null)
                resultExpect = listXmlResult.FindAll(w => w.Status == ResponseType.Success).Count == iterations;

            Assert.IsTrue((bool)resultExpect);
        }
    }
}
