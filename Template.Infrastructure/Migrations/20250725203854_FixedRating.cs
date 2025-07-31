using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Template.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Procedures_ProcedureId",
                table: "Ratings");

            migrationBuilder.RenameColumn(
                name: "RatingValue",
                table: "Ratings",
                newName: "Rate");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "Ratings",
                newName: "Note");

            migrationBuilder.AlterColumn<int>(
                name: "ProcedureId",
                table: "Ratings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AssistantId",
                table: "Ratings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorId",
                table: "Ratings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_AssistantId",
                table: "Ratings",
                column: "AssistantId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_DoctorId",
                table: "Ratings",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_AssistantId",
                table: "Ratings",
                column: "AssistantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_AspNetUsers_DoctorId",
                table: "Ratings",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Procedures_ProcedureId",
                table: "Ratings",
                column: "ProcedureId",
                principalTable: "Procedures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_AssistantId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_AspNetUsers_DoctorId",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Procedures_ProcedureId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_AssistantId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_DoctorId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "AssistantId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Ratings");

            migrationBuilder.RenameColumn(
                name: "Rate",
                table: "Ratings",
                newName: "RatingValue");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "Ratings",
                newName: "Comment");

            migrationBuilder.AlterColumn<int>(
                name: "ProcedureId",
                table: "Ratings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Procedures_ProcedureId",
                table: "Ratings",
                column: "ProcedureId",
                principalTable: "Procedures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
