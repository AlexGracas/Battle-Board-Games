using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BattleBoardGame.Model;
using BattleBoardGame.Model.DAL;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Battle_Board_Games.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BatalhasAPIController : ControllerBase
    {
        private readonly ModelJogosDeGuerra _context;

        public BatalhasAPIController(ModelJogosDeGuerra context)
        {
            _context = context;
        }

        // GET: api/BatalhasAPI
        [Authorize]
        [HttpGet]
        public IEnumerable<Batalha> GetBatalhas(bool Finalizada = false)
        {
            IEnumerable<Batalha> batalhas;
            if (Finalizada)
            {
                batalhas = _context.Batalhas.Where(b => b.Vencedor != null).ToList();
            }
            else
            {
                batalhas = _context.Batalhas.ToList();
            }
            return batalhas;
        }

        // GET: api/BatalhasAPI
        [Route("BatalhasSize")]
        public int GetBatalhasSize(bool somenteEmAndamento = false)
        {
            IEnumerable<Batalha> batalhas;
            if (somenteEmAndamento)
            {
                batalhas = _context.Batalhas.Where(b => b.Vencedor == null).ToList();
            }
            else
            {
                batalhas = _context.Batalhas.ToList();
            }
            return batalhas.Count();
        }

        // GET: api/BatalhasAPI?id=5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBatalha([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var batalha =  await _context.Batalhas.Include(b => b.ExercitoPreto)
                .Include(b => b.ExercitoPreto.Usuario)
                .Include(b => b.ExercitoBranco)
                .Include(b => b.ExercitoBranco.Usuario)
                .Include(b => b.Tabuleiro)
                .Include(b => b.Tabuleiro.ElementosDoExercito)
                .Include(b => b.Turno)
                .Include(b => b.Turno.Usuario).Where(b => b.Id == id).FirstOrDefaultAsync();

            if (batalha == null)
            {
                return NotFound();
            }

            return Ok(batalha);
        }

        [Route("IniciarBatalha")]
        [HttpGet("{id}")]
        public async Task<IActionResult> IniciarBatalha(int id)
        {
            var usuario = this.User;

            //Get batalha
            var batalha = _context.Batalhas
                .Include(b => b.ExercitoPreto)
                .Include(b => b.ExercitoBranco)
                .Include(b => b.Tabuleiro)
                .Include(b => b.Turno)
                .Include(b => b.Turno.Usuario)
                .Where(b =>
                (b.ExercitoBranco.Usuario.Email == usuario.Identity.Name
                || b.ExercitoPreto.Usuario.Email == usuario.Identity.Name)
                && (b.ExercitoBranco != null && b.ExercitoPreto != null)
                && b.Id == id).FirstOrDefault();
            if (batalha == null)
            {
                return NotFound();
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
            _context.SaveChanges();
            return Ok(batalha);
        }

        [Authorize]
        [Route("Jogar")]
        [HttpPost]
        public async Task<IActionResult> Jogar(Movimento movimento)
        {
            movimento.Elemento =
                _context.ElementosDoExercitos.Find(movimento.ElementoId);
            if (movimento.Elemento == null)
            {
                return NotFound();
            }

            movimento.Batalha =
                _context.Batalhas.Find(movimento.BatalhaId);
            var usuario = this.User.Identity;
            
            if (usuario.Name != movimento.AutorId)
            {
                return Forbid("O usuário autenticado não é o autor da jogada");
            }
            
            var batalha = movimento.Batalha;
            if (movimento.AutorId != movimento.Elemento.Exercito.UsuarioId)
            {
                //Usuário não é o dono do exercito.
                return Forbid("O jogador não é dono do exercito");
            }
            if (movimento.AutorId == batalha.Turno.UsuarioId)
            {
                if (!batalha.Jogar(movimento))
                {
                    return BadRequest("A jogada é invalida");
                }
                batalha.Turno = null;
                batalha.TurnoId = batalha.TurnoId == batalha.ExercitoBrancoId ?
                    batalha.ExercitoPretoId : batalha.ExercitoBrancoId;
                await _context.SaveChangesAsync();
                return Ok(batalha);
            }
            return BadRequest("Operação não realizada");
            
        }

        // PUT: api/BatalhasAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBatalha([FromRoute] int id, [FromBody] Batalha batalha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != batalha.Id)
            {
                return BadRequest();
            }

            _context.Entry(batalha).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatalhaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BatalhasAPI
        [HttpPost]
        public async Task<IActionResult> PostBatalha([FromBody] Batalha batalha)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Batalhas.Add(batalha);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBatalha", new { id = batalha.Id }, batalha);
        }

        [HttpGet]
        [Route("CriarBatalha")]
        public async Task<IActionResult> CriarBatalha()
        {
            var batalha = _context.Batalhas.FirstOrDefault(b =>
            (b.ExercitoBrancoId == null 
            || b.ExercitoPretoId== null) &&
            (b.ExercitoBranco.UsuarioId != User.Identity.Name
            && b.ExercitoPreto.UsuarioId != User.Identity.Name));

            var usuario = _context
                .Usuarios
                .FirstOrDefault(u => u.Email == User.Identity.Name);

            if (usuario != null)
            {
                usuario =
                    new Usuario()
                    {
                        Id = User.Identity.Name,
                        Email = User.Identity.Name
                    };
                _context.Add(usuario);
                _context.SaveChanges();
            }

            if(batalha == null)
            {
                batalha = new Batalha();
                _context.Add(batalha);
            }        
            Exercito e = new Exercito();
            e.UsuarioId = User.Identity.Name;
            if(batalha.ExercitoBrancoId == null)
            {
                batalha.ExercitoBranco = e;
            }
            else
            {
                batalha.ExercitoPreto = e;
            }
            _context.SaveChanges();
            return Ok(batalha);
        }



        // DELETE: api/BatalhasAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBatalha([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var batalha = await _context.Batalhas.FindAsync(id);
            if (batalha == null)
            {
                return NotFound();
            }

            _context.Batalhas.Remove(batalha);
            await _context.SaveChangesAsync();

            return Ok(batalha);
        }

        private bool BatalhaExists(int id)
        {
            return _context.Batalhas.Any(e => e.Id == id);
        }
    }
}