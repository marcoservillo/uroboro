using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Uroboro.DAL.SQLServer.Migrations
{
    public partial class FixModifiedAt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 3, 11, 12, 1, 43, 243, DateTimeKind.Utc).AddTicks(5171), new DateTime(2021, 3, 11, 12, 1, 43, 243, DateTimeKind.Utc).AddTicks(5185) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TodoItems",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2021, 3, 11, 12, 0, 36, 557, DateTimeKind.Utc).AddTicks(7422), null });
        }
    }
}