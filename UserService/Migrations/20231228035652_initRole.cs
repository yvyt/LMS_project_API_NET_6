using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    public partial class initRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2d183a8c-0727-412b-8ab3-840fa6b0f361", "3a64b0c0-e726-49d8-9f04-cc08b2350dc1", "Leadership", "Leadership" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "73e0d362-3fc0-4c2a-9c6d-7202531d61f8", "81dce39f-9d55-46a6-ba78-099f90908fbd", "Student", "Student" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f584988d-57d3-4b94-9b69-4dcedb8760a9", "f9439411-89ee-4c58-a943-2476f881bd99", "Teacher", "Teacher" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2d183a8c-0727-412b-8ab3-840fa6b0f361");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73e0d362-3fc0-4c2a-9c6d-7202531d61f8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f584988d-57d3-4b94-9b69-4dcedb8760a9");
        }
    }
}
