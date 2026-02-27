using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SW.Services.Issue;
using SW.Services.Stamp;
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
            var zipBytes = File.ReadAllBytes("Resources/fileIssue40.zip");
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
            var zipBytes = File.ReadAllBytes("Resources/fileIssue40.zip");
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
    }
}
