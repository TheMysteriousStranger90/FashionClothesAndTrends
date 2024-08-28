using FashionClothesAndTrends.Application.DTOs;
using FashionClothesAndTrends.Application.Services.Interfaces;
using FashionClothesAndTrends.WebAPI.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FashionClothesAndTrends.WebAPI.Tests;

public class AccountControllerTests
{
    private readonly Mock<IAuthService> _authServiceMock;
    private readonly AccountController _accountController;

    public AccountControllerTests()
    {
        _authServiceMock = new Mock<IAuthService>();
        _accountController = new AccountController(_authServiceMock.Object);
    }

    [Fact]
    public async Task Register_ShouldReturnOk_WhenRegistrationIsSuccessful()
    {
        // Arrange
        var registerDto = new RegisterDto
        {
            FirstName = "Dima",
            LastName = "Pupkin",
            Gender = "male",
            DateOfBirth = new DateOnly(1991, 2, 2),
            Email = "user93@example.com",
            Password = "Pa$$w0rd"
        };

        var userDto = new UserDto
        {
            Username = "dima93",
            Email = "user93@example.com",
            Token = "some-token"
        };

        _authServiceMock.Setup(service => service.RegisterAsync(registerDto))
            .ReturnsAsync(userDto);

        // Act
        var result = await _accountController.Register(registerDto);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult.Value.Should().BeEquivalentTo(userDto);
    }

    [Fact]
    public async Task Register_ShouldReturnBadRequest_WhenRegistrationFails()
    {
        // Arrange
        var registerDto = new RegisterDto
        {
            FirstName = "Dima",
            LastName = "Pupkin",
            Gender = "male",
            DateOfBirth = new DateOnly(1991, 2, 2),
            Email = "user93@example.com",
            Password = "Pa$$w0rd"
        };

        _authServiceMock.Setup(service => service.RegisterAsync(registerDto))
            .ThrowsAsync(new Exception("Registration failed"));

        // Act
        var result = await _accountController.Register(registerDto);

        // Assert
        result.Result.Should().BeOfType<BadRequestObjectResult>();
        var badRequestResult = result.Result as BadRequestObjectResult;
        badRequestResult.Value.Should().Be("Registration failed");
    }

    [Fact]
    public async Task Login_ShouldReturnOk_WhenLoginIsSuccessful()
    {
        // Arrange
        var loginDto = new LoginDto
        {
            Email = "user93@example.com",
            Password = "Pa$$w0rd"
        };

        var userDto = new UserDto
        {
            Username = "dima93",
            Email = "user93@example.com",
            Token = "some-token"
        };

        _authServiceMock.Setup(service => service.LoginAsync(loginDto))
            .ReturnsAsync(userDto);

        // Act
        var result = await _accountController.Login(loginDto);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult.Value.Should().BeEquivalentTo(userDto);
    }

    [Fact]
    public async Task Login_ShouldReturnUnauthorized_WhenLoginFails()
    {
        // Arrange
        var loginDto = new LoginDto
        {
            Email = "user93@example.com",
            Password = "Pa$$w0rd"
        };

        _authServiceMock.Setup(service => service.LoginAsync(loginDto))
            .ThrowsAsync(new Exception("Invalid credentials"));

        // Act
        var result = await _accountController.Login(loginDto);

        // Assert
        result.Result.Should().BeOfType<UnauthorizedObjectResult>();
        var unauthorizedResult = result.Result as UnauthorizedObjectResult;
        unauthorizedResult.Value.Should().Be("Invalid credentials");
    }

    [Fact]
    public async Task ConfirmEmail_ShouldReturnOk_WhenConfirmationIsSuccessful()
    {
        // Arrange
        var userName = "dima93";
        var token = "some-token";

        _authServiceMock.Setup(service => service.ConfirmEmailAsync(userName, token))
            .ReturnsAsync(true);

        // Act
        var result = await _accountController.ConfirmEmail(userName, token);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult.Value.Should().Be(true);
    }

    [Fact]
    public async Task ConfirmEmail_ShouldReturnBadRequest_WhenConfirmationFails()
    {
        // Arrange
        var userName = "dima93";
        var token = "some-token";

        _authServiceMock.Setup(service => service.ConfirmEmailAsync(userName, token))
            .ThrowsAsync(new Exception("Confirmation failed"));

        // Act
        var result = await _accountController.ConfirmEmail(userName, token);

        // Assert
        result.Result.Should().BeOfType<BadRequestObjectResult>();
        var badRequestResult = result.Result as BadRequestObjectResult;
        badRequestResult.Value.Should().Be("Confirmation failed");
    }

