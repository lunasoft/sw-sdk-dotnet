using SW.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SW.Services.StatusSW
{
    public class StatusServiceSW : StatusSWService
    {
        StatusSWResponseHandler _handler;
        public StatusServiceSW(string url, string user, string password, int proxyPort = 0, string proxy = null) : base(url, user, password, proxy, proxyPort)
        {
            _handler = new StatusSWResponseHandler();
        }
        public StatusServiceSW(string url, string token, int proxyPort = 0, string proxy = null) : base(url, token, proxy, proxyPort)
        {
            _handler = new StatusSWResponseHandler();
        }
        public override StatusSWResponse GetStatusSWServices(SWEnviroment swEnviroment)
        {
            return StatusRequest(swEnviroment, StatusSWType.authentication);
        }
        internal StatusSWResponse StatusRequest(SWEnviroment swEnviromentRequest, StatusSWType StatusType)
        {
            try
            {
                new Validation(Url, User, Password, Token).ValidateHeaderParameters();
                this.SetupRequest();
                Dictionary<string, string> headers = new Dictionary<string, string>() {
                    { "Authorization", "bearer " + this.Token }
                };
                var proxy = RequestHelper.ProxySettings(this.Proxy, this.ProxyPort);
                return _handler.GetResponse(GetDescription(swEnviromentRequest), headers, StatusType.ToString(), proxy);
            }
            catch (Exception e)
            {
                return _handler.HandleException(e);
            }
        }
        internal static string GetDescription(SWEnviroment Enviroment)
        {
            FieldInfo oFieldInfo = Enviroment.GetType().GetField(Enviroment.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])oFieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return Enviroment.ToString();
            }
        }
    }
}
