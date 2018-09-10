using System;
using System.Text;
using SW.Helpers;

namespace SW.Services.Pdf
{
    public class Pdf : BasePdf
    {
        public Pdf(string url, string user, string password) : base(url, user, password, "pdf")
        {
        }
        public Pdf(string url, string token) : base(url, token, "pdf")
        {
        }
    }


}
