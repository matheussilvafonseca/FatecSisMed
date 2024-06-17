using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FatecSisMed.MedicoAPI.Migrations
{
    public partial class AdicionandoRemédioEMarca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RemedioId",
                table: "Medicos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Observacao = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Remedios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Preco = table.Column<double>(type: "double", maxLength: 100, nullable: false),
                    MarcaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remedios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Remedios_Marcas_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marcas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Marcas",
                columns: new[] { "Id", "Nome", "Observacao" },
                values: new object[] { 1, "Germed", "Essa marca é Germed de todas" });

            migrationBuilder.InsertData(
                table: "Marcas",
                columns: new[] { "Id", "Nome", "Observacao" },
                values: new object[] { 2, "GeoLab", "Essa marca é mais Geolab de todas" });

            migrationBuilder.InsertData(
                table: "Marcas",
                columns: new[] { "Id", "Nome", "Observacao" },
                values: new object[] { 3, "Solaris", "Essa marca é a mais Solaris de todas" });

            migrationBuilder.InsertData(
                table: "Remedios",
                columns: new[] { "Id", "MarcaId", "Nome", "Preco" },
                values: new object[] { 1, 1, "Alprazolam", 15.0 });

            migrationBuilder.InsertData(
                table: "Remedios",
                columns: new[] { "Id", "MarcaId", "Nome", "Preco" },
                values: new object[] { 2, 2, "Rivotril", 20.0 });

            migrationBuilder.InsertData(
                table: "Remedios",
                columns: new[] { "Id", "MarcaId", "Nome", "Preco" },
                values: new object[] { 3, 3, "Carbamazepina", 40.0 });

            migrationBuilder.CreateIndex(
                name: "IX_Medicos_RemedioId",
                table: "Medicos",
                column: "RemedioId");

            migrationBuilder.CreateIndex(
                name: "IX_Remedios_MarcaId",
                table: "Remedios",
                column: "MarcaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicos_Remedios_RemedioId",
                table: "Medicos",
                column: "RemedioId",
                principalTable: "Remedios",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicos_Remedios_RemedioId",
                table: "Medicos");

            migrationBuilder.DropTable(
                name: "Remedios");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropIndex(
                name: "IX_Medicos_RemedioId",
                table: "Medicos");

            migrationBuilder.DropColumn(
                name: "RemedioId",
                table: "Medicos");
        }
    }
}
