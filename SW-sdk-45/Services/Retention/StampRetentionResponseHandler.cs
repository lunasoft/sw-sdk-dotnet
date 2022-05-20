using System;
using SW.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW.Services.Retention
{
    internal class StampRetentionResponseHandler : ResponseHandler<StampRetentionResponse>
    {
        internal StampRetentionResponse HandleSuccess(string result)
        {
            return new StampRetentionResponse() 
            { 
                status = "success",
                data = new Data()
                {
                    retencion = result
                }
            };
        }
        public override StampRetentionResponse HandleException(Exception ex)
        {
            return ex.ToRetentionStampResponse();
        }
    }
}
