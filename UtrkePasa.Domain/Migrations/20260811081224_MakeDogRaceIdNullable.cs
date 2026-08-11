using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UtrkePasa.Domain.Migrations
{
    /// <inheritdoc />
    public partial class MakeDogRaceIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dog_Race_race_Id",
                table: "Dog");

            migrationBuilder.AlterColumn<int>(
                name: "race_Id",
                table: "Dog",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Dog_Race_race_Id",
                table: "Dog",
                column: "race_Id",
                principalTable: "Race",
                principalColumn: "race_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dog_Race_race_Id",
                table: "Dog");

            migrationBuilder.AlterColumn<int>(
                name: "race_Id",
                table: "Dog",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Dog_Race_race_Id",
                table: "Dog",
                column: "race_Id",
                principalTable: "Race",
                principalColumn: "race_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
