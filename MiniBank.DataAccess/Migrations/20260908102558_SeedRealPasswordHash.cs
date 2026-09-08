using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniBank.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedRealPasswordHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$U62BKHFnsQZutN5WVy3XaukmPrGzOS4Xc.yudqZlU0XV9f19JYK92");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "...");
        }
    }
}
