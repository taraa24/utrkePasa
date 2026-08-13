using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtrkePasa.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddStartOfBetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "start_Of_Betting",
                table: "Race",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "start_Of_Betting",
                table: "Race");
        }
    }
}
