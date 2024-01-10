using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrivateFileService.Migrations
{
    public partial class createPrivateFileTb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentId",
                table: "Private File",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "Private File");
        }
    }
}
