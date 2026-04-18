using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Universe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateStudentAssessment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentAssessments",
                table: "StudentAssessments");

            migrationBuilder.DropIndex(
                name: "IX_StudentAssessments_StudentId",
                table: "StudentAssessments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StudentAssessments");

            migrationBuilder.DropColumn(
                name: "CourseOfferingId",
                table: "StudentAssessments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentAssessments",
                table: "StudentAssessments",
                columns: new[] { "StudentId", "CourseOfferingAssessmentId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentAssessments",
                table: "StudentAssessments");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "StudentAssessments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CourseOfferingId",
                table: "StudentAssessments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentAssessments",
                table: "StudentAssessments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssessments_StudentId",
                table: "StudentAssessments",
                column: "StudentId");
        }
    }
}
