using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;
using Test_SW.Helpers;
using SW.Services.Retention;

namespace Test_SW_sdk_45.Services.Retention
{
    [TestClass]
    public class StampRetention_Test
    {
        /// <summary>
        /// Success.
        /// </summary>
        [TestMethod]
        public void StampRetention_Ok()
        {
            BuildSettings build = new BuildSettings();
            var xml = GetXml(build, "retencion20.xml");
            StampRetention stamp = new StampRetention(build.UrlRetention);
            var response = stamp.StampV2(xml, build.Token);
            Assert.IsTrue(response.status == "success");
            Assert.IsTrue(!String.IsNullOrEmpty(response.data.retencion));
        }
        /// <summary>
        /// Error de URL.
        /// </summary>
        [TestMethod]
        public void StampRetention_InvalidUrl_Error()
        {
            BuildSettings build = new BuildSettings();
            var xml = GetXml(build, "retencion20.xml");
            StampRetention stamp = new StampRetention(String.Empty);
            var response = stamp.StampV2(xml, build.Token);
            Assert.IsTrue(response.status == "error");
            Assert.IsTrue(!String.IsNullOrEmpty(response.message));
            Assert.IsNull(response.data);
        }
        /// <summary>
        /// XML Invalido.
        /// </summary>
        [TestMethod]
        public void StampRetention_InvalidXml_Error()
        {
            BuildSettings build = new BuildSettings();
            var xml = GetXml(build, "retencion20_Error.xml");
            StampRetention stamp = new StampRetention(build.UrlRetention);
            var response = stamp.StampV2(xml, build.Token);
            Assert.IsTrue(response.status == "error");
            Assert.IsTrue(!String.IsNullOrEmpty(response.message));
            Assert.IsNull(response.data);
        }
        /// <summary>
        /// Token invalido.
        /// </summary>
        [TestMethod]
        public void StampRetention_InvalidToken_Error()
        {
            BuildSettings build = new BuildSettings();
            var xml = GetXml(build, "retencion20_Error.xml");
            StampRetention stamp = new StampRetention(build.UrlRetention);
            var response = stamp.StampV2(xml, "");
            Assert.IsTrue(response.status == "error");
            Assert.IsTrue(!String.IsNullOrEmpty(response.message));
            Assert.IsNull(response.data);
        }
        private string GetXml(BuildSettings build, string fileName)
        {
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes(String.Format("Resources/Retenciones/{0}", fileName)));
            xml = SignTools.SigXml(xml, Convert.FromBase64String(build.Pfx), build.CerPassword, true, true);
            return xml;
        }
    }
}
