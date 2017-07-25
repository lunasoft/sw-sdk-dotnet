using System.Collections.Generic;
using SW.Helpers;

namespace SW.Services.Account
{
    public class AccountResponse : Response
    {        
        public Data Data { get; set; }
    }
    public partial class Data
    {
        public string idSaldoCliente { get; set; }
        public string idClienteUsuario { get; set; }
        public int saldoTimbres { get; set; }
        public int timbresUtilizados { get; set; }
        public string fechaExpiracion { get; set; }
        public bool unlimited { get; set; }
        public int unlimitimbresAsignadosted { get; set; }
    }
}