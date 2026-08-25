using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtrkePasa.Domain.Migrations
{
    /// <inheritdoc />
    public partial class MakeisLeaderAndUUIDUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"

                ALTER TABLE ""Register""
                ADD CONSTRAINT ""UQ_Register_isLeader_app_guid""
                UNIQUE (is_leader, app_guid);

            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                ALTER TABLE ""Register""
                DROP CONSTRAINT ""UQ_Register_isLeader_app_guid"";
            ");
        }
    }
}
