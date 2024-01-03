using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseService.Migrations
{
    public partial class addData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          

            migrationBuilder.InsertData(
                table: "TypeFile",
                columns: new[] { "Id", "Name" },
                values: new object[] { "04587a6d-bde7-4359-a473-cb5afe218f54", "Documents" });

            migrationBuilder.InsertData(
                table: "TypeFile",
                columns: new[] { "Id", "Name" },
                values: new object[] { "cd5e91b8-c3c0-4f30-8b52-c16da307db1b", "Slides" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TypeFile",
                keyColumn: "Id",
                keyValue: "04587a6d-bde7-4359-a473-cb5afe218f54");

            migrationBuilder.DeleteData(
                table: "TypeFile",
                keyColumn: "Id",
                keyValue: "cd5e91b8-c3c0-4f30-8b52-c16da307db1b");

        }
    }
}
