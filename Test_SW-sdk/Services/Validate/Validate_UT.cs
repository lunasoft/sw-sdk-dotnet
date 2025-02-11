using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SW.Helpers;
using SW.Services.Validate;
using Test_SW.Helpers;
using System.Xml;

namespace Test_SW.Services.Validate_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Validate_Test_ValidateXML()
        {
            var build = new BuildSettings();
            Validate validate = new Validate(build.Url, build.User, build.Password);
            var xml = GetXml(build);
            var response = (ValidateXMLResponse)validate.ValidateXML(xml);
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
            var response = (ValidateXMLResponse)validate.ValidateXML(xml,true);
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
            var response = (ValidateXMLResponse)validate.ValidateXML(xml, false);
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
            var response = (ValidateXMLResponse)validate.ValidateXML(xml);
            Assert.IsTrue(response.status == "error"
                && !string.IsNullOrEmpty(response.status), "Error al leer el documento XML. La estructura del documento no es un Xml valido y/o la codificación del documento no es UTF8. Root element is missing.");
        }

        private string GetXml(BuildSettings build)
        {
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/cfdi40_stamp.xml"));
            return xml;
        }
    }
}