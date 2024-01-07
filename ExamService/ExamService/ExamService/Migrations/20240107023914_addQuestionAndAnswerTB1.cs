using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamService.Migrations
{
    public partial class addQuestionAndAnswerTB1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Question",
                newName: "ContentQuestion");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Answer",
                newName: "ContentAnswer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContentQuestion",
                table: "Question",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "ContentAnswer",
                table: "Answer",
                newName: "Content");
        }
    }
}
