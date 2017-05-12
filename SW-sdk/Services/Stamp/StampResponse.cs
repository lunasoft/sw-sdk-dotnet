using SW.Helpers;

namespace SW.Services.Stamp
{
    public class StampResponseV1 : Response
    {
        public Data_TFD data { get; set; }
    }
    public class StampResponseV2 : Response
    {
        public Data_CFDI_TFD data { get; set; }
    }
    public class StampResponseV3 : Response
    {
        public Data_CFDI data { get; set; }
    }

    public class StampResponseV4 : Response
    {
        public Data_Complete data { get; set; }
    }
    public class Data_TFD
    {
        public string tfd { get; set; }
    }

    public class Data_CFDI
    {
        public string cfdi { get; set; }
    }

    public class Data_CFDI_TFD : Data_TFD
    {
        public string cfdi { get; set; }
    }

    public class Data_Complete : Data_CFDI
    {
        public string cadenaOriginalSAT { get; set; }
        public string noCertificadoSAT { get; set; }
        public string noCertificadoCFDI { get; set; }
        public string uuid { get; set; }
        public string selloSAT { get; set; }
        public string selloCFDI { get; set; }
        public string fechaTimbrado { get; set; }
        public string qrCode { get; set; }
    }
}