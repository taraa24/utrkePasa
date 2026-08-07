using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtrkePasa.Domain.Migrations
{
    /// <inheritdoc />
    public partial class FixTicketForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_RaceOdds_race_Odds_Id1",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Race_race_Id1",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_User_user_Id1",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_race_Id1",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_race_Odds_Id1",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_user_Id1",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "race_Id1",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "race_Odds_Id1",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "user_Id1",
                table: "Ticket");

            migrationBuilder.AlterColumn<decimal>(
                name: "paid_For_Ticket",
                table: "Ticket",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_race_Id",
                table: "Ticket",
                column: "race_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_race_Odds_Id",
                table: "Ticket",
                column: "race_Odds_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_user_Id",
                table: "Ticket",
                column: "user_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_RaceOdds_race_Odds_Id",
                table: "Ticket",
                column: "race_Odds_Id",
                principalTable: "RaceOdds",
                principalColumn: "race_Odds_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Race_race_Id",
                table: "Ticket",
                column: "race_Id",
                principalTable: "Race",
                principalColumn: "race_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_User_user_Id",
                table: "Ticket",
                column: "user_Id",
                principalTable: "User",
                principalColumn: "user_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_RaceOdds_race_Odds_Id",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Race_race_Id",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_User_user_Id",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_race_Id",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_race_Odds_Id",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_user_Id",
                table: "Ticket");

            migrationBuilder.AlterColumn<float>(
                name: "paid_For_Ticket",
                table: "Ticket",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<int>(
                name: "race_Id1",
                table: "Ticket",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "race_Odds_Id1",
                table: "Ticket",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "user_Id1",
                table: "Ticket",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_race_Id1",
                table: "Ticket",
                column: "race_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_race_Odds_Id1",
                table: "Ticket",
                column: "race_Odds_Id1");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_user_Id1",
                table: "Ticket",
                column: "user_Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_RaceOdds_race_Odds_Id1",
                table: "Ticket",
                column: "race_Odds_Id1",
                principalTable: "RaceOdds",
                principalColumn: "race_Odds_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Race_race_Id1",
                table: "Ticket",
                column: "race_Id1",
                principalTable: "Race",
                principalColumn: "race_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_User_user_Id1",
                table: "Ticket",
                column: "user_Id1",
                principalTable: "User",
                principalColumn: "user_Id");
        }
    }
}
