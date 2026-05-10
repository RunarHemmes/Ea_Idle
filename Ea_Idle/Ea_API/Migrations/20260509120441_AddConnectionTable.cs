using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ea_API.Migrations
{
    /// <inheritdoc />
    public partial class AddConnectionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Password", "Role" },
                values: new object[] { "Harold@mail.com", "passwordHarold", "Player" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Password", "Role" },
                values: new object[] { "John@mail.com", "passwordJohn", "Parent" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Password", "Role" },
                values: new object[] { "passwordHarold", "Player", "Harold@mail.com" });

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "Password", "Role" },
                values: new object[] { "passwordJohn", "Parent", "John@mail.com" });
        }
    }
}
