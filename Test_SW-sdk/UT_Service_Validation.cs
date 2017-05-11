using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test_SW.Helpers;
using SW.Services.Stamp;
using System.IO;

namespace Test_SW
{
    /// <summary>
    /// Summary description for UT_Service_Validation
    /// </summary>
    [TestClass]
    public class UT_Service_Validation
    {
        public UT_Service_Validation()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private static void GetEnviromentVariables()
        {
            if (Environment.GetEnvironmentVariable("sw-sdk-url") != null)
            {
                Build.Url = Environment.GetEnvironmentVariable("sw-sdk-url");
            }
            if (Environment.GetEnvironmentVariable("sw-sdk-user") != null)
            {
                Build.User = Environment.GetEnvironmentVariable("sw-sdk-user");
            }
            if (Environment.GetEnvironmentVariable("sw-sdk-password") != null)
            {
                Build.Password = Environment.GetEnvironmentVariable("sw-sdk-password");
            }
            if (Environment.GetEnvironmentVariable("sw-sdk-token") != null)
            {
                Build.Token = Environment.GetEnvironmentVariable("sw-sdk-token");
            }
        }
        private static BuildSettings Build;
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes        
        [ClassInitialize()]
        public static void UT_Service_Validation_Initialize(TestContext testContext)
        {
            Build = new BuildSettings();
            GetEnviromentVariables();
        }
        #endregion
        [TestMethod]
        public void UT_Service_Validation_ErrorException()
        {
            Stamp stamp = new Stamp(Build.Url + "fakeurl", Build.User, Build.Password);
            string xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml"));
            var response = (StampResponseV1)stamp.TimbrarV1(xml);
            Assert.IsTrue(response.message == "ErrorException");
        }
        [TestMethod]
        public void UT_Service_Validation_401()
        {
            Stamp stamp = new Stamp(Build.Url, Build.Token + "FakeToken");
            string xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml"));
            var response = (StampResponseV1)stamp.TimbrarV1(xml);
            Assert.IsTrue(response.message == "401");
        }

        [TestMethod]
        public void UT_Service_Validation_404()
        {
            Stamp stamp = new Stamp(Build.Url + "/fakeurl", Build.User, Build.Password);
            string xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml"));
            var response = (StampResponseV1)stamp.TimbrarV1(xml);
            Assert.IsTrue(response.message == "404");
        }
    }
}
