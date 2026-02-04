using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Universe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CollegeId", "ConcurrencyStamp", "DeletedAt", "Email", "EmailConfirmed", "ImageUrl", "IsDeleted", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("019c0582-3473-7802-8f11-50cc1e6513d5"), 0, new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), "01993360-1c17-7054-bdee-1d5a9e780f23", null, "SVNU@Universe.com", true, null, false, false, null, "SVNU", "SVNU@UNIVERSE.COM", "SVNU", "AQAAAAIAAYagAAAAEFhcy5yaaQ5/9U5cfv8MnI3DBzUZ0ido47Hf7N0qKI20sJp8yGuUPuwPOIGdkNQJjA==", null, false, "55BF92C9EF0249CDA210D85D1A851BC9", false, "SVNU" },
                    { new Guid("019c1e76-6f5a-7522-8327-a2a72adbbbe8"), 0, new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), "019c1e76-a7da-76a1-a6c8-96163fb4a2fc", null, "Admin@Universe.com", true, null, false, false, null, "Admin", "ADMIN@UNIVERSE.COM", "ADMIN", "AQAAAAIAAYagAAAAEFhcy5yaaQ5/9U5cfv8MnI3DBzUZ0ido47Hf7N0qKI20sJp8yGuUPuwPOIGdkNQJjA==", null, false, "96A84AD3C17B4EBD95CE5AC8266BD761", false, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("0191a4b6-c4fc-752e-9d95-40b5e4e68054"), new Guid("019c0582-3473-7802-8f11-50cc1e6513d5") },
                    { new Guid("019c1e6e-5518-7479-b749-b1c5d4a21430"), new Guid("019c1e76-6f5a-7522-8327-a2a72adbbbe8") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("0191a4b6-c4fc-752e-9d95-40b5e4e68054"), new Guid("019c0582-3473-7802-8f11-50cc1e6513d5") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("019c1e6e-5518-7479-b749-b1c5d4a21430"), new Guid("019c1e76-6f5a-7522-8327-a2a72adbbbe8") });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("019c0582-3473-7802-8f11-50cc1e6513d5"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("019c1e76-6f5a-7522-8327-a2a72adbbbe8"));
        }
    }
}
