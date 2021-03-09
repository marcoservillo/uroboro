using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Uroboro.DAL.SQLServer.Migrations
{
    public partial class InitialCrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UroboroItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UroboroItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "UroboroItems",
                columns: new[] { "Id", "CompletedAt", "CompletedBy", "CreateAt", "CreateBy", "DeletedAt", "DeletedBy", "IsCompleted", "IsDeleted", "ModifiedAt", "ModifiedBy", "Name" },
                values: new object[] { 1, null, null, new DateTime(2021, 3, 9, 14, 57, 13, 681, DateTimeKind.Utc).AddTicks(7438), "system", null, null, true, false, null, null, "UroboroItem" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UroboroItems");
        }
    }
}
