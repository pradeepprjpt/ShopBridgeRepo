using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopBridgeApi.Migrations
{
    public partial class DBInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "ModifiedBy", "ModifiedOn", "Name", "Price", "Quantity" },
                values: new object[] { 1, "SYSTEM", new DateTime(2021, 5, 14, 0, 0, 0, 0, DateTimeKind.Local), "N95 Mask", null, null, "N95", 100.0, 500 });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "ModifiedBy", "ModifiedOn", "Name", "Price", "Quantity" },
                values: new object[] { 2, "SYSTEM", new DateTime(2021, 5, 14, 0, 0, 0, 0, DateTimeKind.Local), "PPE Kit", null, null, "PPE Kit", 5000.0, 3500 });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "ModifiedBy", "ModifiedOn", "Name", "Price", "Quantity" },
                values: new object[] { 3, "SYSTEM", new DateTime(2021, 5, 14, 0, 0, 0, 0, DateTimeKind.Local), "Himalaya Sanitizer", null, null, "Himalaya Sanitizer", 150.0, 5000 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventories");
        }
    }
}
