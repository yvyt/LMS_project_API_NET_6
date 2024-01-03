using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseService.Migrations
{
    public partial class updateDocumentTB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TypeFile",
                keyColumn: "Id",
                keyValue: "10873fbc-486c-4103-bbad-86c79106a652");

            migrationBuilder.DeleteData(
                table: "TypeFile",
                keyColumn: "Id",
                keyValue: "d8e47b1b-92c2-495f-a506-e60e78af6582");

            migrationBuilder.AddColumn<string>(
                name: "link",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "TypeFile",
                columns: new[] { "Id", "Name" },
                values: new object[] { "c3769f7e-6c37-417c-a734-62411ab32cc5", "Documents" });

            migrationBuilder.InsertData(
                table: "TypeFile",
                columns: new[] { "Id", "Name" },
                values: new object[] { "dc84389b-980c-4399-bdd3-32e2b1bd1cb3", "Slides" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TypeFile",
                keyColumn: "Id",
                keyValue: "c3769f7e-6c37-417c-a734-62411ab32cc5");

            migrationBuilder.DeleteData(
                table: "TypeFile",
                keyColumn: "Id",
                keyValue: "dc84389b-980c-4399-bdd3-32e2b1bd1cb3");

            migrationBuilder.DropColumn(
                name: "link",
                table: "Documents");

            migrationBuilder.InsertData(
                table: "TypeFile",
                columns: new[] { "Id", "Name" },
                values: new object[] { "10873fbc-486c-4103-bbad-86c79106a652", "Documents" });

            migrationBuilder.InsertData(
                table: "TypeFile",
                columns: new[] { "Id", "Name" },
                values: new object[] { "d8e47b1b-92c2-495f-a506-e60e78af6582", "Slides" });
        }
    }
}
