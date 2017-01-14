using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TODO_APP1.Migrations
{
    public partial class AddedIsDone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDone",
                table: "TODOS",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UsersTodo",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    TodoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersTodo", x => new { x.UserId, x.TodoId });
                    table.ForeignKey(
                        name: "FK_UsersTodo_TODOS_TodoId",
                        column: x => x.TodoId,
                        principalTable: "TODOS",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersTodo_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersTodo_TodoId",
                table: "UsersTodo",
                column: "TodoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersTodo");

            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "TODOS");
        }
    }
}
