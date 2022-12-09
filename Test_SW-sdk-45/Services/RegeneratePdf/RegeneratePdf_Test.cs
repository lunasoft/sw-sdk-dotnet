using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Test_SW.Helpers;
using SW.Services.RegeneratePdf;

namespace Test_SW.Services.RegeneratePdf_Test
{
    [TestClass]
    public class RegeneratePdf_Test
    {
        [TestMethod]
        public void UT_RegeneratePdf_SuccessAuth()
        {
            var build = new BuildSettings();
            RegeneratePdf regeneratePdf = new RegeneratePdf(build.UrlApi, build.Url, build.User, build.Password);
            var response = regeneratePdf.GetByUUID(new Guid("a519df30-f591-4c33-bbb9-277df20597e9"));
            Assert.IsTrue(response.message == "Solicitud se proceso correctamente.");
        }

        [TestMethod]
        public void UT_RegeneratePdf_SuccessToken()
        {
            var build = new BuildSettings();
            RegeneratePdf regeneratePdf = new RegeneratePdf(build.UrlApi, build.Token);
            var response = regeneratePdf.GetByUUID(new Guid("3f7477b2-6cca-45af-95f9-e4ca3d8fe68d"));
            Assert.IsTrue(response.message == "Solicitud se proceso correctamente.");
        }

        [TestMethod]
        public void UT_RegeneratePdf_ErrorToken()
        {
            var build = new BuildSettings();
            RegeneratePdf regeneratePdf = new RegeneratePdf(build.UrlApi, "T2lYQ0t4L0RHVkR4dHZ5Nkk1VHNEakZ3Y0J4Nk9GODZuRyt4cE1wVm5tbXB3YVZxTHdOdHAwVXY2NTdJb1hkREtXTzE3dk9pMmdMdkFDR2xFWFVPUXpTUm9mTG1ySXdZbFNja3FRa0RlYURqbzdzdlI2UUx1WGJiKzViUWY2dnZGbFloUDJ6RjhFTGF4M1BySnJ4cHF0YjUvbmRyWWpjTkVLN3ppd3RxL0dJPQ.T2lYQ0t4L0RHVkR4dHZ5Nkk1VHNEakZ3Y0J4Nk9GODZuRyt4cE1wVm5tbFlVcU92YUJTZWlHU3pER1kySnlXRTF4alNUS0ZWcUlVS0NhelhqaXdnWTRncklVSWVvZlFZMWNyUjVxYUFxMWFxcStUL1IzdGpHRTJqdS9Zakw2UGRDN2VkNEVBUGM1UUt3NVhZVC9QUTVhK0pZV3d3VDlKY2tHNDRLbTBEMHMvaDQ4RmhpZW43cSs3cGpkYzE5VE5KS2VwbVBUa2dRYlQ5UTVhK3QySVlHbDRnRTNhRFRSL0hJS0JCY0Y3MXBERi82eTZyTkFWNFNuQm5oSW0rM2RpWmczM0ZIdnBzN3lvQTQzYm0wOStobmdyNlJ3bEk0OXBheUI2TURwKzNpd243RW9NdUpPRFc3OStYS1lBTUs1Kzl1bHNsOHRZZlJJdkFmNkV6OFNtY0MzdWtKcm5ETFZoNUQvUTVpMHRaczlBRWZzSmpwSTFZYjFWYVJBL1RrNnFpdFhiZGRpcy9aQUZpbUEzc2Y4ZFdieWFxOENHek96RFJVcXBEUzhscFJmeWVxV3grSDVxOXBhOHp4MnpuWEd4Mlo3MTNoMnYxQytuOCs1VW4zMlNEcURBakxXK2ErU3lZZk91ekJTYWhuVzY2QTRxeTV4ZEpGTFI1Mm40VUhoMEw.wmTtS2YXCNm5yukC3lo3jeKV6bNmnngMssb-Tr28g");
            var response = regeneratePdf.GetByUUID(new Guid("3f7477b2-6cca-45af-95f9-e4ca3d8fe68d"));
            Assert.IsTrue(response.status == "error");
        }

        [TestMethod]
        public void UT_RegeneratePdf_ErrorAuth()
        {
            var build = new BuildSettings();
            RegeneratePdf regeneratePdf = new RegeneratePdf(build.UrlApi, build.Url, "user", build.Password);
            var response = regeneratePdf.GetByUUID(new Guid("a519df30-f591-4c33-bbb9-277df20597e9"));
            Assert.IsTrue(response.status=="error");
        }

        [TestMethod]
        public void UT_RegeneratePdf_ErrorUuid()
        {
            var build = new BuildSettings();
            RegeneratePdf regeneratePdf = new RegeneratePdf(build.UrlApi, build.Url, build.User, build.Password);
            var response = regeneratePdf.GetByUUID(new Guid("21348cb0-a94a-466c-a8e0-abef7f35a71b"));  
            Assert.IsTrue(response.status == "error");
        }


    }
}
