using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FashionClothesAndTrends.Application.Services;
using FashionClothesAndTrends.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;

namespace FashionClothesAndTrends.Application.Tests.Services;

public class TokenServiceTests
{
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly IConfiguration _configuration;
    private readonly TokenService _sut;

    public TokenServiceTests()
    {
        var store = new Mock<IUserStore<User>>();
        _userManagerMock = new Mock<UserManager<User>>(
            store.Object, null!, null!, null!, null!, null!, null!, null!, null!);

        var inMemorySettings = new Dictionary<string, string?>
        {
            ["Token:Key"] = "super_secret_key_that_is_longer_than_64_chars_total_ffffffffffffff",
            ["Token:Issuer"] = "https://localhost:5001",
            ["Token:Audience"] = "https://localhost:4200"
        };

        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _sut = new TokenService(_configuration, _userManagerMock.Object);
    }

    private static User CreateTestUser(string id = "user-id-1", string userName = "testuser", string email = "test@example.com") =>
        new User
        {
            Id = id,
            UserName = userName,
            Email = email,
            FirstName = "Test",
            LastName = "User",
            Gender = "Male",
            DateOfBirth = new DateOnly(1990, 1, 1)
        };

    [Fact]
    public async Task CreateToken_ShouldReturnNonEmptyToken()
    {
        // Arrange
        var user = CreateTestUser();
        _userManagerMock.Setup(m => m.GetRolesAsync(user)).ReturnsAsync(["Member"]);

        // Act
        var token = await _sut.CreateToken(user);

        // Assert
        token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task CreateToken_ShouldContainEmailAndUsernameClaims()
    {
        // Arrange
        var user = CreateTestUser();
        _userManagerMock.Setup(m => m.GetRolesAsync(user)).ReturnsAsync(["Member"]);

        // Act
        var token = await _sut.CreateToken(user);

        // Assert
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        jwtToken.Claims.Should().Contain(c =>
            c.Type == JwtRegisteredClaimNames.Email && c.Value == user.Email);
        jwtToken.Claims.Should().Contain(c =>
            c.Type == JwtRegisteredClaimNames.UniqueName && c.Value == user.UserName);
    }

    [Fact]
    public async Task CreateToken_ShouldIncludeAllRoles()
    {
        // Arrange
        var user = CreateTestUser(id: "user-id-2", userName: "adminuser", email: "admin@example.com");
        _userManagerMock.Setup(m => m.GetRolesAsync(It.IsAny<User>()))
            .ReturnsAsync((IList<string>)["Administrator", "Member"]);

        // Act
        var token = await _sut.CreateToken(user);

        // Assert
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var roleClaims = jwtToken.Claims
            .Where(c => c.Type == "role")
            .Select(c => c.Value)
            .ToList();

        roleClaims.Should().Contain("Administrator");
        roleClaims.Should().Contain("Member");
    }

    [Fact]
    public async Task CreateToken_WhenUserHasNoRoles_ShouldStillCreateToken()
    {
        // Arrange
        var user = CreateTestUser();
        _userManagerMock.Setup(m => m.GetRolesAsync(user)).ReturnsAsync([]);

        // Act
        var token = await _sut.CreateToken(user);

        // Assert
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        jwtToken.Claims.Where(c => c.Type == "role").Should().BeEmpty();
    }

    [Fact]
    public async Task CreateToken_ShouldHaveCorrectIssuer()
    {
        // Arrange
        var user = CreateTestUser();
        _userManagerMock.Setup(m => m.GetRolesAsync(user)).ReturnsAsync([]);

        // Act
        var token = await _sut.CreateToken(user);

        // Assert
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        jwtToken.Issuer.Should().Be("https://localhost:5001");
    }

    [Fact]
    public async Task CreateToken_ShouldExpireApproximatelyInSevenDays()
    {
        // Arrange
        var user = CreateTestUser();
        _userManagerMock.Setup(m => m.GetRolesAsync(user)).ReturnsAsync([]);

        // Act
        var token = await _sut.CreateToken(user);

        // Assert
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        jwtToken.ValidTo.Should().BeCloseTo(DateTime.UtcNow.AddDays(7), TimeSpan.FromMinutes(5));
    }

    [Fact]
    public async Task CreateToken_ShouldContainNameIdClaim()
    {
        // Arrange
        var user = CreateTestUser(id: "specific-id-123");
        _userManagerMock.Setup(m => m.GetRolesAsync(user)).ReturnsAsync([]);

        // Act
        var token = await _sut.CreateToken(user);

        // Assert
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        jwtToken.Claims.Should().Contain(c =>
            c.Type == JwtRegisteredClaimNames.NameId && c.Value == "specific-id-123");
    }
}
