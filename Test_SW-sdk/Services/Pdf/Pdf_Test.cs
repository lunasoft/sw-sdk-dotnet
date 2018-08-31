using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SW.Services.Pdf;
using Test_SW.Helpers;

namespace Pdf_Test.Services.Pdf_Tests
{
    [TestClass]
    public class PdfUnit
    {
        [TestMethod]
        public void CreatePdf()
        {
            var build = new BuildSettings();
            Pdf pdf = new Pdf(build.UrlPdf, build.Token);
            var xml = GetXml(build);
            var pdfResult = pdf.GenerarPdf(xml, build.templateId, build.observaciones);

        }
        private string GetXml(BuildSettings build)
        {
            var xml = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/file.xml"));
            xml = SignTools.SigXml(xml, Convert.FromBase64String(build.Pfx), build.CerPassword);
            return xml;
        }
    }
}
