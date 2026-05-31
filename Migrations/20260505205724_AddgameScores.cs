using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypingTest.Migrations
{
    /// <inheritdoc />
    public partial class AddgameScores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameScores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GameType = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    BestStreak = table.Column<int>(type: "int", nullable: false),
                    WordsCompleted = table.Column<int>(type: "int", nullable: false),
                    BestWpm = table.Column<int>(type: "int", nullable: false),
                    PerfectHits = table.Column<int>(type: "int", nullable: false),
                    PlayedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameScores_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameScores_GameType_Score",
                table: "GameScores",
                columns: new[] { "GameType", "Score" });

            migrationBuilder.CreateIndex(
                name: "IX_GameScores_PlayedAt",
                table: "GameScores",
                column: "PlayedAt");

            migrationBuilder.CreateIndex(
                name: "IX_GameScores_UserId",
                table: "GameScores",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameScores");
        }
    }
}
