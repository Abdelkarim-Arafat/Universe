using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Universe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProgramRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Colleges_CollegeId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Levels_Colleges_CollegeId",
                table: "Levels");

            migrationBuilder.DropForeignKey(
                name: "FK_StudyLoadRules_Colleges_CollegeId",
                table: "StudyLoadRules");

            migrationBuilder.DropIndex(
                name: "IX_StudyLoadRules_CollegeId",
                table: "StudyLoadRules");

            migrationBuilder.DropColumn(
                name: "CollegeId",
                table: "StudyLoadRules");

            migrationBuilder.RenameColumn(
                name: "CollegeId",
                table: "Levels",
                newName: "AcademicProgramd");

            migrationBuilder.RenameIndex(
                name: "IX_Levels_CollegeId",
                table: "Levels",
                newName: "IX_Levels_AcademicProgramd");

            migrationBuilder.RenameColumn(
                name: "CollegeId",
                table: "Grades",
                newName: "AcademicProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_CollegeId",
                table: "Grades",
                newName: "IX_Grades_AcademicProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_AcademicPrograms_AcademicProgramId",
                table: "Grades",
                column: "AcademicProgramId",
                principalTable: "AcademicPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_AcademicPrograms_AcademicProgramd",
                table: "Levels",
                column: "AcademicProgramd",
                principalTable: "AcademicPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_AcademicPrograms_AcademicProgramId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Levels_AcademicPrograms_AcademicProgramd",
                table: "Levels");

            migrationBuilder.RenameColumn(
                name: "AcademicProgramd",
                table: "Levels",
                newName: "CollegeId");

            migrationBuilder.RenameIndex(
                name: "IX_Levels_AcademicProgramd",
                table: "Levels",
                newName: "IX_Levels_CollegeId");

            migrationBuilder.RenameColumn(
                name: "AcademicProgramId",
                table: "Grades",
                newName: "CollegeId");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_AcademicProgramId",
                table: "Grades",
                newName: "IX_Grades_CollegeId");

            migrationBuilder.AddColumn<Guid>(
                name: "CollegeId",
                table: "StudyLoadRules",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudyLoadRules_CollegeId",
                table: "StudyLoadRules",
                column: "CollegeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Colleges_CollegeId",
                table: "Grades",
                column: "CollegeId",
                principalTable: "Colleges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_Colleges_CollegeId",
                table: "Levels",
                column: "CollegeId",
                principalTable: "Colleges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudyLoadRules_Colleges_CollegeId",
                table: "StudyLoadRules",
                column: "CollegeId",
                principalTable: "Colleges",
                principalColumn: "Id");
        }
    }
}
