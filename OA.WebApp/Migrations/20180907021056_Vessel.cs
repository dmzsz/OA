using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OA.WebApp.Migrations
{
    public partial class Vessel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "st_employee",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Vessels",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    LocalName = table.Column<string>(nullable: true),
                    Voy = table.Column<string>(nullable: true),
                    Port = table.Column<string>(nullable: true),
                    OpDate = table.Column<DateTime>(nullable: false),
                    CloseDate = table.Column<DateTime>(nullable: false),
                    ETD = table.Column<DateTime>(nullable: false),
                    Trade = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vessels", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vessels");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "st_employee");
        }
    }
}
