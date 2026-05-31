using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypingTest.Migrations
{
    /// <inheritdoc />
    public partial class ADdCurrentStageToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalKeyStrokes",
                table: "TestResults",
                newName: "TotalKeystrokes");

            migrationBuilder.AddColumn<int>(
                name: "CurrentStage",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentStage",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "TotalKeystrokes",
                table: "TestResults",
                newName: "TotalKeyStrokes");
        }
    }
}
