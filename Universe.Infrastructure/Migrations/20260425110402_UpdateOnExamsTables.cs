using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Universe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOnExamsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamSeats_ExamSessions_ExamSessionId",
                table: "ExamSeats");

            migrationBuilder.DropTable(
                name: "ExamSessions");

            migrationBuilder.DropIndex(
                name: "IX_ExamSeats_ExamSessionId",
                table: "ExamSeats");

            migrationBuilder.DropColumn(
                name: "RoomTypeId",
                table: "Rooms");

            migrationBuilder.AddColumn<Guid>(
                name: "ExamCommitteeId",
                table: "ExamSeats",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CourseOfferingExams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    CourseOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamTermId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseOfferingExams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseOfferingExams_CourseOfferings_CourseOfferingId",
                        column: x => x.CourseOfferingId,
                        principalTable: "CourseOfferings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseOfferingExams_ExamTerms_ExamTermId",
                        column: x => x.ExamTermId,
                        principalTable: "ExamTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamCommittees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaxCapacity = table.Column<int>(type: "int", nullable: false),
                    CommitteeNumber = table.Column<int>(type: "int", nullable: false),
                    CourseOfferingExamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamCommittees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamCommittees_CourseOfferingExams_CourseOfferingExamId",
                        column: x => x.CourseOfferingExamId,
                        principalTable: "CourseOfferingExams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamCommittees_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamSeats_ExamCommitteeId",
                table: "ExamSeats",
                column: "ExamCommitteeId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferingExams_CourseOfferingId",
                table: "CourseOfferingExams",
                column: "CourseOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferingExams_ExamTermId",
                table: "CourseOfferingExams",
                column: "ExamTermId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamCommittees_CourseOfferingExamId",
                table: "ExamCommittees",
                column: "CourseOfferingExamId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamCommittees_RoomId",
                table: "ExamCommittees",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSeats_ExamCommittees_ExamCommitteeId",
                table: "ExamSeats",
                column: "ExamCommitteeId",
                principalTable: "ExamCommittees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamSeats_ExamCommittees_ExamCommitteeId",
                table: "ExamSeats");

            migrationBuilder.DropTable(
                name: "ExamCommittees");

            migrationBuilder.DropTable(
                name: "CourseOfferingExams");

            migrationBuilder.DropIndex(
                name: "IX_ExamSeats_ExamCommitteeId",
                table: "ExamSeats");

            migrationBuilder.DropColumn(
                name: "ExamCommitteeId",
                table: "ExamSeats");

            migrationBuilder.AddColumn<Guid>(
                name: "RoomTypeId",
                table: "Rooms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ExamSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamTermId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    MaxCapacity = table.Column<int>(type: "int", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamSessions_CourseOfferings_CourseOfferingId",
                        column: x => x.CourseOfferingId,
                        principalTable: "CourseOfferings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamSessions_ExamTerms_ExamTermId",
                        column: x => x.ExamTermId,
                        principalTable: "ExamTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamSessions_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamSeats_ExamSessionId",
                table: "ExamSeats",
                column: "ExamSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSessions_CourseOfferingId",
                table: "ExamSessions",
                column: "CourseOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSessions_ExamTermId",
                table: "ExamSessions",
                column: "ExamTermId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSessions_RoomId",
                table: "ExamSessions",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSeats_ExamSessions_ExamSessionId",
                table: "ExamSeats",
                column: "ExamSessionId",
                principalTable: "ExamSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
