using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Test_SW.Helpers
{
    public static class SignTools
    {
        static Random randomNumber = new Random(1);
        public static string SigXml(string xml, byte[] pfx, string password, string version = "4.0")
        {
            xml = SW.Tools.Fiscal.RemoverCaracteresInvalidosXml(xml);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            doc.DocumentElement.SetAttribute("Fecha", DateTime.Now.AddHours(-12).ToString("s"));
            doc.DocumentElement.SetAttribute("Folio", DateTime.Now.Ticks.ToString() + randomNumber.Next(100));
            xml = doc.OuterXml;
            xml = version != "4.0" ? SW.Tools.Sign.SellarCFDIv33(pfx, password, xml) : SW.Tools.Sign.SellarCFDIv40(pfx, password, xml);
            return xml;
        }
    }
}
