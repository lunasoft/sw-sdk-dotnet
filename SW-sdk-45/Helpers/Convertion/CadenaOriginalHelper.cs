using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;

namespace SW.Helpers.Convertion
{
    internal static class CadenaOriginalHelper
    {
        internal static string GetCadenaOriginalTfd(string xml)
        {
            try
            {
                var xsltCadenaOriginal = new XslCompiledTransform();
                xsltCadenaOriginal.Load(typeof(cadenaoriginal_TFD_1_1));
                return TransformXml(xsltCadenaOriginal, xml);
            }
            catch (Exception e)
            {
                throw new Exception("El XML proporcionado no es valido.", e);
            }
        }

        private static string TransformXml(XslCompiledTransform xslt, string xml)
        {
            using (StringWriter writer = new StringWriter())
            {
                using (XmlReader xmlReader = XmlReader.Create(new StringReader(xml)))
                {
                    xslt.Transform(xmlReader, null, writer);
                }
                return writer.ToString().Trim();
            }
        }
    }
}
