using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniBank.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixSecondUserEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Email",
                value: "test2@minibank.com");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Email",
                value: "test2");
        }
    }
}
