
namespace SW.Services.Authentication
{
    public abstract class AuthenticationService : Services
    {

        public AuthenticationService(string url, string user, string password) : base(url, user, password)
        {
        }
        public abstract AuthResponse GetToken();
    }
}
