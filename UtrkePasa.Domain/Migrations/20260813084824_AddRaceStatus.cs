using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtrkePasa.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddRaceStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RaceOdds_Race_race_Id1",
                table: "RaceOdds");

            migrationBuilder.RenameColumn(
                name: "race_Id1",
                table: "RaceOdds",
                newName: "RaceId");

            migrationBuilder.RenameIndex(
                name: "IX_RaceOdds_race_Id1",
                table: "RaceOdds",
                newName: "IX_RaceOdds_RaceId");

            migrationBuilder.AddColumn<string>(
                name: "race_Status",
                table: "Race",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_RaceOdds_Race_RaceId",
                table: "RaceOdds",
                column: "RaceId",
                principalTable: "Race",
                principalColumn: "race_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RaceOdds_Race_RaceId",
                table: "RaceOdds");

            migrationBuilder.DropColumn(
                name: "race_Status",
                table: "Race");

            migrationBuilder.RenameColumn(
                name: "RaceId",
                table: "RaceOdds",
                newName: "race_Id1");

            migrationBuilder.RenameIndex(
                name: "IX_RaceOdds_RaceId",
                table: "RaceOdds",
                newName: "IX_RaceOdds_race_Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_RaceOdds_Race_race_Id1",
                table: "RaceOdds",
                column: "race_Id1",
                principalTable: "Race",
                principalColumn: "race_Id");
        }
    }
}
