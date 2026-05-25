using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ea_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDBForConnections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChildStatus",
                table: "Connections",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ParentStatus",
                table: "Connections",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ConnectionCode",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConnectionCode",
                value: 123456);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConnectionCode",
                value: 666666);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChildStatus",
                table: "Connections");

            migrationBuilder.DropColumn(
                name: "ParentStatus",
                table: "Connections");

            migrationBuilder.DropColumn(
                name: "ConnectionCode",
                table: "Accounts");
        }
    }
}
