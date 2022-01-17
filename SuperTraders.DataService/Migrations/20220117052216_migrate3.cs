using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SuperTraders.DataService.Migrations
{
    public partial class migrate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    TotalBalance = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountShare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Symbol = table.Column<string>(type: "text", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    AccountId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountShare", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountShare_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Share",
                columns: new[] { "Id", "LastUpDateTime", "Rate", "Symbol" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 1, 17, 8, 22, 15, 793, DateTimeKind.Local).AddTicks(3299), 10.56m, "BTC" },
                    { 2, new DateTime(2022, 1, 17, 7, 22, 15, 794, DateTimeKind.Local).AddTicks(1359), 11.56m, "USD" },
                    { 3, new DateTime(2022, 1, 17, 6, 22, 15, 794, DateTimeKind.Local).AddTicks(1374), 12.56m, "TRY" },
                    { 4, new DateTime(2022, 1, 17, 5, 22, 15, 794, DateTimeKind.Local).AddTicks(1377), 13.56m, "EUR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountShare_AccountId",
                table: "AccountShare",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountShare");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DeleteData(
                table: "Share",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Share",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Share",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Share",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
