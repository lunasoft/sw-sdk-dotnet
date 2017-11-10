using SW.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using SW.Entities;

namespace SW.Services.Authentication
{
    internal class AuthenticationResponseHandler : ResponseHandler<AuthResponse>
    {
        public override AuthResponse HandleException(Exception ex)
        {
            return ex.ToAuthResponse();
        }
    }
}
