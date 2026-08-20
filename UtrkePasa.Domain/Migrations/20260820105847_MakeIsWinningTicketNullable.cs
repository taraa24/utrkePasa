using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtrkePasa.Domain.Migrations
{
    /// <inheritdoc />
    public partial class MakeIsWinningTicketNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                ALTER TABLE ""Ticket""
                ALTER COLUMN ""is_Winning_Ticket""
                DROP NOT NULL;

            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                ALTER TABLE ""Ticket""
                ALTER COLUMN ""is_Winning_Ticket""
                SET NOT NULL;
            ");
        }
    }
}
