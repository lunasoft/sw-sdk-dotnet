using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SW.Services.StatusSW
{
    public enum SWEnviroment
    {
        [Description(@"https://status-api-test.sw.com.mx/authentication")] SandboxAuthentication,
        [Description(@"https://status-api.sw.com.mx/authentication")] Authentication,
        [Description(@"https://status-api-test.sw.com.mx/stamp")] SandboxStamp,
        [Description("https://status-api.sw.com.mx/stamp")] Stamp
    }
}
