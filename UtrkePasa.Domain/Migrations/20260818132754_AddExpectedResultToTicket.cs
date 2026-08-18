using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtrkePasa.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddExpectedResultToTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RaceOdds_Race_RaceId",
                table: "RaceOdds");

            migrationBuilder.DropIndex(
                name: "IX_RaceOdds_RaceId",
                table: "RaceOdds");

            migrationBuilder.DropColumn(
                name: "RaceId",
                table: "RaceOdds");

            migrationBuilder.AddColumn<string>(
                name: "expected_Result",
                table: "Ticket",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "is_Winning_Ticket",
                table: "Ticket",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_RaceOdds_race_Id",
                table: "RaceOdds",
                column: "race_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RaceOdds_Race_race_Id",
                table: "RaceOdds",
                column: "race_Id",
                principalTable: "Race",
                principalColumn: "race_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RaceOdds_Race_race_Id",
                table: "RaceOdds");

            migrationBuilder.DropIndex(
                name: "IX_RaceOdds_race_Id",
                table: "RaceOdds");

            migrationBuilder.DropColumn(
                name: "expected_Result",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "is_Winning_Ticket",
                table: "Ticket");

            migrationBuilder.AddColumn<int>(
                name: "RaceId",
                table: "RaceOdds",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RaceOdds_RaceId",
                table: "RaceOdds",
                column: "RaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_RaceOdds_Race_RaceId",
                table: "RaceOdds",
                column: "RaceId",
                principalTable: "Race",
                principalColumn: "race_Id");
        }
    }
}
