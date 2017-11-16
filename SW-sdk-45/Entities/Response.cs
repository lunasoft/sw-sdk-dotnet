using System.Runtime.Serialization;

namespace SW.Entities
{
    [DataContract]
    public class Response
    {
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public string messageDetail { get; set; }
    }
}