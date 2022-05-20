using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW.Services.Retention
{
    public class StampRetention : StampRetentionService
    {
        private StampRetentionResponseHandler _handler;
        public StampRetention(string url) : base(url)
        {
            _handler = new StampRetentionResponseHandler();
        }
        /// <summary>
        /// Timbrar Retenciones 1.0 y 2.0
        /// </summary>
        /// <param name="xml">XML Retencion.</param>
        /// <param name="token">Token de autenticación.</param>
        /// <returns></returns>
        public StampRetentionResponse StampV2(string xml, string token)
        {
            try
            {
                var response = StampRetention(xml, token);
                return _handler.HandleSuccess(response);
            }
            catch(Exception e)
            {
                return _handler.HandleException(e);
            }
        }
    }
}
