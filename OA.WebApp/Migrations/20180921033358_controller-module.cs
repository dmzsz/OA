using Microsoft.EntityFrameworkCore.Migrations;

namespace OA.WebApp.Migrations
{
    public partial class controllermodule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModelName",
                table: "st_user_privilege");

            migrationBuilder.DropColumn(
                name: "Identity",
                table: "st_privilege");

            migrationBuilder.DropColumn(
                name: "ModelName",
                table: "st_privilege");

            migrationBuilder.RenameColumn(
                name: "PropertyName",
                table: "st_user_privilege",
                newName: "UniquePriv");

            migrationBuilder.RenameColumn(
                name: "PropertyName",
                table: "st_privilege",
                newName: "Module");

            migrationBuilder.AddColumn<string>(
                name: "UniquePriv",
                table: "st_role_privilege",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "st_privilege",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniquePriv",
                table: "st_role_privilege");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "st_privilege");

            migrationBuilder.RenameColumn(
                name: "UniquePriv",
                table: "st_user_privilege",
                newName: "PropertyName");

            migrationBuilder.RenameColumn(
                name: "Module",
                table: "st_privilege",
                newName: "PropertyName");

            migrationBuilder.AddColumn<string>(
                name: "ModelName",
                table: "st_user_privilege",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Identity",
                table: "st_privilege",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModelName",
                table: "st_privilege",
                nullable: true);
        }
    }
}
