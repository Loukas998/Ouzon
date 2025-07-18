using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Template.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OnDeleteCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcedureAssistants_Procedures_ProcedureId",
                table: "ProcedureAssistants");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcedureAssistants_Procedures_ProcedureId",
                table: "ProcedureAssistants",
                column: "ProcedureId",
                principalTable: "Procedures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcedureAssistants_Procedures_ProcedureId",
                table: "ProcedureAssistants");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcedureAssistants_Procedures_ProcedureId",
                table: "ProcedureAssistants",
                column: "ProcedureId",
                principalTable: "Procedures",
                principalColumn: "Id");
        }
    }
}
