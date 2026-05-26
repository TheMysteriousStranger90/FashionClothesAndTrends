using AutoMapper;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Exceptions;
using FashionClothesAndTrends.Application.Services;
using FashionClothesAndTrends.Application.UoW;
using FashionClothesAndTrends.Domain.Entities;
using FashionClothesAndTrends.Domain.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace FashionClothesAndTrends.Application.Tests.Services;

public class CommentServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly CommentService _sut;

    public CommentServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();

        var store = new Mock<IUserStore<User>>();
        _userManagerMock = new Mock<UserManager<User>>(
            store.Object, null!, null!, null!, null!, null!, null!, null!, null!);

        _unitOfWorkMock.Setup(u => u.UserManager).Returns(_userManagerMock.Object);

        _sut = new CommentService(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    private static User CreateUser(string id = "user-1") => new User
    {
        Id = id,
        UserName = $"user_{id}",
        Email = $"{id}@example.com",
        FirstName = "Test",
        LastName = "User",
        Gender = "Male",
        DateOfBirth = new DateOnly(1990, 1, 1)
    };

    [Fact]
    public async Task AddCommentAsync_WhenCommentDtoIsNull_ShouldThrowArgumentNullException()
    {
        // Act
        Func<Task> act = async () => await _sut.AddCommentAsync(null!);

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task AddCommentAsync_WhenUserNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        var dto = new CommentDto { Text = "Hi", UserId = "missing-user" };
        _userManagerMock.Setup(m => m.FindByIdAsync(dto.UserId)).ReturnsAsync((User?)null);

        // Act
        Func<Task> act = async () => await _sut.AddCommentAsync(dto);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage("*User not found*");
    }

    [Fact]
    public async Task RemoveCommentAsync_WhenOwnerDeletes_ShouldRemoveAndSave()
    {
        // Arrange
        var userId = "user-1";
        var user = CreateUser(userId);
        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            Text = "Old comment",
            UserId = userId,
            ClothingItemId = Guid.NewGuid()
        };

        _unitOfWorkMock.Setup(u => u.CommentRepository.GetByIdAsync(comment.Id)).ReturnsAsync(comment);
        _unitOfWorkMock.Setup(u => u.UserRepository.GetUserByIdAsync(userId)).ReturnsAsync(user);
        _userManagerMock.Setup(m => m.IsInRoleAsync(user, "Administrator")).ReturnsAsync(false);
        _unitOfWorkMock.Setup(u => u.CommentRepository.RemoveCommentAsync(comment)).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.SaveAsync()).ReturnsAsync(1);

        // Act
        await _sut.RemoveCommentAsync(comment.Id, userId);

        // Assert
        _unitOfWorkMock.Verify(u => u.CommentRepository.RemoveCommentAsync(comment), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task RemoveCommentAsync_WhenAdminDeletes_ShouldSucceed()
    {
        // Arrange
        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            Text = "Some comment",
            UserId = "other-user",
            ClothingItemId = Guid.NewGuid()
        };
        var admin = CreateUser("admin-user");

        _unitOfWorkMock.Setup(u => u.CommentRepository.GetByIdAsync(comment.Id)).ReturnsAsync(comment);
        _unitOfWorkMock.Setup(u => u.UserRepository.GetUserByIdAsync("admin-user")).ReturnsAsync(admin);
        _userManagerMock.Setup(m => m.IsInRoleAsync(admin, "Administrator")).ReturnsAsync(true);
        _unitOfWorkMock.Setup(u => u.CommentRepository.RemoveCommentAsync(comment)).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.SaveAsync()).ReturnsAsync(1);

        // Act
        await _sut.RemoveCommentAsync(comment.Id, "admin-user");

        // Assert
        _unitOfWorkMock.Verify(u => u.CommentRepository.RemoveCommentAsync(comment), Times.Once);
    }

    [Fact]
    public async Task RemoveCommentAsync_WhenCommentNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        _unitOfWorkMock.Setup(u => u.CommentRepository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Comment?)null);

        // Act
        Func<Task> act = async () => await _sut.RemoveCommentAsync(Guid.NewGuid(), "user-1");

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage("*Comment not found*");
    }

    [Fact]
    public async Task RemoveCommentAsync_WhenNonOwnerNonAdmin_ShouldThrowUnauthorizedAccessException()
    {
        // Arrange
        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            Text = "Someone else comment",
            UserId = "owner-user",
            ClothingItemId = Guid.NewGuid()
        };
        var requester = CreateUser("requester-user");

        _unitOfWorkMock.Setup(u => u.CommentRepository.GetByIdAsync(comment.Id)).ReturnsAsync(comment);
        _unitOfWorkMock.Setup(u => u.UserRepository.GetUserByIdAsync("requester-user")).ReturnsAsync(requester);
        _userManagerMock.Setup(m => m.IsInRoleAsync(requester, "Administrator")).ReturnsAsync(false);

        // Act
        Func<Task> act = async () => await _sut.RemoveCommentAsync(comment.Id, "requester-user");

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>();
    }

    [Fact]
    public async Task GetCommentsForClothingItemAsync_WhenCommentsExist_ShouldReturnMappedDtos()
    {
        // Arrange
        var clothingItemId = Guid.NewGuid();
        var comments = new List<Comment>
        {
            new() { Id = Guid.NewGuid(), Text = "Great!", UserId = "u1", ClothingItemId = clothingItemId },
            new() { Id = Guid.NewGuid(), Text = "Nice!", UserId = "u2", ClothingItemId = clothingItemId }
        };
        var dtos = comments.Select(c => new CommentDto { Text = c.Text, UserId = c.UserId }).ToList();

        _unitOfWorkMock.Setup(u => u.CommentRepository.GetCommentsForClothingItemIdAsync(clothingItemId))
            .ReturnsAsync(comments);
        _mapperMock.Setup(m => m.Map<IEnumerable<CommentDto>>(comments)).Returns(dtos);

        // Act
        var result = await _sut.GetCommentsForClothingItemAsync(clothingItemId);

        // Assert
        result.Should().HaveCount(2);
    }
}
