using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Universe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOnStudyLoad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudyLoadByLevels_Semesters_SemesterId",
                table: "StudyLoadByLevels");

            migrationBuilder.DropIndex(
                name: "IX_StudyLoadByLevels_SemesterId",
                table: "StudyLoadByLevels");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "StudyLoadByLevels");

            migrationBuilder.AddColumn<int>(
                name: "SemesterType",
                table: "StudyLoadByLevels",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SemesterType",
                table: "StudyLoadByLevels");

            migrationBuilder.AddColumn<Guid>(
                name: "SemesterId",
                table: "StudyLoadByLevels",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_StudyLoadByLevels_SemesterId",
                table: "StudyLoadByLevels",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudyLoadByLevels_Semesters_SemesterId",
                table: "StudyLoadByLevels",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
