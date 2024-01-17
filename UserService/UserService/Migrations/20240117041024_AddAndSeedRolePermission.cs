using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Migrations
{
    public partial class AddAndSeedRolePermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PermissionId = table.Column<string>(type: "nvarchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { "0c016ea4-4c99-42e8-a947-964afb693a6d", "a184a406-b527-4c2f-a123-f0a99d1737cd", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "0d4a6a51-9246-45c7-b9b0-9758173551ef", "e1fb5d6a-f234-4b7b-8f4c-9c02a80e7396", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "15fa835d-0ef5-47e8-aeae-935db0db40e5", "6d6ad73b-9a19-449f-a809-57794239e882", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "1d70ba56-f01c-4928-abee-03fa291c05a7", "2e8e852d-9e69-4f43-a9bf-deaead6485d1", "48f64b2c-f113-4d03-a342-42429ea39f15" },
                    { "1e0d46bc-ec00-46dc-9849-d51a6071200c", "a92d095a-50bf-4ff6-b774-bdd81df6313d", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "2da17aab-cd56-4bec-8681-75f8e55068a4", "75530804-26db-41f3-9d1b-e58ed9e8fee4", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "2dfd8c55-4ca9-4231-98a3-05f513a8a2f2", "a3ebb2ea-fc78-4d7a-bc34-b620600a59c4", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "352e5714-80d6-4384-bb30-bc398d2cc2d9", "1b183555-3067-4485-b563-65b34b29475f", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "36a34f90-ca02-418c-9a5b-b17bc4b2fcbf", "940748d7-c97d-4157-8104-fde614869fd9", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "3bc87d2d-e445-498c-98f3-bcc3a0a286b5", "80e94b56-160d-4450-a1d1-0f17f6fe1456", "fd455dd4-097a-457e-95a3-21c40374fb01" },
                    { "4ca8a39d-7383-4c5d-8878-d8b92f77211d", "de75d96d-fdb4-439b-8cf8-43d587974865", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "517422df-0c7d-45fb-9895-ce2d32052862", "28147748-0067-48d7-95d7-4aca515a58c7", "fd455dd4-097a-457e-95a3-21c40374fb01" },
                    { "5572d744-c870-47a0-b04b-858b86454cf1", "88dbd1f9-d943-41ed-a607-866def46d6a6", "48f64b2c-f113-4d03-a342-42429ea39f15" },
                    { "5666bcb2-d5a3-4600-b514-e84ac5abf416", "c57dadf9-5da7-4166-b0d0-ef64667f2572", "48f64b2c-f113-4d03-a342-42429ea39f15" },
                    { "5a673e3f-73cc-4dc6-ba08-893b5ee54741", "7b15cfff-a2ed-4126-8860-e3b99df91dd9", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "5a9fbf03-421d-46a6-9a90-41d953cf5e2c", "0db91594-128b-4aa5-bf04-77aa417143f4", "48f64b2c-f113-4d03-a342-42429ea39f15" },
                    { "5b3a9623-79e4-4544-af7c-6268d6e6f7bd", "28147748-0067-48d7-95d7-4aca515a58c7", "48f64b2c-f113-4d03-a342-42429ea39f15" },
                    { "6024a48c-cab5-4b6b-8b6c-f151de5127d1", "1e77c1c6-478e-4111-8d76-c03c835c1151", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "60464c39-3b39-4777-bc3d-d94f9ab688bc", "9aafbadc-9401-4ebb-95b1-1adf64ecef00", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "67074e00-ed48-451a-bdf9-2c2ded591450", "c57dadf9-5da7-4166-b0d0-ef64667f2572", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "67947f2f-6fea-4003-8921-eb8d53df97b3", "7b15cfff-a2ed-4126-8860-e3b99df91dd9", "48f64b2c-f113-4d03-a342-42429ea39f15" },
                    { "6a3261d2-dad6-4abe-8998-17859ca00c65", "90065181-d7c7-4258-8c5b-237871454c5a", "48f64b2c-f113-4d03-a342-42429ea39f15" },
                    { "6f6a0649-9f15-4135-9f45-a838ddcf6ad8", "ce808469-ac15-40cc-a317-cbc2d7b1c7d2", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "71429366-7110-41ba-94bb-0e5573f57336", "ee255870-c765-4451-a019-a408d9e6ee27", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "74edb382-15d0-4fb2-86b8-d1dbde619c80", "ead1e55a-8213-442d-b8a4-a49fbeaad9a2", "48f64b2c-f113-4d03-a342-42429ea39f15" },
                    { "81bd9ecd-5709-46ae-b409-76e3b268c556", "86e1b9f0-acf0-43f2-9f52-9fb253d19c6d", "fd455dd4-097a-457e-95a3-21c40374fb01" },
                    { "8911ae15-664b-4762-a253-5fdcfeb03a0d", "34ffbd7d-0ec7-4a56-8d83-179568ba07c5", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "8b5e9da9-7c0b-4ead-bd10-432356449e7a", "28147748-0067-48d7-95d7-4aca515a58c7", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "8e9883c0-74db-44d1-bab6-0947f1d221a9", "9c840343-8004-4d7f-b670-9a63c0237bce", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "95cf3fa0-65b1-4969-8d28-b8a6befe73da", "80e94b56-160d-4450-a1d1-0f17f6fe1456", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "998f5787-4548-42a4-b716-c77e7dfc1bc3", "c0030dfa-5bae-4dfa-8e7c-b367ed42a3db", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "9a587e38-e203-4fbd-b2bf-e937f9506a06", "95aeb353-24d8-4a8d-8b43-ae4ef3860a03", "48f64b2c-f113-4d03-a342-42429ea39f15" },
                    { "9ca2f642-b042-4f46-b610-ca47794fae25", "d8f4aaed-cd0b-46e8-8b9e-4ffefe9b2192", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "9df6c415-ad42-46d0-9fb5-75b84a1b3ed9", "6a886d29-1f69-459e-80ca-5b37ef7f2fe6", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "a1b14a91-2e4f-4b50-8faf-edeeb04edec6", "86e1b9f0-acf0-43f2-9f52-9fb253d19c6d", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "a5d514d9-a22d-4b5f-a582-ce41262b4366", "b0c9c2e8-aa9e-4a02-a5ac-732152e72261", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "aa015b02-7bcf-4b27-9cf8-6d80056a072a", "c5feb7be-eeea-46e3-b44c-588d37eff052", "48f64b2c-f113-4d03-a342-42429ea39f15" },
                    { "ab6ae13a-24f0-47e4-9cec-c33d922e6b76", "a184a406-b527-4c2f-a123-f0a99d1737cd", "48f64b2c-f113-4d03-a342-42429ea39f15" },
                    { "ac89235c-d330-4a48-8d6c-1ad088739063", "46f87f72-afaa-4df4-85d9-87dcd4c9b076", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "ad60ed98-9f3f-4809-94ae-8f85602b3b16", "ee255870-c765-4451-a019-a408d9e6ee27", "fd455dd4-097a-457e-95a3-21c40374fb01" },
                    { "ae34f41c-398f-4b13-9496-83a8ef816546", "e1fb5d6a-f234-4b7b-8f4c-9c02a80e7396", "48f64b2c-f113-4d03-a342-42429ea39f15" },
                    { "b4da1aff-4401-4a6d-b1de-78c0bbbd662b", "3c858bdd-3ef3-4ba6-b4f5-12b45f14ea02", "a9465f72-ff0b-4919-92c7-d0d049a91a51" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissions",
                columns: new[] { "Id", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { "b50419e4-3a4e-49b5-a749-8eefefa5aac3", "80e94b56-160d-4450-a1d1-0f17f6fe1456", "48f64b2c-f113-4d03-a342-42429ea39f15" },
                    { "b8028e6b-9356-4fc1-8554-b59d486d14bf", "2a6f25ea-83aa-4c94-a1eb-0434671d5230", "48f64b2c-f113-4d03-a342-42429ea39f15" },
                    { "b9521dd8-53a8-4a32-bf2c-d861a1b779fd", "0db91594-128b-4aa5-bf04-77aa417143f4", "fd455dd4-097a-457e-95a3-21c40374fb01" },
                    { "c2c44050-e317-4157-afaf-69d1b6d7fc32", "a995e571-59cb-4f70-8aa9-2f637bb67e13", "48f64b2c-f113-4d03-a342-42429ea39f15" },
                    { "ca5df13e-762b-4db3-aeb2-22b2ffcc11bb", "e368179b-9797-4018-8867-72723cecc92e", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "cd5bdff6-c2ed-40e6-8895-27faef4d6182", "1793ca7e-61e6-4961-bdfb-766a3555fd20", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "ce63f979-24e1-4c47-98c6-647c89c2a1a0", "a9ea9725-b0d3-4d31-b6c3-1ac263ac463a", "48f64b2c-f113-4d03-a342-42429ea39f15" },
                    { "d4222f06-d189-4876-875b-5a1b77e6fdbd", "c986d59c-57fb-4669-86e0-7b51fd57b3ae", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "d5b87180-1a10-407f-8f41-41b83322acf0", "75530804-26db-41f3-9d1b-e58ed9e8fee4", "48f64b2c-f113-4d03-a342-42429ea39f15" },
                    { "d97040c8-d896-4570-b3f2-37c77f3bda1b", "3c858bdd-3ef3-4ba6-b4f5-12b45f14ea02", "fd455dd4-097a-457e-95a3-21c40374fb01" },
                    { "dbc0af5f-fb02-40b4-8ed6-a27f1b74f13b", "86e1b9f0-acf0-43f2-9f52-9fb253d19c6d", "48f64b2c-f113-4d03-a342-42429ea39f15" },
                    { "e2d68255-f58b-4c1a-bf1c-72736c2173b3", "f702360a-f73b-46e7-adc0-ccaaf9c75db8", "48f64b2c-f113-4d03-a342-42429ea39f15" },
                    { "e7eb1479-d609-4db0-90cd-661dcbcc6296", "0bda25ae-f034-43f3-b6fb-2f06d32870a4", "48f64b2c-f113-4d03-a342-42429ea39f15" },
                    { "ed5d0a3a-0dea-4e09-8e79-976124a54ca9", "c986d59c-57fb-4669-86e0-7b51fd57b3ae", "48f64b2c-f113-4d03-a342-42429ea39f15" },
                    { "ee90a2a6-3e9b-4223-ae10-b34520bc9af4", "0db91594-128b-4aa5-bf04-77aa417143f4", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "f0884738-420d-46b0-a2a4-9faf9e987a55", "27a0b056-02b8-4cb7-a81c-26757569d9cf", "48f64b2c-f113-4d03-a342-42429ea39f15" },
                    { "f11d6812-71ab-4f25-9e4c-3eb603250fb3", "13adaeba-2ce2-4f7d-a16d-9af743399612", "48f64b2c-f113-4d03-a342-42429ea39f15" },
                    { "f80a8fe9-20ca-4b6a-9435-3c3647eb9f69", "c5196f8c-95a8-4512-b5f2-c576958ff5cd", "a9465f72-ff0b-4919-92c7-d0d049a91a51" },
                    { "fabd91ed-ad71-43be-8fba-b235e563edd6", "ee255870-c765-4451-a019-a408d9e6ee27", "48f64b2c-f113-4d03-a342-42429ea39f15" },
                    { "fe88792b-5498-44a8-98a6-35afc9209c9f", "c0030dfa-5bae-4dfa-8e7c-b367ed42a3db", "48f64b2c-f113-4d03-a342-42429ea39f15" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId",
                table: "RolePermissions",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermissions");

            

            
        }
    }
}
