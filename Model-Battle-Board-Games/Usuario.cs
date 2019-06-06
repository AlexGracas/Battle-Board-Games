using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace BattleBoardGame.Model
{
    /// <summary>
    /// Classe de Usuário. Utilizada para registrar informações básicas de usuário.
    /// Também usada na autenticação.
    /// http://jasonwatmore.com/post/2018/09/08/aspnet-core-21-basic-authentication-tutorial-with-example-api
    /// </summary>
    [DataContract(IsReference = true)]
    public class Usuario
    {
        [DataMember]
        public string Id { get; set; }
        public IList<Batalha> Batalhas { get; set; }
        [DataMember]
        public string PrimeiroNome { get; set; }
        [DataMember]
        public string SobreNome { get; set; }
        [DataMember]
        public string Username { get; set; }

        public string Password { get; set; }
        [DataMember]
        public String Email { get; set; }

    }
}
