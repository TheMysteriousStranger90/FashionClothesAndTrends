using AutoMapper;
using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Exceptions;
using FashionClothesAndTrends.Application.Services;
using FashionClothesAndTrends.Application.UoW;
using FashionClothesAndTrends.Domain.Entities;
using FluentAssertions;
using Moq;

namespace FashionClothesAndTrends.Application.Tests.Services;

public class LikeDislikeServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly LikeDislikeService _sut;

    public LikeDislikeServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();
        _sut = new LikeDislikeService(_unitOfWorkMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task AddLikeDislikeAsync_WhenDtoIsNull_ShouldThrowArgumentNullException()
    {
        // Act
        Func<Task> act = async () => await _sut.AddLikeDislikeAsync(null!);

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task RemoveLikeDislikeAsync_WhenEntityExists_ShouldDeleteAndSave()
    {
        // Arrange
        var likeId = Guid.NewGuid();
        var entity = new LikeDislike { Id = likeId, IsLike = true, CommentId = Guid.NewGuid(), UserId = "user-1" };

        _unitOfWorkMock.Setup(u => u.LikeDislikeRepository.GetByIdAsync(likeId)).ReturnsAsync(entity);
        _unitOfWorkMock.Setup(u => u.SaveAsync()).ReturnsAsync(1);

        // Act
        await _sut.RemoveLikeDislikeAsync(likeId);

        // Assert
        _unitOfWorkMock.Verify(u => u.LikeDislikeRepository.Delete(entity), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
    }

    [Fact]
    public async Task RemoveLikeDislikeAsync_WhenEntityNotFound_ShouldThrowNotFoundException()
    {
        // Arrange
        _unitOfWorkMock.Setup(u => u.LikeDislikeRepository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((LikeDislike?)null);

        // Act
        Func<Task> act = async () => await _sut.RemoveLikeDislikeAsync(Guid.NewGuid());

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage("*Like/Dislike not found*");
    }

    [Fact]
    public async Task GetLikesDislikesByCommentIdAsync_WhenDataExists_ShouldReturnMappedDtos()
    {
        // Arrange
        var commentId = Guid.NewGuid();
        var entities = new List<LikeDislike>
        {
            new() { IsLike = true, CommentId = commentId, UserId = "user-1" },
            new() { IsLike = false, CommentId = commentId, UserId = "user-2" }
        };
        var dtos = entities.Select(e => new LikeDislikeDto { IsLike = e.IsLike, CommentId = e.CommentId }).ToList();

        _unitOfWorkMock.Setup(u => u.LikeDislikeRepository.GetLikesDislikesByCommentIdAsync(commentId))
            .ReturnsAsync(entities);
        _mapperMock.Setup(m => m.Map<IEnumerable<LikeDislikeDto>>(entities)).Returns(dtos);

        // Act
        var result = await _sut.GetLikesDislikesByCommentIdAsync(commentId);

        // Assert
        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task CountLikesAsync_ShouldReturnCorrectCount()
    {
        // Arrange
        var commentId = Guid.NewGuid();
        _unitOfWorkMock.Setup(u => u.LikeDislikeRepository.CountLikesAsync(commentId)).ReturnsAsync(5);

        // Act
        var result = await _sut.CountLikesAsync(commentId);

        // Assert
        result.Should().Be(5);
    }

    [Fact]
    public async Task CountDislikesAsync_ShouldReturnCorrectCount()
    {
        // Arrange
        var commentId = Guid.NewGuid();
        _unitOfWorkMock.Setup(u => u.LikeDislikeRepository.CountDislikesAsync(commentId)).ReturnsAsync(3);

        // Act
        var result = await _sut.CountDislikesAsync(commentId);

        // Assert
        result.Should().Be(3);
    }
}
