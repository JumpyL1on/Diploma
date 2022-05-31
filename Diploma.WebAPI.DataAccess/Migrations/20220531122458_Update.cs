using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Diploma.WebAPI.DataAccess.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_SteamGames_GameId",
                table: "Team");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSteamGame_SteamGames_GameId",
                table: "UserSteamGame");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SteamGames",
                table: "SteamGames");

            migrationBuilder.RenameTable(
                name: "SteamGames",
                newName: "Games");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Games_GameId",
                table: "Team",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSteamGame_Games_GameId",
                table: "UserSteamGame",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_Games_GameId",
                table: "Team");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSteamGame_Games_GameId",
                table: "UserSteamGame");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "SteamGames");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SteamGames",
                table: "SteamGames",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_SteamGames_GameId",
                table: "Team",
                column: "GameId",
                principalTable: "SteamGames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSteamGame_SteamGames_GameId",
                table: "UserSteamGame",
                column: "GameId",
                principalTable: "SteamGames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
