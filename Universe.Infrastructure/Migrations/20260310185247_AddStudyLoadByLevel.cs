using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Universe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStudyLoadByLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseOfferingSession_CourseOfferings_CourseOfferingId",
                table: "CourseOfferingSession");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseOfferingSession_TeachingSession_TeachingSessionId",
                table: "CourseOfferingSession");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramSchedule_AcademicPrograms_ProgramId",
                table: "ProgramSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramSchedule_Semesters_SemesterId",
                table: "ProgramSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachingSession_AspNetUsers_InstructorId",
                table: "TeachingSession");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachingSession_Rooms_RoomId",
                table: "TeachingSession");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Name",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeachingSession",
                table: "TeachingSession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramSchedule",
                table: "ProgramSchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseOfferingSession",
                table: "CourseOfferingSession");

            migrationBuilder.DropColumn(
                name: "EnglishName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProgramSchedule");

            migrationBuilder.RenameTable(
                name: "TeachingSession",
                newName: "TeachingSessions");

            migrationBuilder.RenameTable(
                name: "ProgramSchedule",
                newName: "ProgramSchedules");

            migrationBuilder.RenameTable(
                name: "CourseOfferingSession",
                newName: "CourseOfferingSessions");

            migrationBuilder.RenameIndex(
                name: "IX_TeachingSession_RoomId",
                table: "TeachingSessions",
                newName: "IX_TeachingSessions_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_TeachingSession_InstructorId",
                table: "TeachingSessions",
                newName: "IX_TeachingSessions_InstructorId");

            migrationBuilder.RenameIndex(
                name: "IX_ProgramSchedule_SemesterId",
                table: "ProgramSchedules",
                newName: "IX_ProgramSchedules_SemesterId");

            migrationBuilder.RenameIndex(
                name: "IX_ProgramSchedule_ProgramId_SemesterId",
                table: "ProgramSchedules",
                newName: "IX_ProgramSchedules_ProgramId_SemesterId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseOfferingSession_TeachingSessionId",
                table: "CourseOfferingSessions",
                newName: "IX_CourseOfferingSessions_TeachingSessionId");

            migrationBuilder.AlterColumn<int>(
                name: "Religion",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "MilitaryInfo_MilitaryStatus",
                table: "Students",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "MilitaryInfo_MilitaryNumber",
                table: "Students",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "MilitaryInfo_EnrollmentDate",
                table: "Students",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "MilitaryInfo_EndDate",
                table: "Students",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "MilitaryInfo_DecisionNumber",
                table: "Students",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "MilitaryInfo_DecisionDate",
                table: "Students",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfGroups",
                table: "CourseOfferings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GroupNumber",
                table: "TeachingSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeachingSessions",
                table: "TeachingSessions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramSchedules",
                table: "ProgramSchedules",
                columns: new[] { "ProgramId", "SemesterId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseOfferingSessions",
                table: "CourseOfferingSessions",
                columns: new[] { "CourseOfferingId", "TeachingSessionId" });

            migrationBuilder.CreateTable(
                name: "StudyLoadByLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MinHours = table.Column<int>(type: "int", nullable: false),
                    MaxHours = table.Column<int>(type: "int", nullable: false),
                    LevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SemesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyLoadByLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyLoadByLevels_AcademicPrograms_AcademicProgramId",
                        column: x => x.AcademicProgramId,
                        principalTable: "AcademicPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudyLoadByLevels_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudyLoadByLevels_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Name",
                table: "AspNetUsers",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_StudyLoadByLevels_AcademicProgramId",
                table: "StudyLoadByLevels",
                column: "AcademicProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyLoadByLevels_LevelId",
                table: "StudyLoadByLevels",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyLoadByLevels_SemesterId",
                table: "StudyLoadByLevels",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseOfferingSessions_CourseOfferings_CourseOfferingId",
                table: "CourseOfferingSessions",
                column: "CourseOfferingId",
                principalTable: "CourseOfferings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseOfferingSessions_TeachingSessions_TeachingSessionId",
                table: "CourseOfferingSessions",
                column: "TeachingSessionId",
                principalTable: "TeachingSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramSchedules_AcademicPrograms_ProgramId",
                table: "ProgramSchedules",
                column: "ProgramId",
                principalTable: "AcademicPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramSchedules_Semesters_SemesterId",
                table: "ProgramSchedules",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachingSessions_AspNetUsers_InstructorId",
                table: "TeachingSessions",
                column: "InstructorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachingSessions_Rooms_RoomId",
                table: "TeachingSessions",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseOfferingSessions_CourseOfferings_CourseOfferingId",
                table: "CourseOfferingSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseOfferingSessions_TeachingSessions_TeachingSessionId",
                table: "CourseOfferingSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramSchedules_AcademicPrograms_ProgramId",
                table: "ProgramSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_ProgramSchedules_Semesters_SemesterId",
                table: "ProgramSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachingSessions_AspNetUsers_InstructorId",
                table: "TeachingSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_TeachingSessions_Rooms_RoomId",
                table: "TeachingSessions");

            migrationBuilder.DropTable(
                name: "StudyLoadByLevels");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Name",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeachingSessions",
                table: "TeachingSessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgramSchedules",
                table: "ProgramSchedules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseOfferingSessions",
                table: "CourseOfferingSessions");

            migrationBuilder.DropColumn(
                name: "NumberOfGroups",
                table: "CourseOfferings");

            migrationBuilder.DropColumn(
                name: "GroupNumber",
                table: "TeachingSessions");

            migrationBuilder.RenameTable(
                name: "TeachingSessions",
                newName: "TeachingSession");

            migrationBuilder.RenameTable(
                name: "ProgramSchedules",
                newName: "ProgramSchedule");

            migrationBuilder.RenameTable(
                name: "CourseOfferingSessions",
                newName: "CourseOfferingSession");

            migrationBuilder.RenameIndex(
                name: "IX_TeachingSessions_RoomId",
                table: "TeachingSession",
                newName: "IX_TeachingSession_RoomId");

            migrationBuilder.RenameIndex(
                name: "IX_TeachingSessions_InstructorId",
                table: "TeachingSession",
                newName: "IX_TeachingSession_InstructorId");

            migrationBuilder.RenameIndex(
                name: "IX_ProgramSchedules_SemesterId",
                table: "ProgramSchedule",
                newName: "IX_ProgramSchedule_SemesterId");

            migrationBuilder.RenameIndex(
                name: "IX_ProgramSchedules_ProgramId_SemesterId",
                table: "ProgramSchedule",
                newName: "IX_ProgramSchedule_ProgramId_SemesterId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseOfferingSessions_TeachingSessionId",
                table: "CourseOfferingSession",
                newName: "IX_CourseOfferingSession_TeachingSessionId");

            migrationBuilder.AlterColumn<int>(
                name: "Religion",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MilitaryInfo_MilitaryStatus",
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
                name: "MilitaryInfo_MilitaryNumber",
                table: "Students",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "MilitaryInfo_EnrollmentDate",
                table: "Students",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "MilitaryInfo_EndDate",
                table: "Students",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MilitaryInfo_DecisionNumber",
                table: "Students",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "MilitaryInfo_DecisionDate",
                table: "Students",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EnglishName",
                table: "Students",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ProgramSchedule",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeachingSession",
                table: "TeachingSession",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgramSchedule",
                table: "ProgramSchedule",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseOfferingSession",
                table: "CourseOfferingSession",
                columns: new[] { "CourseOfferingId", "TeachingSessionId" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Name",
                table: "AspNetUsers",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseOfferingSession_CourseOfferings_CourseOfferingId",
                table: "CourseOfferingSession",
                column: "CourseOfferingId",
                principalTable: "CourseOfferings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseOfferingSession_TeachingSession_TeachingSessionId",
                table: "CourseOfferingSession",
                column: "TeachingSessionId",
                principalTable: "TeachingSession",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramSchedule_AcademicPrograms_ProgramId",
                table: "ProgramSchedule",
                column: "ProgramId",
                principalTable: "AcademicPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramSchedule_Semesters_SemesterId",
                table: "ProgramSchedule",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachingSession_AspNetUsers_InstructorId",
                table: "TeachingSession",
                column: "InstructorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeachingSession_Rooms_RoomId",
                table: "TeachingSession",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
