using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ea_API.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeLimitToConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "TimeLimit",
                table: "Connections",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeLimit",
                table: "Connections");
        }
    }
}
