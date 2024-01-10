using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrivateFileService.Migrations
{
    public partial class updatePFTB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Private File",
                table: "Private File");

            migrationBuilder.RenameTable(
                name: "Private File",
                newName: "PrivateFile");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrivateFile",
                table: "PrivateFile",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PrivateFile",
                table: "PrivateFile");

            migrationBuilder.RenameTable(
                name: "PrivateFile",
                newName: "Private File");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Private File",
                table: "Private File",
                column: "Id");
        }
    }
}
