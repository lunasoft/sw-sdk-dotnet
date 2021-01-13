using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SW.Helpers;
using SW.Services.Stamp;
using Test_SW.Helpers;
using System.Xml;
using System.IO;

namespace Test_SW.Services.Stamp_Test
{
    [TestClass]
    class StampV4_Test
    {
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
        private string GetXml(BuildSettings build)
        {
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml"));
            xml = SignTools.SigXml(xml, Convert.FromBase64String(build.Pfx), build.CerPassword);
            return xml;
        }
    }
}
