using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml;
using SW.Services.Issue;
using SW.Services.Stamp;
using SW.Tools.Services.Fiscal;
using Test_SW.Helpers;

namespace Test_SW.Services.Issue
{
    [TestClass]
    public class IssueZip_Test_45
    {
        
        [TestMethod]
        public void IssueZip_Test_45_IssueZipV1()
        {
            var build = new BuildSettings();
            IssueZip issue = new IssueZip(build.Url, build.User, build.Password);
            var zipBytes = GetZip();
            var response = issue.TimbrarZipV1(zipBytes);
            Assert.IsTrue(
                (response.status == "success" && !string.IsNullOrEmpty(response.data.tfd))
                || (response.status == "error" && response.message.Contains("307")),
                "Se esperaba timbrado exitoso o error 307 de timbre previo.");
        }

        [TestMethod]
        public void IssueZip_Test_45_IssueZipV1ByToken()
        {
            var build = new BuildSettings();
            IssueZip issue = new IssueZip(build.Url, build.Token);
            var zipBytes = GetZip();
            var response = issue.TimbrarZipV1(zipBytes);
            Assert.IsTrue(
                (response.status == "success" && !string.IsNullOrEmpty(response.data.tfd))
                || (response.status == "error" && response.message.Contains("307")),
                "Se esperaba timbrado exitoso o error 307 de timbre previo.");
        }

        [TestMethod]
        public void IssueZip_Test_45_ValidateFormatToken()
        {
            var build = new BuildSettings();
            IssueZip issue = new IssueZip(build.Url, build.Token + ".");
            var zipBytes = File.ReadAllBytes("Resources/fileIssue40.zip");
            var response = issue.TimbrarZipV1(zipBytes);
            Assert.IsTrue(response.message.Contains("El token debe contener 3 partes"));
        }

        [TestMethod]
        public void IssueZip_Test_45_ValidateEmptyToken()
        {
            var build = new BuildSettings();
            IssueZip issue = new IssueZip(build.Url, "");
            var zipBytes = File.ReadAllBytes("Resources/fileIssue40.zip");
            var response = issue.TimbrarZipV1(zipBytes);
            Assert.IsTrue(response.message.Contains("El token debe contener 3 partes"));
        }

        [TestMethod]
        public void IssueZip_Test_45_ValidateEmptyZip()
        {
            var build = new BuildSettings();
            IssueZip issue = new IssueZip(build.Url, build.Token);
            var zipBytes = File.ReadAllBytes("Resources/empty.zip");
            var response = issue.TimbrarZipV1(zipBytes);
            Assert.IsTrue(response.status == "error", "Se esperaba respuesta de error con ZIP vacio.");
        }
        static Random randomNumber = new Random(1);
        private byte[] GetZip(string zipPath = "Resources/fileIssue40.zip")
        {
            var zipBytes = File.ReadAllBytes(zipPath);
            using (var zipStream = new MemoryStream())
            {
                zipStream.Write(zipBytes, 0, zipBytes.Length);
                using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Update, true))
                {
                    var entry = archive.Entries[0];
                    var entryName = entry.FullName;
                    string xml;
                    using (var reader = new StreamReader(entry.Open(), Encoding.UTF8))
                    {
                        xml = reader.ReadToEnd();
                    }
                    xml = Fiscal.RemoverCaracteresInvalidosXml(xml);
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);
                    doc.DocumentElement.SetAttribute("Fecha", DateTime.Now.AddHours(-12).ToString("s"));
                    doc.DocumentElement.SetAttribute("Folio", DateTime.Now.Ticks.ToString() + randomNumber.Next(100));
                    entry.Delete();
                    var newEntry = archive.CreateEntry(entryName);
                    using (var writer = new StreamWriter(newEntry.Open(), Encoding.UTF8))
                    {
                        writer.Write(doc.OuterXml);
                    }
                }
                return zipStream.ToArray();
            }
        }
    }
}
