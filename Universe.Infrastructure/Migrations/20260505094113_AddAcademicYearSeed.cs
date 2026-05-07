using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Universe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAcademicYearSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AcademicYears",
                columns: new[] { "Id", "CollegeId", "CreatedAt", "CreatedById", "DeletedAt", "EndDate", "IsDeleted", "Name", "StartDate", "UpdatedAt", "UpdatedById" },
                values: new object[,]
                {
                    { new Guid("019df776-1f8f-76e6-abf8-02c380bbdf4f"), new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2021, 8, 31), false, "2020-2021", new DateOnly(2020, 9, 1), null, null },
                    { new Guid("019df776-1f8f-76e6-abf8-02c488f9b604"), new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2022, 8, 31), false, "2021-2022", new DateOnly(2021, 9, 1), null, null },
                    { new Guid("019df776-1f8f-76e6-abf8-02c591133e7d"), new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2023, 8, 31), false, "2022-2023", new DateOnly(2022, 9, 1), null, null },
                    { new Guid("019df776-1f8f-76e6-abf8-02c6d020e3cf"), new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2024, 8, 31), false, "2023-2024", new DateOnly(2023, 9, 1), null, null },
                    { new Guid("019df776-1f8f-76e6-abf8-02c790dab613"), new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2025, 8, 31), false, "2024-2025", new DateOnly(2024, 9, 1), null, null },
                    { new Guid("019df776-1f8f-76e6-abf8-02c8c484f18c"), new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2026, 8, 31), false, "2025-2026", new DateOnly(2025, 9, 1), null, null }
                });

            migrationBuilder.InsertData(
                table: "Semesters",
                columns: new[] { "Id", "AcademicYearId", "CreatedAt", "CreatedById", "DeletedAt", "EndDate", "IsCurrent", "IsDeleted", "IsResultAnnounced", "Name", "StartDate", "UpdatedAt", "UpdatedById" },
                values: new object[,]
                {
                    { new Guid("019df776-9108-75b2-9389-ac1049b0159f"), new Guid("019df776-1f8f-76e6-abf8-02c380bbdf4f"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2021, 1, 31), false, false, true, 1, new DateOnly(2020, 9, 1), null, null },
                    { new Guid("019df776-9108-75b2-9389-ac110706f6d0"), new Guid("019df776-1f8f-76e6-abf8-02c380bbdf4f"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2021, 6, 30), true, false, true, 2, new DateOnly(2021, 2, 1), null, null },
                    { new Guid("019df776-9108-75b2-9389-ac1257cd67fe"), new Guid("019df776-1f8f-76e6-abf8-02c380bbdf4f"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2021, 8, 31), false, false, true, 3, new DateOnly(2021, 7, 1), null, null },
                    { new Guid("019df776-bc77-7ca8-a0f6-3c3845c23a04"), new Guid("019df776-1f8f-76e6-abf8-02c488f9b604"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2022, 1, 31), true, false, true, 1, new DateOnly(2021, 9, 1), null, null },
                    { new Guid("019df776-bc77-7ca8-a0f6-3c39222b791a"), new Guid("019df776-1f8f-76e6-abf8-02c488f9b604"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2022, 6, 30), false, false, true, 2, new DateOnly(2022, 2, 1), null, null },
                    { new Guid("019df776-bc77-7ca8-a0f6-3c3a1ab74d05"), new Guid("019df776-1f8f-76e6-abf8-02c488f9b604"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2022, 8, 31), false, false, true, 3, new DateOnly(2022, 7, 1), null, null },
                    { new Guid("019df776-f240-7e2a-bef0-a153c6a90cdc"), new Guid("019df776-1f8f-76e6-abf8-02c591133e7d"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2023, 1, 31), false, false, true, 1, new DateOnly(2022, 9, 1), null, null },
                    { new Guid("019df776-f240-7e2a-bef0-a1549332c599"), new Guid("019df776-1f8f-76e6-abf8-02c591133e7d"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2023, 6, 30), false, false, true, 2, new DateOnly(2023, 2, 1), null, null },
                    { new Guid("019df776-f240-7e2a-bef0-a1557960866d"), new Guid("019df776-1f8f-76e6-abf8-02c591133e7d"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2023, 8, 31), false, false, true, 3, new DateOnly(2023, 7, 1), null, null },
                    { new Guid("019df777-2097-7f6a-84f3-97e02fa63d59"), new Guid("019df776-1f8f-76e6-abf8-02c6d020e3cf"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2024, 1, 31), false, false, true, 1, new DateOnly(2023, 9, 1), null, null },
                    { new Guid("019df777-2097-7f6a-84f3-97e1d0a5750b"), new Guid("019df776-1f8f-76e6-abf8-02c6d020e3cf"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2024, 6, 30), false, false, true, 2, new DateOnly(2024, 2, 1), null, null },
                    { new Guid("019df777-2097-7f6a-84f3-97e210d2106a"), new Guid("019df776-1f8f-76e6-abf8-02c6d020e3cf"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2024, 8, 31), true, false, true, 3, new DateOnly(2024, 7, 1), null, null },
                    { new Guid("019df777-504a-712e-b906-df4fabc6b69e"), new Guid("019df776-1f8f-76e6-abf8-02c790dab613"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2025, 1, 31), false, false, false, 1, new DateOnly(2024, 9, 1), null, null },
                    { new Guid("019df777-504a-712e-b906-df50def24feb"), new Guid("019df776-1f8f-76e6-abf8-02c790dab613"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2025, 6, 30), true, false, false, 2, new DateOnly(2025, 2, 1), null, null },
                    { new Guid("019df777-504a-712e-b906-df51d6c28ef4"), new Guid("019df776-1f8f-76e6-abf8-02c790dab613"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2025, 8, 31), false, false, false, 3, new DateOnly(2025, 7, 1), null, null },
                    { new Guid("019df777-7a6a-7c4b-af7e-6a7903807bf7"), new Guid("019df776-1f8f-76e6-abf8-02c8c484f18c"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2026, 1, 31), false, false, false, 1, new DateOnly(2025, 9, 1), null, null },
                    { new Guid("019df777-7a6a-7c4b-af7e-6a7affd69cb7"), new Guid("019df776-1f8f-76e6-abf8-02c8c484f18c"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2026, 6, 30), true, false, false, 2, new DateOnly(2026, 2, 1), null, null },
                    { new Guid("019df777-7a6a-7c4b-af7e-6a7b7b27fa44"), new Guid("019df776-1f8f-76e6-abf8-02c8c484f18c"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, new DateOnly(2026, 8, 31), false, false, false, 3, new DateOnly(2026, 7, 1), null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Semesters",
                keyColumn: "Id",
                keyValue: new Guid("019df776-9108-75b2-9389-ac1049b0159f"));

            migrationBuilder.DeleteData(
                table: "Semesters",
                keyColumn: "Id",
                keyValue: new Guid("019df776-9108-75b2-9389-ac110706f6d0"));

            migrationBuilder.DeleteData(
                table: "Semesters",
                keyColumn: "Id",
                keyValue: new Guid("019df776-9108-75b2-9389-ac1257cd67fe"));

            migrationBuilder.DeleteData(
                table: "Semesters",
                keyColumn: "Id",
                keyValue: new Guid("019df776-bc77-7ca8-a0f6-3c3845c23a04"));

            migrationBuilder.DeleteData(
                table: "Semesters",
                keyColumn: "Id",
                keyValue: new Guid("019df776-bc77-7ca8-a0f6-3c39222b791a"));

            migrationBuilder.DeleteData(
                table: "Semesters",
                keyColumn: "Id",
                keyValue: new Guid("019df776-bc77-7ca8-a0f6-3c3a1ab74d05"));

            migrationBuilder.DeleteData(
                table: "Semesters",
                keyColumn: "Id",
                keyValue: new Guid("019df776-f240-7e2a-bef0-a153c6a90cdc"));

            migrationBuilder.DeleteData(
                table: "Semesters",
                keyColumn: "Id",
                keyValue: new Guid("019df776-f240-7e2a-bef0-a1549332c599"));

            migrationBuilder.DeleteData(
                table: "Semesters",
                keyColumn: "Id",
                keyValue: new Guid("019df776-f240-7e2a-bef0-a1557960866d"));

            migrationBuilder.DeleteData(
                table: "Semesters",
                keyColumn: "Id",
                keyValue: new Guid("019df777-2097-7f6a-84f3-97e02fa63d59"));

            migrationBuilder.DeleteData(
                table: "Semesters",
                keyColumn: "Id",
                keyValue: new Guid("019df777-2097-7f6a-84f3-97e1d0a5750b"));

            migrationBuilder.DeleteData(
                table: "Semesters",
                keyColumn: "Id",
                keyValue: new Guid("019df777-2097-7f6a-84f3-97e210d2106a"));

            migrationBuilder.DeleteData(
                table: "Semesters",
                keyColumn: "Id",
                keyValue: new Guid("019df777-504a-712e-b906-df4fabc6b69e"));

            migrationBuilder.DeleteData(
                table: "Semesters",
                keyColumn: "Id",
                keyValue: new Guid("019df777-504a-712e-b906-df50def24feb"));

            migrationBuilder.DeleteData(
                table: "Semesters",
                keyColumn: "Id",
                keyValue: new Guid("019df777-504a-712e-b906-df51d6c28ef4"));

            migrationBuilder.DeleteData(
                table: "Semesters",
                keyColumn: "Id",
                keyValue: new Guid("019df777-7a6a-7c4b-af7e-6a7903807bf7"));

            migrationBuilder.DeleteData(
                table: "Semesters",
                keyColumn: "Id",
                keyValue: new Guid("019df777-7a6a-7c4b-af7e-6a7affd69cb7"));

            migrationBuilder.DeleteData(
                table: "Semesters",
                keyColumn: "Id",
                keyValue: new Guid("019df777-7a6a-7c4b-af7e-6a7b7b27fa44"));

            migrationBuilder.DeleteData(
                table: "AcademicYears",
                keyColumn: "Id",
                keyValue: new Guid("019df776-1f8f-76e6-abf8-02c380bbdf4f"));

            migrationBuilder.DeleteData(
                table: "AcademicYears",
                keyColumn: "Id",
                keyValue: new Guid("019df776-1f8f-76e6-abf8-02c488f9b604"));

            migrationBuilder.DeleteData(
                table: "AcademicYears",
                keyColumn: "Id",
                keyValue: new Guid("019df776-1f8f-76e6-abf8-02c591133e7d"));

            migrationBuilder.DeleteData(
                table: "AcademicYears",
                keyColumn: "Id",
                keyValue: new Guid("019df776-1f8f-76e6-abf8-02c6d020e3cf"));

            migrationBuilder.DeleteData(
                table: "AcademicYears",
                keyColumn: "Id",
                keyValue: new Guid("019df776-1f8f-76e6-abf8-02c790dab613"));

            migrationBuilder.DeleteData(
                table: "AcademicYears",
                keyColumn: "Id",
                keyValue: new Guid("019df776-1f8f-76e6-abf8-02c8c484f18c"));
        }
    }
}
