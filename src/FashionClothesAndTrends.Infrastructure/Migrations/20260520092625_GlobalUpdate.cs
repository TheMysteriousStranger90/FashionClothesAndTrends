using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FashionClothesAndTrends.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GlobalUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClothingItemPhotos",
                keyColumn: "Id",
                keyValue: new Guid("13a2ad8f-038a-49b8-9e79-7a47658b210c"));

            migrationBuilder.DeleteData(
                table: "ClothingItemPhotos",
                keyColumn: "Id",
                keyValue: new Guid("22ba3b6a-0b1e-47ab-98d2-cd893914856c"));

            migrationBuilder.DeleteData(
                table: "ClothingItemPhotos",
                keyColumn: "Id",
                keyValue: new Guid("3dea3695-c11e-4982-afb2-4090a111cdd6"));

            migrationBuilder.DeleteData(
                table: "ClothingItemPhotos",
                keyColumn: "Id",
                keyValue: new Guid("79ac798c-358a-4e10-84cf-4b36c8dfc2eb"));

            migrationBuilder.DeleteData(
                table: "ClothingItemPhotos",
                keyColumn: "Id",
                keyValue: new Guid("79be8bee-e4df-4f5e-b6a0-7207a7a5f577"));

            migrationBuilder.DeleteData(
                table: "ClothingItemPhotos",
                keyColumn: "Id",
                keyValue: new Guid("f7f48a1f-7a32-494f-bca8-ab237d3a864c"));

            migrationBuilder.DeleteData(
                table: "DeliveryMethods",
                keyColumn: "Id",
                keyValue: new Guid("6a727c01-36be-41a5-86a6-30e815f81558"));

            migrationBuilder.DeleteData(
                table: "DeliveryMethods",
                keyColumn: "Id",
                keyValue: new Guid("a2da0761-0d91-4eda-b404-64c476dc62e4"));

            migrationBuilder.DeleteData(
                table: "DeliveryMethods",
                keyColumn: "Id",
                keyValue: new Guid("a87a5774-7b0f-4ac4-af52-97b82e3f098e"));

            migrationBuilder.DeleteData(
                table: "DeliveryMethods",
                keyColumn: "Id",
                keyValue: new Guid("a8950550-472e-448e-ab20-28329637fdb8"));

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "Id",
                keyValue: new Guid("1f82a6f2-078f-4725-8e3a-3d8b23c59dea"));

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "Id",
                keyValue: new Guid("3143fd79-0836-41b6-b374-8cdfef1abb47"));

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "Id",
                keyValue: new Guid("596ed824-1533-407c-8d7e-22d8f7610801"));

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "Id",
                keyValue: new Guid("7b50864a-2943-4680-ae52-5e49a2de5068"));

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "Id",
                keyValue: new Guid("86c41dc6-efab-4214-b776-ec7e78414b8a"));

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "Id",
                keyValue: new Guid("d1ebab8c-1451-4e34-981c-dcf6530b71b4"));

            migrationBuilder.InsertData(
                table: "ClothingItems",
                columns: new[] { "Id", "Category", "ClothingBrandId", "CreatedAt", "Description", "Discount", "Gender", "IsInStock", "LastUpdatedAt", "Name", "Price", "Size" },
                values: new object[,]
                {
                    { new Guid("0e300a7b-22ac-4e66-9675-d36423e7d60f"), 3, new Guid("b5d6b8f8-dad4-4f2f-8c52-2911d856b3ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The LV Gram Square Cat Eye sunglasses feature a distinctive signature from Louis Vuitton’s jewelry and belts collections. The slim acetate and metal temples are adorned with the LV Initials and two Monogram Flowers finely crafted in gold-tone metal. Monogram Flower details on the lenses and end tips add an extra House touch. These stylish, feminine sunglasses are ideal for accenting a summer outfit.", null, 1, true, null, "LV Gram Square Cat Eye Sunglasses", 3200.00m, 2 },
                    { new Guid("197a6281-5f53-43d5-b7d7-9b983d6a3a13"), 2, new Guid("3d6f79a2-c462-4c28-ae5f-0ec93b7f4e01"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Classic Chanel tweed jacket in black.", null, 1, true, null, "Chanel JACKET", 5000.00m, 2 },
                    { new Guid("21d08196-0c31-4ad5-813e-176a5c43c4e0"), 3, new Guid("c981db82-b2f1-48c3-9864-efc6c56a5b0e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The GG Marmont belt continues to enrich each new collection with its streamlined design. Inspired by an archival design from the 1970s, the line's monogram Double G hardware is presented in a shiny silver tone atop this black leather variation.", null, 0, true, null, "Gucci GG MARMONT THIN BELT", 450.00m, 3 },
                    { new Guid("4705582c-8db9-41ac-9671-2c8dd7b05e78"), 4, new Guid("e96c60b6-09df-4e1a-9d6c-617bdd48eaf5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "New for Winter 2024, the Dior Icon heeled ankle boot transcends House codes of couture refinement. The black suede calfskin upper is elevated by elastic bands on the sides and the gold-finish metal CD signature on the back. The 8-cm (3) Graphic Cannage cylindrical heel in gold-finish metal offers a modern 3D version of the House's iconic motif. Featuring a square toe, the sophisticated and comfortable ankle boot will add the finishing touch to any of the season's looks.", null, 1, true, null, "Dior Dior Icon Heeled Ankle Boot", 2900.00m, 2 },
                    { new Guid("910a8e68-73c0-404d-be31-0b836bef5cfd"), 0, new Guid("5d24a48b-6c72-4e2a-9ef2-64d0f657bfc6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A regular-fit, long-sleeved fluid shirt featuring an all-over tonal Barocco devore motif.", null, 0, true, null, "Versace Barocco Devore Shirt", 1200.00m, 3 },
                    { new Guid("a33ccfc8-cd9d-463c-904e-cbad3dcbc71f"), 0, new Guid("a2c5c305-f2c2-45e7-8f7d-c489bb7f7e8a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "An essential item of the brand, the Prada jersey T-shirt embodies the luxury of simplicity that becomes an attitude and search to reinvent the bases and propose new meanings. The design is accented with the brand's emblematic lettering logo presented here in a silicone version.", null, 0, true, null, "Prada Cotton T-shirt", 950.00m, 4 }
                });

            migrationBuilder.InsertData(
                table: "DeliveryMethods",
                columns: new[] { "Id", "CreatedAt", "DeliveryTime", "Description", "LastUpdatedAt", "Price", "ShortName" },
                values: new object[,]
                {
                    { new Guid("1a557902-f2a8-45da-a364-1f8d475a78d8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2-5 Days", "Get it within 5 days", null, 5m, "UPS2" },
                    { new Guid("7daf49da-1183-456b-84ae-debb4fe44820"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1-2 Days", "Fastest delivery time", null, 10m, "UPS1" },
                    { new Guid("b7669aa8-dcc0-408e-a1b1-778dd0831177"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1-2 Weeks", "Free! You get what you pay for", null, 0m, "FREE" },
                    { new Guid("f59c5d03-e854-46b2-b054-d6d34ca24272"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "5-10 Days", "Slower but cheap", null, 2m, "UPS3" }
                });

            migrationBuilder.InsertData(
                table: "ClothingItemPhotos",
                columns: new[] { "Id", "ClothingItemId", "CreatedAt", "IsMain", "LastUpdatedAt", "PublicId", "Url" },
                values: new object[,]
                {
                    { new Guid("37de7c1f-7838-4cd9-a323-5962a193ddb1"), new Guid("0e300a7b-22ac-4e66-9675-d36423e7d60f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId5", "https://eu.louisvuitton.com/images/is/image/lv/1/PP_VP_L/louis-vuitton-lv-gram-square-cat-eye-sunglasses-s00-sunglasses--Z2459U_PM2_Front%20view.png?wid=1090&hei=1090" },
                    { new Guid("795e4940-0f70-4234-8aa7-c9d19bc109a4"), new Guid("910a8e68-73c0-404d-be31-0b836bef5cfd"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId1", "https://www.versace.com/dw/image/v2/BGWN_PRD/on/demandware.static/-/Sites-ver-master-catalog/default/dwf9d0b70e/original/90_1012141-1A11358_1B000_10_BaroccoDevorShirt-Shirts-Versace-online-store_0_2.jpg?sw=1200&q=85&strip=true" },
                    { new Guid("a7c8924c-a47b-47f3-8efa-2d58e5e651d7"), new Guid("4705582c-8db9-41ac-9671-2c8dd7b05e78"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId4", "https://www.dior.com/couture/ecommerce/media/catalog/product/Q/K/1721839565_KCT067VVV_S900_E03_GHC.jpg?imwidth=720" },
                    { new Guid("d6f97c75-d219-43c4-b80d-86fb5a17f726"), new Guid("21d08196-0c31-4ad5-813e-176a5c43c4e0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId3", "https://media.gucci.com/style/DarkGray_Center_0_0_2400x2400/1714409103/414516_0AABG_1000_001_100_0000_Light-GG-Marmont-thin-belt.jpg" },
                    { new Guid("e05a4a09-2abf-463a-8146-f693014afb37"), new Guid("197a6281-5f53-43d5-b7d7-9b983d6a3a13"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId6", "https://www.chanel.com/images//t_zoomportee/f_auto//jacket-black-lambskin-lambskin-packshot-alternative-p78125c7009094305-9548808159262.jpg" },
                    { new Guid("ec6386f9-e640-4620-b17f-ae66e27da772"), new Guid("a33ccfc8-cd9d-463c-904e-cbad3dcbc71f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId2", "https://www.prada.com/content/dam/pradabkg_products/U/UJN/UJN815/1052F0002/UJN815_1052_F0002_S_221_SLF.jpg/_jcr_content/renditions/cq5dam.web.hebebed.1000.1000.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClothingItemPhotos",
                keyColumn: "Id",
                keyValue: new Guid("37de7c1f-7838-4cd9-a323-5962a193ddb1"));

            migrationBuilder.DeleteData(
                table: "ClothingItemPhotos",
                keyColumn: "Id",
                keyValue: new Guid("795e4940-0f70-4234-8aa7-c9d19bc109a4"));

            migrationBuilder.DeleteData(
                table: "ClothingItemPhotos",
                keyColumn: "Id",
                keyValue: new Guid("a7c8924c-a47b-47f3-8efa-2d58e5e651d7"));

            migrationBuilder.DeleteData(
                table: "ClothingItemPhotos",
                keyColumn: "Id",
                keyValue: new Guid("d6f97c75-d219-43c4-b80d-86fb5a17f726"));

            migrationBuilder.DeleteData(
                table: "ClothingItemPhotos",
                keyColumn: "Id",
                keyValue: new Guid("e05a4a09-2abf-463a-8146-f693014afb37"));

            migrationBuilder.DeleteData(
                table: "ClothingItemPhotos",
                keyColumn: "Id",
                keyValue: new Guid("ec6386f9-e640-4620-b17f-ae66e27da772"));

            migrationBuilder.DeleteData(
                table: "DeliveryMethods",
                keyColumn: "Id",
                keyValue: new Guid("1a557902-f2a8-45da-a364-1f8d475a78d8"));

            migrationBuilder.DeleteData(
                table: "DeliveryMethods",
                keyColumn: "Id",
                keyValue: new Guid("7daf49da-1183-456b-84ae-debb4fe44820"));

            migrationBuilder.DeleteData(
                table: "DeliveryMethods",
                keyColumn: "Id",
                keyValue: new Guid("b7669aa8-dcc0-408e-a1b1-778dd0831177"));

            migrationBuilder.DeleteData(
                table: "DeliveryMethods",
                keyColumn: "Id",
                keyValue: new Guid("f59c5d03-e854-46b2-b054-d6d34ca24272"));

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "Id",
                keyValue: new Guid("0e300a7b-22ac-4e66-9675-d36423e7d60f"));

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "Id",
                keyValue: new Guid("197a6281-5f53-43d5-b7d7-9b983d6a3a13"));

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "Id",
                keyValue: new Guid("21d08196-0c31-4ad5-813e-176a5c43c4e0"));

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "Id",
                keyValue: new Guid("4705582c-8db9-41ac-9671-2c8dd7b05e78"));

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "Id",
                keyValue: new Guid("910a8e68-73c0-404d-be31-0b836bef5cfd"));

            migrationBuilder.DeleteData(
                table: "ClothingItems",
                keyColumn: "Id",
                keyValue: new Guid("a33ccfc8-cd9d-463c-904e-cbad3dcbc71f"));

            migrationBuilder.InsertData(
                table: "ClothingItems",
                columns: new[] { "Id", "Category", "ClothingBrandId", "CreatedAt", "Description", "Discount", "Gender", "IsInStock", "LastUpdatedAt", "Name", "Price", "Size" },
                values: new object[,]
                {
                    { new Guid("1f82a6f2-078f-4725-8e3a-3d8b23c59dea"), 3, new Guid("c981db82-b2f1-48c3-9864-efc6c56a5b0e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The GG Marmont belt continues to enrich each new collection with its streamlined design. Inspired by an archival design from the 1970s, the line's monogram Double G hardware is presented in a shiny silver tone atop this black leather variation.", null, 0, true, null, "Gucci GG MARMONT THIN BELT", 450.00m, 3 },
                    { new Guid("3143fd79-0836-41b6-b374-8cdfef1abb47"), 2, new Guid("3d6f79a2-c462-4c28-ae5f-0ec93b7f4e01"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Classic Chanel tweed jacket in black.", null, 1, true, null, "Chanel JACKET", 5000.00m, 2 },
                    { new Guid("596ed824-1533-407c-8d7e-22d8f7610801"), 4, new Guid("e96c60b6-09df-4e1a-9d6c-617bdd48eaf5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "New for Winter 2024, the Dior Icon heeled ankle boot transcends House codes of couture refinement. The black suede calfskin upper is elevated by elastic bands on the sides and the gold-finish metal CD signature on the back. The 8-cm (3) Graphic Cannage cylindrical heel in gold-finish metal offers a modern 3D version of the House's iconic motif. Featuring a square toe, the sophisticated and comfortable ankle boot will add the finishing touch to any of the season's looks.", null, 1, true, null, "Dior Dior Icon Heeled Ankle Boot", 2900.00m, 2 },
                    { new Guid("7b50864a-2943-4680-ae52-5e49a2de5068"), 0, new Guid("5d24a48b-6c72-4e2a-9ef2-64d0f657bfc6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A regular-fit, long-sleeved fluid shirt featuring an all-over tonal Barocco devore motif.", null, 0, true, null, "Versace Barocco Devore Shirt", 1200.00m, 3 },
                    { new Guid("86c41dc6-efab-4214-b776-ec7e78414b8a"), 0, new Guid("a2c5c305-f2c2-45e7-8f7d-c489bb7f7e8a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "An essential item of the brand, the Prada jersey T-shirt embodies the luxury of simplicity that becomes an attitude and search to reinvent the bases and propose new meanings. The design is accented with the brand's emblematic lettering logo presented here in a silicone version.", null, 0, true, null, "Prada Cotton T-shirt", 950.00m, 4 },
                    { new Guid("d1ebab8c-1451-4e34-981c-dcf6530b71b4"), 3, new Guid("b5d6b8f8-dad4-4f2f-8c52-2911d856b3ad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The LV Gram Square Cat Eye sunglasses feature a distinctive signature from Louis Vuitton’s jewelry and belts collections. The slim acetate and metal temples are adorned with the LV Initials and two Monogram Flowers finely crafted in gold-tone metal. Monogram Flower details on the lenses and end tips add an extra House touch. These stylish, feminine sunglasses are ideal for accenting a summer outfit.", null, 1, true, null, "LV Gram Square Cat Eye Sunglasses", 3200.00m, 2 }
                });

            migrationBuilder.InsertData(
                table: "DeliveryMethods",
                columns: new[] { "Id", "CreatedAt", "DeliveryTime", "Description", "LastUpdatedAt", "Price", "ShortName" },
                values: new object[,]
                {
                    { new Guid("6a727c01-36be-41a5-86a6-30e815f81558"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1-2 Weeks", "Free! You get what you pay for", null, 0m, "FREE" },
                    { new Guid("a2da0761-0d91-4eda-b404-64c476dc62e4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "5-10 Days", "Slower but cheap", null, 2m, "UPS3" },
                    { new Guid("a87a5774-7b0f-4ac4-af52-97b82e3f098e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1-2 Days", "Fastest delivery time", null, 10m, "UPS1" },
                    { new Guid("a8950550-472e-448e-ab20-28329637fdb8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2-5 Days", "Get it within 5 days", null, 5m, "UPS2" }
                });

            migrationBuilder.InsertData(
                table: "ClothingItemPhotos",
                columns: new[] { "Id", "ClothingItemId", "CreatedAt", "IsMain", "LastUpdatedAt", "PublicId", "Url" },
                values: new object[,]
                {
                    { new Guid("13a2ad8f-038a-49b8-9e79-7a47658b210c"), new Guid("7b50864a-2943-4680-ae52-5e49a2de5068"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId1", "https://www.versace.com/dw/image/v2/BGWN_PRD/on/demandware.static/-/Sites-ver-master-catalog/default/dwf9d0b70e/original/90_1012141-1A11358_1B000_10_BaroccoDevorShirt-Shirts-Versace-online-store_0_2.jpg?sw=1200&q=85&strip=true" },
                    { new Guid("22ba3b6a-0b1e-47ab-98d2-cd893914856c"), new Guid("3143fd79-0836-41b6-b374-8cdfef1abb47"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId6", "https://www.chanel.com/images//t_zoomportee/f_auto//jacket-black-lambskin-lambskin-packshot-alternative-p78125c7009094305-9548808159262.jpg" },
                    { new Guid("3dea3695-c11e-4982-afb2-4090a111cdd6"), new Guid("1f82a6f2-078f-4725-8e3a-3d8b23c59dea"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId3", "https://media.gucci.com/style/DarkGray_Center_0_0_2400x2400/1714409103/414516_0AABG_1000_001_100_0000_Light-GG-Marmont-thin-belt.jpg" },
                    { new Guid("79ac798c-358a-4e10-84cf-4b36c8dfc2eb"), new Guid("596ed824-1533-407c-8d7e-22d8f7610801"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId4", "https://www.dior.com/couture/ecommerce/media/catalog/product/Q/K/1721839565_KCT067VVV_S900_E03_GHC.jpg?imwidth=720" },
                    { new Guid("79be8bee-e4df-4f5e-b6a0-7207a7a5f577"), new Guid("d1ebab8c-1451-4e34-981c-dcf6530b71b4"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId5", "https://eu.louisvuitton.com/images/is/image/lv/1/PP_VP_L/louis-vuitton-lv-gram-square-cat-eye-sunglasses-s00-sunglasses--Z2459U_PM2_Front%20view.png?wid=1090&hei=1090" },
                    { new Guid("f7f48a1f-7a32-494f-bca8-ab237d3a864c"), new Guid("86c41dc6-efab-4214-b776-ec7e78414b8a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, "PublicId2", "https://www.prada.com/content/dam/pradabkg_products/U/UJN/UJN815/1052F0002/UJN815_1052_F0002_S_221_SLF.jpg/_jcr_content/renditions/cq5dam.web.hebebed.1000.1000.jpg" }
                });
        }
    }
}
