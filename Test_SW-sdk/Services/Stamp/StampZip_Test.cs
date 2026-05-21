using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using SW.Services.Stamp;
using Test_SW.Helpers;

namespace Test_SW.Services.Stamp_Test
{
    [TestClass]
    public class StampZip_Test
    {
        
        [TestMethod]
        public void StampZipV1()
        {
            var build = new BuildSettings();
            StampZip stamp = new StampZip(build.Url, build.User, build.Password);
            var zipBytes = File.ReadAllBytes("Resources/cfdi40_stamp.zip");
            var response = stamp.TimbrarZipV1(zipBytes);
            Assert.IsTrue(
                (response.status == "success" && !string.IsNullOrEmpty(response.data.tfd))
                || (response.status == "error" && response.message.Contains("307")),
                "Se esperaba timbrado exitoso o error 307 de timbre previo.");
        }

        [TestMethod]
        public void StampZipV1ByToken()
        {
            var build = new BuildSettings();
            StampZip stamp = new StampZip(build.Url, build.Token);
            var zipBytes = File.ReadAllBytes("Resources/cfdi40_stamp.zip");
            var response = stamp.TimbrarZipV1(zipBytes);
            Assert.IsTrue(
                (response.status == "success" && !string.IsNullOrEmpty(response.data.tfd))
                || (response.status == "error" && response.message.Contains("307")),
                "Se esperaba timbrado exitoso o error 307 de timbre previo.");
        }

        [TestMethod]
        public void StampZipV1_ValidateFormatToken()
        {
            var build = new BuildSettings();
            StampZip stamp = new StampZip(build.Url, build.Token + ".");
            var zipBytes = File.ReadAllBytes("Resources/cfdi40_stamp.zip");
            var response = stamp.TimbrarZipV1(zipBytes);
            Assert.IsTrue(response.message.Contains("El token debe contener 3 partes"));
        }

        [TestMethod]
        public void StampZipV1_ValidateEmptyToken()
        {
            var build = new BuildSettings();
            StampZip stamp = new StampZip(build.Url, "");
            var zipBytes = File.ReadAllBytes("Resources/cfdi40_stamp.zip");
            var response = stamp.TimbrarZipV1(zipBytes);
            Assert.IsTrue(response.message.Contains("El token debe contener 3 partes"));
        }

        [TestMethod]
        public void StampZipV1_ValidateEmptyZip()
        {
            var build = new BuildSettings();
            StampZip stamp = new StampZip(build.Url, build.Token);
            var zipBytes = File.ReadAllBytes("Resources/empty.zip");
            var response = stamp.TimbrarZipV1(zipBytes);
            Assert.IsTrue(response.status == "error", "Se esperaba respuesta de error con ZIP vacio.");
        }
    }
}
