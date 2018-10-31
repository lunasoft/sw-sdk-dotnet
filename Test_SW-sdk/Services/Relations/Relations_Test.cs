using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            RelationsResponse response = relations.RelationsByCSD(build.Cer, build.Key, build.Rfc, build.CerPassword, "");
            Assert.IsTrue(response.messageDetail.Contains((string)resultExpect));
        }
        [TestMethod]
        public void RelationsByRfcUuid()
        {
            var build = new BuildSettings();
            Relations relations = new Relations(build.Url, build.User, build.Password);
            RelationsResponse response = relations.RelationsByRfcUuid(build.Rfc, "31c885c8-6dcb-4d82-9cfd-01707c828c50");
            Assert.IsTrue(response.status == "success");
        }
        [TestMethod]
        public void RelationsByCSD()
        {
            var build = new BuildSettings();
            Relations relations = new Relations(build.Url, build.User, build.Password);
            RelationsResponse response = relations.RelationsByCSD(build.Cer, build.Key, build.Rfc, build.CerPassword, "31c885c8-6dcb-4d82-9cfd-01707c828c50");
            Assert.IsTrue(response.status == "success");
        }
        [TestMethod]
        public void RelationsRejectByPfx()
        {
            var build = new BuildSettings();
            Relations relations = new Relations(build.Url, build.User, build.Password);
            RelationsResponse response = relations.RelationsByPFX(build.Pfx, build.Rfc, build.CerPassword, "31c885c8-6dcb-4d82-9cfd-01707c828c50");
            Assert.IsTrue(response.status == "success");
        }
        [TestMethod]
        public void RelationsByXml()
        {
            var build = new BuildSettings();
            Relations relations = new Relations(build.Url, build.User, build.Password);
            RelationsResponse response = relations.RelationsByXML(build.RelationsXML);
            Assert.IsTrue(response.status == "success");
        }
    }
}
