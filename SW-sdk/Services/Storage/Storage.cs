using SW.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace SW.Services.Storage
{
    public class Storage : StorageService
    {
        StorageResponseHandler _handler;
        public Storage(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, proxy, proxyPort)
        {
            _handler = new StorageResponseHandler();
        }
        public StorageResponse GetByUUID(Guid uuid)
        {
            return StorageByUUID(uuid);
        }
        internal override StorageResponse StorageByUUID(Guid uuid)
        {
            StorageResponseHandler handler = new StorageResponseHandler();
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                HttpWebRequest request = this.RequestStorage(uuid);
                return handler.GetResponse(request);
            }
            catch (Exception e)
            {
                return handler.HandleException(e);
            }
        }
    }
}
