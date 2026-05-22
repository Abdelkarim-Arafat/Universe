using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Universe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addCourseType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RequirementType",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-95890ee5ba62"),
                column: "RequirementType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-958a05c9c32a"),
                column: "RequirementType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-958b81dee3d6"),
                column: "RequirementType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-958c542ea45f"),
                column: "RequirementType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-958d58bae05b"),
                column: "RequirementType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-958e1b56b349"),
                column: "RequirementType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-958f9e72f562"),
                column: "RequirementType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-9590bdb364c0"),
                column: "RequirementType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-9591262e88f8"),
                column: "RequirementType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-95920fdf4a77"),
                column: "RequirementType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-9593115238f8"),
                column: "RequirementType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-9594f9d4ca6a"),
                column: "RequirementType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-9595b8b7914a"),
                column: "RequirementType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-959678c41dea"),
                column: "RequirementType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-95977f4e315c"),
                column: "RequirementType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-9598a067e97e"),
                column: "RequirementType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-9599074ce97b"),
                column: "RequirementType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-959aa16694f7"),
                column: "RequirementType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-959b0bc724f7"),
                column: "RequirementType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: new Guid("019df349-a51c-7aba-9caf-959c5b7e0ee1"),
                column: "RequirementType",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequirementType",
                table: "Courses");
        }
    }
}
