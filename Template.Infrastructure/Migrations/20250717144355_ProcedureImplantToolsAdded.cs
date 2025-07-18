using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Template.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProcedureImplantToolsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcedureImplantTools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcedureId = table.Column<int>(type: "int", nullable: false),
                    ImplantId = table.Column<int>(type: "int", nullable: false),
                    ToolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcedureImplantTools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcedureImplantTools_Implants_ImplantId",
                        column: x => x.ImplantId,
                        principalTable: "Implants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcedureImplantTools_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcedureImplantTools_Tools_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcedureImplantTools_ImplantId",
                table: "ProcedureImplantTools",
                column: "ImplantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedureImplantTools_ProcedureId",
                table: "ProcedureImplantTools",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedureImplantTools_ToolId",
                table: "ProcedureImplantTools",
                column: "ToolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcedureImplantTools");
        }
    }
}
