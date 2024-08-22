using System.Reflection;
using System.Text.Json;
using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Entities.Enums;
using FashionClothesAndTrends.Domain.Entities.OrderAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FashionClothesAndTrends.Infrastructure.SeedData;

public static class SeedDataInitializer
{
    public static void ContextSeed(ModelBuilder modelBuilder)
    {
       var clothingBrands = new[]
        {
            new ClothingBrand
            {
                Id = Guid.Parse("3d6f79a2-c462-4c28-ae5f-0ec93b7f4e01"),
                Name = "Chanel",
                Description = "Luxury fashion brand from France"
            },
            new ClothingBrand
            {
                Id = Guid.Parse("b5d6b8f8-dad4-4f2f-8c52-2911d856b3ad"),
                Name = "Louis Vuitton",
                Description = "High-end French fashion house"
            },
            new ClothingBrand
            {
                Id = Guid.Parse("e96c60b6-09df-4e1a-9d6c-617bdd48eaf5"),
                Name = "Dior",
                Description = "French luxury fashion house"
            },
            new ClothingBrand
            {
                Id = Guid.Parse("c981db82-b2f1-48c3-9864-efc6c56a5b0e"),
                Name = "Gucci",
                Description = "Italian luxury fashion brand"
            },
            new ClothingBrand
            {
                Id = Guid.Parse("a2c5c305-f2c2-45e7-8f7d-c489bb7f7e8a"),
                Name = "Prada",
                Description = "Italian luxury fashion house"
            },
            new ClothingBrand
            {
                Id = Guid.Parse("5d24a48b-6c72-4e2a-9ef2-64d0f657bfc6"),
                Name = "Versace",
                Description = "Italian luxury fashion company"
            }
        };

        modelBuilder.Entity<ClothingBrand>().HasData(clothingBrands);

        var clothingItems = new[]
        {
            new ClothingItem
            {
                Id = Guid.NewGuid(),
                Name = "Chanel JACKET",
                Description = "Classic Chanel tweed jacket in black.",
                Price = 5000.00M,
                Gender = Gender.Female,
                Size = Size.M,
                Category = Category.Outerwear,
                IsInStock = true,
                ClothingBrandId = clothingBrands[0].Id
            },
            new ClothingItem
            {
                Id = Guid.NewGuid(),
                Name = "LV Gram Square Cat Eye Sunglasses",
                Description = "The LV Gram Square Cat Eye sunglasses feature a distinctive signature from Louis Vuitton’s jewelry and belts collections. The slim acetate and metal temples are adorned with the LV Initials and two Monogram Flowers finely crafted in gold-tone metal. Monogram Flower details on the lenses and end tips add an extra House touch. These stylish, feminine sunglasses are ideal for accenting a summer outfit.",
                Price = 3200.00M,
                Gender = Gender.Female,
                Size = Size.M,
                Category = Category.Accessories,
                IsInStock = true,
                ClothingBrandId = clothingBrands[1].Id
            },
            new ClothingItem
            {
                Id = Guid.NewGuid(),
                Name = "Dior Dior Icon Heeled Ankle Boot",
                Description = "New for Winter 2024, the Dior Icon heeled ankle boot transcends House codes of couture refinement. The black suede calfskin upper is elevated by elastic bands on the sides and the gold-finish metal CD signature on the back. The 8-cm (3) Graphic Cannage cylindrical heel in gold-finish metal offers a modern 3D version of the House's iconic motif. Featuring a square toe, the sophisticated and comfortable ankle boot will add the finishing touch to any of the season's looks.",
                Price = 2900.00M,
                Gender = Gender.Female,
                Size = Size.M,
                Category = Category.Shoes,
                IsInStock = true,
                ClothingBrandId = clothingBrands[2].Id
            },
            new ClothingItem
            {
                Id = Guid.NewGuid(),
                Name = "Gucci GG MARMONT THIN BELT",
                Description = "The GG Marmont belt continues to enrich each new collection with its streamlined design. Inspired by an archival design from the 1970s, the line's monogram Double G hardware is presented in a shiny silver tone atop this black leather variation.",
                Price = 450.00M,
                Gender = Gender.Male,
                Size = Size.L,
                Category = Category.Accessories,
                IsInStock = true,
                ClothingBrandId = clothingBrands[3].Id
            },
            new ClothingItem
            {
                Id = Guid.NewGuid(),
                Name = "Prada Cotton T-shirt",
                Description = "An essential item of the brand, the Prada jersey T-shirt embodies the luxury of simplicity that becomes an attitude and search to reinvent the bases and propose new meanings. The design is accented with the brand's emblematic lettering logo presented here in a silicone version.",
                Price = 950.00M,
                Gender = Gender.Male,
                Size = Size.XL,
                Category = Category.Top,
                IsInStock = true,
                ClothingBrandId = clothingBrands[4].Id
            },
            new ClothingItem
            {
                Id = Guid.NewGuid(),
                Name = "Versace Barocco Devore Shirt",
                Description = "A regular-fit, long-sleeved fluid shirt featuring an all-over tonal Barocco devore motif.",
                Price = 1200.00M,
                Gender = Gender.Male,
                Size = Size.L,
                Category = Category.Top,
                IsInStock = true,
                ClothingBrandId = clothingBrands[5].Id
            }
        };

        modelBuilder.Entity<ClothingItem>().HasData(clothingItems);

        var clothingItemPhotos = new[]
        {
            new ClothingItemPhoto
            {
                Id = Guid.NewGuid(),
                Url = "https://www.chanel.com/images//t_zoomportee/f_auto//jacket-black-lambskin-lambskin-packshot-alternative-p78125c7009094305-9548808159262.jpg",
                IsMain = true,
                PublicId = "PublicId6",
                ClothingItemId = clothingItems[0].Id
            },
            new ClothingItemPhoto
            {
                Id = Guid.NewGuid(),
                Url = "https://eu.louisvuitton.com/images/is/image/lv/1/PP_VP_L/louis-vuitton-lv-gram-square-cat-eye-sunglasses-s00-sunglasses--Z2459U_PM2_Front%20view.png?wid=1090&hei=1090",
                IsMain = true,
                PublicId = "PublicId5",
                ClothingItemId = clothingItems[1].Id
            },
            new ClothingItemPhoto
            {
                Id = Guid.NewGuid(),
                Url = "https://www.dior.com/couture/ecommerce/media/catalog/product/Q/K/1721839565_KCT067VVV_S900_E03_GHC.jpg?imwidth=720",
                IsMain = true,
                PublicId = "PublicId4",
                ClothingItemId = clothingItems[2].Id
            },
            new ClothingItemPhoto
            {
                Id = Guid.NewGuid(),
                Url = "https://media.gucci.com/style/DarkGray_Center_0_0_2400x2400/1714409103/414516_0AABG_1000_001_100_0000_Light-GG-Marmont-thin-belt.jpg",
                IsMain = true,
                PublicId = "PublicId3",
                ClothingItemId = clothingItems[3].Id
            },
            new ClothingItemPhoto
            {
                Id = Guid.NewGuid(),
                Url = "https://www.prada.com/content/dam/pradabkg_products/U/UJN/UJN815/1052F0002/UJN815_1052_F0002_S_221_SLF.jpg/_jcr_content/renditions/cq5dam.web.hebebed.1000.1000.jpg",
                IsMain = true,
                PublicId = "PublicId2",
                ClothingItemId = clothingItems[4].Id
            },
            new ClothingItemPhoto
            {
                Id = Guid.NewGuid(),
                Url = "https://www.versace.com/dw/image/v2/BGWN_PRD/on/demandware.static/-/Sites-ver-master-catalog/default/dwf9d0b70e/original/90_1012141-1A11358_1B000_10_BaroccoDevorShirt-Shirts-Versace-online-store_0_2.jpg?sw=1200&q=85&strip=true",
                IsMain = true,
                PublicId = "PublicId1",
                ClothingItemId = clothingItems[5].Id
            }
        };

        modelBuilder.Entity<ClothingItemPhoto>().HasData(clothingItemPhotos);
        
        modelBuilder.Entity<DeliveryMethod>().HasData(
            new DeliveryMethod()
            {
                Id = Guid.NewGuid(), ShortName = "UPS1", Description = "Fastest delivery time",
                DeliveryTime = "1-2 Days", Price = 10
            },
            new DeliveryMethod()
            {
                Id = Guid.NewGuid(), ShortName = "UPS2", Description = "Get it within 5 days",
                DeliveryTime = "2-5 Days", Price = 5
            },
            new DeliveryMethod()
            {
                Id = Guid.NewGuid(), ShortName = "UPS3", Description = "Slower but cheap", DeliveryTime = "5-10 Days",
                Price = 2
            },
            new DeliveryMethod()
            {
                Id = Guid.NewGuid(), ShortName = "FREE", Description = "Free! You get what you pay for",
                DeliveryTime = "1-2 Weeks", Price = 0
            }
        );
    }

