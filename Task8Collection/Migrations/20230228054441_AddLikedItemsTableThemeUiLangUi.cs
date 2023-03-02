using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task8Collection.Migrations
{
    /// <inheritdoc />
    public partial class AddLikedItemsTableThemeUiLangUi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LanguageUi",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThemeUi",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LikedItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemIdGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikedItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikedItems_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikedItems_UserId",
                table: "LikedItems",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikedItems");

            migrationBuilder.DropColumn(
                name: "LanguageUi",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ThemeUi",
                table: "AspNetUsers");
        }
    }
}
