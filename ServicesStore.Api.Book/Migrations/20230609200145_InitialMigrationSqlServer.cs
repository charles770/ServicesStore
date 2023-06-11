using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServicesStore.Api.Book.Migrations
{
    public partial class InitialMigrationSqlServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookShopItem",
                columns: table => new
                {
                    BookShopItemId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    PublishDate = table.Column<DateTime>(nullable: true),
                    AuthorBook = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookShopItem", x => x.BookShopItemId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookShopItem");
        }
    }
}
