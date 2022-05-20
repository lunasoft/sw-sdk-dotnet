using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW.Services.Retention
{
    public abstract class StampRetentionService
    {
        private string _url;
        protected StampRetentionService(string url)
        {
            _url = url;
        }
        internal string StampRetention(string xml, string token)
        {
            SW.WSRetention.wcfTimbradoRetencionesEndpoint1 client = new SW.WSRetention.wcfTimbradoRetencionesEndpoint1(_url);
            var response = client.TimbrarRetencionXMLV2(xml, token);
            return response;
        }
    }
}
