using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SW.Helpers;

namespace SW.Services.Stamp
{

    internal class StampResponseHandlerV1 : ResponseHandler<StampResponseV1>
    {
        public override StampResponseV1 HandleException(Exception ex)
        {
            return ex.ToStampResponseV1();
        }
    }
    internal class StampResponseHandlerV2 : ResponseHandler<StampResponseV2>
    {
        public override StampResponseV2 HandleException(Exception ex)
        {
            return ex.ToStampResponseV2();
        }
    }
    internal class StampResponseHandlerV3 : ResponseHandler<StampResponseV3>
    {
        public override StampResponseV3 HandleException(Exception ex)
        {
            return ex.ToStampResponseV3();
        }
    }
    internal class StampResponseHandlerV4 : ResponseHandler<StampResponseV4>
    {
        public override StampResponseV4 HandleException(Exception ex)
        {
            return ex.ToStampResponseV4();
        }
    }
    internal class StampResponseHandlerV2XML : ResponseHandler<StampResponseV2>
    {
        public override StampResponseV2 HandleException(Exception ex)
        {
            return ex.ToStampResponseV2();
        }
    }
}
