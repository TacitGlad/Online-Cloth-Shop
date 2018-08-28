using Microsoft.EntityFrameworkCore.Migrations;

namespace Obaju.Data.Migrations
{
    public partial class AddedSubscribedColumnToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Subscribed",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subscribed",
                table: "AspNetUsers");
        }
    }
}
