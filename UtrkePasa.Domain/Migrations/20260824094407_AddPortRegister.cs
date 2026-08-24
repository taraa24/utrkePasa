using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtrkePasa.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddPortRegister : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            
                ALTER TABLE ""Register""
                ADD COLUMN port INTEGER NOT NULL DEFAULT 0;
            ");

            /* migrationBuilder.AddColumn<int>(
                name: "port",
                table: "Register",
                type: "integer",
                nullable: false,
                defaultValue: 0); */
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"

                ALTER TABLE ""Register""
                DROP COLUMN port;
            ");

            /* migrationBuilder.DropColumn(
                name: "port",
                table: "Register"); */
        }
    }
}
