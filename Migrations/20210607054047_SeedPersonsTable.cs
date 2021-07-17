using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewnetCore.Migrations
{
    public partial class SeedPersonsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Date", "Name", "Task" },
                values: new object[] { 1, new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mohammad", "Break fast" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
