using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ServicesStore.Api.Author.Migrations
{
    public partial class InitialMigrationPostgres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthorBooks",
                columns: table => new
                {
                    AuthorBookId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    AuthorBookGuid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBooks", x => x.AuthorBookId);
                });

            migrationBuilder.CreateTable(
                name: "AcademicGrades",
                columns: table => new
                {
                    AcademicGradeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    AcademicCenter = table.Column<string>(nullable: true),
                    GradeDate = table.Column<DateTime>(nullable: true),
                    AuthorBookId = table.Column<int>(nullable: false),
                    AcademicGradeGuid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicGrades", x => x.AcademicGradeId);
                    table.ForeignKey(
                        name: "FK_AcademicGrades_AuthorBooks_AuthorBookId",
                        column: x => x.AuthorBookId,
                        principalTable: "AuthorBooks",
                        principalColumn: "AuthorBookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicGrades_AuthorBookId",
                table: "AcademicGrades",
                column: "AuthorBookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicGrades");

            migrationBuilder.DropTable(
                name: "AuthorBooks");
        }
    }
}
