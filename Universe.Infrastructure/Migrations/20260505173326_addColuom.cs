using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Universe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addColuom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colleges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaxGpa = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colleges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    RoomType = table.Column<int>(type: "int", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AcademicPrograms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RequiredCreditHours = table.Column<int>(type: "int", nullable: false),
                    CertificateTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AcademicDegree = table.Column<int>(type: "int", nullable: true),
                    AcademicLoad = table.Column<int>(type: "int", nullable: true),
                    CollegeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicPrograms_Colleges_CollegeId",
                        column: x => x.CollegeId,
                        principalTable: "Colleges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AcademicYears",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CollegeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicYears", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicYears_Colleges_CollegeId",
                        column: x => x.CollegeId,
                        principalTable: "Colleges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RequirementType = table.Column<int>(type: "int", nullable: true),
                    CollegeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Colleges_CollegeId",
                        column: x => x.CollegeId,
                        principalTable: "Colleges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CollegeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Colleges_CollegeId",
                        column: x => x.CollegeId,
                        principalTable: "Colleges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CollegeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AcademicProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AcademicPrograms_AcademicProgramId",
                        column: x => x.AcademicProgramId,
                        principalTable: "AcademicPrograms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Colleges_CollegeId",
                        column: x => x.CollegeId,
                        principalTable: "Colleges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    MinScore = table.Column<int>(type: "int", nullable: false),
                    MaxScore = table.Column<int>(type: "int", nullable: false),
                    MinGradePoint = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxGradePoint = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AcademicProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grades_AcademicPrograms_AcademicProgramId",
                        column: x => x.AcademicProgramId,
                        principalTable: "AcademicPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinHours = table.Column<int>(type: "int", nullable: false),
                    MaxHours = table.Column<int>(type: "int", nullable: false),
                    AcademicProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Levels_AcademicPrograms_AcademicProgramId",
                        column: x => x.AcademicProgramId,
                        principalTable: "AcademicPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudyLoadRules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MinHours = table.Column<int>(type: "int", nullable: false),
                    MaxHours = table.Column<int>(type: "int", nullable: false),
                    GpaFrom = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GpaTo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AcademicProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyLoadRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyLoadRules_AcademicPrograms_AcademicProgramId",
                        column: x => x.AcademicProgramId,
                        principalTable: "AcademicPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    AcademicYearId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false),
                    IsResultAnnounced = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Semesters_AcademicYears_AcademicYearId",
                        column: x => x.AcademicYearId,
                        principalTable: "AcademicYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoursePrerequisites",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PrerequisiteCourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursePrerequisites", x => new { x.CourseId, x.PrerequisiteCourseId });
                    table.ForeignKey(
                        name: "FK_CoursePrerequisites_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoursePrerequisites_Courses_PrerequisiteCourseId",
                        column: x => x.PrerequisiteCourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PasswordResetOtps",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodeHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false),
                    isUsed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Attempts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResetOtps", x => new { x.UserId, x.Id });
                    table.ForeignKey(
                        name: "FK_PasswordResetOtps_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevokedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => new { x.UserId, x.Id });
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StudentCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Religion = table.Column<int>(type: "int", nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    PlaceOfBirth = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NationalIdOrPassport = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ContactInfo_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ContactInfo_Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ContactInfo_PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ContactInfo_PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ContactInfo_Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ParentInfo_GuardianName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParentInfo_RelationshipDegree = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ParentInfo_MotherName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParentInfo_Job = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ParentInfo_GuardianCity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParentInfo_GuardianAddress = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ParentInfo_GuardianPhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ParentInfo_GuardianEmail = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PreviousQualification_SchoolName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PreviousQualification_EnrollmentYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreviousQualification_SeatNumber = table.Column<int>(type: "int", nullable: false),
                    PreviousQualification_Qualification = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PreviousQualification_GraduationYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreviousQualification_TotalGrade = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    PreviousQualification_AdmissionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MilitaryInfo_MilitaryStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MilitaryInfo_MilitaryNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MilitaryInfo_DecisionNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MilitaryInfo_DecisionDate = table.Column<DateOnly>(type: "date", nullable: true),
                    MilitaryInfo_EnrollmentDate = table.Column<DateOnly>(type: "date", nullable: true),
                    MilitaryInfo_EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    AdvisorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CollegeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_AspNetUsers_AdvisorId",
                        column: x => x.AdvisorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Students_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Colleges_CollegeId",
                        column: x => x.CollegeId,
                        principalTable: "Colleges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeachingSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    GroupNumber = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    InstructorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeachingSessions_AspNetUsers_InstructorId",
                        column: x => x.InstructorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeachingSessions_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AcademicEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SemesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicEvents_AcademicPrograms_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "AcademicPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AcademicEvents_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseOfferings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreditHours = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    TotalGrade = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    SuccessPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    IsOptional = table.Column<bool>(type: "bit", nullable: false),
                    IsIncludedInGpa = table.Column<bool>(type: "bit", nullable: false),
                    OptionalGroupCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NumberOfGroups = table.Column<int>(type: "int", nullable: false),
                    IsOpenForControl = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SemesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseOfferings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseOfferings_AcademicPrograms_AcademicProgramId",
                        column: x => x.AcademicProgramId,
                        principalTable: "AcademicPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseOfferings_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseOfferings_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseOfferings_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamTerms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamType = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    SemesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamTerms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamTerms_AcademicPrograms_AcademicProgramId",
                        column: x => x.AcademicProgramId,
                        principalTable: "AcademicPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamTerms_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProgramSchedules",
                columns: table => new
                {
                    ProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SemesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayStartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    DayEndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    SlotDurationMinutes = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramSchedules", x => new { x.ProgramId, x.SemesterId });
                    table.ForeignKey(
                        name: "FK_ProgramSchedules_AcademicPrograms_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "AcademicPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgramSchedules_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudyLoadByLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MinHours = table.Column<int>(type: "int", nullable: false),
                    MaxHours = table.Column<int>(type: "int", nullable: false),
                    LevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SemesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyLoadByLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyLoadByLevels_AcademicPrograms_AcademicProgramId",
                        column: x => x.AcademicProgramId,
                        principalTable: "AcademicPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudyLoadByLevels_Levels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudyLoadByLevels_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentAcademicPrograms",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Currently = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAcademicPrograms", x => new { x.StudentId, x.AcademicProgramId });
                    table.ForeignKey(
                        name: "FK_StudentAcademicPrograms_AcademicPrograms_AcademicProgramId",
                        column: x => x.AcademicProgramId,
                        principalTable: "AcademicPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentAcademicPrograms_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseOfferingAssessments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CourseOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaxScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseOfferingAssessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseOfferingAssessments_CourseOfferings_CourseOfferingId",
                        column: x => x.CourseOfferingId,
                        principalTable: "CourseOfferings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseOfferingSessions",
                columns: table => new
                {
                    TeachingSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseOfferingSessions", x => new { x.CourseOfferingId, x.TeachingSessionId });
                    table.ForeignKey(
                        name: "FK_CourseOfferingSessions_CourseOfferings_CourseOfferingId",
                        column: x => x.CourseOfferingId,
                        principalTable: "CourseOfferings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseOfferingSessions_TeachingSessions_TeachingSessionId",
                        column: x => x.TeachingSessionId,
                        principalTable: "TeachingSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupNumber = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollments_CourseOfferings_CourseOfferingId",
                        column: x => x.CourseOfferingId,
                        principalTable: "CourseOfferings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseOfferingExams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    CourseOfferingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamTermId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseOfferingExams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseOfferingExams_CourseOfferings_CourseOfferingId",
                        column: x => x.CourseOfferingId,
                        principalTable: "CourseOfferings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseOfferingExams_ExamTerms_ExamTermId",
                        column: x => x.ExamTermId,
                        principalTable: "ExamTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamCommittees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaxCapacity = table.Column<int>(type: "int", nullable: false),
                    CommitteeNumber = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamTermId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamCommittees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExamCommittees_ExamTerms_ExamTermId",
                        column: x => x.ExamTermId,
                        principalTable: "ExamTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamCommittees_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRequests_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceRequests_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceRequests_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentAssessments",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseOfferingAssessmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    degree = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAssessments", x => new { x.StudentId, x.CourseOfferingAssessmentId });
                    table.ForeignKey(
                        name: "FK_StudentAssessments_CourseOfferingAssessments_CourseOfferingAssessmentId",
                        column: x => x.CourseOfferingAssessmentId,
                        principalTable: "CourseOfferingAssessments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentAssessments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TeachingSessionEnrollments",
                columns: table => new
                {
                    TeachingSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrollmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingSessionEnrollments", x => new { x.EnrollmentId, x.TeachingSessionId });
                    table.ForeignKey(
                        name: "FK_TeachingSessionEnrollments_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeachingSessionEnrollments_TeachingSessions_TeachingSessionId",
                        column: x => x.TeachingSessionId,
                        principalTable: "TeachingSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseOfferingCommittees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseOfferingExamId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExamCommitteeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseOfferingCommittees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseOfferingCommittees_CourseOfferingExams_CourseOfferingExamId",
                        column: x => x.CourseOfferingExamId,
                        principalTable: "CourseOfferingExams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CourseOfferingCommittees_ExamCommittees_ExamCommitteeId",
                        column: x => x.ExamCommitteeId,
                        principalTable: "ExamCommittees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExamSeats",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseOfferingCommitteeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeatNumber = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamSeats", x => new { x.StudentId, x.CourseOfferingCommitteeId });
                    table.ForeignKey(
                        name: "FK_ExamSeats_CourseOfferingCommittees_CourseOfferingCommitteeId",
                        column: x => x.CourseOfferingCommitteeId,
                        principalTable: "CourseOfferingCommittees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExamSeats_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Level", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0191a4b6-c4fc-752e-9d95-40b5e4e68054"), "0191a4b6-c4fc-752e-9d95-40b631d1866d", false, false, 1, "Admin", "ADMIN" },
                    { new Guid("0191a4b6-c4fc-752e-9d95-40b7a5cb88f0"), "0191a4b6-c4fc-752e-9d95-40b85cf3fd22", true, false, 4, "Student", "STUDENT" },
                    { new Guid("019c1e67-90d0-72a4-a602-9a98388515e9"), "019c1e68-2418-723e-8a28-5638fd18e4e7", false, false, 3, "Staff", "STAFF" },
                    { new Guid("019c1e6e-5518-7479-b749-b1c5d4a21430"), "019c1e6e-8a41-7129-a8f1-28dc8a042458", false, false, 2, "AcademicAdvising", "ACADEMICADVISING" }
                });

            migrationBuilder.InsertData(
                table: "Colleges",
                columns: new[] { "Id", "CreatedAt", "CreatedById", "DeletedAt", "Description", "ImageUrl", "IsDeleted", "MaxGpa", "Name", "UpdatedAt", "UpdatedById" },
                values: new object[] { new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, false, 0m, "Computers and Artificial Intelligence", null, null });

            migrationBuilder.InsertData(
                table: "AcademicPrograms",
                columns: new[] { "Id", "AcademicDegree", "AcademicLoad", "CertificateTitle", "Code", "CollegeId", "CreatedAt", "CreatedById", "DeletedAt", "Description", "IsDeleted", "Name", "RequiredCreditHours", "UpdatedAt", "UpdatedById" },
                values: new object[,]
                {
                    { new Guid("019df1d0-68a6-7696-9aa6-4014a33a997f"), 2, 1, "Bachelor of Computer Science", "CS", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Study of computation, algorithms, and software systems.", false, "Computer Science", 144, null, null },
                    { new Guid("019df1d0-b671-75b2-9c14-69ce00131461"), 2, 1, "Bachelor of Artificial Intelligence", "AI", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Study of intelligent systems, machine learning, and data science.", false, "Artificial Intelligence", 144, null, null },
                    { new Guid("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), 2, 1, "Bachelor of Information Systems", "IS", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Focus on information management and business systems.", false, "Information Systems", 138, null, null },
                    { new Guid("019df1d1-0356-789e-840b-56d31396608a"), 2, 1, "Bachelor of Software Engineering", "SE", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Engineering principles applied to software development.", false, "Software Engineering", 150, null, null },
                    { new Guid("019df1d1-2da1-78d2-adeb-84cb6b07a459"), 2, 1, "Bachelor of Cyber Security", "CY", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Protection of systems, networks, and data from cyber threats.", false, "Cyber Security", 144, null, null }
                });

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
                table: "AspNetUsers",
                columns: new[] { "Id", "AcademicProgramId", "AccessFailedCount", "CollegeId", "ConcurrencyStamp", "DeletedAt", "Email", "EmailConfirmed", "ImageUrl", "IsDeleted", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "StudentId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("019c0582-3473-7802-8f11-50cc1e6513d5"), null, 0, new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), "01993360-1c17-7054-bdee-1d5a9e780f23", null, "SVNU@Universe.com", true, null, false, false, null, "SVNU", "SVNU@UNIVERSE.COM", "SVNU", "AQAAAAIAAYagAAAAEFhcy5yaaQ5/9U5cfv8MnI3DBzUZ0ido47Hf7N0qKI20sJp8yGuUPuwPOIGdkNQJjA==", null, false, "55BF92C9EF0249CDA210D85D1A851BC9", null, false, "SVNU" },
                    { new Guid("019c1e76-6f5a-7522-8327-a2a72adbbbe8"), null, 0, new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), "019c1e76-a7da-76a1-a6c8-96163fb4a2fc", null, "Admin@Universe.com", true, null, false, false, null, "Admin", "ADMIN@UNIVERSE.COM", "ADMIN", "AQAAAAIAAYagAAAAEFhcy5yaaQ5/9U5cfv8MnI3DBzUZ0ido47Hf7N0qKI20sJp8yGuUPuwPOIGdkNQJjA==", null, false, "96A84AD3C17B4EBD95CE5AC8266BD761", null, false, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Code", "CollegeId", "CreatedAt", "CreatedById", "DeletedAt", "Description", "IsDeleted", "Name", "RequirementType", "UpdatedAt", "UpdatedById" },
                values: new object[,]
                {
                    { new Guid("019df349-a51c-7aba-9caf-95890ee5ba62"), "CS101", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Introduction to programming concepts.", false, "Programming 1", null, null, null },
                    { new Guid("019df349-a51c-7aba-9caf-958a05c9c32a"), "CS102", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Advanced programming and problem solving.", false, "Programming 2", null, null, null },
                    { new Guid("019df349-a51c-7aba-9caf-958b81dee3d6"), "CS103", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Concepts of OOP and design principles.", false, "Object Oriented Programming", null, null, null },
                    { new Guid("019df349-a51c-7aba-9caf-958c542ea45f"), "CS201", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Data organization and structures.", false, "Data Structures", null, null, null },
                    { new Guid("019df349-a51c-7aba-9caf-958d58bae05b"), "CS202", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Algorithm design and analysis.", false, "Algorithms", null, null, null },
                    { new Guid("019df349-a51c-7aba-9caf-958e1b56b349"), "CS203", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Database design and SQL.", false, "Database Systems", null, null, null },
                    { new Guid("019df349-a51c-7aba-9caf-958f9e72f562"), "CS204", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Concepts of OS and processes.", false, "Operating Systems", null, null, null },
                    { new Guid("019df349-a51c-7aba-9caf-9590bdb364c0"), "CS205", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Network architecture and protocols.", false, "Computer Networks", null, null, null },
                    { new Guid("019df349-a51c-7aba-9caf-9591262e88f8"), "CS301", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Software development lifecycle.", false, "Software Engineering", null, null, null },
                    { new Guid("019df349-a51c-7aba-9caf-95920fdf4a77"), "AI301", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Introduction to AI concepts.", false, "Artificial Intelligence", null, null, null },
                    { new Guid("019df349-a51c-7aba-9caf-9593115238f8"), "AI302", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Supervised and unsupervised learning.", false, "Machine Learning", null, null, null },
                    { new Guid("019df349-a51c-7aba-9caf-9594f9d4ca6a"), "AI303", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Neural networks and deep models.", false, "Deep Learning", null, null, null },
                    { new Guid("019df349-a51c-7aba-9caf-9595b8b7914a"), "AI304", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Image processing and vision systems.", false, "Computer Vision", null, null, null },
                    { new Guid("019df349-a51c-7aba-9caf-959678c41dea"), "AI305", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Text processing and NLP techniques.", false, "Natural Language Processing", null, null, null },
                    { new Guid("019df349-a51c-7aba-9caf-95977f4e315c"), "CS302", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Security principles and practices.", false, "Cyber Security", null, null, null },
                    { new Guid("019df349-a51c-7aba-9caf-9598a067e97e"), "CS303", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Encryption and security algorithms.", false, "Cryptography", null, null, null },
                    { new Guid("019df349-a51c-7aba-9caf-9599074ce97b"), "CS304", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Concepts of distributed computing.", false, "Distributed Systems", null, null, null },
                    { new Guid("019df349-a51c-7aba-9caf-959aa16694f7"), "CS305", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Cloud platforms and services.", false, "Cloud Computing", null, null, null },
                    { new Guid("019df349-a51c-7aba-9caf-959b0bc724f7"), "CS306", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "User interface and UX design.", false, "Human Computer Interaction", null, null, null },
                    { new Guid("019df349-a51c-7aba-9caf-959c5b7e0ee1"), "CS307", new Guid("019c1ea6-1738-71cb-8cfd-a90e126d177e"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, "Design of compilers and interpreters.", false, "Compiler Design", null, null, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "ApplicationUserId" },
                values: new object[,]
                {
                    { new Guid("0191a4b6-c4fc-752e-9d95-40b5e4e68054"), new Guid("019c0582-3473-7802-8f11-50cc1e6513d5"), null },
                    { new Guid("019c1e6e-5518-7479-b749-b1c5d4a21430"), new Guid("019c1e76-6f5a-7522-8327-a2a72adbbbe8"), null }
                });

            migrationBuilder.InsertData(
                table: "Levels",
                columns: new[] { "Id", "AcademicProgramId", "CreatedAt", "CreatedById", "DeletedAt", "IsDeleted", "MaxHours", "MinHours", "Name", "UpdatedAt", "UpdatedById" },
                values: new object[,]
                {
                    { new Guid("019df4f8-4012-7469-9aa8-7cf36f1d180a"), new Guid("019df1d0-68a6-7696-9aa6-4014a33a997f"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 36, 0, "Level 1", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7cf4d268fbec"), new Guid("019df1d0-68a6-7696-9aa6-4014a33a997f"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 72, 37, "Level 2", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7cf568231cba"), new Guid("019df1d0-68a6-7696-9aa6-4014a33a997f"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 108, 73, "Level 3", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7cf6124b1ecb"), new Guid("019df1d0-68a6-7696-9aa6-4014a33a997f"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 144, 109, "Level 4", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7cf7ad92b89e"), new Guid("019df1d0-b671-75b2-9c14-69ce00131461"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 36, 0, "Level 1", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7cf8937d84f4"), new Guid("019df1d0-b671-75b2-9c14-69ce00131461"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 72, 37, "Level 2", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7cf9e82c4b82"), new Guid("019df1d0-b671-75b2-9c14-69ce00131461"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 108, 73, "Level 3", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7cfa9abd172d"), new Guid("019df1d0-b671-75b2-9c14-69ce00131461"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 144, 109, "Level 4", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7cfb22e7bb31"), new Guid("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 34, 0, "Level 1", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7cfcf91e0ee9"), new Guid("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 68, 35, "Level 2", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7cfdd431525b"), new Guid("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 102, 69, "Level 3", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7cfeb8e8d41d"), new Guid("019df1d0-ddcc-7f90-9a82-1e1d8d1c0cfe"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 138, 103, "Level 4", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7cffb491c63b"), new Guid("019df1d1-0356-789e-840b-56d31396608a"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 38, 0, "Level 1", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7d00e6c3ddce"), new Guid("019df1d1-0356-789e-840b-56d31396608a"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 76, 39, "Level 2", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7d01aebd87c1"), new Guid("019df1d1-0356-789e-840b-56d31396608a"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 114, 77, "Level 3", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7d024664404f"), new Guid("019df1d1-0356-789e-840b-56d31396608a"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 150, 115, "Level 4", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7d033b7e618f"), new Guid("019df1d1-2da1-78d2-adeb-84cb6b07a459"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 36, 0, "Level 1", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7d049703d7cb"), new Guid("019df1d1-2da1-78d2-adeb-84cb6b07a459"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 72, 37, "Level 2", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7d05feaad5d1"), new Guid("019df1d1-2da1-78d2-adeb-84cb6b07a459"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 108, 73, "Level 3", null, null },
                    { new Guid("019df4f8-4012-7469-9aa8-7d065b1fbceb"), new Guid("019df1d1-2da1-78d2-adeb-84cb6b07a459"), new DateTime(2026, 5, 4, 8, 0, 0, 0, DateTimeKind.Utc), null, null, false, 144, 109, "Level 4", null, null }
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

            migrationBuilder.CreateIndex(
                name: "IX_AcademicEvents_ProgramId",
                table: "AcademicEvents",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicEvents_SemesterId",
                table: "AcademicEvents",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicPrograms_CollegeId",
                table: "AcademicPrograms",
                column: "CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicYears_CollegeId",
                table: "AcademicYears",
                column: "CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_ApplicationUserId",
                table: "AspNetUserRoles",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AcademicProgramId",
                table: "AspNetUsers",
                column: "AcademicProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CollegeId",
                table: "AspNetUsers",
                column: "CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Name",
                table: "AspNetUsers",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_Code",
                table: "Buildings",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Colleges_Name",
                table: "Colleges",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferingAssessments_CourseOfferingId",
                table: "CourseOfferingAssessments",
                column: "CourseOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferingCommittees_CourseOfferingExamId",
                table: "CourseOfferingCommittees",
                column: "CourseOfferingExamId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferingCommittees_ExamCommitteeId",
                table: "CourseOfferingCommittees",
                column: "ExamCommitteeId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferingExams_CourseOfferingId",
                table: "CourseOfferingExams",
                column: "CourseOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferingExams_ExamTermId",
                table: "CourseOfferingExams",
                column: "ExamTermId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferings_AcademicProgramId",
                table: "CourseOfferings",
                column: "AcademicProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferings_CourseId",
                table: "CourseOfferings",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferings_LevelId",
                table: "CourseOfferings",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferings_SemesterId",
                table: "CourseOfferings",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseOfferingSessions_TeachingSessionId",
                table: "CourseOfferingSessions",
                column: "TeachingSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursePrerequisites_PrerequisiteCourseId",
                table: "CoursePrerequisites",
                column: "PrerequisiteCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Code",
                table: "Courses",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CollegeId",
                table: "Courses",
                column: "CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseOfferingId",
                table: "Enrollments",
                column: "CourseOfferingId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamCommittees_ExamTermId",
                table: "ExamCommittees",
                column: "ExamTermId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamCommittees_RoomId",
                table: "ExamCommittees",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamSeats_CourseOfferingCommitteeId",
                table: "ExamSeats",
                column: "CourseOfferingCommitteeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamTerms_AcademicProgramId",
                table: "ExamTerms",
                column: "AcademicProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamTerms_SemesterId",
                table: "ExamTerms",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_AcademicProgramId",
                table: "Grades",
                column: "AcademicProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_Code",
                table: "Grades",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Levels_AcademicProgramId",
                table: "Levels",
                column: "AcademicProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ServiceId",
                table: "Payments",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_StudentId",
                table: "Payments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramSchedules_ProgramId_SemesterId",
                table: "ProgramSchedules",
                columns: new[] { "ProgramId", "SemesterId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgramSchedules_SemesterId",
                table: "ProgramSchedules",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_BuildingId",
                table: "Rooms",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_AcademicYearId",
                table: "Semesters",
                column: "AcademicYearId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_PaymentId",
                table: "ServiceRequests",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_ServiceId",
                table: "ServiceRequests",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_StudentId",
                table: "ServiceRequests",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_CollegeId",
                table: "Services",
                column: "CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAcademicPrograms_AcademicProgramId",
                table: "StudentAcademicPrograms",
                column: "AcademicProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAssessments_CourseOfferingAssessmentId",
                table: "StudentAssessments",
                column: "CourseOfferingAssessmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_AdvisorId",
                table: "Students",
                column: "AdvisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_CollegeId",
                table: "Students",
                column: "CollegeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyLoadByLevels_AcademicProgramId",
                table: "StudyLoadByLevels",
                column: "AcademicProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyLoadByLevels_LevelId",
                table: "StudyLoadByLevels",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyLoadByLevels_SemesterId",
                table: "StudyLoadByLevels",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyLoadRules_AcademicProgramId",
                table: "StudyLoadRules",
                column: "AcademicProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingSessionEnrollments_TeachingSessionId",
                table: "TeachingSessionEnrollments",
                column: "TeachingSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingSessions_InstructorId",
                table: "TeachingSessions",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingSessions_RoomId",
                table: "TeachingSessions",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicEvents");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CourseOfferingSessions");

            migrationBuilder.DropTable(
                name: "CoursePrerequisites");

            migrationBuilder.DropTable(
                name: "ExamSeats");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "PasswordResetOtps");

            migrationBuilder.DropTable(
                name: "ProgramSchedules");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "ServiceRequests");

            migrationBuilder.DropTable(
                name: "StudentAcademicPrograms");

            migrationBuilder.DropTable(
                name: "StudentAssessments");

            migrationBuilder.DropTable(
                name: "StudyLoadByLevels");

            migrationBuilder.DropTable(
                name: "StudyLoadRules");

            migrationBuilder.DropTable(
                name: "TeachingSessionEnrollments");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CourseOfferingCommittees");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "CourseOfferingAssessments");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "TeachingSessions");

            migrationBuilder.DropTable(
                name: "CourseOfferingExams");

            migrationBuilder.DropTable(
                name: "ExamCommittees");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "CourseOfferings");

            migrationBuilder.DropTable(
                name: "ExamTerms");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "AcademicPrograms");

            migrationBuilder.DropTable(
                name: "AcademicYears");

            migrationBuilder.DropTable(
                name: "Colleges");
        }
    }
}
