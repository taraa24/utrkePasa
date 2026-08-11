using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UtrkePasa.Domain.Migrations
{
    /// <inheritdoc />
    public partial class IntroduceRaceHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dog_Race_race_Id",
                table: "Dog");

            migrationBuilder.DropIndex(
                name: "IX_Dog_race_Id",
                table: "Dog");

            migrationBuilder.DropColumn(
                name: "race_Id",
                table: "Dog");

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

            migrationBuilder.CreateIndex(
                name: "IX_RaceHistory_dog_Id",
                table: "RaceHistory",
                column: "dog_Id");

            migrationBuilder.CreateIndex(
                name: "IX_RaceHistory_race_Id",
                table: "RaceHistory",
                column: "race_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RaceHistory");

            migrationBuilder.AddColumn<int>(
                name: "race_Id",
                table: "Dog",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dog_race_Id",
                table: "Dog",
                column: "race_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Dog_Race_race_Id",
                table: "Dog",
                column: "race_Id",
                principalTable: "Race",
                principalColumn: "race_Id");
        }
    }
}
