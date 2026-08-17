using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace UtrkePasa.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddProcessingTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessingTicket",
                columns: table => new
                {
                    processing_Ticket_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    processing_Ticket_Status = table.Column<string>(type: "text", nullable: false),
                    race_Id = table.Column<int>(type: "integer", nullable: false),
                    race_History_Id = table.Column<int>(type: "integer", nullable: false)
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
                name: "IX_ProcessingTicket_race_History_Id",
                table: "ProcessingTicket",
                column: "race_History_Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessingTicket_race_Id",
                table: "ProcessingTicket",
                column: "race_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessingTicket");
        }
    }
}
