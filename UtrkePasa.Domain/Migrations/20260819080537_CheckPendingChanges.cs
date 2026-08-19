using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtrkePasa.Domain.Migrations
{
    /// <inheritdoc />
    public partial class CheckPendingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"
                ALTER TABLE ""RaceOdds""
                DROP CONSTRAINT ""FK_RaceOdds_Race_RaceId"";

                DROP INDEX ""IX_RaceOdds_RaceId"";

                ALTER TABLE ""RaceOdds""
                DROP COLUMN ""RaceId"";

                CREATE INDEX ""IX_RaceOdds_race_Id""
                ON ""RaceOdds""(""race_Id"");

                ALTER TABLE ""RaceOdds""
                ADD CONSTRAINT ""FK_RaceOdds_Race_race_Id""
                FOREIGN KEY (""race_Id"") REFERENCES ""Race"" (""race_Id"")
                ON DELETE CASCADE;

                ALTER TABLE ""Ticket"" 
                ADD COLUMN ""expected_Result"" TEXT NOT NULL DEFAULT '';

                ALTER TABLE ""Ticket""
                ADD COLUMN ""is_Winning_Ticket"" BOOL NOT NULL DEFAULT FALSE;
           ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"
                ALTER TABLE ""RaceOdds""
                DROP CONSTRAINT ""FK_RaceOdds_Race_race_Id"";

                ALTER TABLE ""RaceOdds"" 
                ADD COLUMN ""RaceId"" INTEGER;

                CREATE INDEX ""IX_RaceOdds_RaceId""
                ON ""RaceOdds"" (""RaceId"");

                ALTER TABLE ""RaceOdds""
                ADD CONSTRAINT ""FK_RaceOdds_Race_RaceId""
                FOREIGN KEY (""RaceId"") REFERENCES ""Race"" (""race_Id"");

                ALTER TABLE ""Ticket""
                DROP COLUMN ""expected_Result"";

                ALTER TABLE ""Ticket""
                DROP COLUMN ""is_Winning_Ticket"";            
            ");
        }
    }
}
