using Microsoft.EntityFrameworkCore.Migrations;

namespace Lab4.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Memb",
                table: "AnswerImage",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Memb",
                table: "AnswerImage");
        }
    }
}
