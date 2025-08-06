using SW.Entities;
using System.Runtime.Serialization;

namespace SW.Services.StampRetention
{
    public class StampRetentionResponseV3 : Response
    {
        [DataMember]
        public Data_CFDI data { get; set; }
    }
    public class Data_CFDI
    {
        [DataMember]
        public string retencion { get; set; }
    }
}
