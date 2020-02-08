using Microsoft.EntityFrameworkCore.Migrations;

namespace enjaz.Data.Migrations
{
    public partial class addmi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgFile",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgFile",
                table: "AspNetUsers");
        }
    }
}
