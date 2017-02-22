using SW.Helpers;

namespace SW.Services.Authentication
{
    public class AuthResponse : Response
    {
        public new Data Data { get; set; }
    }
    public partial class Data
    {
        public string token { get; set; }
    }
}