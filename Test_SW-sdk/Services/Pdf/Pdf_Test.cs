using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SW.Helpers;
using SW.Services.Issue;
using SW.Services.Pdf;
using SW.Services.Stamp;
using Test_SW.Helpers;

namespace Pdf_Test.Services.Pdf_Tests
{
    [TestClass]
    public class PdfUnit
    {
        [TestMethod]
        public void UT_GeneratePdf_Token()
        {
            var build = new BuildSettings();
            Pdf pdf = new Pdf(build.UrlApi, build.Token);
            var getXml = GetXml(build,null);
            var xml = StampXml(build, getXml);
            var pdfResult = pdf.GenerarPdf(xml,build.Logo, TemplatesId.cfdi40);
            Assert.IsTrue(pdfResult.status == "success");

        }
        [TestMethod]
        public void UT_GeneratePdf_User()
        {
            var build = new BuildSettings();
            Pdf pdf = new Pdf(build.Url, build.UrlApi, build.User, build.Password);
            var getXml = GetXml(build,null);
            var xml = StampXml(build, getXml);
            var pdfResult = pdf.GenerarPdf(xml, build.Logo, "cfdi40");
            Assert.IsTrue(pdfResult.status == "success");

        }
        [TestMethod]
        public void UT_GeneratePdf_Extras()
        {
            var build = new BuildSettings();
            Pdf pdf = new Pdf(build.UrlApi, build.Token);
            var getXml = GetXml(build,null);
            var xml = StampXml(build, getXml);
            var pdfResult = pdf.GenerarPdf(xml, build.Logo, TemplatesId.cfdi40, build.observaciones);
            Assert.IsTrue(pdfResult.status == "success");

        }
        [TestMethod]
        public void UT_GeneratePdf_isb64true()
        {
            var build = new BuildSettings();
            Pdf pdf = new Pdf(build.UrlApi, build.Token);
            var getXml = GetXml(build,null);
            var xml = StampXml(build, getXml);
            var xmlB64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(xml));
            var pdfResult = pdf.GenerarPdf(xmlB64, build.Logo, TemplatesId.cfdi40, null, true);
            Assert.IsTrue(pdfResult.status == "success");

        }
        [TestMethod]
        public void UT_GeneratePdf_Pagos20()
        {
            var build = new BuildSettings();
            Pdf pdf = new Pdf(build.UrlApi, build.Token);
            var getXml = GetXml(build,"pagos20");
            var xml = StampXml(build, getXml);
            var pdfResult = pdf.GenerarPdf(xml, build.Logo, TemplatesId.payment20);
            Assert.IsTrue(pdfResult.status == "success");

        }
        [TestMethod]
        public void UT_GeneratePdf_Carta_Porte20()
        {
            var build = new BuildSettings();
            Pdf pdf = new Pdf(build.UrlApi, build.Token);
            var getXml = GetXml(build, "Carta_Porte20");
            var xml = StampXml(build, getXml);
            var pdfResult = pdf.GenerarPdf(xml, build.Logo, TemplatesId.billoflading40);
            Assert.IsTrue(pdfResult.status == "success");

        }
        [TestMethod]
        public void UT_GeneratePdf_Nomina40()
        {
            var build = new BuildSettings();
            Pdf pdf = new Pdf(build.UrlApi, build.Token);
            var getXml = GetXml(build, "Nomina40");
            var xml = StampXml(build, getXml);
            var pdfResult = pdf.GenerarPdf(xml, build.Logo, TemplatesId.payroll40);
            Assert.IsTrue(pdfResult.status == "success");

        }
        [TestMethod]
        public void UT_GeneratePdf_EmptyXML_Error()
        {
            var build = new BuildSettings();
            Pdf pdf = new Pdf(build.UrlApi, build.Token);
            var pdfResult = pdf.GenerarPdf(null, build.Logo, TemplatesId.cfdi40, build.observaciones);
            Assert.IsTrue(pdfResult.status == "error");

        }
        [TestMethod]
        public void UT_GeneratePdf_NTFD_Error()
        {
            var build = new BuildSettings();
            Pdf pdf = new Pdf(build.Url, build.UrlApi, build.User, build.Password);
            var getXml = GetXml(build,null);
            var pdfResult = pdf.GenerarPdf(getXml, build.Logo, TemplatesId.cfdi40);
            Assert.IsTrue(pdfResult.status == "error");

        }
        [TestMethod]
        public void UT_GeneratePdf_invalidb64()
        {
            var build = new BuildSettings();
            Pdf pdf = new Pdf(build.UrlApi, build.Token);
            var xmlB64Expected = "JVBERi0xLjQKJaqrrK0KMSAwIG9iago8PAovQ3JlYXRvciAoQWx0b3ZhIFN0eWxlVmlzaW9uIEVudGVycHJpc2UgRWRpdGlvbiAyMDIwIHNwMSBcKHg2NFwpIFwoaHR0cDovL3d3dy5hbHRvdmEuY2";
            var pdfResult = pdf.GenerarPdf(xmlB64Expected, build.Logo, TemplatesId.cfdi40, null, true);
            Assert.IsTrue(pdfResult.status == "error");

        }
        private static string GetXml(BuildSettings build, string fileName)
        {
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/fileXml"+ fileName+".xml"));
            xml = SignTools.SigXml(xml, Convert.FromBase64String(build.Pfx), build.CerPassword);
            return xml;
        }
        private static string StampXml(BuildSettings build, string xml)
        {

            Issue sellar = new Issue(build.Url, build.User, build.Password);
            var response = (StampResponseV2)sellar.TimbrarV2(xml);
            return response.data.cfdi;
        }
       

    }
}
