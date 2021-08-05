namespace SW.Services.Stamp
{
    public class StampV4XML : BaseStampV4XML
    {
        public StampV4XML(string url, string urlApi, string token, int proxyPort = 0, string proxy = null) : base(url, urlApi, token, "stamp", proxy, proxyPort)
        {
        }
    }
}