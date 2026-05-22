using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Universe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseOfferingSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CourseOfferings",
                columns: new[] { "Id", "AcademicProgramId", "CourseId", "CreatedAt", "CreatedById", "CreditHours", "DeletedAt", "IsDeleted", "IsIncludedInGpa", "IsOpenForControl", "IsOptional", "LevelId", "NumberOfGroups", "OptionalGroupCode", "SemesterId", "SuccessPercentage", "TotalGrade", "Type", "UpdatedAt", "UpdatedById" },
                values: new object[,]
                {
                    { new Guid("019dfd68-2592-7804-b003-edf2f934b392"), new Guid("019df1d0-68a6-7696-9aa6-4014a33a997f"), new Guid("019df349-a51c-7aba-9caf-9599074ce97b"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, 3m, null, false, true, false, false, new Guid("019df4f8-4012-7469-9aa8-7cf36f1d180a"), 2, null, new Guid("019df777-504a-712e-b906-df51d6c28ef4"), 50m, 100m, "Program", null, null },
                    { new Guid("019dfd68-2592-7804-b003-edf351a1d258"), new Guid("019df1d0-68a6-7696-9aa6-4014a33a997f"), new Guid("019df349-a51c-7aba-9caf-959aa16694f7"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, 3m, null, false, true, false, false, new Guid("019df4f8-4012-7469-9aa8-7cf4d268fbec"), 2, null, new Guid("019df777-7a6a-7c4b-af7e-6a7903807bf7"), 55m, 100m, "Program", null, null },
                    { new Guid("019dfd68-2592-7804-b003-edf43f0472eb"), new Guid("019df1d0-b671-75b2-9c14-69ce00131461"), new Guid("019df349-a51c-7aba-9caf-959b0bc724f7"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, 3m, null, false, true, false, false, new Guid("019df4f8-4012-7469-9aa8-7cf4d268fbec"), 3, null, new Guid("019df777-7a6a-7c4b-af7e-6a7affd69cb7"), 55m, 100m, "Program", null, null },
                    { new Guid("019dfd68-2592-7804-b003-edf5375368d1"), new Guid("019df1d0-b671-75b2-9c14-69ce00131461"), new Guid("019df349-a51c-7aba-9caf-959c5b7e0ee1"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, 3m, null, false, true, false, false, new Guid("019df4f8-4012-7469-9aa8-7cf568231cba"), 2, null, new Guid("019df777-504a-712e-b906-df51d6c28ef4"), 60m, 100m, "Program", null, null },
                    { new Guid("019dfd68-2592-7804-b003-edf682ae1f8d"), new Guid("019df1d0-b671-75b2-9c14-69ce00131461"), new Guid("019df349-a51c-7aba-9caf-95890ee5ba62"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, 3m, null, false, true, false, false, new Guid("019df4f8-4012-7469-9aa8-7cf7ad92b89e"), 2, null, new Guid("019df777-7a6a-7c4b-af7e-6a7903807bf7"), 50m, 100m, "Program", null, null },
                    { new Guid("019dfd68-5448-7bac-a12f-aa70587164d3"), new Guid("019df1d0-b671-75b2-9c14-69ce00131461"), new Guid("019df349-a51c-7aba-9caf-958a05c9c32a"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, 3m, null, false, true, false, false, new Guid("019df4f8-4012-7469-9aa8-7cf8937d84f4"), 2, null, new Guid("019df777-7a6a-7c4b-af7e-6a7affd69cb7"), 50m, 100m, "Program", null, null },
                    { new Guid("019dfd68-5448-7bac-a12f-aa7174f6b010"), new Guid("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), new Guid("019df349-a51c-7aba-9caf-958b81dee3d6"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, 3m, null, false, true, false, false, new Guid("019df4f8-4012-7469-9aa8-7cf9e82c4b82"), 2, null, new Guid("019df777-504a-712e-b906-df51d6c28ef4"), 55m, 100m, "Program", null, null },
                    { new Guid("019dfd68-5448-7bac-a12f-aa7242c9ea80"), new Guid("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), new Guid("019df349-a51c-7aba-9caf-958c542ea45f"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, 3m, null, false, true, false, false, new Guid("019df4f8-4012-7469-9aa8-7cfa9abd172d"), 3, null, new Guid("019df777-7a6a-7c4b-af7e-6a7903807bf7"), 55m, 100m, "Program", null, null },
                    { new Guid("019dfd68-5448-7bac-a12f-aa73c25e495e"), new Guid("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), new Guid("019df349-a51c-7aba-9caf-958d58bae05b"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, 3m, null, false, true, false, false, new Guid("019df4f8-4012-7469-9aa8-7cfb22e7bb31"), 2, null, new Guid("019df777-7a6a-7c4b-af7e-6a7affd69cb7"), 55m, 100m, "Program", null, null },
                    { new Guid("019dfd68-5448-7bac-a12f-aa74e02a987d"), new Guid("019df1d1-0356-789e-840b-56d31396608a"), new Guid("019df349-a51c-7aba-9caf-958e1b56b349"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, 3m, null, false, true, false, false, new Guid("019df4f8-4012-7469-9aa8-7cfcf91e0ee9"), 2, null, new Guid("019df777-504a-712e-b906-df51d6c28ef4"), 60m, 100m, "Program", null, null },
                    { new Guid("019dfd68-7d5c-74be-9adc-de7dd1e252bd"), new Guid("019df1d1-0356-789e-840b-56d31396608a"), new Guid("019df349-a51c-7aba-9caf-958f9e72f562"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, 3m, null, false, true, false, false, new Guid("019df4f8-4012-7469-9aa8-7cfdd431525b"), 2, null, new Guid("019df777-7a6a-7c4b-af7e-6a7903807bf7"), 60m, 100m, "Program", null, null },
                    { new Guid("019dfd68-7d5d-76da-9139-52b1a45f0cf6"), new Guid("019df1d1-0356-789e-840b-56d31396608a"), new Guid("019df349-a51c-7aba-9caf-9590bdb364c0"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, 3m, null, false, true, false, false, new Guid("019df4f8-4012-7469-9aa8-7cfeb8e8d41d"), 2, null, new Guid("019df777-7a6a-7c4b-af7e-6a7affd69cb7"), 60m, 100m, "Program", null, null },
                    { new Guid("019dfd68-7d5d-76da-9139-52b2b0563e46"), new Guid("019df1d1-2da1-78d2-adeb-84cb6b07a459"), new Guid("019df349-a51c-7aba-9caf-9591262e88f8"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, 3m, null, false, true, false, false, new Guid("019df4f8-4012-7469-9aa8-7cffb491c63b"), 1, null, new Guid("019df777-504a-712e-b906-df51d6c28ef4"), 65m, 100m, "Program", null, null },
                    { new Guid("019dfd68-7d5d-76da-9139-52b301bf0bb4"), new Guid("019df1d1-2da1-78d2-adeb-84cb6b07a459"), new Guid("019df349-a51c-7aba-9caf-95920fdf4a77"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, 3m, null, false, true, false, false, new Guid("019df4f8-4012-7469-9aa8-7d00e6c3ddce"), 1, null, new Guid("019df777-7a6a-7c4b-af7e-6a7903807bf7"), 65m, 100m, "Program", null, null },
                    { new Guid("019dfd68-7d5d-76da-9139-52b45425dfbc"), new Guid("019df1d1-2da1-78d2-adeb-84cb6b07a459"), new Guid("019df349-a51c-7aba-9caf-9593115238f8"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, 3m, null, false, true, false, false, new Guid("019df4f8-4012-7469-9aa8-7d01aebd87c1"), 1, null, new Guid("019df777-7a6a-7c4b-af7e-6a7affd69cb7"), 65m, 100m, "Program", null, null },
                    { new Guid("019dfd68-a692-7f08-971c-2c8a3cc190cd"), new Guid("019df1d1-2da1-78d2-adeb-84cb6b07a459"), new Guid("019df349-a51c-7aba-9caf-9594f9d4ca6a"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, 3m, null, false, true, false, true, new Guid("019df4f8-4012-7469-9aa8-7d024664404f"), 1, "SEC-B", new Guid("019df777-504a-712e-b906-df51d6c28ef4"), 60m, 100m, "College", null, null },
                    { new Guid("019dfd68-a692-7f08-971c-2c8ba2eb138f"), new Guid("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), new Guid("019df349-a51c-7aba-9caf-9595b8b7914a"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, 3m, null, false, true, false, true, new Guid("019df4f8-4012-7469-9aa8-7d033b7e618f"), 2, "SEC-C", new Guid("019df777-7a6a-7c4b-af7e-6a7903807bf7"), 60m, 100m, "College", null, null },
                    { new Guid("019dfd68-a692-7f08-971c-2c8c649f9da8"), new Guid("019df1d1-0356-789e-840b-56d31396608a"), new Guid("019df349-a51c-7aba-9caf-959678c41dea"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, 3m, null, false, true, false, true, new Guid("019df4f8-4012-7469-9aa8-7d049703d7cb"), 2, "SEC-D", new Guid("019df777-7a6a-7c4b-af7e-6a7affd69cb7"), 60m, 100m, "College", null, null },
                    { new Guid("019dfd68-a692-7f08-971c-2c8df1a1fd0d"), new Guid("019df1d0-68a6-7696-9aa6-4014a33a997f"), new Guid("019df349-a51c-7aba-9caf-95977f4e315c"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, 3m, null, false, true, false, false, new Guid("019df4f8-4012-7469-9aa8-7d05feaad5d1"), 1, null, new Guid("019df777-504a-712e-b906-df51d6c28ef4"), 65m, 100m, "Program", null, null },
                    { new Guid("019dfd68-a692-7f08-971c-2c8e8bbca914"), new Guid("019df1d0-b671-75b2-9c14-69ce00131461"), new Guid("019df349-a51c-7aba-9caf-9598a067e97e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, 3m, null, false, true, false, false, new Guid("019df4f8-4012-7469-9aa8-7d065b1fbceb"), 1, null, new Guid("019df777-7a6a-7c4b-af7e-6a7903807bf7"), 65m, 100m, "Program", null, null }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "CollegeId", "CreatedAt", "CreatedById", "DeletedAt", "Description", "IsActive", "IsDeleted", "Name", "Price", "UpdatedAt", "UpdatedById" },
                values: new object[,]
                {
                    { new Guid("019df8a8-8616-75c5-a883-d313c0e6105e"), new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Request official academic transcript.", true, false, "Transcript Request", 100m, null, null },
                    { new Guid("019df8a8-8618-7a77-b23e-8622839dba17"), new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Issue enrollment certificate.", true, false, "Enrollment Certificate", 50m, null, null },
                    { new Guid("019df8a8-8618-7a77-b23e-8623a7995017"), new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Withdraw from a registered course.", true, false, "Course Withdrawal", 75m, null, null },
                    { new Guid("019df8a8-8618-7a77-b23e-8624951a0f61"), new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Request re-evaluation of exam results.", true, false, "Regrade Request", 60m, null, null },
                    { new Guid("019df8a8-8618-7a77-b23e-86258a38447f"), new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Issue replacement student ID card.", true, false, "ID Replacement", 40m, null, null },
                    { new Guid("019df8a8-8618-7a77-b23e-8626f39bba2e"), new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Request graduation certificate.", true, false, "Graduation Certificate", 150m, null, null },
                    { new Guid("019df8a8-8618-7a77-b23e-8627e2d629d7"), new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Adjust course registration.", true, false, "Course Registration Adjustment", 80m, null, null },
                    { new Guid("019df8a8-8618-7a77-b23e-8628f56ee1f8"), new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Register for courses after deadline.", true, false, "Late Registration", 120m, null, null }
                });

            migrationBuilder.InsertData(
                table: "CourseOfferingAssessments",
                columns: new[] { "Id", "CourseOfferingId", "CreatedAt", "CreatedById", "DeletedAt", "IsDeleted", "MaxScore", "Type", "UpdatedAt", "UpdatedById" },
                values: new object[,]
                {
                    { new Guid("019dfd6d-4d55-74fd-9659-521a1328449e"), new Guid("019dfd68-2592-7804-b003-edf2f934b392"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 60m, 1, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-521b47f7b9da"), new Guid("019dfd68-2592-7804-b003-edf2f934b392"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 20m, 2, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-521c0bc23ed0"), new Guid("019dfd68-2592-7804-b003-edf2f934b392"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 20m, 10, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-521db4714a1b"), new Guid("019dfd68-2592-7804-b003-edf351a1d258"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 50m, 1, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-521edadbeedd"), new Guid("019dfd68-2592-7804-b003-edf351a1d258"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 25m, 2, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-521f983c4154"), new Guid("019dfd68-2592-7804-b003-edf351a1d258"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 25m, 7, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-5220eeac8ef6"), new Guid("019dfd68-2592-7804-b003-edf43f0472eb"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 60m, 1, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-522196b3e3e2"), new Guid("019dfd68-2592-7804-b003-edf43f0472eb"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 20m, 6, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-522235c95745"), new Guid("019dfd68-2592-7804-b003-edf43f0472eb"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 20m, 8, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-5223b06879a8"), new Guid("019dfd68-2592-7804-b003-edf5375368d1"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 70m, 1, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-5224ac8cfe76"), new Guid("019dfd68-2592-7804-b003-edf5375368d1"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 30m, 2, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-52250ddebeb3"), new Guid("019dfd68-2592-7804-b003-edf682ae1f8d"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 50m, 1, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-5226f41b1c02"), new Guid("019dfd68-2592-7804-b003-edf682ae1f8d"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 25m, 6, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-5227dd77ac47"), new Guid("019dfd68-2592-7804-b003-edf682ae1f8d"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 25m, 10, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-522819c6d43d"), new Guid("019dfd68-5448-7bac-a12f-aa70587164d3"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 60m, 1, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-5229e0278c84"), new Guid("019dfd68-5448-7bac-a12f-aa70587164d3"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 20m, 5, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-522a398ece4e"), new Guid("019dfd68-5448-7bac-a12f-aa70587164d3"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 20m, 8, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-522bfe93d0db"), new Guid("019dfd68-5448-7bac-a12f-aa7174f6b010"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 70m, 1, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-522c02c64110"), new Guid("019dfd68-5448-7bac-a12f-aa7174f6b010"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 30m, 7, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-522d3922adf5"), new Guid("019dfd68-5448-7bac-a12f-aa7242c9ea80"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 50m, 1, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-522e68442586"), new Guid("019dfd68-5448-7bac-a12f-aa7242c9ea80"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 25m, 2, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-522f348abd5f"), new Guid("019dfd68-5448-7bac-a12f-aa7242c9ea80"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 25m, 10, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-523078141e2c"), new Guid("019dfd68-5448-7bac-a12f-aa73c25e495e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 60m, 1, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-5231fedfaa23"), new Guid("019dfd68-5448-7bac-a12f-aa73c25e495e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 40m, 8, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-523203cfc55b"), new Guid("019dfd68-5448-7bac-a12f-aa74e02a987d"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 80m, 1, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-5233a14c80d7"), new Guid("019dfd68-5448-7bac-a12f-aa74e02a987d"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 20m, 9, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-523491c49da3"), new Guid("019dfd68-7d5c-74be-9adc-de7dd1e252bd"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 60m, 1, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-5235615ad8b0"), new Guid("019dfd68-7d5c-74be-9adc-de7dd1e252bd"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 40m, 6, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-5236775caeaa"), new Guid("019dfd68-7d5d-76da-9139-52b1a45f0cf6"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 70m, 1, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-523766d785bb"), new Guid("019dfd68-7d5d-76da-9139-52b1a45f0cf6"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 30m, 5, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-52388ed2bb4c"), new Guid("019dfd68-7d5d-76da-9139-52b2b0563e46"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 60m, 1, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-5239d097942a"), new Guid("019dfd68-7d5d-76da-9139-52b2b0563e46"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 40m, 4, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-523a41158b63"), new Guid("019dfd68-7d5d-76da-9139-52b301bf0bb4"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 50m, 1, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-523b94551137"), new Guid("019dfd68-7d5d-76da-9139-52b301bf0bb4"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 50m, 3, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-523c1980f760"), new Guid("019dfd68-7d5d-76da-9139-52b45425dfbc"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 60m, 1, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-523d144f7b4a"), new Guid("019dfd68-7d5d-76da-9139-52b45425dfbc"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 40m, 10, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-523e925070ba"), new Guid("019dfd68-a692-7f08-971c-2c8a3cc190cd"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 70m, 1, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-523f2eb83120"), new Guid("019dfd68-a692-7f08-971c-2c8a3cc190cd"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 30m, 8, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-524006c646b2"), new Guid("019dfd68-a692-7f08-971c-2c8ba2eb138f"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 60m, 1, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-52413d106d4e"), new Guid("019dfd68-a692-7f08-971c-2c8ba2eb138f"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 40m, 7, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-5242bf73d625"), new Guid("019dfd68-a692-7f08-971c-2c8c649f9da8"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 80m, 1, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-524356673eb5"), new Guid("019dfd68-a692-7f08-971c-2c8c649f9da8"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 20m, 9, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-524462ed43e7"), new Guid("019dfd68-a692-7f08-971c-2c8df1a1fd0d"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 60m, 1, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-52451286622f"), new Guid("019dfd68-a692-7f08-971c-2c8df1a1fd0d"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 40m, 6, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-524679c16b31"), new Guid("019dfd68-a692-7f08-971c-2c8e8bbca914"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 70m, 1, null, null },
                    { new Guid("019dfd6d-4d55-74fd-9659-5247363bb793"), new Guid("019dfd68-a692-7f08-971c-2c8e8bbca914"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 30m, 2, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-521a1328449e"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-521b47f7b9da"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-521c0bc23ed0"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-521db4714a1b"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-521edadbeedd"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-521f983c4154"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-5220eeac8ef6"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-522196b3e3e2"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-522235c95745"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-5223b06879a8"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-5224ac8cfe76"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-52250ddebeb3"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-5226f41b1c02"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-5227dd77ac47"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-522819c6d43d"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-5229e0278c84"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-522a398ece4e"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-522bfe93d0db"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-522c02c64110"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-522d3922adf5"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-522e68442586"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-522f348abd5f"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-523078141e2c"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-5231fedfaa23"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-523203cfc55b"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-5233a14c80d7"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-523491c49da3"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-5235615ad8b0"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-5236775caeaa"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-523766d785bb"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-52388ed2bb4c"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-5239d097942a"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-523a41158b63"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-523b94551137"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-523c1980f760"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-523d144f7b4a"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-523e925070ba"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-523f2eb83120"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-524006c646b2"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-52413d106d4e"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-5242bf73d625"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-524356673eb5"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-524462ed43e7"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-52451286622f"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-524679c16b31"));

            migrationBuilder.DeleteData(
                table: "CourseOfferingAssessments",
                keyColumn: "Id",
                keyValue: new Guid("019dfd6d-4d55-74fd-9659-5247363bb793"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("019df8a8-8616-75c5-a883-d313c0e6105e"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("019df8a8-8618-7a77-b23e-8622839dba17"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("019df8a8-8618-7a77-b23e-8623a7995017"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("019df8a8-8618-7a77-b23e-8624951a0f61"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("019df8a8-8618-7a77-b23e-86258a38447f"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("019df8a8-8618-7a77-b23e-8626f39bba2e"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("019df8a8-8618-7a77-b23e-8627e2d629d7"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("019df8a8-8618-7a77-b23e-8628f56ee1f8"));

            migrationBuilder.DeleteData(
                table: "CourseOfferings",
                keyColumn: "Id",
                keyValue: new Guid("019dfd68-2592-7804-b003-edf2f934b392"));

            migrationBuilder.DeleteData(
                table: "CourseOfferings",
                keyColumn: "Id",
                keyValue: new Guid("019dfd68-2592-7804-b003-edf351a1d258"));

            migrationBuilder.DeleteData(
                table: "CourseOfferings",
                keyColumn: "Id",
                keyValue: new Guid("019dfd68-2592-7804-b003-edf43f0472eb"));

            migrationBuilder.DeleteData(
                table: "CourseOfferings",
                keyColumn: "Id",
                keyValue: new Guid("019dfd68-2592-7804-b003-edf5375368d1"));

            migrationBuilder.DeleteData(
                table: "CourseOfferings",
                keyColumn: "Id",
                keyValue: new Guid("019dfd68-2592-7804-b003-edf682ae1f8d"));

            migrationBuilder.DeleteData(
                table: "CourseOfferings",
                keyColumn: "Id",
                keyValue: new Guid("019dfd68-5448-7bac-a12f-aa70587164d3"));

            migrationBuilder.DeleteData(
                table: "CourseOfferings",
                keyColumn: "Id",
                keyValue: new Guid("019dfd68-5448-7bac-a12f-aa7174f6b010"));

            migrationBuilder.DeleteData(
                table: "CourseOfferings",
                keyColumn: "Id",
                keyValue: new Guid("019dfd68-5448-7bac-a12f-aa7242c9ea80"));

            migrationBuilder.DeleteData(
                table: "CourseOfferings",
                keyColumn: "Id",
                keyValue: new Guid("019dfd68-5448-7bac-a12f-aa73c25e495e"));

            migrationBuilder.DeleteData(
                table: "CourseOfferings",
                keyColumn: "Id",
                keyValue: new Guid("019dfd68-5448-7bac-a12f-aa74e02a987d"));

            migrationBuilder.DeleteData(
                table: "CourseOfferings",
                keyColumn: "Id",
                keyValue: new Guid("019dfd68-7d5c-74be-9adc-de7dd1e252bd"));

            migrationBuilder.DeleteData(
                table: "CourseOfferings",
                keyColumn: "Id",
                keyValue: new Guid("019dfd68-7d5d-76da-9139-52b1a45f0cf6"));

            migrationBuilder.DeleteData(
                table: "CourseOfferings",
                keyColumn: "Id",
                keyValue: new Guid("019dfd68-7d5d-76da-9139-52b2b0563e46"));

            migrationBuilder.DeleteData(
                table: "CourseOfferings",
                keyColumn: "Id",
                keyValue: new Guid("019dfd68-7d5d-76da-9139-52b301bf0bb4"));

            migrationBuilder.DeleteData(
                table: "CourseOfferings",
                keyColumn: "Id",
                keyValue: new Guid("019dfd68-7d5d-76da-9139-52b45425dfbc"));

            migrationBuilder.DeleteData(
                table: "CourseOfferings",
                keyColumn: "Id",
                keyValue: new Guid("019dfd68-a692-7f08-971c-2c8a3cc190cd"));

            migrationBuilder.DeleteData(
                table: "CourseOfferings",
                keyColumn: "Id",
                keyValue: new Guid("019dfd68-a692-7f08-971c-2c8ba2eb138f"));

            migrationBuilder.DeleteData(
                table: "CourseOfferings",
                keyColumn: "Id",
                keyValue: new Guid("019dfd68-a692-7f08-971c-2c8c649f9da8"));

            migrationBuilder.DeleteData(
                table: "CourseOfferings",
                keyColumn: "Id",
                keyValue: new Guid("019dfd68-a692-7f08-971c-2c8df1a1fd0d"));

            migrationBuilder.DeleteData(
                table: "CourseOfferings",
                keyColumn: "Id",
                keyValue: new Guid("019dfd68-a692-7f08-971c-2c8e8bbca914"));
        }
    }
}
