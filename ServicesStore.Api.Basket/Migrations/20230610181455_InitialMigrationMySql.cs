using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace ServicesStore.Api.Basket.Migrations
{
    public partial class InitialMigrationMySql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BasketSession",
                columns: table => new
                {
                    BasketSessionId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketSession", x => x.BasketSessionId);
                });

            migrationBuilder.CreateTable(
                name: "BasketSessionDetail",
                columns: table => new
                {
                    BasketSessionDetailId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: true),
                    Product = table.Column<string>(nullable: true),
                    BasketSessionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketSessionDetail", x => x.BasketSessionDetailId);
                    table.ForeignKey(
                        name: "FK_BasketSessionDetail_BasketSession_BasketSessionId",
                        column: x => x.BasketSessionId,
                        principalTable: "BasketSession",
                        principalColumn: "BasketSessionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketSessionDetail_BasketSessionId",
                table: "BasketSessionDetail",
                column: "BasketSessionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketSessionDetail");

            migrationBuilder.DropTable(
                name: "BasketSession");
        }
    }
}
