using System;
using SW.Services.StatusSW;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test_SW.Helpers;
using System.Diagnostics;

namespace Test_SW_sdk_45.Services.StatusSW_test
{
    class StatusSW_test
    {
        [TestClass]
        public class StatusSW_Test_45
        {
            BuildSettings build = new BuildSettings();

            [TestMethod]
            public void StatusSW_Test_45_Authentication_User()
            {
                StatusServiceSW Service = new StatusServiceSW(build.Url, build.User, build.Password);
                StatusSWResponse statusService = Service.GetStatusSWServices(SWEnviroment.SandboxAuthentication);

                Debug.WriteLine(statusService.data.description);
                Debug.WriteLine(statusService.data.name);
                Debug.WriteLine(statusService.data.status);
                Assert.IsTrue(statusService.status == "success", statusService.message);

            }
            [TestMethod]
            public void StatusSW_Test_45_stamp_User()
            {
                StatusServiceSW Service = new StatusServiceSW(build.Url, build.User, build.Password);
                StatusSWResponse statusService = Service.GetStatusSWServices(SWEnviroment.SandboxStamp);

                Debug.WriteLine(statusService.data.description);
                Debug.WriteLine(statusService.data.name);
                Debug.WriteLine(statusService.data.status);
                Assert.IsTrue(statusService.status == "success", statusService.message);
            }
            [TestMethod]
            public void StatusSW_Test_45_Authentication_token()
            {
                StatusServiceSW Service = new StatusServiceSW(build.Url, build.Token);
                StatusSWResponse statusService = Service.GetStatusSWServices(SWEnviroment.SandboxAuthentication);

                Debug.WriteLine(statusService.data.description);
                Debug.WriteLine(statusService.data.name);
                Debug.WriteLine(statusService.data.status);
                Assert.IsTrue(statusService.status == "success", statusService.message);

            }
            [TestMethod]
            public void StatusSW_Test_45_stamp_token()
            {
                StatusServiceSW Service = new StatusServiceSW(build.Url, build.Token);
                StatusSWResponse statusService = Service.GetStatusSWServices(SWEnviroment.SandboxStamp);

                Debug.WriteLine(statusService.data.description);
                Debug.WriteLine(statusService.data.name);
                Debug.WriteLine(statusService.data.status);
                Assert.IsTrue(statusService.status == "success", statusService.message);
            }
        }
    }
}
