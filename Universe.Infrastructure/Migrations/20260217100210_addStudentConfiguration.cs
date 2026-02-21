using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Universe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addStudentConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArabicName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EnglishName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StudentCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Religion = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    PlaceOfBirth = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NationalIdOrPassport = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactInfo_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactInfo_Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ContactInfo_HomePhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ContactInfo_Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ContactInfo_Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ParentInfo_GuardianName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParentInfo_Job = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ParentInfo_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParentInfo_Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ParentInfo_Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ParentInfo_Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ParentInfo_Fax = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PreviousQualification_SchoolName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PreviousQualification_Qualification = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PreviousQualification_GraduationYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreviousQualification_TotalGrade = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    PreviousQualification_AdmissionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MilitaryInfo_MilitaryStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MilitaryInfo_MilitaryNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MilitaryInfo_DecisionNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MilitaryInfo_DecisionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    MilitaryInfo_EnrollmentDate = table.Column<DateOnly>(type: "date", nullable: false),
                    MilitaryInfo_EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CollegeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Colleges_CollegeId",
                        column: x => x.CollegeId,
                        principalTable: "Colleges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("019c0582-3473-7802-8f11-50cc1e6513d5"),
                column: "StudentId",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("019c1e76-6f5a-7522-8327-a2a72adbbbe8"),
                column: "StudentId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Students_CollegeId",
                table: "Students",
                column: "CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_DepartmentId",
                table: "Students",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_UserId",
                table: "Students",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "AspNetUsers");
        }
    }
}
