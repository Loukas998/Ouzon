using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Template.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EverythingAsIts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tools_Kits_KitId",
                table: "Tools");

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_Kits_KitId",
                table: "Tools",
                column: "KitId",
                principalTable: "Kits",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tools_Kits_KitId",
                table: "Tools");

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_Kits_KitId",
                table: "Tools",
                column: "KitId",
                principalTable: "Kits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
