using System;
using System.Collections.Generic;
using System.Linq;
using Test_SW.Helpers;
using SW.Services.Csd;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test_SW_sdk.Services.Csd
{
    [TestClass]
    public class Csd_Test
    {
        [TestMethod]
        public void Csd_Test_UploadCsd()
        {
            var build = new BuildSettings();
            CsdUtils csd = new CsdUtils(build.Url, build.User, build.Password);
            var response = csd.UploadMyCsd(build.Cer, build.Key, build.CerPassword, "stamp", true);
            Assert.IsTrue(response.data != null && response.status == "success");
        }
        [TestMethod]
        public void Csd_Test_UploadCsd_EmptyCsd()
        {
            var build = new BuildSettings();
            CsdUtils csd = new CsdUtils(build.Url, build.User, build.Password);
            var response = csd.UploadMyCsd("", build.Key, build.CerPassword, "stamp", true);
            Assert.IsTrue(response.message == "El certificado o llave privada vienen vacios" && response.status == "error");
        }
        [Ignore]
        public void Csd_Test_DisableCsd()
        {
            var build = new BuildSettings();
            CsdUtils csd = new CsdUtils(build.Url, build.User, build.Password);
            var response = csd.DisableMyCsd("30001000000400002463");
            Assert.IsTrue(response.data != null && response.status == "success");
        }
        [TestMethod]
        public void Csd_Test_ListCsd()
        {
            var build = new BuildSettings();
            CsdUtils csd = new CsdUtils(build.Url, build.User, build.Password);
            var response = csd.GetListCsd();
            Assert.IsTrue(response.data != null && response.status == "success");
        }
        [TestMethod]
        public void Csd_Test_ListCsdByType()
        {
            var build = new BuildSettings();
            CsdUtils csd = new CsdUtils(build.Url, build.User, build.Password);
            var response = csd.GetListCsdByType("stamp");
            Assert.IsTrue(response.data != null && response.status == "success");
        }
        [TestMethod]
        public void Csd_Test_ListCsdByRfc()
        {
            var build = new BuildSettings();
            CsdUtils csd = new CsdUtils(build.Url, build.User, build.Password);
            var response = csd.GetListCsdByRfc(build.Rfc);
            Assert.IsTrue(response.data != null && response.status == "success");
        }
        [Ignore]
        [TestMethod]
        public void Csd_Test_SearchCsd()
        {
            var build = new BuildSettings();
            CsdUtils csd = new CsdUtils(build.Url, build.User, build.Password);
            var response = csd.SearchMyCsd("30001000000400002434");
            Assert.IsTrue(response.data != null && response.status == "success");
        }

        [TestMethod]
        public void Csd_Test_ListSearchActiveCsd()
        {
            var build = new BuildSettings();
            CsdUtils csd = new CsdUtils(build.Url, build.User, build.Password);
            var response = csd.SearchActiveCsd(build.Rfc, "stamp");
            Assert.IsTrue(response.data != null && response.status == "success");
        }
    }
}
