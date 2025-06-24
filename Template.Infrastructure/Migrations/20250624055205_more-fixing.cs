using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Template.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class morefixing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Category_Category_ParentCategoryId",
            //    table: "Category");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Procedures_Category_CategoryId",
            //    table: "Procedures");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Tools_Category_CategoryId",
            //    table: "Tools");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Category",
            //    table: "Category");

            //migrationBuilder.RenameTable(
            //    name: "Category",
            //    newName: "Categories");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Category_ParentCategoryId",
            //    table: "Categories",
            //    newName: "IX_Categories_ParentCategoryId");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Categories",
            //    table: "Categories",
            //    column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedureTools_ToolId",
                table: "ProcedureTools",
                column: "ToolId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcedureKits_KitId",
                table: "ProcedureKits",
                column: "KitId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Categories_Categories_ParentCategoryId",
            //    table: "Categories",
            //    column: "ParentCategoryId",
            //    principalTable: "Categories",
            //    principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcedureKits_Kits_KitId",
                table: "ProcedureKits",
                column: "KitId",
                principalTable: "Kits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Procedures_Categories_CategoryId",
            //    table: "Procedures",
            //    column: "CategoryId",
            //    principalTable: "Categories",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcedureTools_Tools_ToolId",
                table: "ProcedureTools",
                column: "ToolId",
                principalTable: "Tools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Tools_Categories_CategoryId",
            //    table: "Tools",
            //    column: "CategoryId",
            //    principalTable: "Categories",
            //    principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParentCategoryId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcedureKits_Kits_KitId",
                table: "ProcedureKits");

            migrationBuilder.DropForeignKey(
                name: "FK_Procedures_Categories_CategoryId",
                table: "Procedures");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcedureTools_Tools_ToolId",
                table: "ProcedureTools");

            migrationBuilder.DropForeignKey(
                name: "FK_Tools_Categories_CategoryId",
                table: "Tools");

            migrationBuilder.DropIndex(
                name: "IX_ProcedureTools_ToolId",
                table: "ProcedureTools");

            migrationBuilder.DropIndex(
                name: "IX_ProcedureKits_KitId",
                table: "ProcedureKits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Category",
                newName: "IX_Category_ParentCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Category_ParentCategoryId",
                table: "Category",
                column: "ParentCategoryId",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Procedures_Category_CategoryId",
                table: "Procedures",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_Category_CategoryId",
                table: "Tools",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }
    }
}
