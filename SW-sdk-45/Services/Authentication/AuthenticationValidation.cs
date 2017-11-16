using SW.Helpers;

namespace SW.Services.Authentication
{
    class AuthenticationValidation : Validation
    {
        public AuthenticationValidation(string url, string user, string password, string token) : base(url, user, password, token)
        {
        }
    }
}
