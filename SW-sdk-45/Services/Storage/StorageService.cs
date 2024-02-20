using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW.Services.Storage
{
    public abstract class StorageService : Services
    {
        protected StorageService(string urlApi, string url, string user, string password, string proxy, int proxyPort) : base(url, urlApi, user, password, proxy, proxyPort)
        {
        }
        protected StorageService(string url, string token, string proxy, int proxyPort) : base(url, token, proxy, proxyPort)
        {
        }
        public abstract StorageResponse GetByUUID(Guid uuid);
    }
}
