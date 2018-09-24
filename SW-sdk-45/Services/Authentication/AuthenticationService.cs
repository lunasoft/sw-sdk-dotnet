
namespace SW.Services.Authentication
{
    public abstract class AuthenticationService : Services
    {

        public AuthenticationService(string url, string user, string password, string proxy, int proxyPort) : base(url, user, password, proxy, proxyPort)
        {
        }
        public abstract AuthResponse GetToken();
    }
}
