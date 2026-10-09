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
        /// <summary>
        /// Codigo HTTP de la respuesta (metadato interno, fuera del contrato JSON).
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        internal int HttpStatusCode { get; set; }
        /// <summary>
        /// Respuesta sin cuerpo y lectura sin errores (metadato interno, fuera del contrato JSON).
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        internal bool HadEmptyBody { get; set; }
    }
}