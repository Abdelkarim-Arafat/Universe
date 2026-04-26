using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Universe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatesOnExamTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamCommittees_CourseOfferingExams_CourseOfferingExamId",
                table: "ExamCommittees");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamSeats_ExamCommittees_ExamCommitteeId",
                table: "ExamSeats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamSeats",
                table: "ExamSeats");

            migrationBuilder.DropIndex(
                name: "IX_ExamCommittees_CourseOfferingExamId",
                table: "ExamCommittees");

            migrationBuilder.DropColumn(
                name: "ExamSessionId",
                table: "ExamSeats");

            migrationBuilder.DropColumn(
                name: "CourseOfferingExamId",
                table: "ExamCommittees");

            migrationBuilder.RenameColumn(
                name: "ExamCommitteeId",
                table: "ExamSeats",
                newName: "CourseOfferingCommitteeId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamSeats_ExamCommitteeId",
                table: "ExamSeats",
                newName: "IX_ExamSeats_CourseOfferingCommitteeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamSeats",
                table: "ExamSeats",
                columns: new[] { "StudentId", "CourseOfferingCommitteeId" });

            migrationBuilder.CreateTable(
                name: "CourseOfferingCommittees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseOfferingExamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamCommitteeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseOfferingCommittees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseOfferingCommittees_CourseOfferingExams_CourseOfferingExamId",
                        column: x => x.CourseOfferingExamId,
                        principalTable: "CourseOfferingExams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseOfferingCommittees_ExamCommittees_ExamCommitteeId",
                        column: x => x.ExamCommitteeId,
                        principalTable: "ExamCommittees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferingCommittees_CourseOfferingExamId",
                table: "CourseOfferingCommittees",
                column: "CourseOfferingExamId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferingCommittees_ExamCommitteeId",
                table: "CourseOfferingCommittees",
                column: "ExamCommitteeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSeats_CourseOfferingCommittees_CourseOfferingCommitteeId",
                table: "ExamSeats",
                column: "CourseOfferingCommitteeId",
                principalTable: "CourseOfferingCommittees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamSeats_CourseOfferingCommittees_CourseOfferingCommitteeId",
                table: "ExamSeats");

            migrationBuilder.DropTable(
                name: "CourseOfferingCommittees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExamSeats",
                table: "ExamSeats");

            migrationBuilder.RenameColumn(
                name: "CourseOfferingCommitteeId",
                table: "ExamSeats",
                newName: "ExamCommitteeId");

            migrationBuilder.RenameIndex(
                name: "IX_ExamSeats_CourseOfferingCommitteeId",
                table: "ExamSeats",
                newName: "IX_ExamSeats_ExamCommitteeId");

            migrationBuilder.AddColumn<Guid>(
                name: "ExamSessionId",
                table: "ExamSeats",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CourseOfferingExamId",
                table: "ExamCommittees",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExamSeats",
                table: "ExamSeats",
                columns: new[] { "StudentId", "ExamSessionId" });

            migrationBuilder.CreateIndex(
                name: "IX_ExamCommittees_CourseOfferingExamId",
                table: "ExamCommittees",
                column: "CourseOfferingExamId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamCommittees_CourseOfferingExams_CourseOfferingExamId",
                table: "ExamCommittees",
                column: "CourseOfferingExamId",
                principalTable: "CourseOfferingExams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSeats_ExamCommittees_ExamCommitteeId",
                table: "ExamSeats",
                column: "ExamCommitteeId",
                principalTable: "ExamCommittees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
