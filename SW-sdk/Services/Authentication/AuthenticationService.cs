namespace SW.Services.Authentication
{
    public class AuthenticationService : Services
    {
        public AuthenticationService(string url, string user, string password) : base(url, user, password)
        {
        }
        public virtual AuthResponse GetToken()
        {
            return GetToken();
        }
    }
}
