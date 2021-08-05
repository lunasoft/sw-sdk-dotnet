using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace SW.Helpers
{
    public static class XmlUtils
    {
        internal static string GetUUIDFromTFD(string tfd)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(tfd);
                return doc.DocumentElement.Attributes.GetNamedItem("UUID").Value;
            }
            catch (Exception)
            {
                throw new ServicesException("No es posible obtener el UUID");
            }
        }
    }
}
