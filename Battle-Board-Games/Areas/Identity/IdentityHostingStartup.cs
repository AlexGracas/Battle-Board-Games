using System;
using BattleBoardGames.Areas.Identity.Data;
using BattleBoardGames.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(BattleBoardGames.Areas.Identity.IdentityHostingStartup))]
namespace BattleBoardGames.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {


            builder.ConfigureServices((context, services) => {
                services.AddDbContext<BattleBoardGamesContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("IdentidadeBattleBoardGamesConnection")));

                services.AddDefaultIdentity<BattleBoardGamesUser>()
                    .AddEntityFrameworkStores<BattleBoardGamesContext>();
               
            });
        }
    }
}