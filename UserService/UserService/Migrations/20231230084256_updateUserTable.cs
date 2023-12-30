using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    public partial class updateUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "Gender");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Users",
                newName: "Fullname");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "21c69c2b-f8d2-4153-a2f2-3d5f7abfacb6", "2", "Teacher", "Teacher" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "66e1b405-8388-4296-9c72-1b499f7d23cd", "3", "Student", "Student" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e0bed29b-0f5c-44f6-8788-ea15aff6d8a9", "1", "Leadership", "Leadership" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21c69c2b-f8d2-4153-a2f2-3d5f7abfacb6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66e1b405-8388-4296-9c72-1b499f7d23cd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e0bed29b-0f5c-44f6-8788-ea15aff6d8a9");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Users",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Fullname",
                table: "Users",
                newName: "FirstName");

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
    }
}
