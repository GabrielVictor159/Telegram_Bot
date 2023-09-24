using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Telegram.BOT.Infrastructure.Database.Entities.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Products");

            migrationBuilder.EnsureSchema(
                name: "Chats");

            migrationBuilder.EnsureSchema(
                name: "public");

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
                name: "Chat",
                schema: "Chats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                schema: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Tags = table.Column<string>(type: "character varying(1500)", maxLength: 1500, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    UseCase = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    LogDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marc",
                schema: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "Message",
                schema: "Chats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Messaging = table.Column<string>(type: "text", nullable: false),
                    ChatId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Chat_ChatId",
                        column: x => x.ChatId,
                        principalSchema: "Chats",
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    Tags = table.Column<string>(type: "text", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    MarcId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Marc_MarcId",
                        column: x => x.MarcId,
                        principalSchema: "Products",
                        principalTable: "Marc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Message_ChatId",
                schema: "Chats",
                table: "Message",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_MarcId",
                schema: "Products",
                table: "Product",
                column: "MarcId");

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
                name: "Logs",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Message",
                schema: "Chats");

            migrationBuilder.DropTable(
                name: "ProductGroups",
                schema: "Products");

            migrationBuilder.DropTable(
                name: "Chat",
                schema: "Chats");

            migrationBuilder.DropTable(
                name: "Group",
                schema: "Products");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "Products");

            migrationBuilder.DropTable(
                name: "Marc",
                schema: "Products");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "Products");
        }
    }
}
