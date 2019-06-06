﻿using BattleBoardGame.Model;
using BattleBoardGame.Model.DAL;
using BattleBoardGames.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BattleBoardGames.Services
{
    public class UsuarioService
    {
        ModelJogosDeGuerra _context;
        UserManager<BattleBoardGamesUser> _userManager;

        public UsuarioService(ModelJogosDeGuerra context, UserManager<BattleBoardGamesUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        [Authorize]
        public Usuario ObterUsuarioEmail(ClaimsPrincipal user)
        {

            var userName = _userManager.GetUserName(user);
            var usuario = _context.Usuarios.Where(u => u.Username == userName).FirstOrDefault();
            if(usuario == null)
            {
                usuario = new Usuario() { Username = userName };                
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
            }
            return usuario;
        }
    }
}
