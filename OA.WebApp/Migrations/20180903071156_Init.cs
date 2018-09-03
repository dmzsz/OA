using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OA.WebApp.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Privileges",
                columns: table => new
                {
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<string>(nullable: true),
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ModelName = table.Column<string>(nullable: true),
                    PropertyName = table.Column<string>(nullable: true),
                    ControllerName = table.Column<string>(nullable: true),
                    ActionName = table.Column<string>(nullable: true),
                    ScopeEnable = table.Column<bool>(nullable: false),
                    Identity = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privileges", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "st_company",
                columns: table => new
                {
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<string>(nullable: true),
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ParentCompanyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_company", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "st_department",
                columns: table => new
                {
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<string>(nullable: true),
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ParentDepartmentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_department", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "st_role",
                columns: table => new
                {
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<string>(nullable: true),
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_role", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "st_user",
                columns: table => new
                {
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<string>(nullable: true),
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_user", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "st_role_privilege",
                columns: table => new
                {
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<string>(nullable: true),
                    ID = table.Column<int>(nullable: false),
                    RoleID = table.Column<int>(nullable: false),
                    PrivilegeID = table.Column<int>(nullable: false),
                    Scope = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_role_privilege", x => new { x.RoleID, x.PrivilegeID });
                    table.UniqueConstraint("AK_st_role_privilege_ID", x => x.ID);
                    table.ForeignKey(
                        name: "FK_st_role_privilege_Privileges_PrivilegeID",
                        column: x => x.PrivilegeID,
                        principalTable: "Privileges",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_st_role_privilege_st_role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "st_role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employees_st_user_UserID",
                        column: x => x.UserID,
                        principalTable: "st_user",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "st_user_company",
                columns: table => new
                {
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: false),
                    CompanyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_user_company", x => new { x.UserID, x.CompanyID });
                    table.UniqueConstraint("AK_st_user_company_CompanyID_UserID", x => new { x.CompanyID, x.UserID });
                    table.ForeignKey(
                        name: "FK_st_user_company_st_company_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "st_company",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_st_user_company_st_user_UserID",
                        column: x => x.UserID,
                        principalTable: "st_user",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "st_user_department",
                columns: table => new
                {
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: false),
                    DepartmentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_user_department", x => new { x.UserID, x.DepartmentID });
                    table.UniqueConstraint("AK_st_user_department_DepartmentID_UserID", x => new { x.DepartmentID, x.UserID });
                    table.ForeignKey(
                        name: "FK_st_user_department_st_department_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "st_department",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_st_user_department_st_user_UserID",
                        column: x => x.UserID,
                        principalTable: "st_user",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "st_user_privilege",
                columns: table => new
                {
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ModelName = table.Column<string>(nullable: true),
                    PropertyName = table.Column<string>(nullable: true),
                    ControllerName = table.Column<string>(nullable: true),
                    ActionName = table.Column<string>(nullable: true),
                    Scope = table.Column<string>(nullable: true),
                    UserID1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_user_privilege", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_st_user_privilege_st_user_UserID1",
                        column: x => x.UserID1,
                        principalTable: "st_user",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "st_user_role",
                columns: table => new
                {
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTime>(nullable: true),
                    Deleted = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: false),
                    RoleID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_st_user_role", x => new { x.UserID, x.RoleID });
                    table.UniqueConstraint("AK_st_user_role_RoleID_UserID", x => new { x.RoleID, x.UserID });
                    table.ForeignKey(
                        name: "FK_st_user_role_st_role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "st_role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_st_user_role_st_user_UserID",
                        column: x => x.UserID,
                        principalTable: "st_user",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserID",
                table: "Employees",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_st_role_privilege_PrivilegeID",
                table: "st_role_privilege",
                column: "PrivilegeID");

            migrationBuilder.CreateIndex(
                name: "IX_st_user_privilege_UserID1",
                table: "st_user_privilege",
                column: "UserID1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "st_role_privilege");

            migrationBuilder.DropTable(
                name: "st_user_company");

            migrationBuilder.DropTable(
                name: "st_user_department");

            migrationBuilder.DropTable(
                name: "st_user_privilege");

            migrationBuilder.DropTable(
                name: "st_user_role");

            migrationBuilder.DropTable(
                name: "Privileges");

            migrationBuilder.DropTable(
                name: "st_company");

            migrationBuilder.DropTable(
                name: "st_department");

            migrationBuilder.DropTable(
                name: "st_role");

            migrationBuilder.DropTable(
                name: "st_user");
        }
    }
}
