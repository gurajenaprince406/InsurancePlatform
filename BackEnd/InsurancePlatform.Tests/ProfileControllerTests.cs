// Tests/ProfileControllerTests.cs
using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using InsurancePlatform.Controllers;
using InsurancePlatform.Data;
using InsurancePlatform.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class ProfileControllerTests
{
    private readonly Mock<AppDbContext> _dbContextMock;
    private readonly ProfileController _profileController;

    public ProfileControllerTests()
    {
        _dbContextMock = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
        _profileController = new ProfileController(_dbContextMock.Object);
    }

    [Fact]
    public async Task UpdatePersonalDetails_ReturnsOk_WhenUserExists()
    {
        // Arrange
        var userId = 1;
        var updateDto = new UpdateUserDto { Name = "Updated Name", PrimaryEmail = "updated@example.com" };
        var user = new User { UserId = userId, Name = "Original Name", PrimaryEmail = "original@example.com" };

        _dbContextMock.Setup(db => db.Users.FindAsync(userId)).ReturnsAsync(user);

        // Act
        var result = await _profileController.UpdatePersonalDetails(userId, updateDto);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task UpdatePersonalDetails_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = 1;
        var updateDto = new UpdateUserDto { Name = "Updated Name", PrimaryEmail = "updated@example.com" };

        _dbContextMock.Setup(db => db.Users.FindAsync(userId)).ReturnsAsync((User)null);

        // Act
        var result = await _profileController.UpdatePersonalDetails(userId, updateDto);

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}
