using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OA.WebApp.Migrations
{
    public partial class journals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountType",
                table: "fm_journal",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "fm_journal",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Borrowing",
                table: "fm_journal",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientID",
                table: "fm_journal",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "fm_journal",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "fm_journal",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "fm_journal",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "fm_journal",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "fm_journal",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PayDate",
                table: "fm_journal",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Paytype",
                table: "fm_journal",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiptNo",
                table: "fm_journal",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecordDate",
                table: "fm_journal",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "SalesAmount",
                table: "fm_journal",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Summary",
                table: "fm_journal",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "fm_journal");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "fm_journal");

            migrationBuilder.DropColumn(
                name: "Borrowing",
                table: "fm_journal");

            migrationBuilder.DropColumn(
                name: "ClientID",
                table: "fm_journal");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "fm_journal");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "fm_journal");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "fm_journal");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "fm_journal");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "fm_journal");

            migrationBuilder.DropColumn(
                name: "PayDate",
                table: "fm_journal");

            migrationBuilder.DropColumn(
                name: "Paytype",
                table: "fm_journal");

            migrationBuilder.DropColumn(
                name: "ReceiptNo",
                table: "fm_journal");

            migrationBuilder.DropColumn(
                name: "RecordDate",
                table: "fm_journal");

            migrationBuilder.DropColumn(
                name: "SalesAmount",
                table: "fm_journal");

            migrationBuilder.DropColumn(
                name: "Summary",
                table: "fm_journal");
        }
    }
}
