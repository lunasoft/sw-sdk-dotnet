using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using SW.Entities;
using SW.Helpers;

namespace SW.Services.Validate
{
    public class ValidateXMLResponse : Response
    {
        [DataMember]
        public List<Detail> detail { get; set; }
        [DataMember]
        public string cadenaOriginalSAT { get; set; }
        [DataMember]
        public string cadenaOriginalComprobante { get; set; }
        [DataMember]
        public string uuid { get; set; }
        [DataMember]
        public string statusSat { get; set; }
        [DataMember]
        public string statusCodeSat { get; set; }
    }
    public class Detail
    {
        [DataMember]
        public List<DetailNode> detail { get; set; }
        [DataMember]
        public string section { get; set; }
    }
    public class DetailNode
    {
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public string messageDetail { get; set; }
        [DataMember]
        public int type { get; set; }
    }

    public class ValidateLcoResponse : Response
    {
        [DataMember]
        public Data_Lco data { get; set; }
    }

    public class Data_Lco
    {
        [DataMember]
        public string noCertificado { get; set; }
        [DataMember]
        public string rfc { get; set; }
        [DataMember]
        public string validezObligaciones { get; set; }
        [DataMember]
        public string estatusCertificado { get; set; }
        [DataMember]
        public DateTime fechaInicio { get; set; }
        [DataMember]
        public DateTime fechaFinal { get; set; }
    }

    public class ValidateLrfcResponse : Response
    {
        [DataMember]
        public Data_LRFC data { get; set; }
    }

    public class Data_LRFC
    {
        [DataMember]
        public string contribuyenteRFC { get; set; }
        [DataMember]
        public bool sncf { get; set; }
        [DataMember]
        public bool subcontratacion { get; set; }
    }
}