    [Fact]
    public async Task ResetPassword_ShouldReturnOk_WhenResetIsSuccessful()
    {
        // Arrange
        var userName = "dima93";
        var token = "some-token";
        var newPassword = "NewPa$$w0rd";

        _authServiceMock.Setup(service => service.ResetPasswordAsync(userName, token, newPassword))
            .ReturnsAsync(true);

        // Act
        var result = await _accountController.ResetPassword(userName, token, newPassword);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult.Value.Should().Be(true);
    }

    [Fact]
    public async Task ResetPassword_ShouldReturnBadRequest_WhenResetFails()
    {
        // Arrange
        var userName = "dima93";
        var token = "some-token";
        var newPassword = "NewPa$$w0rd";

        _authServiceMock.Setup(service => service.ResetPasswordAsync(userName, token, newPassword))
            .ThrowsAsync(new Exception("Reset failed"));

        // Act
        var result = await _accountController.ResetPassword(userName, token, newPassword);

        // Assert
        result.Result.Should().BeOfType<BadRequestObjectResult>();
        var badRequestResult = result.Result as BadRequestObjectResult;
        badRequestResult.Value.Should().Be("Reset failed");
    }

    [Fact]
    public async Task ChangePassword_ShouldReturnOk_WhenChangeIsSuccessful()
    {
        // Arrange
        var userName = "dima93";
        var currentPassword = "Pa$$w0rd";
        var newPassword = "NewPa$$w0rd";

        _authServiceMock.Setup(service => service.ChangePasswordAsync(userName, currentPassword, newPassword))
            .ReturnsAsync(true);

        // Act
        var result = await _accountController.ChangePassword(userName, currentPassword, newPassword);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult.Value.Should().Be(true);
    }

    [Fact]
    public async Task ChangePassword_ShouldReturnBadRequest_WhenChangeFails()
    {
        // Arrange
        var userName = "dima93";
        var currentPassword = "Pa$$w0rd";
        var newPassword = "NewPa$$w0rd";

        _authServiceMock.Setup(service => service.ChangePasswordAsync(userName, currentPassword, newPassword))
            .ThrowsAsync(new Exception("Change failed"));

        // Act
        var result = await _accountController.ChangePassword(userName, currentPassword, newPassword);

        // Assert
        result.Result.Should().BeOfType<BadRequestObjectResult>();
        var badRequestResult = result.Result as BadRequestObjectResult;
        badRequestResult.Value.Should().Be("Change failed");
    }

    [Fact]
    public async Task CheckEmailExistsAsync_ShouldReturnOk_WhenEmailExists()
    {
        // Arrange
        var email = "user93@example.com";

        _authServiceMock.Setup(service => service.CheckEmailExistsAsync(email))
            .ReturnsAsync(true);

        // Act
        var result = await _accountController.CheckEmailExistsAsync(email);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult.Value.Should().Be(true);
    }

    [Fact]
    public async Task CheckEmailExistsAsync_ShouldReturnBadRequest_WhenCheckFails()
    {
        // Arrange
        var email = "user93@example.com";

        _authServiceMock.Setup(service => service.CheckEmailExistsAsync(email))
            .ThrowsAsync(new Exception("Check failed"));

        // Act
        var result = await _accountController.CheckEmailExistsAsync(email);

        // Assert
        result.Result.Should().BeOfType<BadRequestObjectResult>();
        var badRequestResult = result.Result as BadRequestObjectResult;
        badRequestResult.Value.Should().Be("Check failed");
    }

    [Fact]
    public async Task CheckUserNameExistsAsync_ShouldReturnOk_WhenUserNameExists()
    {
        // Arrange
        var userName = "dima93";

        _authServiceMock.Setup(service => service.CheckUserNameExistsAsync(userName))
            .ReturnsAsync(true);

        // Act
        var result = await _accountController.CheckUserNameExistsAsync(userName);

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>();
        var okResult = result.Result as OkObjectResult;
        okResult.Value.Should().Be(true);
    }

    [Fact]
    public async Task CheckUserNameExistsAsync_ShouldReturnBadRequest_WhenCheckFails()
    {
        // Arrange
        var userName = "dima93";

        _authServiceMock.Setup(service => service.CheckUserNameExistsAsync(userName))
            .ThrowsAsync(new Exception("Check failed"));

        // Act
        var result = await _accountController.CheckUserNameExistsAsync(userName);

        // Assert
        result.Result.Should().BeOfType<BadRequestObjectResult>();
        var badRequestResult = result.Result as BadRequestObjectResult;
        badRequestResult.Value.Should().Be("Check failed");
    }
}