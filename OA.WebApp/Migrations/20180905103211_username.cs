using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OA.WebApp.Migrations
{
    public partial class username : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_st_user_UserID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_st_role_privilege_Privileges_PrivilegeID",
                table: "st_role_privilege");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Privileges",
                table: "Privileges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Privileges",
                newName: "st_privilege");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "st_employee");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "st_user",
                newName: "UserName");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_UserID",
                table: "st_employee",
                newName: "IX_st_employee_UserID");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "st_user",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "st_user",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_st_privilege",
                table: "st_privilege",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_st_employee",
                table: "st_employee",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_st_employee_st_user_UserID",
                table: "st_employee",
                column: "UserID",
                principalTable: "st_user",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_st_role_privilege_st_privilege_PrivilegeID",
                table: "st_role_privilege",
                column: "PrivilegeID",
                principalTable: "st_privilege",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_st_employee_st_user_UserID",
                table: "st_employee");

            migrationBuilder.DropForeignKey(
                name: "FK_st_role_privilege_st_privilege_PrivilegeID",
                table: "st_role_privilege");

            migrationBuilder.DropPrimaryKey(
                name: "PK_st_privilege",
                table: "st_privilege");

            migrationBuilder.DropPrimaryKey(
                name: "PK_st_employee",
                table: "st_employee");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "st_user");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "st_user");

            migrationBuilder.RenameTable(
                name: "st_privilege",
                newName: "Privileges");

            migrationBuilder.RenameTable(
                name: "st_employee",
                newName: "Employees");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "st_user",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_st_employee_UserID",
                table: "Employees",
                newName: "IX_Employees_UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Privileges",
                table: "Privileges",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_st_user_UserID",
                table: "Employees",
                column: "UserID",
                principalTable: "st_user",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_st_role_privilege_Privileges_PrivilegeID",
                table: "st_role_privilege",
                column: "PrivilegeID",
                principalTable: "Privileges",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
