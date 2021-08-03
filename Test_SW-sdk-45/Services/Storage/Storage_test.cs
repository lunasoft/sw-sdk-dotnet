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
            public void Storage_Test_45_GetByUUIDByToken()
            {
                var build = new BuildSettings();
                Storage storage = new Storage(build.UrlPdf, build.Token);
                var response = storage.GetByUUID(new Guid("f8a573f3-15c7-41d2-bf68-4188f04c60e6"));
                Assert.IsTrue(response.data != null && response.status == "success");
                Assert.IsTrue(response.data.records[0].urlAckCfdi != null);
            }
        }
    }
}
