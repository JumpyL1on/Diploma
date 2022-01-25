using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Infrastructure.Migrations
{
    public partial class matches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Tournaments",
                newName: "TournamentStatus");

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ParticipantAId = table.Column<Guid>(type: "uuid", nullable: true),
                    ParticipantBId = table.Column<Guid>(type: "uuid", nullable: true),
                    TournamentId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParticipantAScore = table.Column<int>(type: "integer", nullable: false),
                    ParticipantBScore = table.Column<int>(type: "integer", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Participants_ParticipantAId",
                        column: x => x.ParticipantAId,
                        principalTable: "Participants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matches_Participants_ParticipantBId",
                        column: x => x.ParticipantBId,
                        principalTable: "Participants",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matches_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_ParticipantAId",
                table: "Matches",
                column: "ParticipantAId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_ParticipantBId",
                table: "Matches",
                column: "ParticipantBId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_TournamentId",
                table: "Matches",
                column: "TournamentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.RenameColumn(
                name: "TournamentStatus",
                table: "Tournaments",
                newName: "Status");
        }
    }
}
