using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypingTest.Migrations
{
    /// <inheritdoc />
    public partial class AddStageToWordPassage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stage",
                table: "WordPassages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 1,
                column: "Stage",
                value: 0);

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 2,
                column: "Stage",
                value: 0);

            migrationBuilder.UpdateData(
                table: "WordPassages",
                keyColumn: "Id",
                keyValue: 3,
                column: "Stage",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stage",
                table: "WordPassages");
        }
    }
}
