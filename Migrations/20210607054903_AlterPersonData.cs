using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewnetCore.Migrations
{
    public partial class AlterPersonData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Task" },
                values: new object[] { "Amina", "Launch" });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "Date", "Name", "Task" },
                values: new object[] { 2, new DateTime(2010, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kolo", "Morning Code" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Task" },
                values: new object[] { "Mohammad", "Break fast" });
        }
    }
}
