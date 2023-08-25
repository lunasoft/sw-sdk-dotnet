using System.Collections.Generic;
using SW.Helpers;
using System.Runtime.Serialization;
using SW.Entities;

namespace SW.Services.Account.AccountBalance
{
    /// <summary>
    /// Estructura de la respuesta que se obtiene del metodo Balance de timbres.
    /// </summary>
    public class AccountResponse : Response
    {
        [DataMember]
        public Data data { get; set; }
    }
    /// <summary>
    /// Estructura de la respuesta que se obtiene en los metodos Añadir y Eliminar timbres.
    /// </summary>
    public class AccountBalanceResponse : Response
    {
        [DataMember]
        public string data { get; set; }
    }
    public partial class Data
    {
        [DataMember]
        public string idSaldoCliente { get; set; }
        [DataMember]
        public string idClienteUsuario { get; set; }
        [DataMember]
        public int saldoTimbres { get; set; }
        [DataMember]
        public int timbresUtilizados { get; set; }
        [DataMember]
        public string fechaExpiracion { get; set; }
        [DataMember]
        public bool unlimited { get; set; }
        [DataMember]
        public int timbresAsignados { get; set; }
    }
}