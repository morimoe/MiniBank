using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniBank.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSecondAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountNumber", "Balance", "Currency", "UserId" },
                values: new object[] { 2, "MB-000002", 500m, "MDL", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
