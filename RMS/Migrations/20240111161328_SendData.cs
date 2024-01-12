using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMS.Migrations
{
    /// <inheritdoc />
    public partial class SendData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseFee = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoursePdf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courses", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Parentage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentAge = table.Column<int>(type: "int", nullable: false),
                    StudentEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adhaar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StudentPhoto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Course = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_Students_courses_Course",
                        column: x => x.Course,
                        principalTable: "courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "feeDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalFee = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    RegistrationDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feeDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_feeDetails_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_feeDetails_StudentId",
                table: "feeDetails",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_Course",
                table: "Students",
                column: "Course");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "feeDetails");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "courses");
        }
    }
}
