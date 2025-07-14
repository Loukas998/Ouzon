using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Template.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProcedureAssistantAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Procedures_AspNetUsers_AssistantId",
                table: "Procedures");

            migrationBuilder.DropIndex(
                name: "IX_Procedures_AssistantId",
                table: "Procedures");

            migrationBuilder.DropColumn(
                name: "AssistantId",
                table: "Procedures");

            migrationBuilder.DropColumn(
                name: "HasAssistant",
                table: "Procedures");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfAsisstants",
                table: "Procedures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.CreateTable(
            //    name: "Devices",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        DeviceToken = table.Column<string>(type: "nvarchar(450)", nullable: true),
            //        LastLoggedInAt = table.Column<DateTime>(type: "datetime2", nullable: true),
            //        OptIn = table.Column<bool>(type: "bit", nullable: true),
            //        UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Devices", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Devices_AspNetUsers_UserId",
            //            column: x => x.UserId,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id");
            //    });

            migrationBuilder.CreateTable(
                name: "ProcedureAssistants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AsisstantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProcedureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcedureAssistants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcedureAssistants_AspNetUsers_AsisstantId",
                        column: x => x.AsisstantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcedureAssistants_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedures",
                        principalColumn: "Id");
                });

            //    migrationBuilder.CreateTable(
            //        name: "Notifications",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(type: "int", nullable: false)
            //                .Annotation("SqlServer:Identity", "1, 1"),
            //            Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //            DeviceId = table.Column<int>(type: "int", nullable: false),
            //            Read = table.Column<bool>(type: "bit", nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Notifications", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Notifications_Devices_DeviceId",
            //                column: x => x.DeviceId,
            //                principalTable: "Devices",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Devices_DeviceToken",
            //        table: "Devices",
            //        column: "DeviceToken",
            //        unique: true,
            //        filter: "[DeviceToken] IS NOT NULL");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Devices_UserId",
            //        table: "Devices",
            //        column: "UserId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Notifications_DeviceId",
            //        table: "Notifications",
            //        column: "DeviceId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_ProcedureAssistants_AsisstantId",
            //        table: "ProcedureAssistants",
            //        column: "AsisstantId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_ProcedureAssistants_ProcedureId",
            //        table: "ProcedureAssistants",
            //        column: "ProcedureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "ProcedureAssistants");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropColumn(
                name: "NumberOfAsisstants",
                table: "Procedures");

            migrationBuilder.AddColumn<string>(
                name: "AssistantId",
                table: "Procedures",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasAssistant",
                table: "Procedures",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Procedures_AssistantId",
                table: "Procedures",
                column: "AssistantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Procedures_AspNetUsers_AssistantId",
                table: "Procedures",
                column: "AssistantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
