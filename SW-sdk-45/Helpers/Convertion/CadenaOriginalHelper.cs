using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;

namespace SW.Helpers.Convertion
{
    /// <summary>
    /// Porte de SW.Tools.Helpers.SignUtils.GetCadenaOriginalTfd/TransformXml
    /// (únicamente estos dos métodos; el resto de SignUtils.cs en SW.Tools sí depende
    /// de BouncyCastle y no se porta aquí).
    /// Requiere el paquete NuGet cadenaoriginalTFD11.dll referenciado directamente en
    /// este proyecto (antes llegaba transitivamente vía SW.Tools).
    /// NOTA: confirmar el namespace exacto de "cadenaoriginal_TFD_1_1" contra el using
    /// real que use SignUtils.cs en tu copia de SW.Tools - en el código fuente revisado
    /// no aparece un "using" adicional, por lo que el tipo probablemente vive sin
    /// namespace (namespace global) en ese paquete.
    /// </summary>
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
