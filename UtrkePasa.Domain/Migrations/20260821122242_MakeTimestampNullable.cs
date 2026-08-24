using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtrkePasa.Domain.Migrations
{
    /// <inheritdoc />
    public partial class MakeTimestampNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"
            
                ALTER TABLE ""Register""
                ALTER COLUMN timestamp DROP NOT NULL;
            ");


            /* migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "timestamp",
                table: "Register",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone"); */
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"
                ALTER TABLE ""Ticket""
                ALTER COLUMN timestamp SET NOT NULL;
            ");

            /* migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "timestamp",
                table: "Register",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true); */
        }
    }
}
