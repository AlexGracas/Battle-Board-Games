using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BattleBoardGame.Model;
using BattleBoardGame.Model.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Battle_Board_Games.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatalhasController : ControllerBase
    {
        /*
        public BatalhasController(ModelJogosDeGuerra context)
        {
            this.ctx = context;
        }

        public ModelJogosDeGuerra ctx { get; set; }

      // GET: api/Batalhas/5
        public Batalha Get(int id)
        {
            var batalha = ctx.Batalhas.Include(b => b.ExercitoPreto)
                .Include(b => b.ExercitoPreto.Usuario)
                .Include(b => b.ExercitoBranco)
                .Include(b => b.ExercitoBranco.Usuario)
                .Include(b => b.Tabuleiro)
                .Include(b => b.Tabuleiro.ElementosDoExercito)
                .Include(b => b.Turno)
                .Include(b => b.Turno.Usuario).Where(b => b.Id == id).FirstOrDefault();
            return batalha;
        }

        [Route("Iniciar")]
        [HttpGet]
        public Batalha IniciarBatalha(int id)
        {
            var usuario = Utils.Utils.ObterUsuarioLogado(ctx);
            //Get batalha
            var batalha = ctx.Batalhas
                .Include(b => b.ExercitoPreto)
                .Include(b => b.ExercitoBranco)
                .Include(b => b.Tabuleiro)
                .Include(b => b.Turno)
                .Include(b => b.Turno.Usuario)
                .Where(b =>
                (b.ExercitoBranco.Usuario.Email == usuario.Email
                || b.ExercitoPreto.Usuario.Email == usuario.Email)
                && (b.ExercitoBranco != null && b.ExercitoPreto != null)
                && b.Id == id).FirstOrDefault();
            if (batalha == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(String.Format("Não foi possível carregar a Batalha.")),
                    ReasonPhrase = "Não foi possível carregar a batalha."
                };
                
                throw new (resp);
            }

            if (batalha.Tabuleiro == null)
            {
                batalha.Tabuleiro = new Tabuleiro();
                batalha.Tabuleiro.Altura = 8;
                batalha.Tabuleiro.Largura = 8;
            }

            if (batalha.Estado == Batalha.EstadoBatalhaEnum.NaoIniciado)
            {
                batalha.Tabuleiro.IniciarJogo(batalha.ExercitoBranco, batalha.ExercitoPreto);
                Random r = new Random();
                batalha.Turno = r.Next(100) < 50
                    ? batalha.ExercitoPreto :
                    batalha.ExercitoBranco;
                batalha.Estado = Batalha.EstadoBatalhaEnum.Iniciado;
            }
            ctx.SaveChanges();
            return batalha;
        }

        [Route("Jogar")]
        [HttpPost]
        public Batalha Jogar(Movimento movimento)
        {
            movimento.Elemento =
                ctx.ElementosDoExercitos.Find(movimento.ElementoId);
            if (movimento.Elemento == null)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(String.Format("O Elemento não existe.")),
                    ReasonPhrase = "O elemento informado para movimento não existe."
                };
                throw new HttpResponseException(resp);
            }

            movimento.Batalha =
                this.Get(movimento.BatalhaId);
            var usuario = Utils.Utils.ObterUsuarioLogado(ctx);

            if (usuario.Id == movimento.AutorId)
            {
                var batalha = Get(movimento.BatalhaId);
                if (movimento.AutorId != movimento.Elemento.Exercito.UsuarioId)
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.Forbidden)
                    {
                        Content = new StringContent(String.Format("A peça não pertence ao usuário.")),
                        ReasonPhrase = "Não foi possível executar o movimento."
                    };
                    throw new HttpResponseException(resp);
                }
                if (movimento.AutorId == batalha.Turno.UsuarioId)
                {
                    if (!batalha.Jogar(movimento))
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                        {
                            Content = new StringContent(String.Format("Não foi possível executar o movimento.")),
                            ReasonPhrase = "Não foi possível executar o movimento."
                        };
                        throw new HttpResponseException(resp);
                    }
                    batalha.Turno = null;
                    batalha.TurnoId = batalha.TurnoId == batalha.ExercitoBrancoId ?
                        batalha.ExercitoPretoId : batalha.ExercitoBrancoId;
                    ctx.SaveChanges();
                    return batalha;
                }
                else
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.Forbidden)
                    {
                        Content = new StringContent(
                            String
                            .Format("O turno atual é do adversário.")),
                        ReasonPhrase = "Você não tem permissão para executar esta ação."
                    };
                    throw new HttpResponseException(resp);
                }
            }
            else
            {
                var resp = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent(
                        String
                        .Format(
                            "O usuário tentou executar uma ação como se fosse outro usuário.")),
                    ReasonPhrase =
                    "Você não tem permissão para executar esta ação."
                };
                throw new HttpResponseException(resp);
            }
        }


        [Route("CriarNovaBatalha")]
        [HttpGet]
        public Batalha CriarNovaBatalha(AbstractFactoryExercito.Nacao Nacao)
        {

            //Obter usuário LOgado
            var usuarioLogado = Utils.Utils.ObterUsuarioLogado(ctx);
            //Verificar se existe uma batalha cujo exercito branco esteja definido
            //E exercito Preto esteja em branco
            var batalha = ctx.Batalhas
                .Include(x => x.ExercitoBranco.Usuario)
                .Where(b => b.ExercitoPreto == null &&
                    b.ExercitoBranco != null &&
                    b.ExercitoBranco.Usuario.Email != usuarioLogado.Email)
                .FirstOrDefault();
            if (batalha == null)
            {
                batalha = new Batalha();
                ctx.Batalhas.AddOrUpdate(batalha);
                ctx.SaveChanges();
            }
            batalha.CriarBatalha(Nacao, usuarioLogado);
            ctx.Batalhas.AddOrUpdate(batalha);
            ctx.SaveChanges();
            //Não iria conseguir os Ids Corretos;
            //ctx.SaveChangesAsync();
            return batalha;
        }
        */
    }
}