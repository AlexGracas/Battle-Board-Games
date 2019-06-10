using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BattleBoardGame.Model
{
    [DataContract(IsReference = true)]
    public class Tabuleiro
    {
        public override bool Equals(object obj)
        {
            if (obj is Tabuleiro)
                return ((Tabuleiro)obj).Id == this.Id;
            return false;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Largura { get; set; }

        [DataMember]
        public int Altura { get; set; }

        [DataMember]
        [InverseProperty("Tabuleiro")]
        public ICollection<ElementoDoExercito> ElementosDoExercito { get; set; }

        public ElementoDoExercito ObterElemento(Posicao p)
        {
            return this.ElementosDoExercito
                .Where(e => e.posicao.Equals(p))
                .FirstOrDefault();
        }
        public Posicao ObterPosicao(ElementoDoExercito elemento)
        {
            return elemento.posicao;
        }

       
        public void IniciarJogo(Exercito exercito1, Exercito exercito2)
        {
            Contract.Requires<ArgumentException>(exercito1.Nacao > 0);
            Contract.Requires<ArgumentException>(exercito2.Nacao > 0);

            for(int i=0; i< this.Largura; i++)
            {
                for( int j=0; j< this.Altura; j++)
                {
                    //Ultima ou primeira fileira?
                    Exercito exercito = (j == 0 || j == 1) ? exercito1 : exercito2;
                    ElementoDoExercito elemento = null;
                    Factory.AbstractFactoryExercito factory = 
                        Factory.AbstractFactoryExercito.CriarFactoryExercito(exercito.Nacao);

                    if (j==0 || j == this.Altura - 1)
                    {             
                        //Cria arqueiro nas posições pares e Cavaleiros nas posições impáres.
                        elemento= 
                            (i%2==0)?
                            (ElementoDoExercito)factory.CriarArqueiro(): 
                            (ElementoDoExercito)factory.CriarCavalaria();                      
                    }else if(j==1 || j == this.Altura - 2)
                    {
                        //Cria guerreiros
                        elemento = (ElementoDoExercito)factory.CriarGuerreiro();
                    }

                    //Se o elemento tiver sido instanciado criará o elemento no tabuleiro.
                    if (elemento != null)
                    {
                        exercito.Elementos.Add(elemento);
                        elemento.posicao = new Posicao(i, j);
                        elemento.Tabuleiro = this;
                    }


                }
            }
        }

        internal void MoverElemento(Movimento movimento)
        {
            //this.Casas[ObterPosicao(movimento.Elemento)] = null;
            //this.Casas[movimento.posicao] = movimento.Elemento;
            movimento.Elemento.posicao = movimento.posicao;
        }
    }
}
