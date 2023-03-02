using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task8Collection.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCommentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId1",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserId1",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Comments");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Comments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId1",
                table: "Comments",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId1",
                table: "Comments",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
