using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtrkePasa.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddOddTypeTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"

                ALTER TABLE ""Ticket""
                ADD COLUMN ""odd_Type""
                TEXT NOT NULL DEFAULT '';
            ");


            /* migrationBuilder.AddColumn<string>(
                name: "odd_Type",
                table: "Ticket",
                type: "text",
                nullable: false,
                defaultValue: ""); */
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"

                ALTER TABLE ""Ticket""
                DROP COLUMN ""odd_Type"";
            
            ");

            /* migrationBuilder.DropColumn(
                name: "odd_Type",
                table: "Ticket"); */
        }
    }
}
