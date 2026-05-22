using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Universe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addCourseSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Code", "CollegeId", "CreatedAt", "CreatedById", "DeletedAt", "Description", "IsDeleted", "Name", "UpdatedAt", "UpdatedById" },
                values: new object[,]
                {
                    { new Guid("019df349-a51c-7aba-9caf-95890ee5ba62"), "CS101", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Introduction to programming concepts.", false, "Programming 1", null, null },
                    { new Guid("019df349-a51c-7aba-9caf-958a05c9c32a"), "CS102", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Advanced programming and problem solving.", false, "Programming 2", null, null },
                    { new Guid("019df349-a51c-7aba-9caf-958b81dee3d6"), "CS103", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Concepts of OOP and design principles.", false, "Object Oriented Programming", null, null },
                    { new Guid("019df349-a51c-7aba-9caf-958c542ea45f"), "CS201", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Data organization and structures.", false, "Data Structures", null, null },
                    { new Guid("019df349-a51c-7aba-9caf-958d58bae05b"), "CS202", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Algorithm design and analysis.", false, "Algorithms", null, null },
                    { new Guid("019df349-a51c-7aba-9caf-958e1b56b349"), "CS203", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Database design and SQL.", false, "Database Systems", null, null },
                    { new Guid("019df349-a51c-7aba-9caf-958f9e72f562"), "CS204", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Concepts of OS and processes.", false, "Operating Systems", null, null },
                    { new Guid("019df349-a51c-7aba-9caf-9590bdb364c0"), "CS205", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Network architecture and protocols.", false, "Computer Networks", null, null },
                    { new Guid("019df349-a51c-7aba-9caf-9591262e88f8"), "CS301", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Software development lifecycle.", false, "Software Engineering", null, null },
                    { new Guid("019df349-a51c-7aba-9caf-95920fdf4a77"), "AI301", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Introduction to AI concepts.", false, "Artificial Intelligence", null, null },
                    { new Guid("019df349-a51c-7aba-9caf-9593115238f8"), "AI302", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Supervised and unsupervised learning.", false, "Machine Learning", null, null },
                    { new Guid("019df349-a51c-7aba-9caf-9594f9d4ca6a"), "AI303", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Neural networks and deep models.", false, "Deep Learning", null, null },
                    { new Guid("019df349-a51c-7aba-9caf-9595b8b7914a"), "AI304", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Image processing and vision systems.", false, "Computer Vision", null, null },
                    { new Guid("019df349-a51c-7aba-9caf-959678c41dea"), "AI305", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Text processing and NLP techniques.", false, "Natural Language Processing", null, null },
                    { new Guid("019df349-a51c-7aba-9caf-95977f4e315c"), "CS302", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Security principles and practices.", false, "Cyber Security", null, null },
                    { new Guid("019df349-a51c-7aba-9caf-9598a067e97e"), "CS303", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Encryption and security algorithms.", false, "Cryptography", null, null },
                    { new Guid("019df349-a51c-7aba-9caf-9599074ce97b"), "CS304", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Concepts of distributed computing.", false, "Distributed Systems", null, null },
                    { new Guid("019df349-a51c-7aba-9caf-959aa16694f7"), "CS305", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Cloud platforms and services.", false, "Cloud Computing", null, null },
                    { new Guid("019df349-a51c-7aba-9caf-959b0bc724f7"), "CS306", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "User interface and UX design.", false, "Human Computer Interaction", null, null },
                    { new Guid("019df349-a51c-7aba-9caf-959c5b7e0ee1"), "CS307", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Design of compilers and interpreters.", false, "Compiler Design", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-95890ee5ba62"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-958a05c9c32a"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-958b81dee3d6"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-958c542ea45f"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-958d58bae05b"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-958e1b56b349"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-958f9e72f562"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-9590bdb364c0"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-9591262e88f8"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-95920fdf4a77"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-9593115238f8"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-9594f9d4ca6a"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-9595b8b7914a"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-959678c41dea"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-95977f4e315c"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-9598a067e97e"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-9599074ce97b"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-959aa16694f7"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-959b0bc724f7"));

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-959c5b7e0ee1"));
        }
    }
}
