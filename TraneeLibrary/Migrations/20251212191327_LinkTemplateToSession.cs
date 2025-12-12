using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TraneeLibrary.Migrations
{
    /// <inheritdoc />
    public partial class LinkTemplateToSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Exercises_ExerciseId",
                table: "Sets");

            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Sessions_TraningSessionId",
                table: "Sets");

            migrationBuilder.DropTable(
                name: "ExerciseTemplateTrainingTemplate");

            migrationBuilder.DropIndex(
                name: "IX_Sets_TraningSessionId",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "TraningSessionId",
                table: "Sets");

            migrationBuilder.AlterColumn<int>(
                name: "ExerciseId",
                table: "Sets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrainingTemplateId",
                table: "Sessions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseTemplates_TrainingTemplateId",
                table: "ExerciseTemplates",
                column: "TrainingTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseTemplates_TrainingTemplates_TrainingTemplateId",
                table: "ExerciseTemplates",
                column: "TrainingTemplateId",
                principalTable: "TrainingTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Exercises_ExerciseId",
                table: "Sets",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseTemplates_TrainingTemplates_TrainingTemplateId",
                table: "ExerciseTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Exercises_ExerciseId",
                table: "Sets");

            migrationBuilder.DropIndex(
                name: "IX_ExerciseTemplates_TrainingTemplateId",
                table: "ExerciseTemplates");

            migrationBuilder.DropColumn(
                name: "TrainingTemplateId",
                table: "Sessions");

            migrationBuilder.AlterColumn<int>(
                name: "ExerciseId",
                table: "Sets",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "TraningSessionId",
                table: "Sets",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ExerciseTemplateTrainingTemplate",
                columns: table => new
                {
                    ExerciseTemplatesId = table.Column<int>(type: "INTEGER", nullable: false),
                    TrainingTemplateId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseTemplateTrainingTemplate", x => new { x.ExerciseTemplatesId, x.TrainingTemplateId });
                    table.ForeignKey(
                        name: "FK_ExerciseTemplateTrainingTemplate_ExerciseTemplates_ExerciseTemplatesId",
                        column: x => x.ExerciseTemplatesId,
                        principalTable: "ExerciseTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseTemplateTrainingTemplate_TrainingTemplates_TrainingTemplateId",
                        column: x => x.TrainingTemplateId,
                        principalTable: "TrainingTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sets_TraningSessionId",
                table: "Sets",
                column: "TraningSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseTemplateTrainingTemplate_TrainingTemplateId",
                table: "ExerciseTemplateTrainingTemplate",
                column: "TrainingTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Exercises_ExerciseId",
                table: "Sets",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Sessions_TraningSessionId",
                table: "Sets",
                column: "TraningSessionId",
                principalTable: "Sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
