using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OA.WebApp.Migrations
{
    public partial class Journal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "fm_Journal",
                columns: table => new
                {
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: true),
                    ModifiedBy = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTime>(nullable: true),
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RecordDate = table.Column<DateTime>(nullable: false),
                    Summary = table.Column<string>(nullable: true),
                    ClientID = table.Column<string>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    Borrowing = table.Column<string>(nullable: true),
                    SalesAmount = table.Column<double>(nullable: false),
                    Paytype = table.Column<string>(nullable: true),
                    PayDate = table.Column<DateTime>(nullable: false),
                    ReceiptNo = table.Column<string>(nullable: true),
                    AccountType = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fm_Journal", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fm_Journal");
        }
    }
}
