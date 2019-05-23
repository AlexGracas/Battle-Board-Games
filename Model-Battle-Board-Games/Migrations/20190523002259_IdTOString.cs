using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ModelBattleBoardGames.Migrations
{
    public partial class IdTOString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tabuleiros",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Largura = table.Column<int>(nullable: false),
                    Altura = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tabuleiros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exercitos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BatalhaId = table.Column<int>(nullable: true),
                    UsuarioId = table.Column<string>(nullable: true),
                    Nacao = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercitos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercitos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Batalhas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TabuleiroId = table.Column<int>(nullable: true),
                    ExercitoBrancoId = table.Column<int>(nullable: true),
                    ExercitoPretoId = table.Column<int>(nullable: true),
                    VencedorId = table.Column<int>(nullable: true),
                    TurnoId = table.Column<int>(nullable: true),
                    Estado = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batalhas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Batalhas_Exercitos_ExercitoBrancoId",
                        column: x => x.ExercitoBrancoId,
                        principalTable: "Exercitos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Batalhas_Exercitos_ExercitoPretoId",
                        column: x => x.ExercitoPretoId,
                        principalTable: "Exercitos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Batalhas_Tabuleiros_TabuleiroId",
                        column: x => x.TabuleiroId,
                        principalTable: "Tabuleiros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Batalhas_Exercitos_TurnoId",
                        column: x => x.TurnoId,
                        principalTable: "Exercitos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Batalhas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Batalhas_Exercitos_VencedorId",
                        column: x => x.VencedorId,
                        principalTable: "Exercitos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ElementosDoExercitos",
                columns: table => new
                {
                    AlcanceMovimento = table.Column<int>(nullable: false),
                    AlcanceAtaque = table.Column<int>(nullable: false),
                    Ataque = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Saude = table.Column<int>(nullable: false),
                    posicao_Largura = table.Column<int>(nullable: false),
                    posicao_Altura = table.Column<int>(nullable: false),
                    TabuleiroId = table.Column<int>(nullable: false),
                    ExercitoId = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    ExercitoId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementosDoExercitos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElementosDoExercitos_Exercitos_ExercitoId",
                        column: x => x.ExercitoId,
                        principalTable: "Exercitos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ElementosDoExercitos_Exercitos_ExercitoId1",
                        column: x => x.ExercitoId1,
                        principalTable: "Exercitos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ElementosDoExercitos_Tabuleiros_TabuleiroId",
                        column: x => x.TabuleiroId,
                        principalTable: "Tabuleiros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Batalhas_ExercitoBrancoId",
                table: "Batalhas",
                column: "ExercitoBrancoId",
                unique: true,
                filter: "[ExercitoBrancoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Batalhas_ExercitoPretoId",
                table: "Batalhas",
                column: "ExercitoPretoId",
                unique: true,
                filter: "[ExercitoPretoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Batalhas_TabuleiroId",
                table: "Batalhas",
                column: "TabuleiroId",
                unique: true,
                filter: "[TabuleiroId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Batalhas_TurnoId",
                table: "Batalhas",
                column: "TurnoId",
                unique: true,
                filter: "[TurnoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Batalhas_UsuarioId",
                table: "Batalhas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Batalhas_VencedorId",
                table: "Batalhas",
                column: "VencedorId",
                unique: true,
                filter: "[VencedorId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ElementosDoExercitos_ExercitoId",
                table: "ElementosDoExercitos",
                column: "ExercitoId");

            migrationBuilder.CreateIndex(
                name: "IX_ElementosDoExercitos_ExercitoId1",
                table: "ElementosDoExercitos",
                column: "ExercitoId1");

            migrationBuilder.CreateIndex(
                name: "IX_ElementosDoExercitos_TabuleiroId",
                table: "ElementosDoExercitos",
                column: "TabuleiroId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercitos_BatalhaId",
                table: "Exercitos",
                column: "BatalhaId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercitos_UsuarioId",
                table: "Exercitos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercitos_Batalhas_BatalhaId",
                table: "Exercitos",
                column: "BatalhaId",
                principalTable: "Batalhas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batalhas_Exercitos_ExercitoBrancoId",
                table: "Batalhas");

            migrationBuilder.DropForeignKey(
                name: "FK_Batalhas_Exercitos_ExercitoPretoId",
                table: "Batalhas");

            migrationBuilder.DropForeignKey(
                name: "FK_Batalhas_Exercitos_TurnoId",
                table: "Batalhas");

            migrationBuilder.DropForeignKey(
                name: "FK_Batalhas_Exercitos_VencedorId",
                table: "Batalhas");

            migrationBuilder.DropTable(
                name: "ElementosDoExercitos");

            migrationBuilder.DropTable(
                name: "Exercitos");

            migrationBuilder.DropTable(
                name: "Batalhas");

            migrationBuilder.DropTable(
                name: "Tabuleiros");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
