using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Migrations
{
    public partial class addddddddhjhjkhkjhkh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookLedgers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Student_FacultyID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateIssued = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateToBeReturn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isReturn = table.Column<bool>(type: "bit", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookLedgers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookLedgers_Books_BookID",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookLedgers_Student_Faculty_Student_FacultyID",
                        column: x => x.Student_FacultyID,
                        principalTable: "Student_Faculty",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookLedgers_BookID",
                table: "BookLedgers",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_BookLedgers_Student_FacultyID",
                table: "BookLedgers",
                column: "Student_FacultyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookLedgers");
        }
    }
}
