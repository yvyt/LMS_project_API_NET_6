using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseService.Migrations
{
    public partial class updateDB3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TypeFile",
                keyColumn: "Id",
                keyValue: "6573f3e4-5fbb-4ef5-8e60-386b3448a724");

            migrationBuilder.DeleteData(
                table: "TypeFile",
                keyColumn: "Id",
                keyValue: "94837d7a-434e-426f-9b1d-0951237535ff");

            migrationBuilder.InsertData(
                table: "TypeFile",
                columns: new[] { "Id", "Name" },
                values: new object[] { "10873fbc-486c-4103-bbad-86c79106a652", "Documents" });

            migrationBuilder.InsertData(
                table: "TypeFile",
                columns: new[] { "Id", "Name" },
                values: new object[] { "d8e47b1b-92c2-495f-a506-e60e78af6582", "Slides" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TypeFile",
                keyColumn: "Id",
                keyValue: "10873fbc-486c-4103-bbad-86c79106a652");

            migrationBuilder.DeleteData(
                table: "TypeFile",
                keyColumn: "Id",
                keyValue: "d8e47b1b-92c2-495f-a506-e60e78af6582");

            migrationBuilder.InsertData(
                table: "TypeFile",
                columns: new[] { "Id", "Name" },
                values: new object[] { "6573f3e4-5fbb-4ef5-8e60-386b3448a724", "Slides" });

            migrationBuilder.InsertData(
                table: "TypeFile",
                columns: new[] { "Id", "Name" },
                values: new object[] { "94837d7a-434e-426f-9b1d-0951237535ff", "Documents" });
        }
    }
}
