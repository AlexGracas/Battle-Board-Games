using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BattleBoardGame.Model
{
    [DataContract(IsReference=true)]
    public class Exercito
    {
        public override bool Equals(object obj)
        {
            if(obj is Exercito)
                return ((Exercito)obj).Id == this.Id;
            return false;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        [DataMember]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DataMember]
        [InverseProperty("Exercito")]
        public ICollection<ElementoDoExercito> Elementos { get; set; } =
            new HashSet<ElementoDoExercito>();
        [DataMember]
        public int? BatalhaId { get; set; }

        [ForeignKey("BatalhaId")]
        public Batalha Batalha { get; set; }
        [DataMember]
        public string UsuarioId { get; set; }
        [DataMember]
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; }
        [DataMember]
        public Factory.AbstractFactoryExercito.Nacao Nacao { get; set; }

        public ICollection<ElementoDoExercito> ElementosVivos { get
            {
                var resultado = from elemento in Elementos
                                where elemento.Saude > 0
                                select elemento;
                return resultado.ToList();
            }
        }
    }
}
