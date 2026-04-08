using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Universe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addmigrationsAddSumarry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
 

            migrationBuilder.AlterColumn<int>(
                name: "Religion",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PreviousQualification_AdmissionType",
                table: "Students",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "MaritalStatus",
                table: "Students",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Students",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            //migrationBuilder.AddColumn<string>(
            //    name: "OptionalGroupCode",
            //    table: "CourseOfferings",
            //    type: "nvarchar(50)",
            //    maxLength: 50,
            //    nullable: true);

            migrationBuilder.CreateTable(
                name: "StudentAssessments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    degree = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CourseOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseOfferingAssessmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAssessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAssessments_CourseOfferingAssessments_CourseOfferingAssessmentId",
                        column: x => x.CourseOfferingAssessmentId,
                        principalTable: "CourseOfferingAssessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentAssessments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentSemesterSummary",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SemesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SemesterGPA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CumulativeGPA = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AttemptedHours = table.Column<int>(type: "int", nullable: false),
                    TotalHoursEarned = table.Column<int>(type: "int", nullable: false),
                    SemesterGrade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CumulativeGrade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFinalized = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentSemesterSummary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentSemesterSummary_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentSemesterSummary_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssessments_CourseOfferingAssessmentId",
                table: "StudentAssessments",
                column: "CourseOfferingAssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssessments_StudentId",
                table: "StudentAssessments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSemesterSummary_SemesterId",
                table: "StudentSemesterSummary",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentSemesterSummary_StudentId",
                table: "StudentSemesterSummary",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentAssessments");

            migrationBuilder.DropTable(
                name: "StudentSemesterSummary");

            migrationBuilder.DropColumn(
                name: "IsCurrent",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "OptionalGroupCode",
                table: "CourseOfferings");

            migrationBuilder.AlterColumn<string>(
                name: "Religion",
                table: "Students",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PreviousQualification_AdmissionType",
                table: "Students",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaritalStatus",
                table: "Students",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Students",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtionalGroupCode",
                table: "CourseOfferings",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
