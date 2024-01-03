using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseService.Migrations
{
    public partial class fix1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TypeId",
                table: "Lesson",
                type: "nvarchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "TypeFile",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeFile", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TypeFile",
                columns: new[] { "Id", "Name" },
                values: new object[] { "5053ab40-feab-48ce-99fc-752f6b87e812", "Documents" });

            migrationBuilder.InsertData(
                table: "TypeFile",
                columns: new[] { "Id", "Name" },
                values: new object[] { "91280a33-3963-459d-838c-bf427e3d16e7", "Slides" });

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_TypeId",
                table: "Lesson",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_TypeFile_TypeId",
                table: "Lesson",
                column: "TypeId",
                principalTable: "TypeFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_TypeFile_TypeId",
                table: "Lesson");

            migrationBuilder.DropTable(
                name: "TypeFile");

            migrationBuilder.DropIndex(
                name: "IX_Lesson_TypeId",
                table: "Lesson");

            migrationBuilder.AlterColumn<string>(
                name: "TypeId",
                table: "Lesson",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)");
        }
    }
}
