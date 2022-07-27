using System.Runtime.Serialization;

namespace SW.Entities
{
    [DataContract]
    public class Response
    {
        /// <summary>
        /// Obtiene el valor que indica si la petición fue correcta.
        /// </summary>
        /// <returns><i>success</i> ó <i>error</i></returns>
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public string messageDetail { get; set; }
    }
}