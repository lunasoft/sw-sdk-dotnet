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
        [TestMethod]
        public void StampRetentionV3()
        {
            var build = new BuildSettings();
            StampRetention stamp = new StampRetention(build.Url, build.User, build.Password);
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/retention20.xml"));
            var response = (StampRetentionResponseV3)stamp.TimbrarV3(xml);
            Assert.IsTrue(response.status == "error"
               && (response.message == "307. El comprobante contiene un timbre previo." ||
                    response.message == "401 - El rango de la fecha de generación no debe de ser mayor a 72 horas para la emisión del timbre."));

        }
    }
}