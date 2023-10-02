using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Telegram.BOT.Infrastructure.Database.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddEnvVariables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ManagementServices");

            migrationBuilder.CreateTable(
                name: "EnvVariable",
                schema: "ManagementServices",
                columns: table => new
                {
                    Key = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvVariable", x => x.Key);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnvVariable",
                schema: "ManagementServices");
        }
    }
}
