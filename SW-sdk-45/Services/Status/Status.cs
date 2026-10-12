using System;
using SW.Helpers;
using System.Net;
using System.Net.Http;
using System.Text;
using SAT.Services.ConsultaCFDIService;

namespace SW.Services.Status
{
    public class Status : StatusService
    {
        public Status(string url) : base(url)
        {
        }
        internal override Acuse StatusRequest(string rfcEmisor, string rfcReceptor, string total, string uuid, string sello)
        {
            return this.RequestStatus(rfcEmisor, rfcReceptor, total, uuid, sello);
        }
        /// <summary>
        /// Consulta el estado del CFDI; reintenta hasta tres veces solo si el SAT devuelve acuse nulo.
        /// </summary>
        /// <returns>El primer acuse, o null si ninguna de las tres respuestas lo trae.</returns>
        /// <remarks>Los errores de transporte, timeout y Fault SOAP se propagan sin reintento.</remarks>
        public Acuse GetStatusCFDI(string rfcEmisor, string rfcReceptor, string Total, string uuid, string sello)
        {
            return StatusRequest(rfcEmisor, rfcReceptor, Total, uuid, sello);
        }
    }
}
