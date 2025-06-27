using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Template.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class QuickFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Procedures_AspNetUsers_AssistantId",
                table: "Procedures");

            migrationBuilder.AddColumn<string>(
                name: "DoctorId",
                table: "Procedures",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Procedures_DoctorId",
                table: "Procedures",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Procedures_AspNetUsers_AssistantId",
                table: "Procedures",
                column: "AssistantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Procedures_AspNetUsers_DoctorId",
                table: "Procedures",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Procedures_AspNetUsers_AssistantId",
                table: "Procedures");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedures_AspNetUsers_DoctorId",
                table: "Procedures");

            migrationBuilder.DropIndex(
                name: "IX_Procedures_DoctorId",
                table: "Procedures");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Procedures");

            migrationBuilder.AddForeignKey(
                name: "FK_Procedures_AspNetUsers_AssistantId",
                table: "Procedures",
                column: "AssistantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
