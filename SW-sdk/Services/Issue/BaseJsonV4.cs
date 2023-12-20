using SW.Helpers;
using SW.Services.Stamp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SW.Services.Issue
{
    public abstract class BaseJsonV4 : IssueService
    {
        private string _operation;
        public BaseJsonV4(string url, string user, string password, string operation, int proxyPort = 0, string proxy = null) : base(url, user, password, proxy, proxyPort)
        {
            _operation = operation;
        }
        public BaseJsonV4(string url, string token, string operation, int proxyPort = 0, string proxy = null) : base(url, token, proxy, proxyPort)
        {
            _operation = operation;
        }
        /// <summary>
        /// Servicio de timbrado de un CFDI en formato JSON. Si se especifica, se recibe un Custom Id y 
        /// puede realizar el envío del CFDI y PDF por correo, así como guardar el PDF en el ADT.
        /// </summary>
        /// <param name="json">String del CFDI en formato JSON.</param>
        /// <param name="email">Correo para el envío del CFDI y PDF.</param>
        /// <param name="customId">Identificador único asignado al comprobante.</param>
        /// <param name="pdf">Opción para habilitar la generación y guardado del PDF en el ADT.</param>
        /// <returns><see cref="StampResponseV1"/></returns>
        public virtual StampResponseV1 TimbrarJsonV1(string json, string[] email = null, string customId = null, bool pdf = false)
        {
            StampResponseHandlerV1 handler = new StampResponseHandlerV1();
            try
            {
                var request = this.RequestStampJson(json, StampTypes.v1.ToString(), _operation, email, customId, pdf);
                return handler.GetResponse(request);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
        /// <summary>
        /// Servicio de timbrado de un CFDI en formato JSON. Si se especifica, se recibe un Custom Id y 
        /// puede realizar el envío del CFDI y PDF por correo, así como guardar el PDF en el ADT.
        /// </summary>
        /// <param name="json">String del CFDI en formato JSON.</param>
        /// <param name="email">Correo para el envío del CFDI y PDF.</param>
        /// <param name="customId">Identificador único asignado al comprobante.</param>
        /// <param name="pdf">Opción para habilitar la generación y guardado del PDF en el ADT.</param>
        /// <returns><see cref="StampResponseV2"/></returns>
        public virtual StampResponseV2 TimbrarJsonV2(string json, string[] email = null, string customId = null, bool pdf = false)
        {
            StampResponseHandlerV2 handler = new StampResponseHandlerV2();
            try
            {
                var request = this.RequestStampJson(json, StampTypes.v2.ToString(), _operation, email, customId, pdf);
                return handler.GetResponse(request);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
        /// <summary>
        /// Servicio de timbrado de un CFDI en formato JSON. Si se especifica, se recibe un Custom Id y 
        /// puede realizar el envío del CFDI y PDF por correo, así como guardar el PDF en el ADT.
        /// </summary>
        /// <param name="json">String del CFDI en formato JSON.</param>
        /// <param name="email">Correo para el envío del CFDI y PDF.</param>
        /// <param name="customId">Identificador único asignado al comprobante.</param>
        /// <param name="pdf">Opción para habilitar la generación y guardado del PDF en el ADT.</param>
        /// <returns><see cref="StampResponseV3"/></returns>
        public virtual StampResponseV3 TimbrarJsonV3(string json, string[] email = null, string customId = null, bool pdf = false)
        {
            StampResponseHandlerV3 handler = new StampResponseHandlerV3();
            try
            {
                var request = this.RequestStampJson(json, StampTypes.v3.ToString(), _operation, email, customId, pdf);
                return handler.GetResponse(request);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
        /// <summary>
        /// Servicio de timbrado de un CFDI en formato JSON. Si se especifica, se recibe un Custom Id y 
        /// puede realizar el envío del CFDI y PDF por correo, así como guardar el PDF en el ADT.
        /// </summary>
        /// <param name="json">String del CFDI en formato JSON.</param>
        /// <param name="email">Correo para el envío del CFDI y PDF.</param>
        /// <param name="customId">Identificador único asignado al comprobante.</param>
        /// <param name="pdf">Opción para habilitar la generación y guardado del PDF en el ADT.</param>
        /// <returns><see cref="StampResponseV4"/></returns>
        public virtual StampResponseV4 TimbrarJsonV4(string json, string[] email = null, string customId = null, bool pdf = false)
        {
            StampResponseHandlerV4 handler = new StampResponseHandlerV4();
            try
            {
                var request = this.RequestStampJson(json, StampTypes.v4.ToString(), _operation, email, customId, pdf);
                return handler.GetResponse(request);
            }
            catch (Exception ex)
            {
                return handler.HandleException(ex);
            }
        }
    }
}
