using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtrkePasa.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddIPAddr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"
                ALTER TABLE ""Register""
                ADD COLUMN ip_addr TEXT NOT NULL DEFAULT '';
            ");

            /* migrationBuilder.AddColumn<string>(
                name: "ip_addr",
                table: "Register",
                type: "text",
                nullable: false,
                defaultValue: ""); */
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                ALTER TABLE ""Register""
                DROP COLUMN ip_addr
            ");

            /* migrationBuilder.DropColumn(
                name: "ip_addr",
                table: "Register"); */
        }
    }
}
