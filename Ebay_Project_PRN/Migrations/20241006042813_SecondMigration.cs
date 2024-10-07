using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ebay_Project_PRN.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2d6ccb97-8004-4afc-9237-a3c33b9ebeef", "f5aaa6f2-534d-4b38-a587-c9e1dafee13e", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7d3d02e2-b214-45b7-9b86-f8e6844d4242", "608aa227-6a1f-48af-ab9f-2023845e1688", "seller", "seller" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9a8ded95-ee9f-4155-86ab-b6cabcc15b61", "93d9a126-b1ce-45b9-a7c2-208f5a99a9fb", "client", "client" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d6ccb97-8004-4afc-9237-a3c33b9ebeef");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7d3d02e2-b214-45b7-9b86-f8e6844d4242");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9a8ded95-ee9f-4155-86ab-b6cabcc15b61");
        }
    }
}
