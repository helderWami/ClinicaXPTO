using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClinicaXPTO.DAL.Migrations
{
    /// <inheritdoc />
    public partial class PrimeiraMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Profissionais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCompleto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Especialidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NumeroOrdem = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profissionais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubsistemasSaude",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubsistemasSaude", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoActoClinicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Codigo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DuracaoPadrao = table.Column<TimeSpan>(type: "time", nullable: true),
                    Preco = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    Observacoes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoActoClinicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utilizadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Perfil = table.Column<int>(type: "int", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilizadores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Utentes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtilizadorId = table.Column<int>(type: "int", nullable: true),
                    NumeroUtente = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Fotografia = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NomeCompleto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Genero = table.Column<int>(type: "int", nullable: false),
                    Telemovel = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    EmailContacto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Morada = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoUtente = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utentes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Utentes_Utilizadores_UtilizadorId",
                        column: x => x.UtilizadorId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PedidoMarcacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UtenteId = table.Column<int>(type: "int", nullable: false),
                    DataPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InicioIntervalo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FimIntervalo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataHoraAgendada = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AgendadoPorId = table.Column<int>(type: "int", nullable: true),
                    DataAgendamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RealizadoPorId = table.Column<int>(type: "int", nullable: true),
                    DataRealizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoMarcacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidoMarcacoes_Utentes_UtenteId",
                        column: x => x.UtenteId,
                        principalTable: "Utentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoMarcacoes_Utilizadores_AgendadoPorId",
                        column: x => x.AgendadoPorId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PedidoMarcacoes_Utilizadores_RealizadoPorId",
                        column: x => x.RealizadoPorId,
                        principalTable: "Utilizadores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ItemPedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoMarcacaoId = table.Column<int>(type: "int", nullable: false),
                    TipoActoClinicoId = table.Column<int>(type: "int", nullable: false),
                    SubsistemaSaudeId = table.Column<int>(type: "int", nullable: false),
                    ProfissionalId = table.Column<int>(type: "int", nullable: true),
                    HorarioSolicitado = table.Column<TimeSpan>(type: "time", nullable: true),
                    Observacoes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemPedidos_PedidoMarcacoes_PedidoMarcacaoId",
                        column: x => x.PedidoMarcacaoId,
                        principalTable: "PedidoMarcacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemPedidos_Profissionais_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "Profissionais",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ItemPedidos_SubsistemasSaude_SubsistemaSaudeId",
                        column: x => x.SubsistemaSaudeId,
                        principalTable: "SubsistemasSaude",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemPedidos_TipoActoClinicos_TipoActoClinicoId",
                        column: x => x.TipoActoClinicoId,
                        principalTable: "TipoActoClinicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedidos_PedidoMarcacaoId",
                table: "ItemPedidos",
                column: "PedidoMarcacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedidos_ProfissionalId",
                table: "ItemPedidos",
                column: "ProfissionalId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedidos_SubsistemaSaudeId",
                table: "ItemPedidos",
                column: "SubsistemaSaudeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPedidos_TipoActoClinicoId",
                table: "ItemPedidos",
                column: "TipoActoClinicoId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoMarcacoes_AgendadoPorId",
                table: "PedidoMarcacoes",
                column: "AgendadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoMarcacoes_RealizadoPorId",
                table: "PedidoMarcacoes",
                column: "RealizadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoMarcacoes_UtenteId",
                table: "PedidoMarcacoes",
                column: "UtenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Utentes_UtilizadorId",
                table: "Utentes",
                column: "UtilizadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemPedidos");

            migrationBuilder.DropTable(
                name: "PedidoMarcacoes");

            migrationBuilder.DropTable(
                name: "Profissionais");

            migrationBuilder.DropTable(
                name: "SubsistemasSaude");

            migrationBuilder.DropTable(
                name: "TipoActoClinicos");

            migrationBuilder.DropTable(
                name: "Utentes");

            migrationBuilder.DropTable(
                name: "Utilizadores");
        }
    }
}
