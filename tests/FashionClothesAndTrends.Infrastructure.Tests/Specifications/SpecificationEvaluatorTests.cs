using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Entities.Enums;
using FashionClothesAndTrends.Domain.Specifications;
using FashionClothesAndTrends.Infrastructure.Context;
using FashionClothesAndTrends.Infrastructure.Specifications;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace FashionClothesAndTrends.Infrastructure.Tests.Specifications;

public class SpecificationEvaluatorTests : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly Guid _brandId = Guid.NewGuid();

    public SpecificationEvaluatorTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ApplicationDbContext(options);
        SeedTestData();
    }

    private void SeedTestData()
    {
        var brand = new ClothingBrand { Id = _brandId, Name = "TestBrand", Description = "Test" };
        _context.ClothingBrands.Add(brand);

        _context.ClothingItems.AddRange(
            new ClothingItem
            {
                Id = Guid.NewGuid(),
                Name = "Blue Jeans",
                Description = "Denim jeans",
                Price = 59.99m,
                Gender = Gender.Male,
                Size = Size.M,
                Category = Category.Bottom,
                IsInStock = true,
                ClothingBrandId = _brandId
            },
            new ClothingItem
            {
                Id = Guid.NewGuid(),
                Name = "Red T-Shirt",
                Description = "Cotton shirt",
                Price = 29.99m,
                Gender = Gender.Female,
                Size = Size.S,
                Category = Category.Top,
                IsInStock = true,
                ClothingBrandId = _brandId
            },
            new ClothingItem
            {
                Id = Guid.NewGuid(),
                Name = "Black Hoodie",
                Description = "Warm hoodie",
                Price = 89.99m,
                Gender = Gender.Male,
                Size = Size.L,
                Category = Category.Outerwear,
                IsInStock = false,
                ClothingBrandId = _brandId
            }
        );
        _context.SaveChanges();
    }

    [Fact]
    public void GetQuery_WithGenderCriteriaFilter_ShouldReturnOnlyMatchingItems()
    {
        // Arrange
        var spec = new ClothingItemsWithTypesAndBrandsSpecification(new ClothingSpecParams
        {
            Gender = Gender.Male
        });

        // Act
        var result = SpecificationEvaluator<ClothingItem>.GetQuery(_context.ClothingItems, spec).ToList();

        // Assert
        result.Should().HaveCount(2);
        result.Should().OnlyContain(i => i.Gender == Gender.Male);
    }

    [Fact]
    public void GetQuery_WithSearchFilter_ShouldReturnMatchingItems()
    {
        // Arrange
        var spec = new ClothingItemsWithTypesAndBrandsSpecification(new ClothingSpecParams
        {
            Search = "jeans"
        });

        // Act
        var result = SpecificationEvaluator<ClothingItem>.GetQuery(_context.ClothingItems, spec).ToList();

        // Assert
        result.Should().HaveCount(1);
        result[0].Name.Should().Be("Blue Jeans");
    }

    [Fact]
    public void GetQuery_WithPagingEnabled_ShouldReturnPagedResults()
    {
        // Arrange
        var spec = new ClothingItemsWithTypesAndBrandsSpecification(new ClothingSpecParams
        {
            PageIndex = 1,
            PageSize = 2
        });

        // Act
        var result = SpecificationEvaluator<ClothingItem>.GetQuery(_context.ClothingItems, spec).ToList();

        // Assert
        result.Should().HaveCount(2);
    }

    [Fact]
    public void GetQuery_WithSecondPage_ShouldReturnRemainingItems()
    {
        // Arrange
        var spec = new ClothingItemsWithTypesAndBrandsSpecification(new ClothingSpecParams
        {
            PageIndex = 2,
            PageSize = 2
        });

        // Act
        var result = SpecificationEvaluator<ClothingItem>.GetQuery(_context.ClothingItems, spec).ToList();

        // Assert
        result.Should().HaveCount(1);
    }

    [Fact]
    public void GetQuery_WithPriceAscSort_ShouldReturnItemsInAscendingOrder()
    {
        // Arrange
        var spec = new ClothingItemsWithTypesAndBrandsSpecification(new ClothingSpecParams
        {
            Sort = "priceAsc"
        });

        // Act
        var result = SpecificationEvaluator<ClothingItem>.GetQuery(_context.ClothingItems, spec).ToList();

        // Assert
        result.Should().BeInAscendingOrder(i => i.Price);
        result[0].Price.Should().Be(29.99m);
    }

    [Fact]
    public void GetQuery_WithPriceDescSort_ShouldReturnItemsInDescendingOrder()
    {
        // Arrange
        var spec = new ClothingItemsWithTypesAndBrandsSpecification(new ClothingSpecParams
        {
            Sort = "priceDesc"
        });

        // Act
        var result = SpecificationEvaluator<ClothingItem>.GetQuery(_context.ClothingItems, spec).ToList();

        // Assert
        result.Should().BeInDescendingOrder(i => i.Price);
        result[0].Price.Should().Be(89.99m);
    }

    [Fact]
    public void GetQuery_WithSpecificId_ShouldReturnSingleItem()
    {
        // Arrange
        var existingItem = _context.ClothingItems.First();
        var spec = new ClothingItemsWithTypesAndBrandsSpecification(existingItem.Id);

        // Act
        var result = SpecificationEvaluator<ClothingItem>.GetQuery(_context.ClothingItems, spec).ToList();

        // Assert
        result.Should().HaveCount(1);
        result[0].Id.Should().Be(existingItem.Id);
    }

    [Fact]
    public void GetQuery_WithNoFilters_ShouldReturnAllItems()
    {
        // Arrange - Note: InMemory db also has seed data from SeedDataInitializer
        var spec = new ClothingItemsWithTypesAndBrandsSpecification(new ClothingSpecParams
        {
            PageSize = 50
        });

        // Act
        var result = SpecificationEvaluator<ClothingItem>.GetQuery(_context.ClothingItems, spec).ToList();

        // Assert - at least our 3 seeded items
        result.Count.Should().BeGreaterThanOrEqualTo(3);
    }

    [Fact]
    public void GetQuery_WithSizeFilter_ShouldReturnMatchingItems()
    {
        // Arrange
        var spec = new ClothingItemsWithTypesAndBrandsSpecification(new ClothingSpecParams
        {
            Size = Size.S
        });

        // Act
        var result = SpecificationEvaluator<ClothingItem>.GetQuery(_context.ClothingItems, spec).ToList();

        // Assert
        result.Should().OnlyContain(i => i.Size == Size.S);
    }

    [Fact]
    public void GetQuery_WithBrandFilter_ShouldReturnOnlyItemsOfBrand()
    {
        // Arrange
        var spec = new ClothingItemsWithTypesAndBrandsSpecification(new ClothingSpecParams
        {
            ClothingBrandId = _brandId,
            PageSize = 50
        });

        // Act
        var result = SpecificationEvaluator<ClothingItem>.GetQuery(_context.ClothingItems, spec).ToList();

        // Assert
        result.Should().HaveCount(3);
        result.Should().OnlyContain(i => i.ClothingBrandId == _brandId);
    }

    public void Dispose() => _context.Dispose();
}
