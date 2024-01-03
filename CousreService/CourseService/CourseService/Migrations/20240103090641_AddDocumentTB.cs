using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseService.Migrations
{
    public partial class AddDocumentTB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TypeFile",
                keyColumn: "Id",
                keyValue: "5053ab40-feab-48ce-99fc-752f6b87e812");

            migrationBuilder.DeleteData(
                table: "TypeFile",
                keyColumn: "Id",
                keyValue: "91280a33-3963-459d-838c-bf427e3d16e7");

            migrationBuilder.AddColumn<string>(
                name: "DocumentId",
                table: "Lesson",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileContent = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocumentId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_DocumentId",
                table: "Lesson",
                column: "DocumentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_Documents_DocumentId",
                table: "Lesson",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "DocumentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_Documents_DocumentId",
                table: "Lesson");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Lesson_DocumentId",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "Lesson");

            migrationBuilder.InsertData(
                table: "TypeFile",
                columns: new[] { "Id", "Name" },
                values: new object[] { "5053ab40-feab-48ce-99fc-752f6b87e812", "Documents" });

            migrationBuilder.InsertData(
                table: "TypeFile",
                columns: new[] { "Id", "Name" },
                values: new object[] { "91280a33-3963-459d-838c-bf427e3d16e7", "Slides" });
        }
    }
}
