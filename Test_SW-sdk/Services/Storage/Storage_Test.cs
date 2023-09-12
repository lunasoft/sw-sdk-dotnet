using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SW.Services.Taxpayer;
using Test_SW.Helpers;
using System.Diagnostics;
using SW.Services.Storage;

namespace Test_SW.Services.Storage_Test
{
    [TestClass]
   public class Storage_Test
    {
        [TestMethod]
        public void StorageByUUID()
        {
            var build = new BuildSettings();
            Storage storage = new Storage(build.UrlApi, build.Token);
            var response = storage.GetByUUID(new Guid("9529824a-24e5-4ea6-900c-476ed11f0ea5"));

            Assert.IsTrue(response.data != null && response.status == "success");
            Assert.IsTrue(response.data.records[0].urlXml != null);
        }
        
    }
}
