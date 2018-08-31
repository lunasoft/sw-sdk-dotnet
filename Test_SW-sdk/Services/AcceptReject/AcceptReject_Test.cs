using Microsoft.VisualStudio.TestTools.UnitTesting;
using SW.Helpers;
using Test_SW.Helpers;
using SW.Services.AcceptReject;
using System.Text;

namespace Test_SW.Services.AcceptReject_Test
{
    [TestClass]
    public class AcceptReject_Test
    {
        private string acuse = @"<?xml version='1.0' encoding='utf-8'?>
<SolicitudAceptacionRechazo xmlns:xsd='http://www.w3.org/2001/XMLSchema' 
    xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' RfcReceptor='LAN7008173R5' RfcPacEnviaSolicitud='DAL050601L35' Fecha='2018-08-22T18:38:05' 
    xmlns='http://cancelacfd.sat.gob.mx'>
    <Folios>
        <UUID>06a46e4b-b154-4c12-bb77-f9a63ed55ff2</UUID>
        <Respuesta>Aceptacion</Respuesta>
    </Folios>
    <Signature xmlns='http://www.w3.org/2000/09/xmldsig#'>
        <SignedInfo>
            <CanonicalizationMethod Algorithm='http://www.w3.org/TR/2001/REC-xml-c14n-20010315' />
            <SignatureMethod Algorithm='http://www.w3.org/2000/09/xmldsig#rsa-sha1' />
            <Reference URI=''>
                <Transforms>
                    <Transform Algorithm='http://www.w3.org/2000/09/xmldsig#enveloped-signature' />
                </Transforms>
                <DigestMethod Algorithm='http://www.w3.org/2000/09/xmldsig#sha1' />
                <DigestValue>AQ36cbqKJKHy5vaS6GhDTWtwKE4=</DigestValue>
            </Reference>
        </SignedInfo>
        <SignatureValue>HVlFUPmRLyxeztem827eaasDObRXi+oqedCNNvDyMsRizqsS99cHt5mJCEE4vWgpDGPGLrph/yd++R4aN+V562DPp9qreFkisFpEvJy5Z8o/KzG7vc5qqaD8z9ohPpRERPHvxFrIm3ryEBqnSV6zqJG02PuxkWvYonVc+B7RdsO5iAiDTMs9guUhOvHBK8BVXQHKCbUAPCp/4YepZ4LUkcdloCAMPsN0x9GaUty2RMtNJuwaRWy+5IIBUCeXXZmQhoQfS0QfPpCByt0ago5v+FocJQiYQrsUV/8mesmNw5JoOCmufQYliQFyZgsstV8+h76dU/rwLr6R8YlFOkTxKg==</SignatureValue>
        <KeyInfo>
            <X509Data>
                <X509IssuerSerial>
                    <X509IssuerName>OID.1.2.840.113549.1.9.2=Responsable: ACDMA, OID.2.5.4.45=SAT970701NN3, L=Coyoacán, S=Distrito Federal, C=MX, PostalCode=06300, STREET='Av. Hidalgo 77, Col. Guerrero', E=asisnet@pruebas.sat.gob.mx, OU=Administración de Seguridad de la Información, O=Servicio de Administración Tributaria, CN=A.C. 2 de pruebas(4096)</X509IssuerName>
                    <X509SerialNumber>3230303031303030303030333030303232383135</X509SerialNumber>
                </X509IssuerSerial>
                <X509Certificate>MIIFxTCCA62gAwIBAgIUMjAwMDEwMDAwMDAzMDAwMjI4MTUwDQYJKoZIhvcNAQELBQAwggFmMSAwHgYDVQQDDBdBLkMuIDIgZGUgcHJ1ZWJhcyg0MDk2KTEvMC0GA1UECgwmU2VydmljaW8gZGUgQWRtaW5pc3RyYWNpw7NuIFRyaWJ1dGFyaWExODA2BgNVBAsML0FkbWluaXN0cmFjacOzbiBkZSBTZWd1cmlkYWQgZGUgbGEgSW5mb3JtYWNpw7NuMSkwJwYJKoZIhvcNAQkBFhphc2lzbmV0QHBydWViYXMuc2F0LmdvYi5teDEmMCQGA1UECQwdQXYuIEhpZGFsZ28gNzcsIENvbC4gR3VlcnJlcm8xDjAMBgNVBBEMBTA2MzAwMQswCQYDVQQGEwJNWDEZMBcGA1UECAwQRGlzdHJpdG8gRmVkZXJhbDESMBAGA1UEBwwJQ295b2Fjw6FuMRUwEwYDVQQtEwxTQVQ5NzA3MDFOTjMxITAfBgkqhkiG9w0BCQIMElJlc3BvbnNhYmxlOiBBQ0RNQTAeFw0xNjEwMjUyMTUyMTFaFw0yMDEwMjUyMTUyMTFaMIGxMRowGAYDVQQDExFDSU5ERU1FWCBTQSBERSBDVjEaMBgGA1UEKRMRQ0lOREVNRVggU0EgREUgQ1YxGjAYBgNVBAoTEUNJTkRFTUVYIFNBIERFIENWMSUwIwYDVQQtExxMQU43MDA4MTczUjUgLyBGVUFCNzcwMTE3QlhBMR4wHAYDVQQFExUgLyBGVUFCNzcwMTE3TURGUk5OMDkxFDASBgNVBAsUC1BydWViYV9DRkRJMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAgvvCiCFDFVaYX7xdVRhp/38ULWto/LKDSZy1yrXKpaqFXqERJWF78YHKf3N5GBoXgzwFPuDX+5kvY5wtYNxx/Owu2shNZqFFh6EKsysQMeP5rz6kE1gFYenaPEUP9zj+h0bL3xR5aqoTsqGF24mKBLoiaK44pXBzGzgsxZishVJVM6XbzNJVonEUNbI25DhgWAd86f2aU3BmOH2K1RZx41dtTT56UsszJls4tPFODr/caWuZEuUvLp1M3nj7Dyu88mhD2f+1fA/g7kzcU/1tcpFXF/rIy93APvkU72jwvkrnprzs+SnG81+/F16ahuGsb2EZ88dKHwqxEkwzhMyTbQIDAQABox0wGzAMBgNVHRMBAf8EAjAAMAsGA1UdDwQEAwIGwDANBgkqhkiG9w0BAQsFAAOCAgEAJ/xkL8I+fpilZP+9aO8n93+20XxVomLJjeSL+Ng2ErL2GgatpLuN5JknFBkZAhxVIgMaTS23zzk1RLtRaYvH83lBH5E+M+kEjFGp14Fne1iV2Pm3vL4jeLmzHgY1Kf5HmeVrrp4PU7WQg16VpyHaJ/eonPNiEBUjcyQ1iFfkzJmnSJvDGtfQK2TiEolDJApYv0OWdm4is9Bsfi9j6lI9/T6MNZ+/LM2L/t72Vau4r7m94JDEzaO3A0wHAtQ97fjBfBiO5M8AEISAV7eZidIl3iaJJHkQbBYiiW2gikreUZKPUX0HmlnIqqQcBJhWKRu6Nqk6aZBTETLLpGrvF9OArV1JSsbdw/ZH+P88RAt5em5/gjwwtFlNHyiKG5w+UFpaZOK3gZP0su0sa6dlPeQ9EL4JlFkGqQCgSQ+NOsXqaOavgoP5VLykLwuGnwIUnuhBTVeDbzpgrg9LuF5dYp/zs+Y9ScJqe5VMAagLSYTShNtN8luV7LvxF9pgWwZdcM7lUwqJmUddCiZqdngg3vzTactMToG16gZA4CWnMgbU4E+r541+FNMpgAZNvs2CiW/eApfaaQojsZEAHDsDv4L5n3M1CC7fYjE/d61aSng1LaO6T1mh+dEfPvLzp7zyzz+UgWMhi5Cs4pcXx1eic5r7uxPoBwcCTt3YI1jKVVnV7/w=</X509Certificate>
            </X509Data>
        </KeyInfo>
    </Signature>
</SolicitudAceptacionRechazo>";
        [TestMethod]
        public void ValidateParameters()
        {
            var resultExpect = "El uuid proporcionado es inválido. Favor de verificar";
            var build = new BuildSettings();
            AcceptReject acceptReject = new AcceptReject(build.Url, build.User, build.Password);
            var response = acceptReject.AcceptByCSD(build.Cer, build.Key, build.Rfc, build.CerPassword, new AceptacionRechazoItem[] { new AceptacionRechazoItem() { uuid = ""} });
            Assert.IsTrue(response.messageDetail.Contains((string)resultExpect));
        }
        [TestMethod]
        public void AcceptRejectByRfcUuid()
        {
            var build = new BuildSettings();
            AcceptReject acceptReject = new AcceptReject(build.Url, build.User, build.Password);
            var response = acceptReject.AcceptByRfcUuid(build.Rfc, "01724196-ac5a-4735-b621-e3b42bcbb459", EnumAcceptReject.Aceptacion);
            Assert.IsTrue(response.status == "success");
        }
        [TestMethod]
        public void AcceptRejectByCSD()
        {
            var build = new BuildSettings();
            AcceptReject acceptReject = new AcceptReject(build.Url, build.User, build.Password);
            var response = acceptReject.AcceptByCSD(build.Cer, build.Key, build.Rfc, build.CerPassword, new AceptacionRechazoItem[] { new AceptacionRechazoItem() { uuid = "01724196-ac5a-4735-b621-e3b42bcbb459", action = EnumAcceptReject.Aceptacion } });
            Assert.IsTrue(response.status == "success");
        }
        [TestMethod]
        public void AcceptRejectByPfx()
        {
            var build = new BuildSettings();
            AcceptReject acceptReject = new AcceptReject(build.Url, build.User, build.Password);
            var response = acceptReject.AcceptByPFX(build.Pfx, build.Rfc, build.CerPassword, new AceptacionRechazoItem[] { new AceptacionRechazoItem() { uuid = "01724196-ac5a-4735-b621-e3b42bcbb459", action = EnumAcceptReject.Aceptacion } });
            Assert.IsTrue(response.status == "success");
        }
        [TestMethod]
        public void AcceptRejectByXml()
        {
            var build = new BuildSettings();
            AcceptReject acceptReject = new AcceptReject(build.Url, build.User, build.Password);
            var response = acceptReject.AcceptByXML(Encoding.UTF8.GetBytes(acuse), EnumAcceptReject.Aceptacion);
            Assert.IsTrue(response.status == "success");
        }
    }
}
