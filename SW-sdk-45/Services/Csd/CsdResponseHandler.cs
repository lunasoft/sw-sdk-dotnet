using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SW.Helpers;

namespace SW.Services.Csd
{
    internal class CsdResponseHandler : ResponseHandler<CargaCsdResponse>
    {
        public override CargaCsdResponse HandleException(Exception ex)
        {
            return ex.ToCargaCsdResponse();
        }
    }
}