    public static async Task SeedUsersAsync(UserManager<User> userManager, RoleManager<AppRole> roleManager)
    {
        var roles = new List<AppRole>
        {
            new AppRole { Name = "Buyer" },
            new AppRole { Name = "Administrator" }
        };

        foreach (var role in roles)
        {
            if (!(await roleManager.RoleExistsAsync(role.Name)))
            {
                await roleManager.CreateAsync(role);
            }
        }

        var users = new List<User>
        {
            new User
            {
                UserName = "JohnDoe254",
                Email = "buyer1@example.com",
                FirstName = "John",
                LastName = "Doe",
                Gender = "male",
                DateOfBirth = new DateOnly(1990, 5, 15),
                UserPhotos = new List<UserPhoto> { new UserPhoto { Url = "https://randomuser.me/api/portraits/men/91.jpg", IsMain = true, PublicId = "PublicId1",
                } },
                Address = new ShippingAddress
                {
                    AddressLine = "123 Main St",
                    City = "New York",
                    State = "NY",
                    PostalCode = "10001",
                    Country = "USA",
                    IsDefault = true
                }
            },
            new User
            {
                UserName = "JaneSmith123",
                Email = "buyer2@example.com",
                FirstName = "Jane",
                LastName = "Smith",
                Gender = "female",
                DateOfBirth = new DateOnly(1985, 10, 25),
                UserPhotos = new List<UserPhoto> { new UserPhoto { Url = "https://randomuser.me/api/portraits/women/85.jpg", IsMain = true, PublicId = "PublicId2",
                } },
                Address = new ShippingAddress
                {
                    AddressLine = "456 Maple Ave",
                    City = "Los Angeles",
                    State = "CA",
                    PostalCode = "90001",
                    Country = "USA",
                    IsDefault = true
                }
            },
            new User
            {
                UserName = "AdminUser111",
                Email = "admin@example.com",
                FirstName = "Admin",
                LastName = "User",
                Gender = "male",
                DateOfBirth = new DateOnly(1980, 1, 1),
                UserPhotos = new List<UserPhoto> { new UserPhoto { Url = "https://randomuser.me/api/portraits/men/72.jpg", IsMain = true, PublicId = "PublicId3",
                } },
                Address = new ShippingAddress
                {
                    AddressLine = "789 Oak St",
                    City = "San Francisco",
                    State = "CA",
                    PostalCode = "94102",
                    Country = "USA",
                    IsDefault = true
                }
            }
        };

        foreach (var user in users)
        {
            if (await userManager.FindByEmailAsync(user.Email) == null)
            {
                await userManager.CreateAsync(user, "Pa$$w0rd");
                if (user.Email.Contains("admin"))
                {
                    await userManager.AddToRoleAsync(user, "Administrator");
                }
                else
                {
                    await userManager.AddToRoleAsync(user, "Buyer");
                }
            }
        }
    }
}