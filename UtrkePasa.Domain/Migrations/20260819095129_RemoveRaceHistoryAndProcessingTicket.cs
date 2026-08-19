using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UtrkePasa.Domain.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRaceHistoryAndProcessingTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                ALTER TABLE ""Dog"" 
                DROP CONSTRAINT ""FK_Dog_RaceOdds_race_Odds_Id"";

                DROP TABLE ""ProcessingTicket"";

                DROP TABLE ""RaceHistory"";

                DROP INDEX ""IX_Dog_race_Odds_Id"";

                ALTER TABLE ""Dog""
                DROP COLUMN ""race_Odds_Id"";

                ALTER TABLE ""RaceOdds""
                RENAME COLUMN ""odds"" TO ""odd"";

                ALTER TABLE ""RaceOdds""
                RENAME COLUMN ""expected_Result"" TO ""odd_Type"";

                UPDATE ""Race"" 
                SET ""result_Of_Race"" = '' 
                WHERE ""result_Of_Race"" IS NULL;

                ALTER TABLE ""Race"" 
                ALTER COLUMN ""result_Of_Race"" 
                SET NOT NULL;

                ALTER TABLE ""Race""
                ALTER COLUMN ""result_Of_Race""
                SET DEFAULT '';

                ALTER TABLE ""Race""
                ADD COLUMN ""dog_Finale_Position"" TEXT[] NOT NULL DEFAULT '{}';
            ");

            /* migrationBuilder.DropForeignKey(
                name: "FK_Dog_RaceOdds_race_Odds_Id",
                table: "Dog");

            migrationBuilder.DropTable(
                name: "ProcessingTicket");

            migrationBuilder.DropTable(
                name: "RaceHistory");

            migrationBuilder.DropIndex(
                name: "IX_Dog_race_Odds_Id",
                table: "Dog");

            migrationBuilder.DropColumn(
                name: "race_Odds_Id",
                table: "Dog");

            migrationBuilder.RenameColumn(
                name: "odds",
                table: "RaceOdds",
                newName: "odd");

            migrationBuilder.RenameColumn(
                name: "expected_Result",
                table: "RaceOdds",
                newName: "odd_Type");

            migrationBuilder.AlterColumn<string>(
                name: "result_Of_Race",
                table: "Race",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "dog_Finale_Position",
                table: "Race",
                type: "text[]",
                nullable: false);*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"

                ALTER TABLE ""Race""
                DROP COLUMN ""dog_Finale_Position"";

                ALTER TABLE ""RaceOdds""
                RENAME COLUMN ""odd_Type"" TO ""expected_Result"";

                ALTER TABLE ""RaceOdds""
                RENAME COLUMN ""odd"" TO ""odds"";

                ALTER TABLE ""Race""
                ALTER COLUMN ""result_Of_Race""
                DROP NOT NULL;

                ALTER TABLE ""Race""
                ALTER COLUMN ""result_Of_Race""
                DROP DEFAULT;

                ALTER TABLE ""Dog""
                ADD COLUMN ""race_Odds_Id""
                INTEGER NOT NULL DEFAULT 0;

                CREATE TABLE ""RaceHistory"" (
                    ""history_Race_Id"" SERIAL PRIMARY KEY,
                    ""dog_Id"" INTEGER NOT NULL,
                    ""race_Id"" INTEGER NOT NULL,
                    ""finale_Position"" INTEGER NOT NULL,
                    ""is_Winner"" BOOLEAN NOT NULL,
                    CONSTRAINT ""FK_RaceHistory_Dog_dog_Id"" FOREIGN KEY (""dog_Id"") REFERENCES ""Dog"" (""dog_Id"") ON DELETE CASCADE,
                    CONSTRAINT ""FK_RaceHistory_Race_race_Id"" FOREIGN KEY (""race_Id"") REFERENCES ""Race"" (""race_Id"") ON DELETE CASCADE
                );

                CREATE TABLE ""ProcessingTicket"" (
                    ""processing_Ticket_Id"" SERIAL PRIMARY KEY,
                    ""race_History_Id"" INTEGER NOT NULL,
                    ""race_Id"" INTEGER NOT NULL,
                    ""processing_Ticket_Status"" TEXT NOT NULL,
                    CONSTRAINT ""FK_ProcessingTicket_RaceHistory_race_History_Id"" FOREIGN KEY (""race_History_Id"") REFERENCES ""RaceHistory"" (""history_Race_Id"") ON DELETE CASCADE,
                    CONSTRAINT ""FK_ProcessingTicket_Race_race_Id"" FOREIGN KEY (""race_Id"") REFERENCES ""Race"" (""race_Id"") ON DELETE CASCADE
                );

                CREATE INDEX ""IX_Dog_race_Odds_Id"" 
                ON ""Dog"" (""race_Odds_Id"");

                CREATE INDEX ""IX_ProcessingTicket_race_History_Id"" 
                ON ""ProcessingTicket"" (""race_History_Id"");

                CREATE INDEX ""IX_ProcessingTicket_race_Id"" 
                ON ""ProcessingTicket"" (""race_Id"");

                CREATE INDEX ""IX_RaceHistory_dog_Id"" 
                ON ""RaceHistory"" (""dog_Id"");

                CREATE INDEX ""IX_RaceHistory_race_Id"" 
                ON ""RaceHistory"" (""race_Id"");

                ALTER TABLE ""Dog""
                ADD CONSTRAINT ""FK_Dog_RaceOdds_race_Odds_Id""
                FOREIGN KEY (""race_Odds_Id"") REFERENCES ""RaceOdds"" (""race_Odds_Id"")
                ON DELETE CASCADE;
            ");

            /* migrationBuilder.DropColumn(
                name: "dog_Finale_Position",
                table: "Race");

            migrationBuilder.RenameColumn(
                name: "odd_Type",
                table: "RaceOdds",
                newName: "expected_Result");

            migrationBuilder.RenameColumn(
                name: "odd",
                table: "RaceOdds",
                newName: "odds");

            migrationBuilder.AlterColumn<string>(
                name: "result_Of_Race",
                table: "Race",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "race_Odds_Id",
                table: "Dog",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RaceHistory",
                columns: table => new
                {
                    history_Race_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    dog_Id = table.Column<int>(type: "integer", nullable: false),
                    race_Id = table.Column<int>(type: "integer", nullable: false),
                    finale_Position = table.Column<int>(type: "integer", nullable: false),
                    is_Winner = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceHistory", x => x.history_Race_Id);
                    table.ForeignKey(
                        name: "FK_RaceHistory_Dog_dog_Id",
                        column: x => x.dog_Id,
                        principalTable: "Dog",
                        principalColumn: "dog_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaceHistory_Race_race_Id",
                        column: x => x.race_Id,
                        principalTable: "Race",
                        principalColumn: "race_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessingTicket",
                columns: table => new
                {
                    processing_Ticket_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    race_History_Id = table.Column<int>(type: "integer", nullable: false),
                    race_Id = table.Column<int>(type: "integer", nullable: false),
                    processing_Ticket_Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessingTicket", x => x.processing_Ticket_Id);
                    table.ForeignKey(
                        name: "FK_ProcessingTicket_RaceHistory_race_History_Id",
                        column: x => x.race_History_Id,
                        principalTable: "RaceHistory",
                        principalColumn: "history_Race_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessingTicket_Race_race_Id",
                        column: x => x.race_Id,
                        principalTable: "Race",
                        principalColumn: "race_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dog_race_Odds_Id",
                table: "Dog",
                column: "race_Odds_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessingTicket_race_History_Id",
                table: "ProcessingTicket",
                column: "race_History_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessingTicket_race_Id",
                table: "ProcessingTicket",
                column: "race_Id");

            migrationBuilder.CreateIndex(
                name: "IX_RaceHistory_dog_Id",
                table: "RaceHistory",
                column: "dog_Id");

            migrationBuilder.CreateIndex(
                name: "IX_RaceHistory_race_Id",
                table: "RaceHistory",
                column: "race_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dog_RaceOdds_race_Odds_Id",
                table: "Dog",
                column: "race_Odds_Id",
                principalTable: "RaceOdds",
                principalColumn: "race_Odds_Id",
                onDelete: ReferentialAction.Cascade); */
        }
    }
}
