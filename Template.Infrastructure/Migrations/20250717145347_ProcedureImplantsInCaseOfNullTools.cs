using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Template.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProcedureImplantsInCaseOfNullTools : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcedureImplants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcedureId = table.Column<int>(type: "int", nullable: false),
                    ImplantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcedureImplants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcedureImplants_Implants_ImplantId",
                        column: x => x.ImplantId,
                        principalTable: "Implants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcedureImplants_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcedureImplants_ImplantId",
                table: "ProcedureImplants",
                column: "ImplantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedureImplants_ProcedureId",
                table: "ProcedureImplants",
                column: "ProcedureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcedureImplants");
        }
    }
}
