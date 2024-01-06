using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamService.Migrations
{
    public partial class addExamTb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ExamType",
                columns: new[] { "Id", "name" },
                values: new object[] { "75438289-ee3c-40b8-9ad4-e9e7c4e0c165", "45 minutes" });

            migrationBuilder.InsertData(
                table: "ExamType",
                columns: new[] { "Id", "name" },
                values: new object[] { "c54dc418-1183-4e73-8fb2-a6f61356bd9c", "15 minutes" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ExamType",
                keyColumn: "Id",
                keyValue: "75438289-ee3c-40b8-9ad4-e9e7c4e0c165");

            migrationBuilder.DeleteData(
                table: "ExamType",
                keyColumn: "Id",
                keyValue: "c54dc418-1183-4e73-8fb2-a6f61356bd9c");
        }
    }
}
