using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseService.Migrations
{
    public partial class updateTB1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FileContent",
                table: "Documents");

            migrationBuilder.InsertData(
                table: "TypeFile",
                columns: new[] { "Id", "Name" },
                values: new object[] { "a9ada079-a177-4f9b-b30b-cfc599e76f44", "Documents" });

            migrationBuilder.InsertData(
                table: "TypeFile",
                columns: new[] { "Id", "Name" },
                values: new object[] { "d68cbea0-20a6-4295-a6d5-a96cca86dc22", "Slides" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TypeFile",
                keyColumn: "Id",
                keyValue: "a9ada079-a177-4f9b-b30b-cfc599e76f44");

            migrationBuilder.DeleteData(
                table: "TypeFile",
                keyColumn: "Id",
                keyValue: "d68cbea0-20a6-4295-a6d5-a96cca86dc22");

            migrationBuilder.AddColumn<byte[]>(
                name: "FileContent",
                table: "Documents",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.InsertData(
                table: "TypeFile",
                columns: new[] { "Id", "Name" },
                values: new object[] { "c3769f7e-6c37-417c-a734-62411ab32cc5", "Documents" });

            migrationBuilder.InsertData(
                table: "TypeFile",
                columns: new[] { "Id", "Name" },
                values: new object[] { "dc84389b-980c-4399-bdd3-32e2b1bd1cb3", "Slides" });
        }
    }
}
