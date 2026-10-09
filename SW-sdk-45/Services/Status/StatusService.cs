using SAT.Services;
using SAT.Services.ConsultaCFDIService;
using SW.Helpers;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.ServiceModel;
using System.Text;

namespace SW.Services.Status
{
    public abstract class StatusService : SATServices
    {
        private readonly BasicHttpBinding _myBinding;
        protected StatusService(string url) : base(url)
        {
            _myBinding = GetBinding();
        }
        internal abstract Acuse StatusRequest(string rfcEmisor, string rfcReceptor, string Total, string uuid, string sello);
        internal virtual Acuse RequestStatus(string rfcEmisor, string rfcReceptor, string total, string uuid, string sello)
        {
            var consulta = "?re=" + rfcEmisor.ToUpper() + "&rr=" + rfcReceptor.ToUpper() + "&tt=" + total + "&id=" + uuid.ToUpper() + "&fe=" + sello;
            Acuse acuse = null;

            for (int i = 0; i < 3; i++)
            {
                acuse = ConsultaCFDIService(consulta);
                if (acuse != null)
                    break;
            }
            if (acuse == null)
            {
                TraceDiagnostic("El servicio SAT devolvio un acuse nulo despues de 3 consultas; no se recibio un estado del CFDI.", null);
            }
            return acuse;
        }
        /// <summary>
        /// Consulta una vez el servicio SAT y conserva el acuse aunque falle el cierre del cliente.
        /// </summary>
        /// <returns>El acuse del SAT, o null si la respuesta no lo trae.</returns>
        /// <remarks>Los errores de consulta se propagan; los de Close/Abort se registran en Trace.</remarks>
        public Acuse ConsultaCFDIService(string consulta)
        {
            var client = new ConsultaCFDIServiceClient(_myBinding, new EndpointAddress(this.Url));
            return InvokeConsulta(() => client.Consulta(consulta), client.Close, client.Abort);
        }

        internal static Acuse InvokeConsulta(Func<Acuse> query, Action close, Action abort)
        {
            Acuse acuse;
            try
            {
                acuse = query();
            }
            catch
            {
                // Close puede fallar en un canal Faulted y ocultar el error de Consulta.
                CleanupClient(abort, "No fue posible abortar el cliente SAT.");
                throw;
            }

            if (!CleanupClient(close, "No fue posible cerrar el cliente SAT; se conserva el acuse recibido."))
            {
                CleanupClient(abort, "No fue posible abortar el cliente SAT.");
            }
            return acuse;
        }

        private static bool CleanupClient(Action cleanup, string message)
        {
            try
            {
                cleanup();
                return true;
            }
            catch (Exception ex)
            {
                // Un fallo de limpieza no debe reemplazar la respuesta o excepcion.
                TraceDiagnostic(message, ex);
                return false;
            }
        }

        private static void TraceDiagnostic(string message, Exception exception)
        {
            try
            {
                Trace.TraceWarning("{0} {1}", message, exception);
            }
            catch (Exception)
            {
                // Un TraceListener del consumidor tampoco debe ocultarlos.
            }
        }
    }
}
