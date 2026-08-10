using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtrkePasa.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddDogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dog_RaceOdds_race_Odds_Id1",
                table: "Dog");

            migrationBuilder.DropForeignKey(
                name: "FK_Dog_Race_race_Id1",
                table: "Dog");

            migrationBuilder.DropIndex(
                name: "IX_Dog_race_Id1",
                table: "Dog");

            migrationBuilder.DropIndex(
                name: "IX_Dog_race_Odds_Id1",
                table: "Dog");

            migrationBuilder.DropColumn(
                name: "race_Id1",
                table: "Dog");

            migrationBuilder.DropColumn(
                name: "race_Odds_Id1",
                table: "Dog");

            migrationBuilder.CreateIndex(
                name: "IX_Dog_race_Id",
                table: "Dog",
                column: "race_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Dog_race_Odds_Id",
                table: "Dog",
                column: "race_Odds_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dog_RaceOdds_race_Odds_Id",
                table: "Dog",
                column: "race_Odds_Id",
                principalTable: "RaceOdds",
                principalColumn: "race_Odds_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dog_Race_race_Id",
                table: "Dog",
                column: "race_Id",
                principalTable: "Race",
                principalColumn: "race_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dog_RaceOdds_race_Odds_Id",
                table: "Dog");

            migrationBuilder.DropForeignKey(
                name: "FK_Dog_Race_race_Id",
                table: "Dog");

            migrationBuilder.DropIndex(
                name: "IX_Dog_race_Id",
                table: "Dog");

            migrationBuilder.DropIndex(
                name: "IX_Dog_race_Odds_Id",
                table: "Dog");

            migrationBuilder.AddColumn<int>(
                name: "race_Id1",
                table: "Dog",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "race_Odds_Id1",
                table: "Dog",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dog_race_Id1",
                table: "Dog",
                column: "race_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_Dog_race_Odds_Id1",
                table: "Dog",
                column: "race_Odds_Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_Dog_RaceOdds_race_Odds_Id1",
                table: "Dog",
                column: "race_Odds_Id1",
                principalTable: "RaceOdds",
                principalColumn: "race_Odds_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dog_Race_race_Id1",
                table: "Dog",
                column: "race_Id1",
                principalTable: "Race",
                principalColumn: "race_Id");
        }
    }
}
