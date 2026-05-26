using System.Text.Json;
using FashionClothesAndTrends.Application.Services;
using FluentAssertions;
using Moq;
using StackExchange.Redis;

namespace FashionClothesAndTrends.Application.Tests.Services;

public class ResponseCacheServiceTests
{
    private readonly Mock<IDatabase> _mockDatabase;
    private readonly ResponseCacheService _sut;

    public ResponseCacheServiceTests()
    {
        _mockDatabase = new Mock<IDatabase>();
        var mockMultiplexer = new Mock<IConnectionMultiplexer>();
        mockMultiplexer
            .Setup(m => m.GetDatabase(It.IsAny<int>(), It.IsAny<object?>()))
            .Returns(_mockDatabase.Object);
        _sut = new ResponseCacheService(mockMultiplexer.Object);
    }

    [Fact]
    public async Task CacheResponseAsync_WithValidResponse_ShouldCallStringSetAsync()
    {
        // Arrange
        var cacheKey = "test:key";
        var response = new { Name = "TestItem", Price = 100 };
        var ttl = TimeSpan.FromMinutes(5);

        _mockDatabase
            .Setup(d => d.StringSetAsync(
                It.IsAny<RedisKey>(),
                It.IsAny<RedisValue>(),
                It.IsAny<TimeSpan?>(),
                It.IsAny<bool>(),
                It.IsAny<When>(),
                It.IsAny<CommandFlags>()))
            .ReturnsAsync(true);

        // Act
        await _sut.CacheResponseAsync(cacheKey, response, ttl);

        // Assert
        _mockDatabase.Verify(d => d.StringSetAsync(
            It.Is<RedisKey>(k => k == cacheKey),
            It.Is<RedisValue>(v => v.ToString().Contains("TestItem")),
            ttl,
            It.IsAny<bool>(),
            It.IsAny<When>(),
            It.IsAny<CommandFlags>()), Times.Once);
    }

    [Fact]
    public async Task CacheResponseAsync_WithNullResponse_ShouldNotCallRedis()
    {
        // Arrange & Act
        await _sut.CacheResponseAsync("test:key", null!, TimeSpan.FromMinutes(5));

        // Assert
        _mockDatabase.Verify(d => d.StringSetAsync(
            It.IsAny<RedisKey>(),
            It.IsAny<RedisValue>(),
            It.IsAny<TimeSpan?>(),
            It.IsAny<bool>(),
            It.IsAny<When>(),
            It.IsAny<CommandFlags>()), Times.Never);
    }

    [Fact]
    public async Task CacheResponseAsync_ShouldSerializeWithCamelCaseNaming()
    {
        // Arrange
        RedisValue capturedValue = default;
        _mockDatabase
            .Setup(d => d.StringSetAsync(
                It.IsAny<RedisKey>(),
                It.IsAny<RedisValue>(),
                It.IsAny<TimeSpan?>(),
                It.IsAny<bool>(),
                It.IsAny<When>(),
                It.IsAny<CommandFlags>()))
            .Callback<RedisKey, RedisValue, TimeSpan?, bool, When, CommandFlags>((_, val, _, _, _, _) =>
                capturedValue = val)
            .ReturnsAsync(true);

        var response = new { ItemName = "Test", TotalPrice = 99.99 };

        // Act
        await _sut.CacheResponseAsync("key", response, TimeSpan.FromSeconds(60));

        // Assert
        var json = capturedValue.ToString();
        json.Should().Contain("itemName");
        json.Should().Contain("Test");
    }

    [Fact]
    public async Task GetCachedResponse_WhenKeyExists_ShouldReturnCachedJson()
    {
        // Arrange
        var cacheKey = "test:key";
        var expectedJson = JsonSerializer.Serialize(new { Name = "TestItem" });
        _mockDatabase
            .Setup(d => d.StringGetAsync(It.Is<RedisKey>(k => k == cacheKey), It.IsAny<CommandFlags>()))
            .ReturnsAsync(new RedisValue(expectedJson));

        // Act
        var result = await _sut.GetCachedResponse(cacheKey);

        // Assert
        result.Should().Be(expectedJson);
    }

    [Fact]
    public async Task GetCachedResponse_WhenKeyDoesNotExist_ShouldReturnNull()
    {
        // Arrange
        _mockDatabase
            .Setup(d => d.StringGetAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
            .ReturnsAsync(RedisValue.Null);

        // Act
        var result = await _sut.GetCachedResponse("missing:key");

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCachedResponse_ShouldPassCorrectKeyToRedis()
    {
        // Arrange
        var cacheKey = "api/products?page=1";
        _mockDatabase
            .Setup(d => d.StringGetAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
            .ReturnsAsync(RedisValue.Null);

        // Act
        await _sut.GetCachedResponse(cacheKey);

        // Assert
        _mockDatabase.Verify(d => d.StringGetAsync(
            It.Is<RedisKey>(k => k == cacheKey),
            It.IsAny<CommandFlags>()), Times.Once);
    }
}
