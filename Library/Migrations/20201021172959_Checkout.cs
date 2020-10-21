using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Migrations
{
    public partial class Checkout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Copies",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "Genre",
                table: "Books",
                newName: "Description");

            migrationBuilder.CreateTable(
                name: "Copies",
                columns: table => new
                {
                    CopiesId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InStock = table.Column<bool>(nullable: false),
                    BookId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Copies", x => x.CopiesId);
                    table.ForeignKey(
                        name: "FK_Copies_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patrons",
                columns: table => new
                {
                    PatronId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patrons", x => x.PatronId);
                    table.ForeignKey(
                        name: "FK_Patrons_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Checkouts",
                columns: table => new
                {
                    CheckoutId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DueDate = table.Column<DateTime>(nullable: false),
                    CheckoutDate = table.Column<DateTime>(nullable: false),
                    CopiesId = table.Column<int>(nullable: false),
                    PatronId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkouts", x => x.CheckoutId);
                    table.ForeignKey(
                        name: "FK_Checkouts_Copies_CopiesId",
                        column: x => x.CopiesId,
                        principalTable: "Copies",
                        principalColumn: "CopiesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Checkouts_Patrons_PatronId",
                        column: x => x.PatronId,
                        principalTable: "Patrons",
                        principalColumn: "PatronId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Checkouts_CopiesId",
                table: "Checkouts",
                column: "CopiesId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkouts_PatronId",
                table: "Checkouts",
                column: "PatronId");

            migrationBuilder.CreateIndex(
                name: "IX_Copies_BookId",
                table: "Copies",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Patrons_UserId",
                table: "Patrons",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Checkouts");

            migrationBuilder.DropTable(
                name: "Copies");

            migrationBuilder.DropTable(
                name: "Patrons");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Books",
                newName: "Genre");

            migrationBuilder.AddColumn<int>(
                name: "Copies",
                table: "Books",
                nullable: false,
                defaultValue: 0);
        }
    }
}
