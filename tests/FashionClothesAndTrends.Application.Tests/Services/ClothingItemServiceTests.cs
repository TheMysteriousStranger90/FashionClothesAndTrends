using AutoMapper;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Exceptions;
using FashionClothesAndTrends.Application.Services;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.Application.UoW;
using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Interfaces;
using FashionClothesAndTrends.Domain.Specifications;
using FashionClothesAndTrends.Domain.Specifications.Interfaces;
using FluentAssertions;
using Moq;

namespace FashionClothesAndTrends.Application.Tests.Services;

public class ClothingItemServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IPhotoService> _photoServiceMock;
    private readonly ClothingItemService _sut;

    public ClothingItemServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _photoServiceMock = new Mock<IPhotoService>();
        _sut = new ClothingItemService(_unitOfWorkMock.Object, _mapperMock.Object, _photoServiceMock.Object);
    }

    private static ClothingItem CreateClothingItem(string name = "Test Shirt") => new()
    {
        Id = Guid.NewGuid(),
        Name = name,
        Description = "Test description",
        Price = 99.99m,
        IsInStock = true,
        ClothingBrand = new ClothingBrand { Name = "Test Brand" },
        ClothingItemPhotos = []
    };

    [Fact]
    public async Task GetClothingItemById_WhenItemExists_ShouldReturnMappedDto()
    {
        // Arrange
        var item = CreateClothingItem();
        var expectedDto = new ClothingItemDto { Id = item.Id, Name = item.Name };
        var genericRepo = new Mock<IGenericRepository<ClothingItem>>();
        genericRepo
            .Setup(r => r.GetEntityWithSpec(It.IsAny<ISpecification<ClothingItem>>()))
            .ReturnsAsync(item);
        _unitOfWorkMock.Setup(u => u.GenericRepository<ClothingItem>()).Returns(genericRepo.Object);
        _mapperMock.Setup(m => m.Map<ClothingItem, ClothingItemDto>(item)).Returns(expectedDto);

        // Act
        var result = await _sut.GetClothingItemById(item.Id);

        // Assert
        result.Should().Be(expectedDto);
    }

    [Fact]
    public async Task GetClothingItemById_WhenItemNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        var genericRepo = new Mock<IGenericRepository<ClothingItem>>();
        genericRepo
            .Setup(r => r.GetEntityWithSpec(It.IsAny<ISpecification<ClothingItem>>()))
            .ReturnsAsync((ClothingItem?)null);
        _unitOfWorkMock.Setup(u => u.GenericRepository<ClothingItem>()).Returns(genericRepo.Object);

        // Act
        Func<Task> act = async () => await _sut.GetClothingItemById(Guid.NewGuid());

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage("*ClothingItem not found*");
    }

    [Fact]
    public async Task GetClothingBrands_ShouldReturnAllMappedBrands()
    {
        // Arrange
        var brands = new List<ClothingBrand>
        {
            new() { Id = Guid.NewGuid(), Name = "Brand A" },
            new() { Id = Guid.NewGuid(), Name = "Brand B" }
        };
        var brandDtos = brands.Select(b => new ClothingBrandDto { Name = b.Name }).ToList();
        var genericRepo = new Mock<IGenericRepository<ClothingBrand>>();
        genericRepo.Setup(r => r.ListAllAsync()).ReturnsAsync(brands);
        _unitOfWorkMock.Setup(u => u.GenericRepository<ClothingBrand>()).Returns(genericRepo.Object);
        _mapperMock.Setup(m => m.Map<IReadOnlyList<ClothingBrandDto>>(brands)).Returns(brandDtos);

        // Act
        var result = await _sut.GetClothingBrands();

        // Assert
        result.Should().HaveCount(2);
        result.Should().ContainSingle(b => b.Name == "Brand A");
    }

    [Fact]
    public async Task AddClothingBrandAsync_ShouldAddEntityAndSave()
    {
        // Arrange
        var dto = new CreateClothingBrandDto { Name = "NewBrand", Description = "Desc" };
        var brand = new ClothingBrand { Name = "NewBrand" };
        var genericRepo = new Mock<IGenericRepository<ClothingBrand>>();
        _mapperMock.Setup(m => m.Map<ClothingBrand>(dto)).Returns(brand);
        _unitOfWorkMock.Setup(u => u.GenericRepository<ClothingBrand>()).Returns(genericRepo.Object);
        _unitOfWorkMock.Setup(u => u.SaveAsync()).ReturnsAsync(1);

        // Act
        await _sut.AddClothingBrandAsync(dto);

        // Assert
        genericRepo.Verify(r => r.Add(brand), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task AddPhotoByClothingItem_WhenItemExists_ShouldAddPhotoAndReturnDto()
    {
        // Arrange
        var item = CreateClothingItem();
        var photoResult = new PhotoUploadResultDto
        {
            SecureUrl = "https://cdn.example.com/photo.jpg",
            PublicId = "pub-id-123"
        };
        var expectedDto = new ClothingItemPhotoDto { Url = photoResult.SecureUrl };

        _unitOfWorkMock.Setup(u => u.ClothingItemRepository.GetClothingByIdAsync(item.Id))
            .ReturnsAsync(item);
        _unitOfWorkMock.Setup(u => u.SaveAsync()).ReturnsAsync(1);
        _mapperMock.Setup(m => m.Map<ClothingItemPhotoDto>(It.IsAny<ClothingItemPhoto>()))
            .Returns(expectedDto);

        // Act
        var result = await _sut.AddPhotoByClothingItem(photoResult, item.Id);

        // Assert
        result.Url.Should().Be(photoResult.SecureUrl);
        item.ClothingItemPhotos.Should().HaveCount(1);
        _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task AddPhotoByClothingItem_WhenItemNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        _unitOfWorkMock.Setup(u => u.ClothingItemRepository.GetClothingByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((ClothingItem?)null);

        // Act
        Func<Task> act = async () => await _sut.AddPhotoByClothingItem(
            new PhotoUploadResultDto { SecureUrl = "url", PublicId = "id" }, Guid.NewGuid());

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage("*Clothing item not found*");
    }

    [Fact]
    public async Task SetMainClothingItemPhotoByClothingItem_ShouldUpdateMainPhoto()
    {
        // Arrange
        var item = CreateClothingItem();
        var oldMain = new ClothingItemPhoto { Id = Guid.NewGuid(), IsMain = true, Url = "old.jpg", PublicId = "old-pub-id" };
        var newMain = new ClothingItemPhoto { Id = Guid.NewGuid(), IsMain = false, Url = "new.jpg", PublicId = "new-pub-id" };
        item.ClothingItemPhotos.Add(oldMain);
        item.ClothingItemPhotos.Add(newMain);

        _unitOfWorkMock.Setup(u => u.ClothingItemRepository.GetClothingByIdAsync(item.Id))
            .ReturnsAsync(item);
        _unitOfWorkMock.Setup(u => u.SaveAsync()).ReturnsAsync(1);

        // Act
        await _sut.SetMainClothingItemPhotoByClothingItem(newMain.Id, item.Id);

        // Assert
        oldMain.IsMain.Should().BeFalse();
        newMain.IsMain.Should().BeTrue();
    }

    [Fact]
    public async Task GetClothingItems_ShouldReturnPaginatedResult()
    {
        // Arrange
        var specParams = new ClothingSpecParams { PageIndex = 1, PageSize = 3 };
        var items = new List<ClothingItem> { CreateClothingItem("Shirt 1"), CreateClothingItem("Shirt 2") };
        var dtos = items.Select(i => new ClothingItemDto { Name = i.Name }).ToList();

        var genericRepo = new Mock<IGenericRepository<ClothingItem>>();
        genericRepo.Setup(r => r.CountAsync(It.IsAny<ISpecification<ClothingItem>>())).ReturnsAsync(2);
        genericRepo.Setup(r => r.ListAsync(It.IsAny<ISpecification<ClothingItem>>()))
            .ReturnsAsync(items);
        _unitOfWorkMock.Setup(u => u.GenericRepository<ClothingItem>()).Returns(genericRepo.Object);
        _mapperMock.Setup(m => m.Map<IReadOnlyList<ClothingItem>, IReadOnlyList<ClothingItemDto>>(It.IsAny<IReadOnlyList<ClothingItem>>()))
            .Returns(dtos);

        // Act
        var result = await _sut.GetClothingItems(specParams);

        // Assert
        result.Count.Should().Be(2);
        result.Data.Should().HaveCount(2);
    }
}
