using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.StampRetention;
using Test_SW.Helpers;

namespace Test_SW.Services.StampRetention_Test
{
    [TestClass]
    public class StampRetention_Test
    {
        [Ignore]//Problema cadena SW Tools
        [TestMethod]
        public void StampRetentionV3()
        {
            var build = new BuildSettings();
            StampRetention stamp = new StampRetention(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (StampRetentionResponseV3)stamp.TimbrarV3(xml);
            Assert.IsTrue(response.status == "success"
               && !string.IsNullOrEmpty(response.data.retencion), "El resultado data.tfd viene vacio.");
        }
        private string GetXml(BuildSettings build, string fileName = null, bool setDate = true)
        {
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes(fileName ?? "Resources/cfdi40.xml"));
            xml = SignTools.SigXml(xml, Convert.FromBase64String(build.Pfx), build.PfxPassword, setDate);
            return xml;
        }
    }
}