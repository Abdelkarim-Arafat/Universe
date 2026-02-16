using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Universe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBaseEntityForTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CoursePrerequisite",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "CoursePrerequisite",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CoursePrerequisite",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CoursePrerequisite",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CourseDepartment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "CourseDepartment",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CourseDepartment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CourseDepartment",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CoursePrerequisite");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "CoursePrerequisite");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CoursePrerequisite");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CoursePrerequisite");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CourseDepartment");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "CourseDepartment");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CourseDepartment");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CourseDepartment");
        }
    }
}
