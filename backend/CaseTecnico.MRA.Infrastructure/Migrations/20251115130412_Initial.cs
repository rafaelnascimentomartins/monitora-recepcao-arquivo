using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CaseTecnico.MRA.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArquivoStatus",
                columns: table => new
                {
                    Identificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Descricao = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DataInsercao = table.Column<DateTime>(type: "datetime2(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArquivoStatus", x => x.Identificador);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Identificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Descricao = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    DataInsercao = table.Column<DateTime>(type: "datetime2(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Identificador);
                });

            migrationBuilder.CreateTable(
                name: "LogErros",
                columns: table => new
                {
                    Identificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Message = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InnerException = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataInsercao = table.Column<DateTime>(type: "datetime2(3)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogErros", x => x.Identificador);
                });

            migrationBuilder.CreateTable(
                name: "Arquivos",
                columns: table => new
                {
                    Identificador = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Estabelecimento = table.Column<int>(type: "int", nullable: false),
                    DataProcessamento = table.Column<DateTime>(type: "datetime2(3)", nullable: false),
                    PeriodoInicial = table.Column<DateTime>(type: "datetime2(3)", nullable: true),
                    PeriodoFinal = table.Column<DateTime>(type: "datetime2(3)", nullable: true),
                    Sequencia = table.Column<int>(type: "int", nullable: false),
                    EstruturaImportada = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ArquivoStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmpresaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataInsercao = table.Column<DateTime>(type: "datetime2(3)", nullable: false)
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

            migrationBuilder.InsertData(
                table: "Empresas",
                columns: new[] { "Identificador", "DataInsercao", "Descricao" },
                values: new object[,]
                {
                    { new Guid("3a86fd10-725f-4143-ae55-7f127bc85a4e"), new DateTime(2025, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "UfCard" },
                    { new Guid("73c71cf2-2681-40d8-ab7f-ceb0b65aaf86"), new DateTime(2025, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "FagammonCard" }
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arquivos");

            migrationBuilder.DropTable(
                name: "LogErros");

            migrationBuilder.DropTable(
                name: "ArquivoStatus");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
