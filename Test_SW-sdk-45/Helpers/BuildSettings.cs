using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Test_SW.Helpers
{
    class BuildSettings
    {
        public string Url = "http://services.test.sw.com.mx";
        public string UrlApi = "http://api.test.sw.com.mx";
        public string UrlRetention = "http://pruebascfdi.smartweb.com.mx";
        public string User = "userforut@ut.com";
        public string Password = "swpassut";
        public string CerPassword = "12345678a";
        public string Token = @"T2lYQ0t4L0RHVkR4dHZ5Nkk1VHNEakZ3Y0J4Nk9GODZuRyt4cE1wVm5tbXB3YVZxTHdOdHAwVXY2NTdJb1hkREtXTzE3dk9pMmdMdkFDR2xFWFVPUXpTUm9mTG1ySXdZbFNja3FRa0RlYURqbzdzdlI2UUx1WGJiKzViUWY2dnZGbFloUDJ6RjhFTGF4M1BySnJ4cHF0YjUvbmRyWWpjTkVLN3ppd3RxL0dJPQ.T2lYQ0t4L0RHVkR4dHZ5Nkk1VHNEakZ3Y0J4Nk9GODZuRyt4cE1wVm5tbFlVcU92YUJTZWlHU3pER1kySnlXRTF4alNUS0ZWcUlVS0NhelhqaXdnWTRncklVSWVvZlFZMWNyUjVxYUFxMWFxcStUL1IzdGpHRTJqdS9Zakw2UGRDN2VkNEVBUGM1UUt3NVhZVC9QUTViWmZ6dGNJUG5JZVJ1d1hPdmFlN2s3cEp3UW5UY2hORXVWeS9mNnZ6YVZQTlg1OTdBbWJUNzJ4NHFJNVJnOFBxTEo3TGQwank2dlVwektHUmJwY2RqNGdYRG5yaTVZUTBaZ05vR1Y0Z0xsNzg5MlM0cWJUK2hRamV2bXUwcFVGM3E4SzZMNFkvVE5LTCtJZFFEVHNob05QVmRzY2dSUGxBUXBoc29JcVp1TW9MV1FkUUtTRVdROVNPTVRMYkg5dmIrM25LM3pRbDBKN2RHaEI5TDZLK2hqVUhJU3RsZ3dEeGc0NnlWUXUvZEpmc3F6c1pNZHF4YitvYzhLQ1BSWW1vejE2ZGNNVHdETitIckl3OGhVRXFSZFFGY2lQSktqQW5LRWdCNm1jT2VzQmR4TWxFRXg1NTFXZ1UzSGNobTNXbGtUaUo5cmNucnYrWXM5cVQ0Q0NlODFPaldKZjVTRHR6alNodjc0VFgwZGE.loaXVczHpJjV8E_3NYByEmxRJKHFCS0qHPOr7LJKtPM";
        public string uuid = "44FE1A46-4BFA-4F72-96CC-DBAAF4BAB129";
        public string Cer = Convert.ToBase64String(File.ReadAllBytes("Resources/CertificadosDePrueba/CSD_XIA190128J61.cer"));
        public string Key = Convert.ToBase64String(File.ReadAllBytes("Resources/CertificadosDePrueba/CSD_XIA190128J61.key"));
        public string Pfx = Convert.ToBase64String(File.ReadAllBytes("Resources/CertificadosDePrueba/CSD_XIA190128J61.pfx"));
        public string Rfc = "XIA190128J61";
        public string noCertificado = "30001000000400002443";
        public byte[] Acuse = File.ReadAllBytes("Resources/acuse.xml");
        public byte[] RelationsXML = File.ReadAllBytes("Resources/RelationsXML.xml");
        public byte[] CancelacionXML = File.ReadAllBytes("Resources/CancelacionXML.xml");

        public Dictionary<string, string> observaciones = new Dictionary<string, string>() { { "Observaciones", "Entregar de 9am a 6pm" } };
        public string templateId = "3a12dabd-66fa-4f18-af09-d1efd77ae9ce";
    }
}
