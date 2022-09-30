using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SW.Helpers;
using SW.Services.Pdf;
using SW.Services.Stamp;
using Test_SW.Helpers;

namespace Pdf_Test.Services.Pdf_Tests
{
    [TestClass]
    public class PdfUnit
    {
        [TestMethod]
        public void UT_GeneratePdf()
        {
            var build = new BuildSettings();
            Pdf pdf = new Pdf(build.UrlApi, build.Token);
            var getXml = GetXml(build);
            var xml = StampXml(build, getXml);
            var pdfResult = pdf.GenerarPdf(xml,build.Logo, TemplatesId.cfdi40);
            Assert.IsTrue(pdfResult.status == "success");

        }
        [TestMethod]
        public void UT_GeneratePdf_Extras()
        {
            var build = new BuildSettings();
            Pdf pdf = new Pdf(build.UrlApi, build.Token);
            var getXml = GetXml(build);
            var xml = StampXml(build, getXml);
            var pdfResult = pdf.GenerarPdf(xml, build.Logo, TemplatesId.cfdi40, build.observaciones);
            Assert.IsTrue(pdfResult.status == "success");

        }
        [TestMethod]
        public void UT_GeneratePdf_Error()
        {
            var build = new BuildSettings();
            Pdf pdf = new Pdf(build.UrlApi, build.Token);
            var getXml = GetXml(build);
            var pdfResult = pdf.GenerarPdf(getXml, build.Logo, TemplatesId.cfdi40, build.observaciones);
            Assert.IsTrue(pdfResult.status == "error");

        }
        private static string GetXml(BuildSettings build)
        {
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/fileXml.xml"));
            xml = SignTools.SigXml(xml, Convert.FromBase64String(build.Pfx), build.CerPassword);
            return xml;
        }
        private static string StampXml(BuildSettings build, string xml)
        {
            Stamp stamp = new Stamp(build.Url, build.Token);
            StampResponseV2 response = stamp.TimbrarV2(xml);
            return response.data.cfdi;
        }

    }
}
