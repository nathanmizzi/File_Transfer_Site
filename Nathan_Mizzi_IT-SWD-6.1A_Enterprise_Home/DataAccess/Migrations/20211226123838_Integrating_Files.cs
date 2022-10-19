using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Integrating_Files : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "FileTranfers",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "FileTranfers");
        }
    }
}
