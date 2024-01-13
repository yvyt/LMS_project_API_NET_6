﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    public partial class createPermissionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "48f64b2c-f113-4d03-a342-42429ea39f15");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a9465f72-ff0b-4919-92c7-d0d049a91a51");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd455dd4-097a-457e-95a3-21c40374fb01");

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PermissionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "PermissionName" },
                values: new object[,]
                {
                    { "00229f71-24d5-44fd-a9c6-66687c5053be", "System.Collections.Generic.List`1[System.String]:Resource" },
                    { "035c9872-10f7-4083-945f-6e0f6c045a41", "System.Collections.Generic.List`1[System.String]:ExamQuestion" },
                    { "0570a508-184f-47e2-86b5-1164d086eb98", "System.Collections.Generic.List`1[System.String]:Answer" },
                    { "0809ccb0-cddf-4890-891c-11e9be325728", "System.Collections.Generic.List`1[System.String]:Course" },
                    { "082d983e-1120-4299-a709-c2c21f371a87", "System.Collections.Generic.List`1[System.String]:Notification" },
                    { "08d50797-3b4a-49ef-9b29-ccaa73d231cf", "System.Collections.Generic.List`1[System.String]:Resource" },
                    { "0918e7d4-85a9-4721-ae0b-893a1c2d8e7c", "System.Collections.Generic.List`1[System.String]:Question" },
                    { "0e834cf7-f3d4-47e7-b17c-92729f8ba2a3", "System.Collections.Generic.List`1[System.String]:Exam" },
                    { "0f67f5d1-ed6a-4ed8-92a6-16c350780904", "System.Collections.Generic.List`1[System.String]:Resource" },
                    { "0fcd1070-eea7-40ba-8742-f231119653c9", "System.Collections.Generic.List`1[System.String]:Resource" },
                    { "1116bb86-2939-4d90-b11a-7855acdb4c44", "System.Collections.Generic.List`1[System.String]:ExamQuestion" },
                    { "155b6f1d-2bc9-4eb0-9215-86526c5c09c0", "System.Collections.Generic.List`1[System.String]:Question" },
                    { "18c7e656-3f7e-4871-9da2-bd6fed9ac26f", "System.Collections.Generic.List`1[System.String]:Course" },
                    { "19a698a5-d03d-49ce-b006-69e970a05920", "System.Collections.Generic.List`1[System.String]:Course" },
                    { "1e07f3ee-86a3-4bb2-9e22-70fc33aa2c91", "System.Collections.Generic.List`1[System.String]:System" },
                    { "21a367fc-0622-4916-b036-e20d4b714dd4", "System.Collections.Generic.List`1[System.String]:System" },
                    { "25cdb595-5cc1-4702-b623-3caa5de5d9f7", "System.Collections.Generic.List`1[System.String]:User" },
                    { "2633e9f7-8044-40bf-9814-13242af63fc7", "System.Collections.Generic.List`1[System.String]:Course" },
                    { "2654caca-d35b-406b-92b7-021952bd4660", "System.Collections.Generic.List`1[System.String]:Lesson" },
                    { "275d4140-3d47-4736-a633-0fa80028cc84", "System.Collections.Generic.List`1[System.String]:Resource" },
                    { "2b95fe90-a1c2-4697-a9b8-1894692f0cc8", "System.Collections.Generic.List`1[System.String]:PrivateFile" },
                    { "30184b08-56fa-4440-a20a-cb486e738976", "System.Collections.Generic.List`1[System.String]:User" },
                    { "30539959-39bd-4f45-aaea-1c4a55dacc88", "System.Collections.Generic.List`1[System.String]:Lesson" },
                    { "3060afb2-b0a1-4230-9ba4-28193be40afd", "System.Collections.Generic.List`1[System.String]:System" },
                    { "33b66fda-b701-401c-ae04-d6d93f22ad01", "System.Collections.Generic.List`1[System.String]:Exam" },
                    { "354e02e0-bca4-4c0c-85f8-a5c87acd3737", "System.Collections.Generic.List`1[System.String]:Notification" },
                    { "359369bf-6344-4e2c-af0a-49618ffacc14", "System.Collections.Generic.List`1[System.String]:Lesson" },
                    { "363d526c-6658-4de3-afb2-cdee9b920700", "System.Collections.Generic.List`1[System.String]:ExamQuestion" },
                    { "369671c0-4f3f-4132-8609-b3e2e670729c", "System.Collections.Generic.List`1[System.String]:Lesson" },
                    { "3a8c25c8-1489-47cf-b359-863d2397ed79", "System.Collections.Generic.List`1[System.String]:Class" },
                    { "3ac4a2ce-2851-46ba-bcc7-c6d62ab63bed", "System.Collections.Generic.List`1[System.String]:System" },
                    { "3b304703-b561-4a70-804c-b0fcd2494013", "System.Collections.Generic.List`1[System.String]:Answer" },
                    { "3b747afa-021e-4a21-ba95-03a5a4a34788", "System.Collections.Generic.List`1[System.String]:Topic" },
                    { "3d4b90b0-2450-4822-814f-4c2f0686a842", "System.Collections.Generic.List`1[System.String]:Exam" },
                    { "3e03e202-749c-4c45-a042-e6c25d1b8f8f", "System.Collections.Generic.List`1[System.String]:Answer" },
                    { "3e2617d5-f553-42a0-a46c-54ad553ae8ce", "System.Collections.Generic.List`1[System.String]:Question" },
                    { "40e02b31-5053-4b36-b061-6b728c8239b5", "System.Collections.Generic.List`1[System.String]:ExamQuestion" },
                    { "422c4a52-08ca-4b58-b59a-f1865a051804", "System.Collections.Generic.List`1[System.String]:Class" },
                    { "4499da1e-e07a-40d6-911f-35e88c28ccb6", "System.Collections.Generic.List`1[System.String]:User" },
                    { "5261e16b-702f-419b-9a6a-c1ecbac57a74", "System.Collections.Generic.List`1[System.String]:System" },
                    { "532d4d21-a332-41d9-be73-09744ea44059", "System.Collections.Generic.List`1[System.String]:Resource" },
                    { "557523de-e619-4ccc-b55b-86d7a9bb5772", "System.Collections.Generic.List`1[System.String]:Lesson" }
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "PermissionName" },
                values: new object[,]
                {
                    { "5956f8d8-bbf1-4390-b908-9b37f5fe7780", "System.Collections.Generic.List`1[System.String]:Answer" },
                    { "596bc7fd-2641-43c7-ae43-4bca11c8f6bf", "System.Collections.Generic.List`1[System.String]:Question" },
                    { "5bc8186e-c302-4a68-a900-5c9498d902ba", "System.Collections.Generic.List`1[System.String]:Exam" },
                    { "5cf5f00a-1706-4826-b13a-853d38c5ba7d", "System.Collections.Generic.List`1[System.String]:Course" },
                    { "5ea15670-0a98-4bbe-a76a-192334769542", "System.Collections.Generic.List`1[System.String]:Course" },
                    { "637d11a3-abfe-4dae-9427-affe78068f56", "System.Collections.Generic.List`1[System.String]:System" },
                    { "6515e811-eb0d-48c2-a33e-aa0da3a55438", "System.Collections.Generic.List`1[System.String]:PrivateFile" },
                    { "665893eb-b4b8-4ada-aa98-a40f39b70a7a", "System.Collections.Generic.List`1[System.String]:System" },
                    { "6a534441-1ea8-4d18-a6f7-2f7c734b92fd", "System.Collections.Generic.List`1[System.String]:PrivateFile" },
                    { "6bbfa895-0154-4710-8107-8ea6679bd479", "System.Collections.Generic.List`1[System.String]:Course" },
                    { "6bcfac16-1a7b-4933-a2fe-b09578989f06", "System.Collections.Generic.List`1[System.String]:Topic" },
                    { "70a5cf5b-381a-4ef2-a96f-96e11ac1d307", "System.Collections.Generic.List`1[System.String]:Topic" },
                    { "71cadb9c-66a1-450a-bcc9-08e066b1ce40", "System.Collections.Generic.List`1[System.String]:Question" },
                    { "71ee5bfe-a219-47ac-b748-5894b9762039", "System.Collections.Generic.List`1[System.String]:Answer" },
                    { "73fa44c0-6d0f-41aa-8106-6e0c28ee7eb4", "System.Collections.Generic.List`1[System.String]:User" },
                    { "75f5a040-9774-4dd6-a2f3-273b057d99a5", "System.Collections.Generic.List`1[System.String]:PrivateFile" },
                    { "76b22d62-90d0-4160-92ca-5c3bb5e6f578", "System.Collections.Generic.List`1[System.String]:ExamQuestion" },
                    { "78173d7c-b05a-431f-b5da-6bfbc8d7f1f8", "System.Collections.Generic.List`1[System.String]:Class" },
                    { "80daf8ca-ce38-42c6-9ca1-b224ff3320e7", "System.Collections.Generic.List`1[System.String]:Topic" },
                    { "82b05ec1-197d-4ec8-8875-f34571504130", "System.Collections.Generic.List`1[System.String]:Exam" },
                    { "84170d2e-7f9e-4338-a61d-4794222fa407", "System.Collections.Generic.List`1[System.String]:Question" },
                    { "86d330b0-e05a-49a8-84ff-76093e13d5e9", "System.Collections.Generic.List`1[System.String]:Class" },
                    { "87fc9f72-428f-4e7e-a492-63e790b5384f", "System.Collections.Generic.List`1[System.String]:Notification" },
                    { "89f613b3-13c5-4a8b-b716-9d41def01769", "System.Collections.Generic.List`1[System.String]:System" },
                    { "8ac87f46-aeac-48aa-9615-0baec4ee5633", "System.Collections.Generic.List`1[System.String]:Question" },
                    { "8ba2c5d0-e609-4a0e-bc43-987faf2d74b5", "System.Collections.Generic.List`1[System.String]:Exam" },
                    { "8c9b415c-4fbf-482a-b464-6a5f588cd81d", "System.Collections.Generic.List`1[System.String]:Question" },
                    { "8e64bb6b-4d6a-40c0-9c11-aad905cf1221", "System.Collections.Generic.List`1[System.String]:Exam" },
                    { "92c881f9-1e00-45b2-9f37-194abc451870", "System.Collections.Generic.List`1[System.String]:Lesson" },
                    { "9cb25205-77f1-4c6b-ba2f-3b2e93fc7fd8", "System.Collections.Generic.List`1[System.String]:Lesson" },
                    { "a03be68e-f836-474f-8acb-49d2eb2ddde3", "System.Collections.Generic.List`1[System.String]:PrivateFile" },
                    { "a0bf215e-e513-4617-8394-2507b66cdc73", "System.Collections.Generic.List`1[System.String]:Lesson" },
                    { "a14048ec-1873-4ad2-b785-0fb15c397443", "System.Collections.Generic.List`1[System.String]:PrivateFile" },
                    { "a1d321f9-eb11-4547-b430-5bfc7419615b", "System.Collections.Generic.List`1[System.String]:Resource" },
                    { "a38f0af9-59da-4477-a6b0-1f4f90d0cebf", "System.Collections.Generic.List`1[System.String]:User" },
                    { "a6dfb3f0-67bd-4fc5-8352-0a3b2d40e246", "System.Collections.Generic.List`1[System.String]:User" },
                    { "a7b06881-77c6-4e32-b64d-31c2fe9d283a", "System.Collections.Generic.List`1[System.String]:ExamQuestion" },
                    { "a82a4c1a-427a-4b4f-82cc-545cd0de542e", "System.Collections.Generic.List`1[System.String]:Class" },
                    { "ae871dd4-dc97-4be9-8e58-206cc175f540", "System.Collections.Generic.List`1[System.String]:User" },
                    { "aff3ad8d-5cc1-4c2d-b63e-6e0467050440", "System.Collections.Generic.List`1[System.String]:Answer" },
                    { "b2122927-8b75-41e4-878f-88155c096385", "System.Collections.Generic.List`1[System.String]:Class" },
                    { "b2b2a6e9-e25a-4dff-bfdd-8e95529facb1", "System.Collections.Generic.List`1[System.String]:Answer" }
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "PermissionName" },
                values: new object[,]
                {
                    { "b7da5921-f207-4e21-b7f2-9bdebc4abc12", "System.Collections.Generic.List`1[System.String]:Notification" },
                    { "b7e05d3a-a8df-4122-909d-3548dee04800", "System.Collections.Generic.List`1[System.String]:ExamQuestion" },
                    { "bb2a19a8-69f5-4aa2-b688-fa49c5d63e4d", "System.Collections.Generic.List`1[System.String]:ExamQuestion" },
                    { "c0bf4128-d380-4084-a5a1-480c9373fafb", "System.Collections.Generic.List`1[System.String]:Lesson" },
                    { "c1cad3dd-268c-4ba6-ab23-22607b38be55", "System.Collections.Generic.List`1[System.String]:Class" },
                    { "c2f41d6c-2aa3-417a-bc9d-4bbd6f7c1f61", "System.Collections.Generic.List`1[System.String]:Resource" },
                    { "c2ffa6ea-a377-4f3c-804e-cf2cf53f9521", "System.Collections.Generic.List`1[System.String]:Answer" },
                    { "c3ebc2f9-2da4-4281-af1c-aa8271e2d305", "System.Collections.Generic.List`1[System.String]:Course" },
                    { "cdc2f98a-5275-4d40-ab8c-6017ac565e6b", "System.Collections.Generic.List`1[System.String]:User" },
                    { "cecabbac-b727-4a19-aa18-165e9c7e66e5", "System.Collections.Generic.List`1[System.String]:Exam" },
                    { "d1590a32-2eed-4dd6-abde-74e19e19ecc9", "System.Collections.Generic.List`1[System.String]:Question" },
                    { "d7d46998-b2f7-4823-9a1f-4098115debd2", "System.Collections.Generic.List`1[System.String]:Notification" },
                    { "d8384b84-b8d6-46c2-8d5a-f41ae58ec837", "System.Collections.Generic.List`1[System.String]:Answer" },
                    { "dad150b1-2931-456e-b717-c93efba9068e", "System.Collections.Generic.List`1[System.String]:Topic" },
                    { "de2e823d-fe8d-4dad-815e-0db3e6c7064b", "System.Collections.Generic.List`1[System.String]:Notification" },
                    { "df31779f-2431-4fe2-b579-2ba35d1718bb", "System.Collections.Generic.List`1[System.String]:User" },
                    { "e13ee633-6cf6-4cbb-9b0b-72e0a329c2e5", "System.Collections.Generic.List`1[System.String]:Topic" },
                    { "e2f8231c-597f-41d5-8d02-1800b4d9c92b", "System.Collections.Generic.List`1[System.String]:Notification" },
                    { "e35cebd3-f082-4e33-9122-d37c6355a64b", "System.Collections.Generic.List`1[System.String]:Topic" },
                    { "e3db3256-b9c0-4830-8fc3-783ae70a6323", "System.Collections.Generic.List`1[System.String]:Class" },
                    { "e48f1879-91a1-4669-a7aa-ce60460baf60", "System.Collections.Generic.List`1[System.String]:Resource" },
                    { "e6c5760a-e4d4-40fa-b11a-8dd457df7036", "System.Collections.Generic.List`1[System.String]:Topic" },
                    { "efdcb2ef-3928-4384-b500-8b711afcf2e7", "System.Collections.Generic.List`1[System.String]:Notification" },
                    { "f0e95ab1-a53b-4ba3-b882-2f1ac0be4a65", "System.Collections.Generic.List`1[System.String]:PrivateFile" },
                    { "f4747f38-130c-4d72-8bda-a82d235718b7", "System.Collections.Generic.List`1[System.String]:Exam" },
                    { "f66ae1cd-839b-4351-8c5f-c06b6718b359", "System.Collections.Generic.List`1[System.String]:PrivateFile" },
                    { "f6fe08ce-3666-4dc9-bee8-fc5ca9b44c9a", "System.Collections.Generic.List`1[System.String]:System" },
                    { "f7f52872-0752-49fb-9f85-a658c178e21b", "System.Collections.Generic.List`1[System.String]:Class" },
                    { "f8123484-48aa-44e4-99ba-3ebcf23f3255", "System.Collections.Generic.List`1[System.String]:PrivateFile" },
                    { "f9d43669-adea-424f-8135-7ab3c2397db0", "System.Collections.Generic.List`1[System.String]:Topic" },
                    { "f9e34253-d279-4223-bc5a-463b855993b4", "System.Collections.Generic.List`1[System.String]:ExamQuestion" },
                    { "fdbfee27-a960-411c-aac2-519266a8a5f2", "System.Collections.Generic.List`1[System.String]:Course" },
                    { "fddd52d4-b3ea-4b25-b18e-ddd53206ee8f", "System.Collections.Generic.List`1[System.String]:Notification" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "48f64b2c-f113-4d03-a342-42429ea39f15", "1", "Leadership", "Leadership" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a9465f72-ff0b-4919-92c7-d0d049a91a51", "2", "Teacher", "Teacher" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fd455dd4-097a-457e-95a3-21c40374fb01", "3", "Student", "Student" });
        }
    }
}
