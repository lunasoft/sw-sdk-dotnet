using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test_SW.Helpers;
using SW.Services.Issue;
using SW.Services.Stamp;
using System.IO;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace Test_SW.Services.Issue
{
    [TestClass]
    public class IssueJson_Test
    {
        [TestMethod]
        public void IssueJsonV1()
        {
            var build = new BuildSettings();
            SW.Services.Issue.IssueJson issue = new SW.Services.Issue.IssueJson(build.Url, build.User, build.Password);
            var json = GetJson(build);
            var response = (StampResponseV1)issue.TimbrarJsonV1(json);
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio.");
        }

        [TestMethod]
        public void IssueJsonV2()
        {
            var build = new BuildSettings();
            SW.Services.Issue.IssueJson issue = new SW.Services.Issue.IssueJson(build.Url, build.User, build.Password);
            var json = GetJson(build);
            var response = (StampResponseV2)issue.TimbrarJsonV2(json);
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.tfd), "El resultado data.tfd viene vacio.");
        }

        [TestMethod]
        public void IssueJsonV3()
        {
            var build = new BuildSettings();
            SW.Services.Issue.IssueJson issue = new SW.Services.Issue.IssueJson(build.Url, build.User, build.Password);
            var json = GetJson(build);
            var response = (StampResponseV3)issue.TimbrarJsonV3(json);
            Assert.IsTrue(response.status == "success"
                && !string.IsNullOrEmpty(response.data.cfdi), "El resultado data.tfd viene vacio.");
        }

        [TestMethod]
        public void StampJsonV4byToken()
        {
            var build = new BuildSettings();
            SW.Services.Issue.IssueJson issue = new SW.Services.Issue.IssueJson(build.Url, build.Token);
            var json = GetJson(build);
            var response = (StampResponseV4)issue.TimbrarJsonV4(json);
            Assert.IsTrue(response.data != null, "El resultado data viene vacio.");
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

        static Random randomNumber = new Random(1);
        private string GetJson(BuildSettings build)
        {
            var file = Encoding.UTF8.GetString(File.ReadAllBytes("Resources/cfdi.json"));
            var json = JObject.Parse(file);
            json["Fecha"] = DateTime.Now.AddHours(-12).ToString("s");
            json["Folio"] = randomNumber.Next(100).ToString();
            return json.ToString();
        }
    }
}
