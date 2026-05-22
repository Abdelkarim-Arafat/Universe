using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Universe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBuildSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Grades_Code",
                table: "Grades");

            migrationBuilder.AlterColumn<string>(
                name: "RoomType",
                table: "Rooms",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Rooms",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<decimal>(
                name: "MinGradePoint",
                table: "Grades",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxGradePoint",
                table: "Grades",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Buildings",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "Buildings",
                columns: new[] { "Id", "Code", "CreatedAt", "CreatedById", "DeletedAt", "IsDeleted", "Name", "UpdatedAt", "UpdatedById" },
                values: new object[,]
                {
                    { new Guid("019dfa7b-3b1a-7b3a-9e1d-0356789e840b"), "LIB", new DateTime(2026, 5, 6, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, "Central Library", null, null },
                    { new Guid("019dfa7b-3b1a-7b3a-9e1d-4014a33a997f"), "ADM01", new DateTime(2026, 5, 6, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, "Main Administration Building", null, null },
                    { new Guid("019dfa7b-3b1a-7b3a-9e1d-b67175b29c14"), "COMP", new DateTime(2026, 5, 6, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, "Faculty of Computing", null, null },
                    { new Guid("019dfa7b-3b1a-7b3a-9e1d-ddcc7f909a82"), "LABS", new DateTime(2026, 5, 6, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, "Scientific Laboratories", null, null }
                });

            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "Id", "AcademicProgramId", "Code", "CreatedAt", "CreatedById", "DeletedAt", "IsDeleted", "MaxGradePoint", "MaxScore", "MinGradePoint", "MinScore", "Name", "UpdatedAt", "UpdatedById" },
                values: new object[,]
                {
                    { new Guid("019dfa31-42cc-7d42-82ad-4b98fd3fe384"), new Guid("019df1d0-68a6-7696-9aa6-4014a33a997f"), "A+", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 4.00m, 100, 4.00m, 95, "Excellent High", null, null },
                    { new Guid("019dfa31-42cc-7d42-82ad-4b99731825a5"), new Guid("019df1d0-68a6-7696-9aa6-4014a33a997f"), "A", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 3.99m, 94, 3.70m, 90, "Excellent", null, null },
                    { new Guid("019dfa31-42cc-7d42-82ad-4b9a8ed4c213"), new Guid("019df1d0-68a6-7696-9aa6-4014a33a997f"), "B+", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 3.69m, 89, 3.30m, 85, "Very Good High", null, null },
                    { new Guid("019dfa31-42cc-7d42-82ad-4b9b4caa2568"), new Guid("019df1d0-68a6-7696-9aa6-4014a33a997f"), "B", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 3.29m, 84, 3.00m, 80, "Very Good", null, null },
                    { new Guid("019dfa31-42cc-7d42-82ad-4b9cc785f14a"), new Guid("019df1d0-68a6-7696-9aa6-4014a33a997f"), "C+", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 2.99m, 79, 2.70m, 75, "Good High", null, null },
                    { new Guid("019dfa31-42cc-7d42-82ad-4b9dcedc1b35"), new Guid("019df1d0-68a6-7696-9aa6-4014a33a997f"), "C", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 2.69m, 74, 2.30m, 70, "Good", null, null },
                    { new Guid("019dfa31-42cc-7d42-82ad-4b9ee24f9179"), new Guid("019df1d0-68a6-7696-9aa6-4014a33a997f"), "D+", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 2.29m, 69, 2.00m, 65, "Fair High", null, null },
                    { new Guid("019dfa31-42cc-7d42-82ad-4b9f01fa845c"), new Guid("019df1d0-68a6-7696-9aa6-4014a33a997f"), "D", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 1.99m, 64, 1.00m, 60, "Fair", null, null },
                    { new Guid("019dfa31-42cc-7d42-82ad-4ba0d37d66eb"), new Guid("019df1d0-68a6-7696-9aa6-4014a33a997f"), "F", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 0.00m, 59, 0.00m, 0, "Failed", null, null },
                    { new Guid("019e020c-1514-7bfa-82ea-af1f399f879a"), new Guid("019df1d1-2da1-78d2-adeb-84cb6b07a459"), "A+", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 4.00m, 100, 4.00m, 95, "Excellent High", null, null },
                    { new Guid("019e020c-1514-7bfa-82ea-af20fecac15e"), new Guid("019df1d1-2da1-78d2-adeb-84cb6b07a459"), "A", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 3.99m, 94, 3.70m, 90, "Excellent", null, null },
                    { new Guid("019e020c-1514-7bfa-82ea-af216971a0ec"), new Guid("019df1d1-2da1-78d2-adeb-84cb6b07a459"), "B+", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 3.69m, 89, 3.30m, 85, "Very Good High", null, null },
                    { new Guid("019e020c-1514-7bfa-82ea-af22fca2beca"), new Guid("019df1d1-2da1-78d2-adeb-84cb6b07a459"), "B", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 3.29m, 84, 3.00m, 80, "Very Good", null, null },
                    { new Guid("019e020c-1515-7cb1-b40b-df30c3ac9339"), new Guid("019df1d1-2da1-78d2-adeb-84cb6b07a459"), "C+", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 2.99m, 79, 2.70m, 75, "Good High", null, null },
                    { new Guid("019e020c-1515-7cb1-b40b-df3125700042"), new Guid("019df1d1-2da1-78d2-adeb-84cb6b07a459"), "C", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 2.69m, 74, 2.30m, 70, "Good", null, null },
                    { new Guid("019e020c-1515-7cb1-b40b-df32510ebbca"), new Guid("019df1d1-2da1-78d2-adeb-84cb6b07a459"), "D+", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 2.29m, 69, 2.00m, 65, "Fair High", null, null },
                    { new Guid("019e020c-1515-7cb1-b40b-df33238e9d43"), new Guid("019df1d1-2da1-78d2-adeb-84cb6b07a459"), "D", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 1.99m, 64, 1.00m, 60, "Fair", null, null },
                    { new Guid("019e020c-1515-7cb1-b40b-df34f31c38c8"), new Guid("019df1d1-2da1-78d2-adeb-84cb6b07a459"), "F", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 0.00m, 59, 0.00m, 0, "Failed", null, null },
                    { new Guid("019e020c-dd27-799f-88db-57ca33299de0"), new Guid("019df1d1-0356-789e-840b-56d31396608a"), "A+", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 4.00m, 100, 4.00m, 95, "Excellent High", null, null },
                    { new Guid("019e020c-dd29-7ea3-967f-81ff9315195a"), new Guid("019df1d1-0356-789e-840b-56d31396608a"), "A", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 3.99m, 94, 3.70m, 90, "Excellent", null, null },
                    { new Guid("019e020c-dd29-7ea3-967f-8200118212d4"), new Guid("019df1d1-0356-789e-840b-56d31396608a"), "B+", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 3.69m, 89, 3.30m, 85, "Very Good High", null, null },
                    { new Guid("019e020c-dd29-7ea3-967f-8201287a9d3d"), new Guid("019df1d1-0356-789e-840b-56d31396608a"), "B", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 3.29m, 84, 3.00m, 80, "Very Good", null, null },
                    { new Guid("019e020c-dd29-7ea3-967f-8202ddc12c0d"), new Guid("019df1d1-0356-789e-840b-56d31396608a"), "C+", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 2.99m, 79, 2.70m, 75, "Good High", null, null },
                    { new Guid("019e020c-dd29-7ea3-967f-8203dcfb21c1"), new Guid("019df1d1-0356-789e-840b-56d31396608a"), "C", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 2.69m, 74, 2.30m, 70, "Good", null, null },
                    { new Guid("019e020c-dd29-7ea3-967f-820419b21584"), new Guid("019df1d1-0356-789e-840b-56d31396608a"), "D+", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 2.29m, 69, 2.00m, 65, "Fair High", null, null },
                    { new Guid("019e020c-dd29-7ea3-967f-820589af7293"), new Guid("019df1d1-0356-789e-840b-56d31396608a"), "D", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 1.99m, 64, 1.00m, 60, "Fair", null, null },
                    { new Guid("019e020c-dd29-7ea3-967f-820623c9aad1"), new Guid("019df1d1-0356-789e-840b-56d31396608a"), "F", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 0.00m, 59, 0.00m, 0, "Failed", null, null },
                    { new Guid("019e020d-4219-7d3c-9f78-3358fa7b1642"), new Guid("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), "A+", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 4.00m, 100, 4.00m, 95, "Excellent High", null, null },
                    { new Guid("019e020d-4219-7d3c-9f78-3359d169fc7d"), new Guid("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), "A", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 3.99m, 94, 3.70m, 90, "Excellent", null, null },
                    { new Guid("019e020d-4219-7d3c-9f78-335a5d1d7019"), new Guid("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), "B+", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 3.69m, 89, 3.30m, 85, "Very Good High", null, null },
                    { new Guid("019e020d-4219-7d3c-9f78-335b999ecbdf"), new Guid("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), "B", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 3.29m, 84, 3.00m, 80, "Very Good", null, null },
                    { new Guid("019e020d-4219-7d3c-9f78-335c4f8424a2"), new Guid("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), "C+", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 2.99m, 79, 2.70m, 75, "Good High", null, null },
                    { new Guid("019e020d-4219-7d3c-9f78-335d0c6b8cae"), new Guid("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), "C", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 2.69m, 74, 2.30m, 70, "Good", null, null },
                    { new Guid("019e020d-4219-7d3c-9f78-335e09e78831"), new Guid("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), "D+", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 2.29m, 69, 2.00m, 65, "Fair High", null, null },
                    { new Guid("019e020d-4219-7d3c-9f78-335f29698be9"), new Guid("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), "D", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 1.99m, 64, 1.00m, 60, "Fair", null, null },
                    { new Guid("019e020d-4219-7d3c-9f78-33605071bfc8"), new Guid("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), "F", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 0.00m, 59, 0.00m, 0, "Failed", null, null },
                    { new Guid("019e020d-89b1-742e-a969-8828c4f7ff4d"), new Guid("019df1d0-b671-75b2-9c14-69ce00131461"), "A+", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 4.00m, 100, 4.00m, 95, "Excellent High", null, null },
                    { new Guid("019e020d-89b1-742e-a969-88297a435f3e"), new Guid("019df1d0-b671-75b2-9c14-69ce00131461"), "A", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 3.99m, 94, 3.70m, 90, "Excellent", null, null },
                    { new Guid("019e020d-89b1-742e-a969-882a3004369f"), new Guid("019df1d0-b671-75b2-9c14-69ce00131461"), "B+", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 3.69m, 89, 3.30m, 85, "Very Good High", null, null },
                    { new Guid("019e020d-89b2-7e48-89a1-387739f72edb"), new Guid("019df1d0-b671-75b2-9c14-69ce00131461"), "B", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 3.29m, 84, 3.00m, 80, "Very Good", null, null },
                    { new Guid("019e020d-89b2-7e48-89a1-38781a368e67"), new Guid("019df1d0-b671-75b2-9c14-69ce00131461"), "C+", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 2.99m, 79, 2.70m, 75, "Good High", null, null },
                    { new Guid("019e020d-89b2-7e48-89a1-387980999f01"), new Guid("019df1d0-b671-75b2-9c14-69ce00131461"), "C", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 2.69m, 74, 2.30m, 70, "Good", null, null },
                    { new Guid("019e020d-89b2-7e48-89a1-387aa29369f1"), new Guid("019df1d0-b671-75b2-9c14-69ce00131461"), "D+", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 2.29m, 69, 2.00m, 65, "Fair High", null, null },
                    { new Guid("019e020d-89b2-7e48-89a1-387bab55e7c3"), new Guid("019df1d0-b671-75b2-9c14-69ce00131461"), "D", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 1.99m, 64, 1.00m, 60, "Fair", null, null },
                    { new Guid("019e020d-89b2-7e48-89a1-387ceff9e85f"), new Guid("019df1d0-b671-75b2-9c14-69ce00131461"), "F", new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, 0.00m, 59, 0.00m, 0, "Failed", null, null }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "BuildingId", "Capacity", "CreatedAt", "CreatedById", "DeletedAt", "IsDeleted", "Name", "RoomNumber", "RoomType", "UpdatedAt", "UpdatedById" },
                values: new object[,]
                {
                    { new Guid("019dfaa3-92f1-7d12-840b-0356789e840b"), new Guid("019dfa7b-3b1a-7b3a-9e1d-ddcc7f909a82"), 40, new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, "Mechanical Workshop", 15, "Workshop", null, null },
                    { new Guid("019dfaa3-92f1-7d12-840b-2da178d2adeb"), new Guid("019dfa7b-3b1a-7b3a-9e1d-4014a33a997f"), 60, new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, "Board of Directors Hall", 500, "ClassRoom", null, null },
                    { new Guid("019dfaa3-92f1-7d12-840b-4014a33a997f"), new Guid("019dfa7b-3b1a-7b3a-9e1d-b67175b29c14"), 200, new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, "Steve Jobs Hall", 101, "LectureHall", null, null },
                    { new Guid("019dfaa3-92f1-7d12-840b-84cb6b07a459"), new Guid("019dfa7b-3b1a-7b3a-9e1d-0356789e840b"), 100, new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, "Quiet Study Zone", 1, "ClassRoom", null, null },
                    { new Guid("019dfaa3-92f1-7d12-840b-a45984cb6b07"), new Guid("019dfa7b-3b1a-7b3a-9e1d-0356789e840b"), 45, new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, "E-Learning Center", 2, "LanguageLab", null, null },
                    { new Guid("019dfaa3-92f1-7d12-840b-b67175b29c14"), new Guid("019dfa7b-3b1a-7b3a-9e1d-b67175b29c14"), 25, new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, "Cyber Security Lab", 302, "ComputerLab", null, null },
                    { new Guid("019dfaa3-92f1-7d12-840b-ddcc7f909a82"), new Guid("019dfa7b-3b1a-7b3a-9e1d-ddcc7f909a82"), 30, new DateTime(2026, 5, 6, 14, 0, 0, 0, DateTimeKind.Utc), null, null, false, "Newton Physics Lab", 10, "ScientificLab", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomNumber_BuildingId",
                table: "Rooms",
                columns: new[] { "RoomNumber", "BuildingId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_Name",
                table: "Buildings",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rooms_RoomNumber_BuildingId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_Name",
                table: "Buildings");

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019dfa31-42cc-7d42-82ad-4b98fd3fe384"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019dfa31-42cc-7d42-82ad-4b99731825a5"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019dfa31-42cc-7d42-82ad-4b9a8ed4c213"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019dfa31-42cc-7d42-82ad-4b9b4caa2568"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019dfa31-42cc-7d42-82ad-4b9cc785f14a"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019dfa31-42cc-7d42-82ad-4b9dcedc1b35"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019dfa31-42cc-7d42-82ad-4b9ee24f9179"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019dfa31-42cc-7d42-82ad-4b9f01fa845c"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019dfa31-42cc-7d42-82ad-4ba0d37d66eb"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020c-1514-7bfa-82ea-af1f399f879a"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020c-1514-7bfa-82ea-af20fecac15e"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020c-1514-7bfa-82ea-af216971a0ec"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020c-1514-7bfa-82ea-af22fca2beca"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020c-1515-7cb1-b40b-df30c3ac9339"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020c-1515-7cb1-b40b-df3125700042"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020c-1515-7cb1-b40b-df32510ebbca"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020c-1515-7cb1-b40b-df33238e9d43"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020c-1515-7cb1-b40b-df34f31c38c8"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020c-dd27-799f-88db-57ca33299de0"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020c-dd29-7ea3-967f-81ff9315195a"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020c-dd29-7ea3-967f-8200118212d4"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020c-dd29-7ea3-967f-8201287a9d3d"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020c-dd29-7ea3-967f-8202ddc12c0d"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020c-dd29-7ea3-967f-8203dcfb21c1"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020c-dd29-7ea3-967f-820419b21584"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020c-dd29-7ea3-967f-820589af7293"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020c-dd29-7ea3-967f-820623c9aad1"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020d-4219-7d3c-9f78-3358fa7b1642"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020d-4219-7d3c-9f78-3359d169fc7d"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020d-4219-7d3c-9f78-335a5d1d7019"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020d-4219-7d3c-9f78-335b999ecbdf"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020d-4219-7d3c-9f78-335c4f8424a2"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020d-4219-7d3c-9f78-335d0c6b8cae"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020d-4219-7d3c-9f78-335e09e78831"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020d-4219-7d3c-9f78-335f29698be9"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020d-4219-7d3c-9f78-33605071bfc8"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020d-89b1-742e-a969-8828c4f7ff4d"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020d-89b1-742e-a969-88297a435f3e"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020d-89b1-742e-a969-882a3004369f"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020d-89b2-7e48-89a1-387739f72edb"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020d-89b2-7e48-89a1-38781a368e67"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020d-89b2-7e48-89a1-387980999f01"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020d-89b2-7e48-89a1-387aa29369f1"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020d-89b2-7e48-89a1-387bab55e7c3"));

            migrationBuilder.DeleteData(
                table: "Grades",
                keyColumn: "Id",
                keyValue: new Guid("019e020d-89b2-7e48-89a1-387ceff9e85f"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("019dfaa3-92f1-7d12-840b-0356789e840b"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("019dfaa3-92f1-7d12-840b-2da178d2adeb"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("019dfaa3-92f1-7d12-840b-4014a33a997f"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("019dfaa3-92f1-7d12-840b-84cb6b07a459"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("019dfaa3-92f1-7d12-840b-a45984cb6b07"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("019dfaa3-92f1-7d12-840b-b67175b29c14"));

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: new Guid("019dfaa3-92f1-7d12-840b-ddcc7f909a82"));

            migrationBuilder.DeleteData(
                table: "Buildings",
                keyColumn: "Id",
                keyValue: new Guid("019dfa7b-3b1a-7b3a-9e1d-0356789e840b"));

            migrationBuilder.DeleteData(
                table: "Buildings",
                keyColumn: "Id",
                keyValue: new Guid("019dfa7b-3b1a-7b3a-9e1d-4014a33a997f"));

            migrationBuilder.DeleteData(
                table: "Buildings",
                keyColumn: "Id",
                keyValue: new Guid("019dfa7b-3b1a-7b3a-9e1d-b67175b29c14"));

            migrationBuilder.DeleteData(
                table: "Buildings",
                keyColumn: "Id",
                keyValue: new Guid("019dfa7b-3b1a-7b3a-9e1d-ddcc7f909a82"));

            migrationBuilder.AlterColumn<int>(
                name: "RoomType",
                table: "Rooms",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Rooms",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<decimal>(
                name: "MinGradePoint",
                table: "Grades",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "MaxGradePoint",
                table: "Grades",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Buildings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_Code",
                table: "Grades",
                column: "Code",
                unique: true);
        }
    }
}
