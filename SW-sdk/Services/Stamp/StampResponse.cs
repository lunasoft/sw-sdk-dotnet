using SW.Entities;
using SW.Helpers;
using System.Runtime.Serialization;

namespace SW.Services.Stamp
{
    public class StampResponseV1 : Response
    {
        [DataMember]
        public Data_TFD data { get; set; }
    }
    public class StampResponseV2 : Response
    {
        [DataMember]
        public Data_CFDI_TFD data { get; set; }
    }
    public class StampResponseV3 : Response
    {
        [DataMember]
        public Data_CFDI data { get; set; }
    }

    public class StampResponseV4 : Response
    {
        [DataMember]
        public Data_Complete data { get; set; }
    }
    public class Data_TFD
    {
        [DataMember]
        public string tfd { get; set; }
    }

    public class Data_CFDI
    {
        [DataMember]
        public string cfdi { get; set; }
    }

    public class Data_CFDI_TFD : Data_TFD
    {
        [DataMember]
        public string cfdi { get; set; }
    }

    public class Data_Complete : Data_CFDI
    {
        [DataMember]
        public string cadenaOriginalSAT { get; set; }
        [DataMember]
        public string noCertificadoSAT { get; set; }
        [DataMember]
        public string noCertificadoCFDI { get; set; }
        [DataMember]
        public string uuid { get; set; }
        [DataMember]
        public string selloSAT { get; set; }
        [DataMember]
        public string selloCFDI { get; set; }
        [DataMember]
        public string fechaTimbrado { get; set; }
        [DataMember]
        public string qrCode { get; set; }
    }
}