using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Migrations
{
    public partial class BookBank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLedgers_Books_BookID",
                table: "BookLedgers");

            migrationBuilder.DropForeignKey(
                name: "FK_BookLedgers_Student_Faculty_Student_FacultyID",
                table: "BookLedgers");

            migrationBuilder.DropIndex(
                name: "IX_BookLedgers_BookID",
                table: "BookLedgers");

            migrationBuilder.DropIndex(
                name: "IX_BookLedgers_Student_FacultyID",
                table: "BookLedgers");

            migrationBuilder.AddColumn<string>(
                name: "BatchFor",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "BookBank",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchFor",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookBank",
                table: "Books");

            migrationBuilder.CreateIndex(
                name: "IX_BookLedgers_BookID",
                table: "BookLedgers",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_BookLedgers_Student_FacultyID",
                table: "BookLedgers",
                column: "Student_FacultyID");

            migrationBuilder.AddForeignKey(
                name: "FK_BookLedgers_Books_BookID",
                table: "BookLedgers",
                column: "BookID",
                principalTable: "Books",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookLedgers_Student_Faculty_Student_FacultyID",
                table: "BookLedgers",
                column: "Student_FacultyID",
                principalTable: "Student_Faculty",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
