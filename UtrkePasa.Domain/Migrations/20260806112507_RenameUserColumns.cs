using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtrkePasa.Domain.Migrations
{
    /// <inheritdoc />
    public partial class RenameUserColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RaceOdds_Race_RaceId",
                table: "RaceOdds");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_RaceOdds_RaceOddsId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Race_RaceId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_User_UserId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_RaceId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_RaceOddsId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_UserId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_RaceOdds_RaceId",
                table: "RaceOdds");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "User",
                newName: "surname");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "User",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "User",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "User",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "WalletState",
                table: "User",
                newName: "wallet_State");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "User",
                newName: "user_Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Ticket",
                newName: "user_Id");

            migrationBuilder.RenameColumn(
                name: "RaceOddsId",
                table: "Ticket",
                newName: "race_Odds_Id");

            migrationBuilder.RenameColumn(
                name: "RaceId",
                table: "Ticket",
                newName: "race_Id");

            migrationBuilder.RenameColumn(
                name: "PlacedAt",
                table: "Ticket",
                newName: "placed_At");

            migrationBuilder.RenameColumn(
                name: "PaidForTicket",
                table: "Ticket",
                newName: "paid_For_Ticket");

            migrationBuilder.RenameColumn(
                name: "TicketId",
                table: "Ticket",
                newName: "ticket_Id");

            migrationBuilder.RenameColumn(
                name: "Odds",
                table: "RaceOdds",
                newName: "odds");

            migrationBuilder.RenameColumn(
                name: "RaceId",
                table: "RaceOdds",
                newName: "race_Id");

            migrationBuilder.RenameColumn(
                name: "ExpectedResult",
                table: "RaceOdds",
                newName: "expected_Result");

            migrationBuilder.RenameColumn(
                name: "RaceOddsId",
                table: "RaceOdds",
                newName: "race_Odds_Id");

            migrationBuilder.RenameColumn(
                name: "StartOfTheRace",
                table: "Race",
                newName: "start_Of_The_Race");

            migrationBuilder.RenameColumn(
                name: "ResultOfRace",
                table: "Race",
                newName: "result_Of_Race");

            migrationBuilder.RenameColumn(
                name: "RaceName",
                table: "Race",
                newName: "race_Name");

            migrationBuilder.RenameColumn(
                name: "EndOfTheRace",
                table: "Race",
                newName: "end_Of_The_Race");

            migrationBuilder.RenameColumn(
                name: "RaceId",
                table: "Race",
                newName: "race_Id");

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

            migrationBuilder.AddColumn<int>(
                name: "race_Id1",
                table: "RaceOdds",
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

            migrationBuilder.CreateIndex(
                name: "IX_RaceOdds_race_Id1",
                table: "RaceOdds",
                column: "race_Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_RaceOdds_Race_race_Id1",
                table: "RaceOdds",
                column: "race_Id1",
                principalTable: "Race",
                principalColumn: "race_Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RaceOdds_Race_race_Id1",
                table: "RaceOdds");

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

            migrationBuilder.DropIndex(
                name: "IX_RaceOdds_race_Id1",
                table: "RaceOdds");

            migrationBuilder.DropColumn(
                name: "race_Id1",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "race_Odds_Id1",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "user_Id1",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "race_Id1",
                table: "RaceOdds");

            migrationBuilder.RenameColumn(
                name: "surname",
                table: "User",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "User",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "User",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "User",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "wallet_State",
                table: "User",
                newName: "WalletState");

            migrationBuilder.RenameColumn(
                name: "user_Id",
                table: "User",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "user_Id",
                table: "Ticket",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "race_Odds_Id",
                table: "Ticket",
                newName: "RaceOddsId");

            migrationBuilder.RenameColumn(
                name: "race_Id",
                table: "Ticket",
                newName: "RaceId");

            migrationBuilder.RenameColumn(
                name: "placed_At",
                table: "Ticket",
                newName: "PlacedAt");

            migrationBuilder.RenameColumn(
                name: "paid_For_Ticket",
                table: "Ticket",
                newName: "PaidForTicket");

            migrationBuilder.RenameColumn(
                name: "ticket_Id",
                table: "Ticket",
                newName: "TicketId");

            migrationBuilder.RenameColumn(
                name: "odds",
                table: "RaceOdds",
                newName: "Odds");

            migrationBuilder.RenameColumn(
                name: "race_Id",
                table: "RaceOdds",
                newName: "RaceId");

            migrationBuilder.RenameColumn(
                name: "expected_Result",
                table: "RaceOdds",
                newName: "ExpectedResult");

            migrationBuilder.RenameColumn(
                name: "race_Odds_Id",
                table: "RaceOdds",
                newName: "RaceOddsId");

            migrationBuilder.RenameColumn(
                name: "start_Of_The_Race",
                table: "Race",
                newName: "StartOfTheRace");

            migrationBuilder.RenameColumn(
                name: "result_Of_Race",
                table: "Race",
                newName: "ResultOfRace");

            migrationBuilder.RenameColumn(
                name: "race_Name",
                table: "Race",
                newName: "RaceName");

            migrationBuilder.RenameColumn(
                name: "end_Of_The_Race",
                table: "Race",
                newName: "EndOfTheRace");

            migrationBuilder.RenameColumn(
                name: "race_Id",
                table: "Race",
                newName: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_RaceId",
                table: "Ticket",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_RaceOddsId",
                table: "Ticket",
                column: "RaceOddsId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_UserId",
                table: "Ticket",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RaceOdds_RaceId",
                table: "RaceOdds",
                column: "RaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_RaceOdds_Race_RaceId",
                table: "RaceOdds",
                column: "RaceId",
                principalTable: "Race",
                principalColumn: "RaceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_RaceOdds_RaceOddsId",
                table: "Ticket",
                column: "RaceOddsId",
                principalTable: "RaceOdds",
                principalColumn: "RaceOddsId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Race_RaceId",
                table: "Ticket",
                column: "RaceId",
                principalTable: "Race",
                principalColumn: "RaceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_User_UserId",
                table: "Ticket",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
