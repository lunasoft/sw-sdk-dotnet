using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SW.Helpers
{
    public static class XmlUtils
    {
        public static string AddAddenda(string cfdiOriginal, string cfdiStamped,bool isb64)
        {
            string cfdi = cfdiStamped;
            try
            {
                cfdiOriginal = isb64 ? Encoding.UTF8.GetString(Convert.FromBase64String(cfdiOriginal)) : cfdiOriginal;
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(cfdiOriginal);
                XmlNode addenda = null;
                var elements = xmlDocument.GetElementsByTagName("cfdi:Addenda");
                if (elements != null && elements.Count > 0)
                {
                    addenda = elements[0];
                    if (addenda != null)

                        if (addenda != null && addenda.HasChildNodes)
                        {
                            XmlDocument xmlDocumentStamped = new XmlDocument();
                            xmlDocumentStamped.LoadXml(cfdiStamped);
                            var addendaEl = xmlDocumentStamped.CreateElement(addenda.Prefix, addenda.LocalName, addenda.NamespaceURI);
                            addendaEl.InnerXml = addenda.InnerXml;
                            xmlDocumentStamped.DocumentElement.AppendChild(addendaEl);
                            cfdi = xmlDocumentStamped.OuterXml;
                        }
                }
            }
            catch (Exception)
            {
                //TODO: Report Error
            }
            return cfdi;
        }
    }
}
