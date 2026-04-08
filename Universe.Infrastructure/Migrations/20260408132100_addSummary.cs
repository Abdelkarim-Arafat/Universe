using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Universe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addSummary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentSemesterSummary_Semesters_SemesterId",
                table: "StudentSemesterSummary");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentSemesterSummary_Students_StudentId",
                table: "StudentSemesterSummary");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentSemesterSummary",
                table: "StudentSemesterSummary");

            migrationBuilder.RenameTable(
                name: "StudentSemesterSummary",
                newName: "StudentSemesterSummarys");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSemesterSummary_StudentId",
                table: "StudentSemesterSummarys",
                newName: "IX_StudentSemesterSummarys_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSemesterSummary_SemesterId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                newName: "StudentSemesterSummary");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSemesterSummarys_StudentId",
                table: "StudentSemesterSummary",
                newName: "IX_StudentSemesterSummary_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_StudentSemesterSummarys_SemesterId",
                table: "StudentSemesterSummary",
                newName: "IX_StudentSemesterSummary_SemesterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentSemesterSummary",
                table: "StudentSemesterSummary",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSemesterSummary_Semesters_SemesterId",
                table: "StudentSemesterSummary",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentSemesterSummary_Students_StudentId",
                table: "StudentSemesterSummary",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
