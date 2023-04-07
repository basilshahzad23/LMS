using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Migrations
{
    public partial class addddddddhjhjk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Edition = table.Column<int>(type: "int", nullable: false),
                    YearofPublishing = table.Column<int>(type: "int", nullable: false),
                    Pages = table.Column<int>(type: "int", nullable: false),
                    PublisherID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AuthorID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SubjectID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LanguageID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BookTypeID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorID",
                        column: x => x.AuthorID,
                        principalTable: "Authors",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Books_BookTypes_BookTypeID",
                        column: x => x.BookTypeID,
                        principalTable: "BookTypes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Books_Languages_LanguageID",
                        column: x => x.LanguageID,
                        principalTable: "Languages",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Books_Publishers_PublisherID",
                        column: x => x.PublisherID,
                        principalTable: "Publishers",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Books_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorID",
                table: "Books",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookTypeID",
                table: "Books",
                column: "BookTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_LanguageID",
                table: "Books",
                column: "LanguageID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_PublisherID",
                table: "Books",
                column: "PublisherID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_SubjectID",
                table: "Books",
                column: "SubjectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
