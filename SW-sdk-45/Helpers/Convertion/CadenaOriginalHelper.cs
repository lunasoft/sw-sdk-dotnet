using System;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Xsl;

namespace SW.Helpers.Convertion
{
    internal static class CadenaOriginalHelper
    {
        private static readonly Lazy<XslCompiledTransform> _xsltCadenaOriginalTfd =
            new Lazy<XslCompiledTransform>(() =>
            {
                var xslt = new XslCompiledTransform();
                xslt.Load(typeof(cadenaoriginal_TFD_1_1));
                return xslt;
            }, LazyThreadSafetyMode.PublicationOnly);

        internal static string GetCadenaOriginalTfd(string xml)
        {
            try
            {
                return TransformXml(_xsltCadenaOriginalTfd.Value, xml);
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
