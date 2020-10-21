using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Migrations
{
    public partial class RemovePatronId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatronId",
                table: "Checkouts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PatronId",
                table: "Checkouts",
                nullable: false,
                defaultValue: 0);
        }
    }
}
