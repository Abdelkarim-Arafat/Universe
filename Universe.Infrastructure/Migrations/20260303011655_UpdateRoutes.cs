using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Universe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRoutes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levels_AcademicPrograms_AcademicProgramd",
                table: "Levels");

            migrationBuilder.RenameColumn(
                name: "AcademicProgramd",
                table: "Levels",
                newName: "AcademicProgramId");

            migrationBuilder.RenameIndex(
                name: "IX_Levels_AcademicProgramd",
                table: "Levels",
                newName: "IX_Levels_AcademicProgramId");

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_AcademicPrograms_AcademicProgramId",
                table: "Levels",
                column: "AcademicProgramId",
                principalTable: "AcademicPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Levels_AcademicPrograms_AcademicProgramId",
                table: "Levels");

            migrationBuilder.RenameColumn(
                name: "AcademicProgramId",
                table: "Levels",
                newName: "AcademicProgramd");

            migrationBuilder.RenameIndex(
                name: "IX_Levels_AcademicProgramId",
                table: "Levels",
                newName: "IX_Levels_AcademicProgramd");

            migrationBuilder.AddForeignKey(
                name: "FK_Levels_AcademicPrograms_AcademicProgramd",
                table: "Levels",
                column: "AcademicProgramd",
                principalTable: "AcademicPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
