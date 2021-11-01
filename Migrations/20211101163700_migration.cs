using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionPersonas.Migrations
{
    public partial class migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CantidadGrupos",
                table: "Personas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "TotalAporte",
                table: "Personas",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateTable(
                name: "TipoAporte",
                columns: table => new
                {
                    TipoAporteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: true),
                    BaseMonto = table.Column<float>(type: "REAL", nullable: false),
                    MetaMonto = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoAporte", x => x.TipoAporteId);
                });

            migrationBuilder.CreateTable(
                name: "Aportes",
                columns: table => new
                {
                    AporteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PersonaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Concepto = table.Column<string>(type: "TEXT", nullable: true),
                    Monto = table.Column<float>(type: "REAL", nullable: false),
                    TipoAporteId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aportes", x => x.AporteId);
                    table.ForeignKey(
                        name: "FK_Aportes_TipoAporte_TipoAporteId",
                        column: x => x.TipoAporteId,
                        principalTable: "TipoAporte",
                        principalColumn: "TipoAporteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AporteDetalle",
                columns: table => new
                {
                    IdDetalle = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AporteId = table.Column<int>(type: "INTEGER", nullable: false),
                    TipoAporteId = table.Column<int>(type: "INTEGER", nullable: false),
                    Monto = table.Column<float>(type: "REAL", nullable: false),
                    PersonaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AporteDetalle", x => x.IdDetalle);
                    table.ForeignKey(
                        name: "FK_AporteDetalle_Aportes_AporteId",
                        column: x => x.AporteId,
                        principalTable: "Aportes",
                        principalColumn: "AporteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AporteDetalle_Personas_PersonaId",
                        column: x => x.PersonaId,
                        principalTable: "Personas",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AporteDetalle_TipoAporte_TipoAporteId",
                        column: x => x.TipoAporteId,
                        principalTable: "TipoAporte",
                        principalColumn: "TipoAporteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GruposDetalle_PersonaId",
                table: "GruposDetalle",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_AporteDetalle_AporteId",
                table: "AporteDetalle",
                column: "AporteId");

            migrationBuilder.CreateIndex(
                name: "IX_AporteDetalle_PersonaId",
                table: "AporteDetalle",
                column: "PersonaId");

            migrationBuilder.CreateIndex(
                name: "IX_AporteDetalle_TipoAporteId",
                table: "AporteDetalle",
                column: "TipoAporteId");

            migrationBuilder.CreateIndex(
                name: "IX_Aportes_TipoAporteId",
                table: "Aportes",
                column: "TipoAporteId");

            migrationBuilder.AddForeignKey(
                name: "FK_GruposDetalle_Personas_PersonaId",
                table: "GruposDetalle",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "PersonaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GruposDetalle_Personas_PersonaId",
                table: "GruposDetalle");

            migrationBuilder.DropTable(
                name: "AporteDetalle");

            migrationBuilder.DropTable(
                name: "Aportes");

            migrationBuilder.DropTable(
                name: "TipoAporte");

            migrationBuilder.DropIndex(
                name: "IX_GruposDetalle_PersonaId",
                table: "GruposDetalle");

            migrationBuilder.DropColumn(
                name: "CantidadGrupos",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "TotalAporte",
                table: "Personas");
        }
    }
}
