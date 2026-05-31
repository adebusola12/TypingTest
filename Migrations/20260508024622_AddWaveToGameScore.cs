using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypingTest.Migrations
{
    /// <inheritdoc />
    public partial class AddWaveToGameScore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Wave",
                table: "GameScores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Wave",
                table: "GameScores");
        }
    }
}
