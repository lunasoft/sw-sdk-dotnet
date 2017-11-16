using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test_SW.Helpers;
using SW.Services.Stamp;
using System.IO;

namespace Test_SW
{
    /// <summary>
    /// Summary description for UT_Service_Validation_45
    /// </summary>
    [TestClass]
    public class UT_Service_Validation_45
    {
        public UT_Service_Validation_45()
        {
            //
            // TODO: Add constructor logic here
            //
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
        public static void UT_Service_Validation_45_Initialize(TestContext testContext)
        {
            Build = new BuildSettings();
        }
        #endregion
        [TestMethod]
        public void UT_Service_Validation_45_ErrorException()
        {
            Stamp stamp = new Stamp("http://fake123999459493494949.com", Build.User, Build.Password);
            string xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml"));
            var response = (StampResponseV1)stamp.TimbrarV1(xml);
            Assert.IsTrue(response.status == "error");
        }
        [TestMethod]
        public void UT_Service_Validation_45_401()
        {
            Stamp stamp = new Stamp(Build.Url, Build.Token + "FakeToken");
            string xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml"));
            var response = (StampResponseV1)stamp.TimbrarV1(xml);
            Assert.IsTrue(response.message.Contains("401"));
        }

        [TestMethod]
        public void UT_Service_Validation_45_404()
        {
            Stamp stamp = new Stamp(Build.Url + "/fakeurl", Build.User, Build.Password);
            string xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml"));
            var response = (StampResponseV1)stamp.TimbrarV1(xml);
            Assert.IsTrue(response.message.Contains("404"));
        }

        [TestMethod]
        public void UT_Service_Validation_45_STAMPV4_BIG_XML()
        {
            Stamp stamp = new Stamp(Build.Url, Build.User, Build.Password);
            string xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/CFDI33_Validacion_Servicio/cfdi33_big.xml"));
            xml = Helpers.SignTools.SigXml(xml, Convert.FromBase64String(Build.Pfx), Build.CerPassword);
            var response = (StampResponseV4)stamp.TimbrarV4(xml);
            if (response.status == "error")
                Assert.IsTrue(response.message.Contains("72 horas"), "Error en el servicio: " + response.message + " " + response.messageDetail);
            else
            {
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.cadenaOriginalSAT), "El resultado data.cadenaOriginalSAT viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.noCertificadoSAT), "El resultado data.noCertificadoSAT viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.noCertificadoCFDI), "El resultado data.noCertificadoCFDI viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.uuid), "El resultado data.uuid viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.selloSAT), "El resultado data.selloSAT viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.selloCFDI), "El resultado data.selloCFDI viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.fechaTimbrado), "El resultado data.fechaTimbrado viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.qrCode), "El resultado data.qrCode viene vacio.");
            }
        }
        [TestMethod]
        public void UT_Service_Validation_45_STAMPV4_BIG_XML_2()
        {
            Stamp stamp = new Stamp(Build.Url, Build.User, Build.Password);
            string xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/CFDI33_Validacion_Servicio/cfdi33_big_2.xml"));
            xml = Helpers.SignTools.SigXml(xml, Convert.FromBase64String(Build.Pfx), Build.CerPassword);
            var response = (StampResponseV4)stamp.TimbrarV4(xml);
            if (response.status == "error")
                Assert.IsTrue(response.message.Contains("72 horas"), "Error en el servicio: " + response.message + " " + response.messageDetail);
            else
            {
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.cadenaOriginalSAT), "El resultado data.cadenaOriginalSAT viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.noCertificadoSAT), "El resultado data.noCertificadoSAT viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.noCertificadoCFDI), "El resultado data.noCertificadoCFDI viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.uuid), "El resultado data.uuid viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.selloSAT), "El resultado data.selloSAT viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.selloCFDI), "El resultado data.selloCFDI viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.fechaTimbrado), "El resultado data.fechaTimbrado viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.qrCode), "El resultado data.qrCode viene vacio.");
            }
        }
        [TestMethod]
        public void UT_Service_Validation_45_STAMPV4_CCE11()
        {
            Stamp stamp = new Stamp(Build.Url, Build.User, Build.Password);
            string xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/CFDI33_Validacion_Servicio/cfdi33_ComercioExterior.xml"));
            xml = Helpers.SignTools.SigXml(xml, Convert.FromBase64String(Build.Pfx), Build.CerPassword);
            var response = (StampResponseV4)stamp.TimbrarV4(xml);
            if (response.status == "error")
                Assert.IsTrue(response.message.Contains("72 horas"), "Error en el servicio: " + response.message + " " + response.messageDetail);
            else
            {
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.cadenaOriginalSAT), "El resultado data.cadenaOriginalSAT viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.noCertificadoSAT), "El resultado data.noCertificadoSAT viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.noCertificadoCFDI), "El resultado data.noCertificadoCFDI viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.uuid), "El resultado data.uuid viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.selloSAT), "El resultado data.selloSAT viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.selloCFDI), "El resultado data.selloCFDI viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.fechaTimbrado), "El resultado data.fechaTimbrado viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.qrCode), "El resultado data.qrCode viene vacio.");
            }
        }
        [TestMethod]
        public void UT_Service_Validation_45_STAMPV4_NOMINA12()
        {
            Stamp stamp = new Stamp(Build.Url, Build.User, Build.Password);
            string xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/CFDI33_Validacion_Servicio/cfdi33_nomina.xml"));
            xml = Helpers.SignTools.SigXml(xml, Convert.FromBase64String(Build.Pfx), Build.CerPassword);
            var response = (StampResponseV4)stamp.TimbrarV4(xml);
            if (response.status == "error")
                Assert.IsTrue(response.message.Contains("72 horas"), "Error en el servicio: " + response.message + " " + response.messageDetail);
            else
            {
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.cadenaOriginalSAT), "El resultado data.cadenaOriginalSAT viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.noCertificadoSAT), "El resultado data.noCertificadoSAT viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.noCertificadoCFDI), "El resultado data.noCertificadoCFDI viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.uuid), "El resultado data.uuid viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.selloSAT), "El resultado data.selloSAT viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.selloCFDI), "El resultado data.selloCFDI viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.fechaTimbrado), "El resultado data.fechaTimbrado viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.qrCode), "El resultado data.qrCode viene vacio.");
            }
        }
        [TestMethod]
        public void UT_Service_Validation_45_STAMPV4_PAGOS10()
        {
            Stamp stamp = new Stamp(Build.Url, Build.User, Build.Password);
            string xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/CFDI33_Validacion_Servicio/cfdi33_pago10.xml"));
            xml = Helpers.SignTools.SigXml(xml, Convert.FromBase64String(Build.Pfx), Build.CerPassword);
            var response = (StampResponseV4)stamp.TimbrarV4(xml);
            if (response.status == "error")
                Assert.IsTrue(response.message.Contains("72 horas"), "Error en el servicio: " + response.message + " " + response.messageDetail);
            else
            {
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.cfdi), "El resultado data.cfdi viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.cadenaOriginalSAT), "El resultado data.cadenaOriginalSAT viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.noCertificadoSAT), "El resultado data.noCertificadoSAT viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.noCertificadoCFDI), "El resultado data.noCertificadoCFDI viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.uuid), "El resultado data.uuid viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.selloSAT), "El resultado data.selloSAT viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.selloCFDI), "El resultado data.selloCFDI viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.fechaTimbrado), "El resultado data.fechaTimbrado viene vacio.");
                Assert.IsTrue(!string.IsNullOrEmpty(response.data.qrCode), "El resultado data.qrCode viene vacio.");
            }
        }
    }
}
