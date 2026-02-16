using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Universe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updatetables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Department_DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Course_Colleges_CollegeId",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseDepartment_Course_CourseId",
                table: "CourseDepartment");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseDepartment_Department_DepartmentId",
                table: "CourseDepartment");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursePrerequisite_Course_CourseId",
                table: "CoursePrerequisite");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursePrerequisite_Course_PrerequisiteCourseId",
                table: "CoursePrerequisite");

            migrationBuilder.DropForeignKey(
                name: "FK_Department_Colleges_CollegeId",
                table: "Department");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Department",
                table: "Department");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursePrerequisite",
                table: "CoursePrerequisite");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseDepartment",
                table: "CourseDepartment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Course",
                table: "Course");

            migrationBuilder.RenameTable(
                name: "Department",
                newName: "Departments");

            migrationBuilder.RenameTable(
                name: "CoursePrerequisite",
                newName: "CoursePrerequisites");

            migrationBuilder.RenameTable(
                name: "CourseDepartment",
                newName: "CourseDepartments");

            migrationBuilder.RenameTable(
                name: "Course",
                newName: "Courses");

            migrationBuilder.RenameIndex(
                name: "IX_Department_CollegeId",
                table: "Departments",
                newName: "IX_Departments_CollegeId");

            migrationBuilder.RenameIndex(
                name: "IX_CoursePrerequisite_PrerequisiteCourseId",
                table: "CoursePrerequisites",
                newName: "IX_CoursePrerequisites_PrerequisiteCourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseDepartment_DepartmentId",
                table: "CourseDepartments",
                newName: "IX_CourseDepartments_DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Course_CollegeId",
                table: "Courses",
                newName: "IX_Courses_CollegeId");

            migrationBuilder.RenameIndex(
                name: "IX_Course_Code",
                table: "Courses",
                newName: "IX_Courses_Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursePrerequisites",
                table: "CoursePrerequisites",
                columns: new[] { "CourseId", "PrerequisiteCourseId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseDepartments",
                table: "CourseDepartments",
                columns: new[] { "CourseId", "DepartmentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseDepartments_Courses_CourseId",
                table: "CourseDepartments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseDepartments_Departments_DepartmentId",
                table: "CourseDepartments",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursePrerequisites_Courses_CourseId",
                table: "CoursePrerequisites",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursePrerequisites_Courses_PrerequisiteCourseId",
                table: "CoursePrerequisites",
                column: "PrerequisiteCourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Colleges_CollegeId",
                table: "Courses",
                column: "CollegeId",
                principalTable: "Colleges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Colleges_CollegeId",
                table: "Departments",
                column: "CollegeId",
                principalTable: "Colleges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseDepartments_Courses_CourseId",
                table: "CourseDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseDepartments_Departments_DepartmentId",
                table: "CourseDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursePrerequisites_Courses_CourseId",
                table: "CoursePrerequisites");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursePrerequisites_Courses_PrerequisiteCourseId",
                table: "CoursePrerequisites");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Colleges_CollegeId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Colleges_CollegeId",
                table: "Departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursePrerequisites",
                table: "CoursePrerequisites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseDepartments",
                table: "CourseDepartments");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "Department");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "Course");

            migrationBuilder.RenameTable(
                name: "CoursePrerequisites",
                newName: "CoursePrerequisite");

            migrationBuilder.RenameTable(
                name: "CourseDepartments",
                newName: "CourseDepartment");

            migrationBuilder.RenameIndex(
                name: "IX_Departments_CollegeId",
                table: "Department",
                newName: "IX_Department_CollegeId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_CollegeId",
                table: "Course",
                newName: "IX_Course_CollegeId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_Code",
                table: "Course",
                newName: "IX_Course_Code");

            migrationBuilder.RenameIndex(
                name: "IX_CoursePrerequisites_PrerequisiteCourseId",
                table: "CoursePrerequisite",
                newName: "IX_CoursePrerequisite_PrerequisiteCourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseDepartments_DepartmentId",
                table: "CourseDepartment",
                newName: "IX_CourseDepartment_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Department",
                table: "Department",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Course",
                table: "Course",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursePrerequisite",
                table: "CoursePrerequisite",
                columns: new[] { "CourseId", "PrerequisiteCourseId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseDepartment",
                table: "CourseDepartment",
                columns: new[] { "CourseId", "DepartmentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Department_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Colleges_CollegeId",
                table: "Course",
                column: "CollegeId",
                principalTable: "Colleges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseDepartment_Course_CourseId",
                table: "CourseDepartment",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseDepartment_Department_DepartmentId",
                table: "CourseDepartment",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursePrerequisite_Course_CourseId",
                table: "CoursePrerequisite",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursePrerequisite_Course_PrerequisiteCourseId",
                table: "CoursePrerequisite",
                column: "PrerequisiteCourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Department_Colleges_CollegeId",
                table: "Department",
                column: "CollegeId",
                principalTable: "Colleges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
