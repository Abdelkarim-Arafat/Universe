using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Universe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addRelationToCommittee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExamTermId",
                table: "ExamCommittees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ExamCommittees_ExamTermId",
                table: "ExamCommittees",
                column: "ExamTermId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamCommittees_ExamTerms_ExamTermId",
                table: "ExamCommittees",
                column: "ExamTermId",
                principalTable: "ExamTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamCommittees_ExamTerms_ExamTermId",
                table: "ExamCommittees");

            migrationBuilder.DropIndex(
                name: "IX_ExamCommittees_ExamTermId",
                table: "ExamCommittees");

            migrationBuilder.DropColumn(
                name: "ExamTermId",
                table: "ExamCommittees");
        }
    }
}
