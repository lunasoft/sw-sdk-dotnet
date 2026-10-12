using System;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using SW.Services.Stamp;
using SW.Helpers.Convertion;

namespace SW.Helpers
{
    internal static class ConvertionHelper
    {
        internal static StampResponseV4 ConvertV2ToV4Response(StampResponseV2 response)
        {
            if (response == null)
            {
                return new StampResponseV4 { status = "error", message = "No se ha recibido la respuesta." };
            }

            StampResponseV4 responseV4 = new StampResponseV4();
            if (response.data != null)
            {
                // Conserva primero el contenido recibido.
                responseV4.data = ConvertAvailableData(response.data);
                if (!string.IsNullOrEmpty(response.data.cfdi) && !string.IsNullOrEmpty(response.data.tfd))
                {
                    try
                    {
                        string json = JsonHelper.SerializeJson(response);
                        json = Helpers.Convertion.Convertion.ConvertResponseToV4(json);
                        responseV4 = JsonHelper.DeserializeJson<StampResponseV4>(json);
                    }
                    catch (Exception)
                    {
                        // Si el XML es invalido, devolver los datos parciales.
                    }
                }
            }
            responseV4.messageDetail = response.messageDetail;
            responseV4.message = response.message;
            responseV4.status = response.status;
            responseV4.HttpStatusCode = response.HttpStatusCode;
            responseV4.HadEmptyBody = response.HadEmptyBody;

            return responseV4;
        }

        private static Data_Complete ConvertAvailableData(Data_CFDI_TFD data)
        {
            Data_Complete result = data.tfd == null
                ? new Data_Complete { cfdi = data.cfdi }
                : new PartialStampData { cfdi = data.cfdi, Tfd = data.tfd };
            XmlDocument cfdi = ReadXml(data.cfdi);
            XmlDocument tfd = ReadXml(data.tfd);
            const string tfdPath = "//*[local-name()='TimbreFiscalDigital' and namespace-uri()='http://www.sat.gob.mx/TimbreFiscalDigital']";
            XmlNode stamp = tfd == null ? null : tfd.SelectSingleNode(tfdPath);
            XmlNode embeddedStamp = cfdi == null ? null : cfdi.SelectSingleNode(tfdPath);
            result.uuid = Attribute(stamp, "UUID") ?? Attribute(embeddedStamp, "UUID");
            result.fechaTimbrado = Attribute(stamp, "FechaTimbrado") ?? Attribute(embeddedStamp, "FechaTimbrado");
            result.selloSAT = Attribute(stamp, "SelloSAT") ?? Attribute(embeddedStamp, "SelloSAT");
            result.selloCFDI = Attribute(stamp, "SelloCFD") ?? Attribute(embeddedStamp, "SelloCFD");
            result.noCertificadoSAT = Attribute(stamp, "NoCertificadoSAT") ?? Attribute(embeddedStamp, "NoCertificadoSAT");
            result.noCertificadoCFDI = Attribute(cfdi == null ? null : cfdi.DocumentElement, "NoCertificado");
            return result;
        }

        private static XmlDocument ReadXml(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }
            try
            {
                XmlDocument document = new XmlDocument { XmlResolver = null };
                XmlReaderSettings settings = new XmlReaderSettings { DtdProcessing = DtdProcessing.Prohibit, XmlResolver = null };
                using (StringReader text = new StringReader(xml))
                using (XmlReader reader = XmlReader.Create(text, settings))
                {
                    document.Load(reader);
                }
                return document;
            }
            catch (XmlException)
            {
                return null;
            }
        }

        private static string Attribute(XmlNode node, string name)
        {
            XmlAttribute attribute = node == null || node.Attributes == null ? null : node.Attributes[name];
            return attribute == null ? null : attribute.Value;
        }

        // Data_Complete no expone TFD; se conserva al serializar la respuesta parcial.
        private sealed class PartialStampData : Data_Complete
        {
            [JsonProperty("tfd")]
            internal string Tfd { get; set; }
        }
    }
}
