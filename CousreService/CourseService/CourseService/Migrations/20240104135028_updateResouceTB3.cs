using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseService.Migrations
{
    public partial class updateResouceTB3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentId",
                table: "Resource",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "createBy",
                table: "Resource",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "updateBy",
                table: "Resource",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Resource_DocumentId",
                table: "Resource",
                column: "DocumentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Resource_Documents_DocumentId",
                table: "Resource",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "DocumentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resource_Documents_DocumentId",
                table: "Resource");

            migrationBuilder.DropIndex(
                name: "IX_Resource_DocumentId",
                table: "Resource");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "Resource");

            migrationBuilder.DropColumn(
                name: "createBy",
                table: "Resource");

            migrationBuilder.DropColumn(
                name: "updateBy",
                table: "Resource");
        }
    }
}
