using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStore.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Books",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Bio", "CreatedAt", "Name" },
                values: new object[,]
                {
                    { 1, "Software engineer and author of Clean Code and Clean Architecture.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Robert C. Martin" },
                    { 2, "Co-author of The Pragmatic Programmer.", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Andrew Hunt" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CreatedAt", "Price", "Stock", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Utc), 150000m, 10, "Clean Code" },
                    { 2, 1, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Utc), 175000m, 7, "Clean Architecture" },
                    { 3, 2, new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), 200000m, 5, "The Pragmatic Programmer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Books",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");
        }
    }
}
