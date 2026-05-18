using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Universe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLevelSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Levels",
                columns: new[] { "Id", "AcademicProgramId", "CreatedAt", "CreatedById", "DeletedAt", "IsDeleted", "MaxHours", "MinHours", "Name", "UpdatedAt", "UpdatedById" },
                values: new object[,]
                {
                    { new Guid("019df4f8-4012-7469-9aa8-7cf36f1d180a"), new Guid("019df1d0-68a6-7696-9aa6-4014a33a997f"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 36, 0, "Level 1", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7cf4d268fbec"), new Guid("019df1d0-68a6-7696-9aa6-4014a33a997f"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 72, 37, "Level 2", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7cf568231cba"), new Guid("019df1d0-68a6-7696-9aa6-4014a33a997f"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 108, 73, "Level 3", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7cf6124b1ecb"), new Guid("019df1d0-68a6-7696-9aa6-4014a33a997f"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 144, 109, "Level 4", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7cf7ad92b89e"), new Guid("019df1d0-b671-75b2-9c14-69ce00131461"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 36, 0, "Level 1", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7cf8937d84f4"), new Guid("019df1d0-b671-75b2-9c14-69ce00131461"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 72, 37, "Level 2", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7cf9e82c4b82"), new Guid("019df1d0-b671-75b2-9c14-69ce00131461"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 108, 73, "Level 3", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7cfa9abd172d"), new Guid("019df1d0-b671-75b2-9c14-69ce00131461"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 144, 109, "Level 4", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7cfb22e7bb31"), new Guid("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 34, 0, "Level 1", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7cfcf91e0ee9"), new Guid("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 68, 35, "Level 2", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7cfdd431525b"), new Guid("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 102, 69, "Level 3", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7cfeb8e8d41d"), new Guid("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 138, 103, "Level 4", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7cffb491c63b"), new Guid("019df1d1-0356-789e-840b-56d31396608a"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 38, 0, "Level 1", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7d00e6c3ddce"), new Guid("019df1d1-0356-789e-840b-56d31396608a"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 76, 39, "Level 2", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7d01aebd87c1"), new Guid("019df1d1-0356-789e-840b-56d31396608a"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 114, 77, "Level 3", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7d024664404f"), new Guid("019df1d1-0356-789e-840b-56d31396608a"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 150, 115, "Level 4", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7d033b7e618f"), new Guid("019df1d1-2da1-78d2-adeb-84cb6b07a459"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 36, 0, "Level 1", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7d049703d7cb"), new Guid("019df1d1-2da1-78d2-adeb-84cb6b07a459"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 72, 37, "Level 2", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7d05feaad5d1"), new Guid("019df1d1-2da1-78d2-adeb-84cb6b07a459"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 108, 73, "Level 3", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7d065b1fbceb"), new Guid("019df1d1-2da1-78d2-adeb-84cb6b07a459"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 144, 109, "Level 4", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: new Guid("019df4f8-4012-7469-9aa8-7cf36f1d180a"));

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: new Guid("019df4f8-4012-7469-9aa8-7cf4d268fbec"));

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: new Guid("019df4f8-4012-7469-9aa8-7cf568231cba"));

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: new Guid("019df4f8-4012-7469-9aa8-7cf6124b1ecb"));

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: new Guid("019df4f8-4012-7469-9aa8-7cf7ad92b89e"));

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: new Guid("019df4f8-4012-7469-9aa8-7cf8937d84f4"));

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: new Guid("019df4f8-4012-7469-9aa8-7cf9e82c4b82"));

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: new Guid("019df4f8-4012-7469-9aa8-7cfa9abd172d"));

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: new Guid("019df4f8-4012-7469-9aa8-7cfb22e7bb31"));

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: new Guid("019df4f8-4012-7469-9aa8-7cfcf91e0ee9"));

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: new Guid("019df4f8-4012-7469-9aa8-7cfdd431525b"));

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: new Guid("019df4f8-4012-7469-9aa8-7cfeb8e8d41d"));

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: new Guid("019df4f8-4012-7469-9aa8-7cffb491c63b"));

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: new Guid("019df4f8-4012-7469-9aa8-7d00e6c3ddce"));

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: new Guid("019df4f8-4012-7469-9aa8-7d01aebd87c1"));

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: new Guid("019df4f8-4012-7469-9aa8-7d024664404f"));

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: new Guid("019df4f8-4012-7469-9aa8-7d033b7e618f"));

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: new Guid("019df4f8-4012-7469-9aa8-7d049703d7cb"));

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: new Guid("019df4f8-4012-7469-9aa8-7d05feaad5d1"));

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: new Guid("019df4f8-4012-7469-9aa8-7d065b1fbceb"));
        }
    }
}
