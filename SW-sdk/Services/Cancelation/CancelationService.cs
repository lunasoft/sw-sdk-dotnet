using SW.Helpers;

namespace SW.Services.Cancelation
{
    public class CancelationService : Services
    {
        protected CancelationService(string url, string user, string password) : base(url, user, password)
        {
        }
        protected CancelationService(string url, string token) : base(url, token)
        {
        }
        public virtual CancelationResponse Cancelar(CancelationTypes cancelationTypes, string cer, string key, 
                                                                                      string password, string[] uuids)
        {
            return Cancelar(cancelationTypes, cer, key, password, uuids);
        }
    }
}
