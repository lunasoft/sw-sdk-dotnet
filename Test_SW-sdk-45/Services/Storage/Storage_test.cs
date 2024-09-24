using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SW.Services.Taxpayer;
using Test_SW.Helpers;
using System.Diagnostics;
using SW.Services.Storage;

namespace Test_SW_sdk_45.Services.TaxpayersService
{
    class Storage_test
    {

        [TestClass]
        public class Storage_Test_45
        {
            [TestMethod]
            public void Storage_Test_45_GetByUUIDByUser()
            {
                var build = new BuildSettings();
                Storage storage = new Storage(build.UrlApi, build.Url, build.User, build.Password);
                var response = storage.GetByUUID(new Guid("23a3788a-3ac1-4b53-bb7b-b64839e6c09b"));
                Assert.IsTrue(response.data != null && response.status == "success");
                Assert.IsTrue(response.data.records[0].urlAckCfdi != null);
            }
            [TestMethod]
            public void Storage_Test_45_GetByUUIDByToken()
            {
                var build = new BuildSettings();
                Storage storage = new Storage(build.UrlApi, build.Token);
                var response = storage.GetByUUID(new Guid("23a3788a-3ac1-4b53-bb7b-b64839e6c09b"));
                Assert.IsTrue(response.data != null && response.status == "success");
                Assert.IsTrue(response.data.records[0].urlAckCfdi != null);
            }
            [TestMethod]
            public void Storage_Test_45_GetByUUIDUserError()
            {
                var build = new BuildSettings();
                Storage storage = new Storage(build.UrlApi, build.Url, "", build.Password);
                var response = storage.GetByUUID(new Guid("23a3788a-3ac1-4b53-bb7b-b64839e6c09b"));
                Assert.IsTrue(response.data == null && response.status == "error");
                Assert.IsTrue(response.message == "Falta Capturar Usuario");
            }
            [TestMethod]
            public void Storage_Test_45_GetByUUIDUrlError()
            {
                var build = new BuildSettings();
                Storage storage = new Storage("", build.Token);
                var response = storage.GetByUUID(new Guid("23a3788a-3ac1-4b53-bb7b-b64839e6c09b"));
                Assert.IsTrue(response.data == null && response.status == "error");
                Assert.IsTrue(response.message == "Falta Capturar URL");
            }
        }
    }
}
