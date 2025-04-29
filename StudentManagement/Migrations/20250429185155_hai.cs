using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagement.Migrations
{
    /// <inheritdoc />
    public partial class hai : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Enrollment_EnrollmentId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_EnrollmentId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "EnrollmentId",
                table: "Grades");

            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstructorName",
                table: "Instructor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GradeId",
                table: "Enrollment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_GradeId",
                table: "Enrollment",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Grades_GradeId",
                table: "Enrollment",
                column: "GradeId",
                principalTable: "Grades",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Grades_GradeId",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_GradeId",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "InstructorName",
                table: "Instructor");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "Enrollment");

            migrationBuilder.AddColumn<int>(
                name: "EnrollmentId",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_EnrollmentId",
                table: "Grades",
                column: "EnrollmentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Enrollment_EnrollmentId",
                table: "Grades",
                column: "EnrollmentId",
                principalTable: "Enrollment",
                principalColumn: "EnrollmentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
