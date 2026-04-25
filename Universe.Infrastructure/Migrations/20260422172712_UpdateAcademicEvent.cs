using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Universe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAcademicEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicEvents",
                table: "AcademicEvents");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "AcademicEvents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicEvents",
                table: "AcademicEvents",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicEvents_ProgramId",
                table: "AcademicEvents",
                column: "ProgramId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicEvents",
                table: "AcademicEvents");

            migrationBuilder.DropIndex(
                name: "IX_AcademicEvents_ProgramId",
                table: "AcademicEvents");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AcademicEvents");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicEvents",
                table: "AcademicEvents",
                columns: new[] { "ProgramId", "SemesterId" });
        }
    }
}
