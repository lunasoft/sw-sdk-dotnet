using SW.Services.Stamp;
using SW.Tools.Helpers;
using SW.Tools.Services.Convertion;

namespace SW.Helpers
{
    internal static class ConvertionHelper
    {
        internal static StampResponseV4 ConvertV2ToV4Response(StampResponseV2 response)
        {
            StampResponseV4 responseV4 = new StampResponseV4();
            if(response.data != null && !string.IsNullOrEmpty(response.data.cfdi) && !string.IsNullOrEmpty(response.data.tfd))
            {
                string json = Serializer.SerializeJson(response);
                json = Convertion.ConvertResponseToV4(json);
                responseV4 = Serializer.DeserializeJson<StampResponseV4>(json);
            }
            responseV4.messageDetail = response.messageDetail;
            responseV4.message = response.message;
            responseV4.status = response.status;

            return responseV4;
        }
    }
}
