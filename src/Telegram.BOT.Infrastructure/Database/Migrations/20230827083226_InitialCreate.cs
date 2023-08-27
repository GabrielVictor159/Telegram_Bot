using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Telegram.BOT.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Products");

            migrationBuilder.CreateTable(
                name: "Group",
                schema: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Tags = table.Column<string>(type: "character varying(1500)", maxLength: 1500, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    Tags = table.Column<string>(type: "text", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductGroups",
                schema: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Product75Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Product50Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Product25Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductGroups_Group_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "Products",
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductGroups_Product_Product25Id",
                        column: x => x.Product25Id,
                        principalSchema: "Products",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductGroups_Product_Product50Id",
                        column: x => x.Product50Id,
                        principalSchema: "Products",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductGroups_Product_Product75Id",
                        column: x => x.Product75Id,
                        principalSchema: "Products",
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroups_GroupId",
                schema: "Products",
                table: "ProductGroups",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroups_Product25Id",
                schema: "Products",
                table: "ProductGroups",
                column: "Product25Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroups_Product50Id",
                schema: "Products",
                table: "ProductGroups",
                column: "Product50Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductGroups_Product75Id",
                schema: "Products",
                table: "ProductGroups",
                column: "Product75Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductGroups",
                schema: "Products");

            migrationBuilder.DropTable(
                name: "Group",
                schema: "Products");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "Products");
        }
    }
}
