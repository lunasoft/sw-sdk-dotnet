using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using SW.Helpers;
using Test_SW.Helpers;
using SW.Services.Relations;

namespace Test_SW.Services.Relations_Test
{
    [TestClass]
    public class Relations_Test
    {
        [TestMethod]
        public void ValidateParameters()
        {
            var resultExpect = "El UUID proporcionado inválido. Favor de verificar.";
            var build = new BuildSettings();
            Relations relations = new Relations(build.Url, build.User, build.Password);
            var response = relations.RelationsByCSD(build.Cer, build.Key, build.Rfc, build.CerPassword, "");
            Assert.IsTrue(response.messageDetail.Contains((string)resultExpect));
        }
        [TestMethod]
        public void RelationsByRfcUuid()
        {
            var build = new BuildSettings();
            Relations relations = new Relations(build.Url, build.User, build.Password);
            var response = relations.RelationsByRfcUuid(build.Rfc, "01724196-ac5a-4735-b621-e3b42bcbb459");
            Assert.IsTrue(response.message != null);
        }
        [TestMethod]
        public void RelationsByCSD()
        {
            var build = new BuildSettings();
            Relations relations = new Relations(build.Url, build.User, build.Password);
            var response = relations.RelationsByCSD(build.Cer, build.Key, build.Rfc, build.CerPassword, "01724196-ac5a-4735-b621-e3b42bcbb459");
            Assert.IsTrue(response.message != null, response.message);
        }
        [TestMethod]
        public void RelationsRejectByPfx()
        {
            var build = new BuildSettings();
            Relations relations = new Relations(build.Url, build.User, build.Password);
            var response = relations.RelationsByPFX(build.Pfx, build.Rfc, build.CerPassword, "01724196-ac5a-4735-b621-e3b42bcbb459");
            Assert.IsTrue(response.message != null, response.message);
        }
        [TestMethod]
        public void RelationsByXml()
        {
            var build = new BuildSettings();
            Relations relations = new Relations(build.Url, build.User, build.Password);
            var response = relations.RelationsByXML(build.RelationsXML);
            Assert.IsTrue(response.message != null, response.message);
        }
    }
}
