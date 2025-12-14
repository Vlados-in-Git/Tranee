using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TraneeLibrary.Migrations
{
    /// <inheritdoc />
    public partial class AddObjectTraningTempalteToTraningsession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Sessions_TrainingTemplateId",
                table: "Sessions",
                column: "TrainingTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_TrainingTemplates_TrainingTemplateId",
                table: "Sessions",
                column: "TrainingTemplateId",
                principalTable: "TrainingTemplates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_TrainingTemplates_TrainingTemplateId",
                table: "Sessions");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_TrainingTemplateId",
                table: "Sessions");
        }
    }
}
