using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Telegram.BOT.Infrastructure.Database.Entities.Migrations
{
    /// <inheritdoc />
    public partial class NewAttributesChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberMessage",
                schema: "Chats",
                table: "Message",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "Chats",
                table: "Chat",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdTelegram",
                schema: "Chats",
                table: "Chat",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                schema: "Chats",
                table: "Chat",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                schema: "Chats",
                table: "Chat",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberMessage",
                schema: "Chats",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "Chats",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "IdTelegram",
                schema: "Chats",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "Location",
                schema: "Chats",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "Username",
                schema: "Chats",
                table: "Chat");
        }
    }
}
