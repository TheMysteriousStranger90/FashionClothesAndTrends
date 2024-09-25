using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FashionClothesAndTrends.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBasketItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClothingItemPhotos",
                keyColumn: "Id",
                keyValue: new Guid("05742d28-7ebc-469f-91a0-7eaefaa150c4"));

            migrationBuilder.DeleteData(
                table: "ClothingItemPhotos",
                keyColumn: "Id",
                keyValue: new Guid("08219b8e-87c5-44d0-8806-3ff7e82fe653"));

            migrationBuilder.DeleteData(
                table: "ClothingItemPhotos",
                keyColumn: "Id",
                keyValue: new Guid("093eaf5c-465d-46ea-b48a-8361e1262924"));

            migrationBuilder.DeleteData(
                table: "ClothingItemPhotos",
                keyColumn: "Id",
                keyValue: new Guid("42405f02-3033-4d7f-b730-c66b6c4aab49"));

            migrationBuilder.DeleteData(
                table: "ClothingItemPhotos",
                keyColumn: "Id",
                keyValue: new Guid("44d6db2d-e1a1-4eac-9ab1-dc79311c805b"));

            migrationBuilder.DeleteData(
                table: "ClothingItemPhotos",
                keyColumn: "Id",
                keyValue: new Guid("4914b710-d3da-4dc3-b93c-b4ef586a7323"));

            migrationBuilder.DeleteData(
                table: "DeliveryMethods",
                keyColumn: "Id",
                keyValue: new Guid("0b4327f1-2f2a-4eec-b5ef-3476206ea28c"));

            migrationBuilder.DeleteData(
                table: "DeliveryMethods",
                keyColumn: "Id",
                keyValue: new Guid("203ae64f-7c6b-4c03-9deb-2eca53218352"));

            migrationBuilder.DeleteData(
                table: "DeliveryMethods",
                keyColumn: "Id",
                keyValue: new Guid("a0854e88-0e39-43ed-bd6b-e3889fc411ed"));

            migrationBuilder.DeleteData(
                table: "DeliveryMethods",
                keyColumn: "Id",
                keyValue: new Guid("df9b33a6-902d-4ada-bb16-ee28af859fc8"));

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "Id",
                keyValue: new Guid("12f0de27-9d46-4252-8f42-40031bc617b5"));

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "Id",
                keyValue: new Guid("52cc6549-df79-4e85-9df9-85afb4970672"));

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "Id",
                keyValue: new Guid("5708d50e-4aeb-461b-adbb-9f9b87acdcfc"));

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "Id",
                keyValue: new Guid("9709762d-5dd9-44fa-8ecb-3d2150052202"));

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "Id",
                keyValue: new Guid("b705424c-d9f1-4553-ae3c-f8d7ecfc4575"));

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "Id",
                keyValue: new Guid("f748241c-5378-4020-96d2-0f181dfb8cf3"));

            migrationBuilder.InsertData(
                table: "ClothingItems",
                columns: new[] { "Id", "Category", "ClothingBrandId", "CreatedAt", "Description", "Discount", "Gender", "IsInStock", "LastUpdatedAt", "Name", "Price", "Size" },
                values: new object[,]
                {
                    { new Guid("0842ed2a-4aa5-48be-b152-838e48fbcf7d"), 0, new Guid("5d24a48b-6c72-4e2a-9ef2-64d0f657bfc6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A regular-fit, long-sleeved fluid shirt featuring an all-over tonal Barocco devore motif.", null, 0, true, null, "Versace Barocco Devore Shirt", 1200.00m, 3 },
                    { new Guid("442793b6-8446-416d-bfe8-cc671b704672"), 3, new Guid("c981db82-b2f1-48c3-9864-efc6c56a5b0e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The GG Marmont belt continues to enrich each new collection with its streamlined design. Inspired by an archival design from the 1970s, the line's monogram Double G hardware is presented in a shiny silver tone atop this black leather variation.", null, 0, true, null, "Gucci GG MARMONT THIN BELT", 450.00m, 3 },
                    { new Guid("7fa74314-332e-4d1d-80e5-d8471ca65035"), 2, new Guid("3d6f79a2-c462-4c28-ae5f-0ec93b7f4e01"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Classic Chanel tweed jacket in black.", null, 1, true, null, "Chanel JACKET", 5000.00m, 2 },
                    { new Guid("89618184-cb3d-4919-988c-e6fdaa57d479"), 0, new Guid("a2c5c305-f2c2-45e7-8f7d-c489bb7f7e8a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "An essential item of the brand, the Prada jersey T-shirt embodies the luxury of simplicity that becomes an attitude and search to reinvent the bases and propose new meanings. The design is accented with the brand's emblematic lettering logo presented here in a silicone version.", null, 0, true, null, "Prada Cotton T-shirt", 950.00m, 4 },
                    { new Guid("b84dc68b-f4d3-404f-b157-346db55e237a"), 4, new Guid("e96c60b6-09df-4e1a-9d6c-617bdd48eaf5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "New for Winter 2024, the Dior Icon heeled ankle boot transcends House codes of couture refinement. The black suede calfskin upper is elevated by elastic bands on the sides and the gold-finish metal CD signature on the back. The 8-cm (3) Graphic Cannage cylindrical heel in gold-finish metal offers a modern 3D version of the House's iconic motif. Featuring a square toe, the sophisticated and comfortable ankle boot will add the finishing touch to any of the season's looks.", null, 1, true, null, "Dior Dior Icon Heeled Ankle Boot", 2900.00m, 2 },
                    { new Guid("d82db94f-13d4-4bbc-910f-05f6ea7df575"), 3, new Guid("b5d6b8f8-dad4-4f2f-8c52-2911d856b3ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The LV Gram Square Cat Eye sunglasses feature a distinctive signature from Louis Vuitton’s jewelry and belts collections. The slim acetate and metal temples are adorned with the LV Initials and two Monogram Flowers finely crafted in gold-tone metal. Monogram Flower details on the lenses and end tips add an extra House touch. These stylish, feminine sunglasses are ideal for accenting a summer outfit.", null, 1, true, null, "LV Gram Square Cat Eye Sunglasses", 3200.00m, 2 }
                });

            migrationBuilder.InsertData(
                table: "DeliveryMethods",
                columns: new[] { "Id", "CreatedAt", "DeliveryTime", "Description", "LastUpdatedAt", "Price", "ShortName" },
                values: new object[,]
                {
                    { new Guid("82f1a73e-b0a1-4c7f-b5ec-c0f47f67bbb3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1-2 Weeks", "Free! You get what you pay for", null, 0m, "FREE" },
                    { new Guid("b846d054-2a09-4dcd-af57-f6d671e8c4b8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2-5 Days", "Get it within 5 days", null, 5m, "UPS2" },
                    { new Guid("bb4b5540-55db-4f34-a047-cf15c0d7a3d6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1-2 Days", "Fastest delivery time", null, 10m, "UPS1" },
                    { new Guid("e41dd91a-9cf2-48e5-8e02-8c3886274f11"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "5-10 Days", "Slower but cheap", null, 2m, "UPS3" }
                });

            migrationBuilder.InsertData(
                table: "ClothingItemPhotos",
                columns: new[] { "Id", "ClothingItemId", "CreatedAt", "IsMain", "LastUpdatedAt", "PublicId", "Url" },
                values: new object[,]
                {
                    { new Guid("2329ab6d-1ef7-4392-9fee-24c09055a3ba"), new Guid("442793b6-8446-416d-bfe8-cc671b704672"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId3", "https://media.gucci.com/style/DarkGray_Center_0_0_2400x2400/1714409103/414516_0AABG_1000_001_100_0000_Light-GG-Marmont-thin-belt.jpg" },
                    { new Guid("23ab4b05-ba28-455b-b11d-96e03dd79fea"), new Guid("0842ed2a-4aa5-48be-b152-838e48fbcf7d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId1", "https://www.versace.com/dw/image/v2/BGWN_PRD/on/demandware.static/-/Sites-ver-master-catalog/default/dwf9d0b70e/original/90_1012141-1A11358_1B000_10_BaroccoDevorShirt-Shirts-Versace-online-store_0_2.jpg?sw=1200&q=85&strip=true" },
                    { new Guid("39bda1cf-b962-46bd-93fb-5ffd0fcdf2df"), new Guid("7fa74314-332e-4d1d-80e5-d8471ca65035"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId6", "https://www.chanel.com/images//t_zoomportee/f_auto//jacket-black-lambskin-lambskin-packshot-alternative-p78125c7009094305-9548808159262.jpg" },
                    { new Guid("539ff052-7e25-4275-a860-590132cef021"), new Guid("89618184-cb3d-4919-988c-e6fdaa57d479"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId2", "https://www.prada.com/content/dam/pradabkg_products/U/UJN/UJN815/1052F0002/UJN815_1052_F0002_S_221_SLF.jpg/_jcr_content/renditions/cq5dam.web.hebebed.1000.1000.jpg" },
                    { new Guid("5b384a6f-3fec-4c6c-850a-8206f27a346c"), new Guid("d82db94f-13d4-4bbc-910f-05f6ea7df575"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId5", "https://eu.louisvuitton.com/images/is/image/lv/1/PP_VP_L/louis-vuitton-lv-gram-square-cat-eye-sunglasses-s00-sunglasses--Z2459U_PM2_Front%20view.png?wid=1090&hei=1090" },
                    { new Guid("8364994e-1d6d-4a4e-8a11-abef3f9330f9"), new Guid("b84dc68b-f4d3-404f-b157-346db55e237a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId4", "https://www.dior.com/couture/ecommerce/media/catalog/product/Q/K/1721839565_KCT067VVV_S900_E03_GHC.jpg?imwidth=720" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClothingItemPhotos",
                keyColumn: "Id",
                keyValue: new Guid("2329ab6d-1ef7-4392-9fee-24c09055a3ba"));

            migrationBuilder.DeleteData(
                table: "ClothingItemPhotos",
                keyColumn: "Id",
                keyValue: new Guid("23ab4b05-ba28-455b-b11d-96e03dd79fea"));

            migrationBuilder.DeleteData(
                table: "ClothingItemPhotos",
                keyColumn: "Id",
                keyValue: new Guid("39bda1cf-b962-46bd-93fb-5ffd0fcdf2df"));

            migrationBuilder.DeleteData(
                table: "ClothingItemPhotos",
                keyColumn: "Id",
                keyValue: new Guid("539ff052-7e25-4275-a860-590132cef021"));

            migrationBuilder.DeleteData(
                table: "ClothingItemPhotos",
                keyColumn: "Id",
                keyValue: new Guid("5b384a6f-3fec-4c6c-850a-8206f27a346c"));

            migrationBuilder.DeleteData(
                table: "ClothingItemPhotos",
                keyColumn: "Id",
                keyValue: new Guid("8364994e-1d6d-4a4e-8a11-abef3f9330f9"));

            migrationBuilder.DeleteData(
                table: "DeliveryMethods",
                keyColumn: "Id",
                keyValue: new Guid("82f1a73e-b0a1-4c7f-b5ec-c0f47f67bbb3"));

            migrationBuilder.DeleteData(
                table: "DeliveryMethods",
                keyColumn: "Id",
                keyValue: new Guid("b846d054-2a09-4dcd-af57-f6d671e8c4b8"));

            migrationBuilder.DeleteData(
                table: "DeliveryMethods",
                keyColumn: "Id",
                keyValue: new Guid("bb4b5540-55db-4f34-a047-cf15c0d7a3d6"));

            migrationBuilder.DeleteData(
                table: "DeliveryMethods",
                keyColumn: "Id",
                keyValue: new Guid("e41dd91a-9cf2-48e5-8e02-8c3886274f11"));

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "Id",
                keyValue: new Guid("0842ed2a-4aa5-48be-b152-838e48fbcf7d"));

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "Id",
                keyValue: new Guid("442793b6-8446-416d-bfe8-cc671b704672"));

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "Id",
                keyValue: new Guid("7fa74314-332e-4d1d-80e5-d8471ca65035"));

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "Id",
                keyValue: new Guid("89618184-cb3d-4919-988c-e6fdaa57d479"));

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "Id",
                keyValue: new Guid("b84dc68b-f4d3-404f-b157-346db55e237a"));

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "Id",
                keyValue: new Guid("d82db94f-13d4-4bbc-910f-05f6ea7df575"));

            migrationBuilder.InsertData(
                table: "ClothingItems",
                columns: new[] { "Id", "Category", "ClothingBrandId", "CreatedAt", "Description", "Discount", "Gender", "IsInStock", "LastUpdatedAt", "Name", "Price", "Size" },
                values: new object[,]
                {
                    { new Guid("12f0de27-9d46-4252-8f42-40031bc617b5"), 4, new Guid("e96c60b6-09df-4e1a-9d6c-617bdd48eaf5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "New for Winter 2024, the Dior Icon heeled ankle boot transcends House codes of couture refinement. The black suede calfskin upper is elevated by elastic bands on the sides and the gold-finish metal CD signature on the back. The 8-cm (3) Graphic Cannage cylindrical heel in gold-finish metal offers a modern 3D version of the House's iconic motif. Featuring a square toe, the sophisticated and comfortable ankle boot will add the finishing touch to any of the season's looks.", null, 1, true, null, "Dior Dior Icon Heeled Ankle Boot", 2900.00m, 2 },
                    { new Guid("52cc6549-df79-4e85-9df9-85afb4970672"), 0, new Guid("5d24a48b-6c72-4e2a-9ef2-64d0f657bfc6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A regular-fit, long-sleeved fluid shirt featuring an all-over tonal Barocco devore motif.", null, 0, true, null, "Versace Barocco Devore Shirt", 1200.00m, 3 },
                    { new Guid("5708d50e-4aeb-461b-adbb-9f9b87acdcfc"), 0, new Guid("a2c5c305-f2c2-45e7-8f7d-c489bb7f7e8a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "An essential item of the brand, the Prada jersey T-shirt embodies the luxury of simplicity that becomes an attitude and search to reinvent the bases and propose new meanings. The design is accented with the brand's emblematic lettering logo presented here in a silicone version.", null, 0, true, null, "Prada Cotton T-shirt", 950.00m, 4 },
                    { new Guid("9709762d-5dd9-44fa-8ecb-3d2150052202"), 3, new Guid("c981db82-b2f1-48c3-9864-efc6c56a5b0e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The GG Marmont belt continues to enrich each new collection with its streamlined design. Inspired by an archival design from the 1970s, the line's monogram Double G hardware is presented in a shiny silver tone atop this black leather variation.", null, 0, true, null, "Gucci GG MARMONT THIN BELT", 450.00m, 3 },
                    { new Guid("b705424c-d9f1-4553-ae3c-f8d7ecfc4575"), 2, new Guid("3d6f79a2-c462-4c28-ae5f-0ec93b7f4e01"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Classic Chanel tweed jacket in black.", null, 1, true, null, "Chanel JACKET", 5000.00m, 2 },
                    { new Guid("f748241c-5378-4020-96d2-0f181dfb8cf3"), 3, new Guid("b5d6b8f8-dad4-4f2f-8c52-2911d856b3ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The LV Gram Square Cat Eye sunglasses feature a distinctive signature from Louis Vuitton’s jewelry and belts collections. The slim acetate and metal temples are adorned with the LV Initials and two Monogram Flowers finely crafted in gold-tone metal. Monogram Flower details on the lenses and end tips add an extra House touch. These stylish, feminine sunglasses are ideal for accenting a summer outfit.", null, 1, true, null, "LV Gram Square Cat Eye Sunglasses", 3200.00m, 2 }
                });

            migrationBuilder.InsertData(
                table: "DeliveryMethods",
                columns: new[] { "Id", "CreatedAt", "DeliveryTime", "Description", "LastUpdatedAt", "Price", "ShortName" },
                values: new object[,]
                {
                    { new Guid("0b4327f1-2f2a-4eec-b5ef-3476206ea28c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2-5 Days", "Get it within 5 days", null, 5m, "UPS2" },
                    { new Guid("203ae64f-7c6b-4c03-9deb-2eca53218352"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1-2 Days", "Fastest delivery time", null, 10m, "UPS1" },
                    { new Guid("a0854e88-0e39-43ed-bd6b-e3889fc411ed"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "5-10 Days", "Slower but cheap", null, 2m, "UPS3" },
                    { new Guid("df9b33a6-902d-4ada-bb16-ee28af859fc8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1-2 Weeks", "Free! You get what you pay for", null, 0m, "FREE" }
                });

            migrationBuilder.InsertData(
                table: "ClothingItemPhotos",
                columns: new[] { "Id", "ClothingItemId", "CreatedAt", "IsMain", "LastUpdatedAt", "PublicId", "Url" },
                values: new object[,]
                {
                    { new Guid("05742d28-7ebc-469f-91a0-7eaefaa150c4"), new Guid("9709762d-5dd9-44fa-8ecb-3d2150052202"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId3", "https://media.gucci.com/style/DarkGray_Center_0_0_2400x2400/1714409103/414516_0AABG_1000_001_100_0000_Light-GG-Marmont-thin-belt.jpg" },
                    { new Guid("08219b8e-87c5-44d0-8806-3ff7e82fe653"), new Guid("12f0de27-9d46-4252-8f42-40031bc617b5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId4", "https://www.dior.com/couture/ecommerce/media/catalog/product/Q/K/1721839565_KCT067VVV_S900_E03_GHC.jpg?imwidth=720" },
                    { new Guid("093eaf5c-465d-46ea-b48a-8361e1262924"), new Guid("f748241c-5378-4020-96d2-0f181dfb8cf3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId5", "https://eu.louisvuitton.com/images/is/image/lv/1/PP_VP_L/louis-vuitton-lv-gram-square-cat-eye-sunglasses-s00-sunglasses--Z2459U_PM2_Front%20view.png?wid=1090&hei=1090" },
                    { new Guid("42405f02-3033-4d7f-b730-c66b6c4aab49"), new Guid("5708d50e-4aeb-461b-adbb-9f9b87acdcfc"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId2", "https://www.prada.com/content/dam/pradabkg_products/U/UJN/UJN815/1052F0002/UJN815_1052_F0002_S_221_SLF.jpg/_jcr_content/renditions/cq5dam.web.hebebed.1000.1000.jpg" },
                    { new Guid("44d6db2d-e1a1-4eac-9ab1-dc79311c805b"), new Guid("52cc6549-df79-4e85-9df9-85afb4970672"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId1", "https://www.versace.com/dw/image/v2/BGWN_PRD/on/demandware.static/-/Sites-ver-master-catalog/default/dwf9d0b70e/original/90_1012141-1A11358_1B000_10_BaroccoDevorShirt-Shirts-Versace-online-store_0_2.jpg?sw=1200&q=85&strip=true" },
                    { new Guid("4914b710-d3da-4dc3-b93c-b4ef586a7323"), new Guid("b705424c-d9f1-4553-ae3c-f8d7ecfc4575"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId6", "https://www.chanel.com/images//t_zoomportee/f_auto//jacket-black-lambskin-lambskin-packshot-alternative-p78125c7009094305-9548808159262.jpg" }
                });
        }
    }
}
