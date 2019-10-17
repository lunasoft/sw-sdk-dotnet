using SW.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW.Services.Taxpayer
{
    internal class TaxpayerResponseHandler : ResponseHandler<TaxpayerResponse>
    {
        public override TaxpayerResponse HandleException(Exception ex)
        {
            return ex.ToTaxpayerResponse();
        }
    }
}
