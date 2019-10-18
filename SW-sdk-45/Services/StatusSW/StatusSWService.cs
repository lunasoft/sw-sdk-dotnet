using SW.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW.Services.StatusSW 
{
   public abstract class StatusSWService : Services
    {

        protected StatusSWService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        protected StatusSWService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        public abstract StatusSWResponse GetStatusSWServices(SWEnviroment urlStatus);      
    }
}
