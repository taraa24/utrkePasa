using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtrkePasa.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddIsLeaderRegister : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"
            
                ALTER TABLE ""Register""
                ADD COLUMN is_leader BOOLEAN NOT NULL DEFAULT FALSE;
            ");

            /* migrationBuilder.AddColumn<bool>(
                name: "is_leader",
                table: "Register",
                type: "boolean",
                nullable: false,
                defaultValue: false); */
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"

                ALTER TABLE ""Register""
                DROP COLUMN is_leader;
            
            ");
            /* migrationBuilder.DropColumn(
                name: "is_leader",
                table: "Register"); */
        }
    }
}
