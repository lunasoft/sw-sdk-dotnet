using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;

namespace SW.Helpers.Convertion
{
    internal static class CadenaOriginalHelper
    {
        private static readonly object _lock = new object();
        private static XslCompiledTransform _xsltCadenaOriginalTfd;

        internal static string GetCadenaOriginalTfd(string xml)
        {
            try
            {
                return TransformXml(GetXslt(), xml);
            }
            catch (Exception e)
            {
                throw new Exception("El XML proporcionado no es valido.", e);
            }
        }

        private static XslCompiledTransform GetXslt()
        {
            if (_xsltCadenaOriginalTfd == null)
            {
                lock (_lock)
                {
                    if (_xsltCadenaOriginalTfd == null)
                    {
                        var xslt = new XslCompiledTransform();
                        xslt.Load(typeof(cadenaoriginal_TFD_1_1));
                        _xsltCadenaOriginalTfd = xslt;
                    }
                }
            }
            return _xsltCadenaOriginalTfd;
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
