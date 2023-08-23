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
            Pdf pdf = new Pdf(build.UrlApi, build.Url, build.User, build.Password);
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
            var getXml = GetXml(build,"Pagos20");
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
            var getXml = GetXml(build, "Nomina12");
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
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/CFDI40_Validacion_Servicio/xml40"+ fileName+".xml"));
            xml = SignTools.SigXml(xml, Convert.FromBase64String(build.Pfx), build.PfxPassword);
            return xml;
        }
        private static string StampXml(BuildSettings build, string xml)
        {

            Issue sellar = new Issue(build.Url, build.User, build.Password);
            var response = (StampResponseV2)sellar.TimbrarV2(xml);
            return response.data.cfdi;
        }
        [TestMethod]
        public void UT_RegeneratePdf_SuccessAuth()
        {
            var build = new BuildSettings();
            Pdf regeneratePdf = new Pdf(build.UrlApi, build.Url, build.User, build.Password);
            var response = regeneratePdf.RegenerarPdf(new Guid("a519df30-f591-4c33-bbb9-277df20597e9"));
            Assert.IsTrue(response.status == "success");
            Assert.IsTrue(response.message == "Solicitud se proceso correctamente.");
        }
        [TestMethod]
        public void UT_RegeneratePdf_SuccessToken()
        {
            var build = new BuildSettings();
            Pdf regeneratePdf = new Pdf(build.UrlApi, build.Token);
            var response = regeneratePdf.RegenerarPdf(new Guid("3f7477b2-6cca-45af-95f9-e4ca3d8fe68d"));
            Assert.IsTrue(response.status == "success");
            Assert.IsTrue(response.message == "Solicitud se proceso correctamente.");
        }

        [TestMethod]
        public void UT_RegeneratePdf_ErrorToken()
        {
            var build = new BuildSettings();
            Pdf regeneratePdf = new Pdf(build.UrlApi, "T2lYQ0t4L0RHVkR4dHZ5Nkk1VHNEakZ3Y0J4Nk9GODZuRyt4cE1wVm5tbXB3YVZxTHdOdHAwVXY2NTdJb1hkREtXTzE3dk9pMmdMdkFDR2xFWFVPUXpTUm9mTG1ySXdZbFNja3FRa0RlYURqbzdzdlI2UUx1WGJiKzViUWY2dnZGbFloUDJ6RjhFTGF4M1BySnJ4cHF0YjUvbmRyWWpjTkVLN3ppd3RxL0dJPQ.T2lYQ0t4L0RHVkR4dHZ5Nkk1VHNEakZ3Y0J4Nk9GODZuRyt4cE1wVm5tbFlVcU92YUJTZWlHU3pER1kySnlXRTF4alNUS0ZWcUlVS0NhelhqaXdnWTRncklVSWVvZlFZMWNyUjVxYUFxMWFxcStUL1IzdGpHRTJqdS9Zakw2UGRDN2VkNEVBUGM1UUt3NVhZVC9QUTVhK0pZV3d3VDlKY2tHNDRLbTBEMHMvaDQ4RmhpZW43cSs3cGpkYzE5VE5KS2VwbVBUa2dRYlQ5UTVhK3QySVlHbDRnRTNhRFRSL0hJS0JCY0Y3MXBERi82eTZyTkFWNFNuQm5oSW0rM2RpWmczM0ZIdnBzN3lvQTQzYm0wOStobmdyNlJ3bEk0OXBheUI2TURwKzNpd243RW9NdUpPRFc3OStYS1lBTUs1Kzl1bHNsOHRZZlJJdkFmNkV6OFNtY0MzdWtKcm5ETFZoNUQvUTVpMHRaczlBRWZzSmpwSTFZYjFWYVJBL1RrNnFpdFhiZGRpcy9aQUZpbUEzc2Y4ZFdieWFxOENHek96RFJVcXBEUzhscFJmeWVxV3grSDVxOXBhOHp4MnpuWEd4Mlo3MTNoMnYxQytuOCs1VW4zMlNEcURBakxXK2ErU3lZZk91ekJTYWhuVzY2QTRxeTV4ZEpGTFI1Mm40VUhoMEw.wmTtS2YXCNm5yukC3lo3jeKV6bNmnngMssb-Tr28g");
            var response = regeneratePdf.RegenerarPdf(new Guid("3f7477b2-6cca-45af-95f9-e4ca3d8fe68d"));
            Assert.IsTrue(response.status == "error");
        }

        [TestMethod]
        public void UT_RegeneratePdf_ErrorAuth()
        {
            var build = new BuildSettings();
            Pdf regeneratePdf = new Pdf(build.UrlApi, build.Url, "user", build.Password);
            var response = regeneratePdf.RegenerarPdf(new Guid("a519df30-f591-4c33-bbb9-277df20597e9"));
            Assert.IsTrue(response.status == "error");
        }

        [TestMethod]
        public void UT_RegeneratePdf_ErrorUuid()
        {
            var build = new BuildSettings();
            Pdf regeneratePdf = new Pdf(build.UrlApi, build.Url, build.User, build.Password);
            var response = regeneratePdf.RegenerarPdf(new Guid("21348cb0-a94a-466c-a8e0-abef7f35a71b"));
            Assert.IsTrue(response.status == "error");
        }
    }
}
