using Microsoft.VisualStudio.TestTools.UnitTesting;
using SW.Services.Stamp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Test_SW.Helpers;

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
            Assert.IsTrue(response.status == "error" && response.message == "No es posible obtener el url para descargar el XML");
        }
        
        private string GetXml(BuildSettings build)
        {
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/cfdi40.xml"));
            xml = SignTools.SigXml(xml, Convert.FromBase64String(build.Pfx), build.PfxPassword);
            return xml;
        }
    }
}
