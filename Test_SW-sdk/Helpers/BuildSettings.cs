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
        public string UrlPdf = "http://api.test.sw.com.mx";
        public string User = "UnitTest@sw.com.mx";
        public string Password = "123456789";
        public string CerPassword = "12345678a";
        public string Token = @"T2lYQ0t4L0RHVkR4dHZ5Nkk1VHNEakZ3Y0J4Nk9GODZuRyt4cE1wVm5tbXB3YVZxTHdOdHAwVXY2NTdJb1hkREtXTzE3dk9pMmdMdkFDR2xFWFVPUXpTUm9mTG1ySXdZbFNja3FRa0RlYURqbzdzdlI2UUx1WGJiKzViUWY2dnZGbFloUDJ6RjhFTGF4M1BySnJ4cHF0YjUvbmRyWWpjTkVLN3ppd3RxL0dJPQ.T2lYQ0t4L0RHVkR4dHZ5Nkk1VHNEakZ3Y0J4Nk9GODZuRyt4cE1wVm5tbFlVcU92YUJTZWlHU3pER1kySnlXRTF4alNUS0ZWcUlVS0NhelhqaXdnWTRncklVSWVvZlFZMWNyUjVxYUFxMWFxcStUL1IzdGpHRTJqdS9Zakw2UGRoWFRkY0lFbUxGMGhuSEhuUXk0MnYrdkVqeTNaZ1RhQ3ovakU0dm1qOVZuem8zSVlIZzVrQWd1NlNWdU14d3lJc0V2N0ZLMzM5YkRKNTkydm5TOG5Fa0Y1UkFxWi9xSEk3d210cmlzQ25rM1VIRURQRGRxSFZkLy84UTBvMStRNy9YM2VtajJXbENXNnVpMWhlQzNXWkZSQ08rSjhiY1AxRlZ1UktnNXMzblFyU0RuMTFkNDdRcTQ1akxsQ0hDMzNZMzNRYlpTZWxlWWVNUlhBVmNzdGR5Uk5oRjZheVlRN2VwNGxpT2s1TEQrMjBOVFFVOFhYcGhTaThNM0JvRzVDSG1BVUpud3IyZVRlWGpmdG1Hc1hya2EreVY0VDF0anNWK3JIcjJONUppbitGRmVBY1luaG1WejVzK3VoeUVNcG5KbFFmWGorcFRrenRtMjlVSXc5QlNnajZBSHlZd3U3d0l3a2N4bVluU0hxa0Nwc3gzY2hURk1sakJDQ0tFVU4.KbI-k69Ow0QTBHEV09-mf0rc9oEaT-TqtHFA70b5G4c";

        public string uuid = "44FE1A46-4BFA-4F72-96CC-DBAAF4BAB129";

        public string Cer = Convert.ToBase64String(File.ReadAllBytes("Resources/CertificadosDePrueba/CSD_XIA190128J61.cer"));
        public string Key = Convert.ToBase64String(File.ReadAllBytes("Resources/CertificadosDePrueba/CSD_XIA190128J61.key"));
        public string Pfx = Convert.ToBase64String(File.ReadAllBytes("Resources/CertificadosDePrueba/CSD_XIA190128J61.pfx"));
        public string Rfc = "XIA190128J61";
        public string noCertificado = "20001000000300022815";
        public byte[] Acuse = File.ReadAllBytes("Resources/acuse.xml");
        public byte[] RelationsXML = File.ReadAllBytes("Resources/RelationsXML.xml");

        public Dictionary<string, string> observaciones = new Dictionary<string, string>() { { "Observaciones", "Entregar de 9am a 6pm" } };
        public string templateId = "44FE1A46-4BFA-4F72-96CC-DBAAF4BAB129";
    }
}
