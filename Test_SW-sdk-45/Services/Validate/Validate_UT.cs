using System;
using System.Text;
using SW.Services.Validate;
using Test_SW.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Test_SW_sdk_45.Services.Validate_Test
{
    [TestClass]
    public class Validate_UT
    {
        [TestMethod]
        public void ValidateXML_UT_Ok()
        {
            var build = new BuildSettings();
            Validate validate = new Validate(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            ValidateXmlResponse response = validate.ValidateXml(xml.ToString());
            Assert.IsTrue(response.status == "success");
            Assert.IsTrue(response.statusSat == "Vigente");
            Assert.IsTrue(response.statusCodeSat == "S - Comprobante obtenido satisfactoriamente");
        }

        [TestMethod]
        public void ValidateXML_UT_Ok_With_Status()
        {
            var build = new BuildSettings();
            Validate validate = new Validate(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            ValidateXmlResponse response = validate.ValidateXml(xml.ToString(),true);
            Assert.IsTrue(response.status == "success");
            Assert.IsTrue(response.statusSat == "Vigente");
            Assert.IsTrue(response.statusCodeSat == "S - Comprobante obtenido satisfactoriamente");
        }

        [TestMethod]
        public void ValidateXML_UT_Ok_Without_Status()
        {
            var build = new BuildSettings();
            Validate validate = new Validate(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            ValidateXmlResponse response = validate.ValidateXml(xml.ToString(),false);
            Assert.IsTrue(response.status == "success");
            Assert.IsTrue(response.statusSat == "No Aplica");
            Assert.IsTrue(response.statusCodeSat == "No Aplica");
        }

        [TestMethod]
        public void Validate_Test_ValidateXMLError()
        {
            var build = new BuildSettings();
            Validate validate = new Validate(build.Url, build.User, build.Password);
            var xml = "";
            ValidateXmlResponse response = validate.ValidateXml(xml);
            Assert.IsTrue(response.status == "error"
                && !string.IsNullOrEmpty(response.status), "Error al leer el documento XML. La estructura del documento no es un Xml valido y/o la codificación del documento no es UTF8. Root element is missing.");
        }

        private object GetXml(BuildSettings build)
        {
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/cfdi40_stamp.xml"));
            return xml;
        }
    }
}