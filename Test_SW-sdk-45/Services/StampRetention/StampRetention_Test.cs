using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.StampRetention;
using Test_SW.Helpers;
using System.Xml;

namespace Test_SW_sdk_45.Services.StampRetention_Test
{
    [TestClass]
    public class Stamp_Test_45
    {
        [TestMethod]
        public void Stamp_Test_45_StampXMLV1()
        {
            var build = new BuildSettings();
            StampRetention stamp = new StampRetention(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampRetentionResponseV3)stamp.TimbrarV3(xml);
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.retencion), "El resultado data.retencion viene vacio.");
        }
        private string GetXml(BuildSettings build)
        {
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/retention20.xml"));
            xml = SignTools.SigXmlRetention(xml, Convert.FromBase64String(build.Pfx), build.PfxPassword);
            return xml;
        }
    }

}
