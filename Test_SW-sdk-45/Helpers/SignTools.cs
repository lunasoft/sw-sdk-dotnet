using SW.Tools;
using SW.Tools.Services.Fiscal;
using SW.Tools.Services.Sign;
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
        public static string SigXml(string xml, byte[] pfx, string password, bool setDate = true)
        {
            xml = Fiscal.RemoverCaracteresInvalidosXml(xml);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            if (setDate)
            {
                doc.DocumentElement.SetAttribute("Fecha", DateTime.Now.AddHours(-12).ToString("s"));
            }
            doc.DocumentElement.SetAttribute("Folio", DateTime.Now.Ticks.ToString() + randomNumber.Next(100));
            xml = doc.OuterXml;
            xml = Sign.SellarCFDIv40(pfx, password, xml);
            return xml;
        }
        public static string SigXmlRetention(string xml, byte[] pfx, string password, bool setDate = true)
        {
            xml = Fiscal.RemoverCaracteresInvalidosXml(xml);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            if (setDate)
            {
                doc.DocumentElement.SetAttribute("FechaExp", DateTime.Now.AddHours(-12).ToString("s"));
            }
            doc.DocumentElement.SetAttribute("FolioInt", DateTime.Now.Ticks.ToString() + randomNumber.Next(100));
            xml = doc.OuterXml;
            xml = Sign.SellarRetencionv20(pfx, password, xml);
            return xml;
        }
    }
}