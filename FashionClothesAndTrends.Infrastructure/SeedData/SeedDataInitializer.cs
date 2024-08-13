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

        modelBuilder.Entity<ClothingItem>().HasData(
            new ClothingItem
            {
                Id = Guid.NewGuid(),
                Name = "Chanel Tweed Jacket",
                Description = "Classic Chanel tweed jacket in black and white.",
                Price = 5000.00M,
                Gender = Gender.Female,
                Size = Size.M,
                Category = Category.Outerwear,
                IsInStock = true,
                PictureUrl = "images/clothing/chanel_tweed_jacket.jpg",
                ClothingBrandId = clothingBrands[0].Id
            },
            new ClothingItem
            {
                Id = Guid.NewGuid(),
                Name = "Louis Vuitton Monogram Bag",
                Description = "Iconic Louis Vuitton bag with monogram canvas.",
                Price = 3200.00M,
                Gender = Gender.Female,
                Size = Size.M,
                Category = Category.Bags,
                IsInStock = true,
                PictureUrl = "images/clothing/lv_monogram_bag.jpg",
                ClothingBrandId = clothingBrands[1].Id
            },
            new ClothingItem
            {
                Id = Guid.NewGuid(),
                Name = "Dior Saddle Bag",
                Description = "Classic Dior Saddle Bag in blue oblique canvas.",
                Price = 2900.00M,
                Gender = Gender.Female,
                Size = Size.M,
                Category = Category.Bags,
                IsInStock = true,
                PictureUrl = "images/clothing/dior_saddle_bag.jpg",
                ClothingBrandId = clothingBrands[2].Id
            },
            new ClothingItem
            {
                Id = Guid.NewGuid(),
                Name = "Gucci GG Belt",
                Description = "Black leather belt with double G buckle from Gucci.",
                Price = 450.00M,
                Gender = Gender.Male,
                Size = Size.L,
                Category = Category.Accessories,
                IsInStock = true,
                PictureUrl = "images/clothing/gucci_belt.jpg",
                ClothingBrandId = clothingBrands[3].Id
            },
            new ClothingItem
            {
                Id = Guid.NewGuid(),
                Name = "Prada Nylon Backpack",
                Description = "Classic black nylon backpack with leather trim.",
                Price = 950.00M,
                Gender = Gender.Female,
                Size = Size.M,
                Category = Category.Bags,
                IsInStock = true,
                PictureUrl = "images/clothing/prada_backpack.jpg",
                ClothingBrandId = clothingBrands[4].Id
            },
            new ClothingItem
            {
                Id = Guid.NewGuid(),
                Name = "Versace Silk Shirt",
                Description = "Versace silk shirt with baroque print in gold and black.",
                Price = 1200.00M,
                Gender = Gender.Male,
                Size = Size.L,
                Category = Category.Top,
                IsInStock = true,
                PictureUrl = "images/clothing/versace_silk_shirt.jpg",
                ClothingBrandId = clothingBrands[5].Id
            }
        );

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
                UserName = "buyer1@example.com",
                Email = "buyer1@example.com",
                Name = "John Doe",
                DateOfBirth = new DateOnly(1990, 5, 15),
                Address = new ShippingAddress
                {
                    AddressLine1 = "123 Main St",
                    City = "New York",
                    State = "NY",
                    PostalCode = "10001",
                    Country = "USA",
                    IsDefault = true
                }
            },
            new User
            {
                UserName = "buyer2@example.com",
                Email = "buyer2@example.com",
                Name = "Jane Smith",
                DateOfBirth = new DateOnly(1985, 10, 25),
                Address = new ShippingAddress
                {
                    AddressLine1 = "456 Maple Ave",
                    City = "Los Angeles",
                    State = "CA",
                    PostalCode = "90001",
                    Country = "USA",
                    IsDefault = true
                }
            },
            new User
            {
                UserName = "admin@example.com",
                Email = "admin@example.com",
                Name = "Admin User",
                DateOfBirth = new DateOnly(1980, 1, 1),
                Address = new ShippingAddress
                {
                    AddressLine1 = "789 Oak St",
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