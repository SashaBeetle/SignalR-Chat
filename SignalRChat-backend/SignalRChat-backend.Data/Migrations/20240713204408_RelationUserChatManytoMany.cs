using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignalRChat_backend.Data.Migrations
{
    /// <inheritdoc />
    public partial class RelationUserChatManytoMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_UserId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_UserId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Chats",
                newName: "CreatorId");

            migrationBuilder.CreateTable(
                name: "UsersChats",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ChatId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersChats", x => new { x.UserId, x.ChatId });
                    table.ForeignKey(
                        name: "FK_UsersChats_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersChats_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersChats_ChatId",
                table: "UsersChats",
                column: "ChatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersChats");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Chats",
                newName: "UserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "Messages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserId",
                table: "Chats",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_UserId",
                table: "Chats",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
