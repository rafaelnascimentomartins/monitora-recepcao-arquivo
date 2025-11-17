using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CaseTecnico.MRA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateArquivoRecepcionaod_NRecepcionado_Remove_Arquivo_ArquivoStatus_Remodelagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arquivos");

            migrationBuilder.DropTable(
                name: "ArquivoStatus");

            migrationBuilder.CreateTable(
                name: "ArquivoNaoRecepcionados",
                columns: table => new
                {
                    Identificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    EstruturaImportada = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Motivos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataInsercao = table.Column<DateTime>(type: "datetime2(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArquivoNaoRecepcionados", x => x.Identificador);
                });

            migrationBuilder.CreateTable(
                name: "ArquivoRecepcionados",
                columns: table => new
                {
                    Identificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Estabelecimento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DataProcessamento = table.Column<DateTime>(type: "datetime2(3)", nullable: false),
                    PeriodoInicial = table.Column<DateTime>(type: "datetime2(3)", nullable: true),
                    PeriodoFinal = table.Column<DateTime>(type: "datetime2(3)", nullable: true),
                    Sequencia = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EstruturaImportada = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataInsercao = table.Column<DateTime>(type: "datetime2(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArquivoRecepcionados", x => x.Identificador);
                    table.ForeignKey(
                        name: "FK_ArquivoRecepcionados_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Identificador",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArquivoRecepcionados_EmpresaId",
                table: "ArquivoRecepcionados",
                column: "EmpresaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArquivoNaoRecepcionados");

            migrationBuilder.DropTable(
                name: "ArquivoRecepcionados");

            migrationBuilder.CreateTable(
                name: "ArquivoStatus",
                columns: table => new
                {
                    Identificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    DataInsercao = table.Column<DateTime>(type: "datetime2(3)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArquivoStatus", x => x.Identificador);
                });

            migrationBuilder.CreateTable(
                name: "Arquivos",
                columns: table => new
                {
                    Identificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ArquivoStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataInsercao = table.Column<DateTime>(type: "datetime2(3)", nullable: false),
                    DataProcessamento = table.Column<DateTime>(type: "datetime2(3)", nullable: false),
                    Estabelecimento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EstruturaImportada = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PeriodoFinal = table.Column<DateTime>(type: "datetime2(3)", nullable: true),
                    PeriodoInicial = table.Column<DateTime>(type: "datetime2(3)", nullable: true),
                    Sequencia = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arquivos", x => x.Identificador);
                    table.ForeignKey(
                        name: "FK_Arquivos_ArquivoStatus_ArquivoStatusId",
                        column: x => x.ArquivoStatusId,
                        principalTable: "ArquivoStatus",
                        principalColumn: "Identificador",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Arquivos_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "Identificador",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ArquivoStatus",
                columns: new[] { "Identificador", "DataInsercao", "Descricao" },
                values: new object[,]
                {
                    { new Guid("8b44e4f4-2d8b-437f-ad3c-ee0d77ef97b0"), new DateTime(2025, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Recepcionado" },
                    { new Guid("d788bcaf-cdb2-495c-985d-355ce1157544"), new DateTime(2025, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Não Recepcionado" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arquivos_ArquivoStatusId",
                table: "Arquivos",
                column: "ArquivoStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Arquivos_EmpresaId",
                table: "Arquivos",
                column: "EmpresaId");
        }
    }
}
