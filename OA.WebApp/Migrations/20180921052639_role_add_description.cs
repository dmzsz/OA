using Microsoft.EntityFrameworkCore.Migrations;

namespace OA.WebApp.Migrations
{
    public partial class role_add_description : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "st_role",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "st_role");
        }
    }
}
