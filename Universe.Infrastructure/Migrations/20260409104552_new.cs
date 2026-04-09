using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Universe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSemesterSummarys_Semesters_SemesterId",
                table: "StudentSemesterSummarys");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSemesterSummarys_Students_StudentId",
                table: "StudentSemesterSummarys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSemesterSummarys",
                table: "StudentSemesterSummarys");

            migrationBuilder.RenameTable(
                name: "StudentSemesterSummarys",
                newName: "StudentSemesterSummaries");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSemesterSummarys_StudentId",
                table: "StudentSemesterSummaries",
                newName: "IX_StudentSemesterSummaries_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSemesterSummarys_SemesterId",
                table: "StudentSemesterSummaries",
                newName: "IX_StudentSemesterSummaries_SemesterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSemesterSummaries",
                table: "StudentSemesterSummaries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSemesterSummaries_Semesters_SemesterId",
                table: "StudentSemesterSummaries",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSemesterSummaries_Students_StudentId",
                table: "StudentSemesterSummaries",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSemesterSummaries_Semesters_SemesterId",
                table: "StudentSemesterSummaries");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSemesterSummaries_Students_StudentId",
                table: "StudentSemesterSummaries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSemesterSummaries",
                table: "StudentSemesterSummaries");

            migrationBuilder.RenameTable(
                name: "StudentSemesterSummaries",
                newName: "StudentSemesterSummarys");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSemesterSummaries_StudentId",
                table: "StudentSemesterSummarys",
                newName: "IX_StudentSemesterSummarys_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSemesterSummaries_SemesterId",
                table: "StudentSemesterSummarys",
                newName: "IX_StudentSemesterSummarys_SemesterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSemesterSummarys",
                table: "StudentSemesterSummarys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSemesterSummarys_Semesters_SemesterId",
                table: "StudentSemesterSummarys",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSemesterSummarys_Students_StudentId",
                table: "StudentSemesterSummarys",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
