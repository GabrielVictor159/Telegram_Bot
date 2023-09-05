using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Telegram.BOT.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryAndMarc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdMarc",
                schema: "Products",
                table: "Product",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MarcId",
                schema: "Products",
                table: "Product",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                schema: "Products",
                table: "Product",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marc",
                schema: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    IdCategory = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marc_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Products",
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Marc_Category_IdCategory",
                        column: x => x.IdCategory,
                        principalSchema: "Products",
                        principalTable: "Category",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_IdMarc",
                schema: "Products",
                table: "Product",
                column: "IdMarc");

            migrationBuilder.CreateIndex(
                name: "IX_Product_MarcId",
                schema: "Products",
                table: "Product",
                column: "MarcId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_Name",
                schema: "Products",
                table: "Category",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Marc_CategoryId",
                schema: "Products",
                table: "Marc",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Marc_IdCategory",
                schema: "Products",
                table: "Marc",
                column: "IdCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Marc_IdMarc",
                schema: "Products",
                table: "Product",
                column: "IdMarc",
                principalSchema: "Products",
                principalTable: "Marc",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Marc_MarcId",
                schema: "Products",
                table: "Product",
                column: "MarcId",
                principalSchema: "Products",
                principalTable: "Marc",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Marc_IdMarc",
                schema: "Products",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Marc_MarcId",
                schema: "Products",
                table: "Product");

            migrationBuilder.DropTable(
                name: "Marc",
                schema: "Products");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Product_IdMarc",
                schema: "Products",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_MarcId",
                schema: "Products",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "IdMarc",
                schema: "Products",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "MarcId",
                schema: "Products",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Price",
                schema: "Products",
                table: "Product");
        }
    }
}
