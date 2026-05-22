using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Universe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRoleSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AcademicProgramId", "AccessFailedCount", "CollegeId", "ConcurrencyStamp", "DeletedAt", "Email", "EmailConfirmed", "ImageUrl", "IsDeleted", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "StudentId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("019e0c87-1cbb-74c7-884a-9916db0669c4"), null, 0, new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), "a1", null, "staff1@universe.edu", true, null, false, false, null, "Ahmed Adel", "STAFF1@UNIVERSE.EDU", "STAFF1", "AQAAAAIAAYagAAAAEFhcy5yaaQ5/9U5cfv8MnI3DBzUZ0ido47Hf7N0qKI20sJp8yGuUPuwPOIGdkNQJjA==", null, false, "a1", null, false, "staff1" },
                    { new Guid("019e0c87-1cbb-74c7-884a-991784974e4e"), null, 0, new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), "a2", null, "staff2@universe.edu", true, null, false, false, null, "Omar Samy", "STAFF2@UNIVERSE.EDU", "STAFF2", "AQAAAAIAAYagAAAAEFhcy5yaaQ5/9U5cfv8MnI3DBzUZ0ido47Hf7N0qKI20sJp8yGuUPuwPOIGdkNQJjA==", null, false, "a2", null, false, "staff2" },
                    { new Guid("019e0c87-1cbb-74c7-884a-99181912a6db"), null, 0, new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), "a3", null, "staff3@universe.edu", true, null, false, false, null, "Youssef Hassan", "STAFF3@UNIVERSE.EDU", "STAFF3", "AQAAAAIAAYagAAAAEFhcy5yaaQ5/9U5cfv8MnI3DBzUZ0ido47Hf7N0qKI20sJp8yGuUPuwPOIGdkNQJjA==", null, false, "a3", null, false, "staff3" },
                    { new Guid("019e0c87-1cbb-74c7-884a-99195dbd3256"), null, 0, new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), "a4", null, "staff4@universe.edu", true, null, false, false, null, "Karim Mostafa", "STAFF4@UNIVERSE.EDU", "STAFF4", "AQAAAAIAAYagAAAAEFhcy5yaaQ5/9U5cfv8MnI3DBzUZ0ido47Hf7N0qKI20sJp8yGuUPuwPOIGdkNQJjA==", null, false, "a4", null, false, "staff4" },
                    { new Guid("019e0c87-1cbb-74c7-884a-991ac3846834"), null, 0, new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), "a5", null, "staff5@universe.edu", true, null, false, false, null, "Ali Tarek", "STAFF5@UNIVERSE.EDU", "STAFF5", "AQAAAAIAAYagAAAAEFhcy5yaaQ5/9U5cfv8MnI3DBzUZ0ido47Hf7N0qKI20sJp8yGuUPuwPOIGdkNQJjA==", null, false, "a5", null, false, "staff5" },
                    { new Guid("019e0c87-1cbb-74c7-884a-991bed8c312c"), null, 0, new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), "a6", null, "staff6@universe.edu", true, null, false, false, null, "Hassan Fathy", "STAFF6@UNIVERSE.EDU", "STAFF6", "AQAAAAIAAYagAAAAEFhcy5yaaQ5/9U5cfv8MnI3DBzUZ0ido47Hf7N0qKI20sJp8yGuUPuwPOIGdkNQJjA==", null, false, "a6", null, false, "staff6" },
                    { new Guid("019e0c87-1cbb-74c7-884a-991ccb84275b"), null, 0, new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), "a7", null, "staff7@universe.edu", true, null, false, false, null, "Mahmoud Emad", "STAFF7@UNIVERSE.EDU", "STAFF7", "AQAAAAIAAYagAAAAEFhcy5yaaQ5/9U5cfv8MnI3DBzUZ0ido47Hf7N0qKI20sJp8yGuUPuwPOIGdkNQJjA==", null, false, "a7", null, false, "staff7" },
                    { new Guid("019e0c87-1cbc-712f-90d1-61c9cbe6e293"), null, 0, new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), "a8", null, "staff8@universe.edu", true, null, false, false, null, "Mostafa Nabil", "STAFF8@UNIVERSE.EDU", "STAFF8", "AQAAAAIAAYagAAAAEFhcy5yaaQ5/9U5cfv8MnI3DBzUZ0ido47Hf7N0qKI20sJp8yGuUPuwPOIGdkNQJjA==", null, false, "a8", null, false, "staff8" },
                    { new Guid("019e0c87-1cbc-712f-90d1-61ca6cf7a3b7"), null, 0, new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), "a9", null, "staff9@universe.edu", true, null, false, false, null, "Khaled Wael", "STAFF9@UNIVERSE.EDU", "STAFF9", "AQAAAAIAAYagAAAAEFhcy5yaaQ5/9U5cfv8MnI3DBzUZ0ido47Hf7N0qKI20sJp8yGuUPuwPOIGdkNQJjA==", null, false, "a9", null, false, "staff9" },
                    { new Guid("019e0c87-1cbc-712f-90d1-61cb963bd6a2"), null, 0, new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), "a10", null, "staff10@universe.edu", true, null, false, false, null, "Ibrahim Hossam", "STAFF10@UNIVERSE.EDU", "STAFF10", "AQAAAAIAAYagAAAAEFhcy5yaaQ5/9U5cfv8MnI3DBzUZ0ido47Hf7N0qKI20sJp8yGuUPuwPOIGdkNQJjA==", null, false, "a10", null, false, "staff10" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "ApplicationUserId" },
                values: new object[,]
                {
                    { new Guid("019c1e67-90d0-72a4-a602-9a98388515e9"), new Guid("019e0c87-1cbb-74c7-884a-9916db0669c4"), null },
                    { new Guid("019c1e6e-5518-7479-b749-b1c5d4a21430"), new Guid("019e0c87-1cbb-74c7-884a-9916db0669c4"), null },
                    { new Guid("019c1e67-90d0-72a4-a602-9a98388515e9"), new Guid("019e0c87-1cbb-74c7-884a-991784974e4e"), null },
                    { new Guid("019c1e6e-5518-7479-b749-b1c5d4a21430"), new Guid("019e0c87-1cbb-74c7-884a-991784974e4e"), null },
                    { new Guid("019c1e67-90d0-72a4-a602-9a98388515e9"), new Guid("019e0c87-1cbb-74c7-884a-99181912a6db"), null },
                    { new Guid("019c1e67-90d0-72a4-a602-9a98388515e9"), new Guid("019e0c87-1cbb-74c7-884a-99195dbd3256"), null },
                    { new Guid("019c1e67-90d0-72a4-a602-9a98388515e9"), new Guid("019e0c87-1cbb-74c7-884a-991ac3846834"), null },
                    { new Guid("019c1e67-90d0-72a4-a602-9a98388515e9"), new Guid("019e0c87-1cbb-74c7-884a-991bed8c312c"), null },
                    { new Guid("019c1e67-90d0-72a4-a602-9a98388515e9"), new Guid("019e0c87-1cbb-74c7-884a-991ccb84275b"), null },
                    { new Guid("019c1e67-90d0-72a4-a602-9a98388515e9"), new Guid("019e0c87-1cbc-712f-90d1-61c9cbe6e293"), null },
                    { new Guid("019c1e67-90d0-72a4-a602-9a98388515e9"), new Guid("019e0c87-1cbc-712f-90d1-61ca6cf7a3b7"), null },
                    { new Guid("019c1e67-90d0-72a4-a602-9a98388515e9"), new Guid("019e0c87-1cbc-712f-90d1-61cb963bd6a2"), null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("019c1e67-90d0-72a4-a602-9a98388515e9"), new Guid("019e0c87-1cbb-74c7-884a-9916db0669c4") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("019c1e6e-5518-7479-b749-b1c5d4a21430"), new Guid("019e0c87-1cbb-74c7-884a-9916db0669c4") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("019c1e67-90d0-72a4-a602-9a98388515e9"), new Guid("019e0c87-1cbb-74c7-884a-991784974e4e") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("019c1e6e-5518-7479-b749-b1c5d4a21430"), new Guid("019e0c87-1cbb-74c7-884a-991784974e4e") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("019c1e67-90d0-72a4-a602-9a98388515e9"), new Guid("019e0c87-1cbb-74c7-884a-99181912a6db") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("019c1e67-90d0-72a4-a602-9a98388515e9"), new Guid("019e0c87-1cbb-74c7-884a-99195dbd3256") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("019c1e67-90d0-72a4-a602-9a98388515e9"), new Guid("019e0c87-1cbb-74c7-884a-991ac3846834") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("019c1e67-90d0-72a4-a602-9a98388515e9"), new Guid("019e0c87-1cbb-74c7-884a-991bed8c312c") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("019c1e67-90d0-72a4-a602-9a98388515e9"), new Guid("019e0c87-1cbb-74c7-884a-991ccb84275b") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("019c1e67-90d0-72a4-a602-9a98388515e9"), new Guid("019e0c87-1cbc-712f-90d1-61c9cbe6e293") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("019c1e67-90d0-72a4-a602-9a98388515e9"), new Guid("019e0c87-1cbc-712f-90d1-61ca6cf7a3b7") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("019c1e67-90d0-72a4-a602-9a98388515e9"), new Guid("019e0c87-1cbc-712f-90d1-61cb963bd6a2") });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("019e0c87-1cbb-74c7-884a-9916db0669c4"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("019e0c87-1cbb-74c7-884a-991784974e4e"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("019e0c87-1cbb-74c7-884a-99181912a6db"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("019e0c87-1cbb-74c7-884a-99195dbd3256"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("019e0c87-1cbb-74c7-884a-991ac3846834"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("019e0c87-1cbb-74c7-884a-991bed8c312c"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("019e0c87-1cbb-74c7-884a-991ccb84275b"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("019e0c87-1cbc-712f-90d1-61c9cbe6e293"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("019e0c87-1cbc-712f-90d1-61ca6cf7a3b7"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("019e0c87-1cbc-712f-90d1-61cb963bd6a2"));
        }
    }
}
