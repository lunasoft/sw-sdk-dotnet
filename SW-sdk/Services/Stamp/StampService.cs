using SW.Helpers;

namespace SW.Services.Stamp
{
    public class StampService : Services
    {
        protected StampService(string url, string user, string password) : base(url, user, password)
        {
        }
        protected StampService(string url, string token) : base(url, token)
        {
        }
        public virtual StampResponse Timbrar(string xml, StampTypes stampTypes)
        {
            return Timbrar(xml, stampTypes);
        }
        public virtual StampResponse TimbrarBase64(string xml, StampTypes stampTypes)
        {
            return TimbrarBase64(xml, stampTypes);
        }
    }
}
