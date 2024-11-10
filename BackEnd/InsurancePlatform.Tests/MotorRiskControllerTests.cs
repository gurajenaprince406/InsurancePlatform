// Tests/MotorRiskControllerTests.cs
using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using InsurancePlatform.Controllers;
using InsurancePlatform.Data;
using InsurancePlatform.Models;
using InsurancePlatform.Models.DTOs;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class MotorRiskControllerTests
{
    private readonly Mock<AppDbContext> _dbContextMock;
    private readonly MotorRiskController _motorRiskController;

    public MotorRiskControllerTests()
    {
        _dbContextMock = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
        _motorRiskController = new MotorRiskController(_dbContextMock.Object);
    }

    [Fact]
    public async Task AddMotorRisk_ReturnsOk_WhenMotorRiskIsCreatedSuccessfully()
    {
        // Arrange
        var motorRiskDto = new MotorRiskDto
        {
            VehicleMake = "Audi",
            VehicleModel = "A3 SPORTBACK 2.0TFSI",
            YearOfManufacture = 2022,
            SumInsured = 25000,
            Rate = 0.02m
        };
        var quoteId = 1;

        _dbContextMock.Setup(db => db.Quotes.FindAsync(quoteId)).ReturnsAsync(new Quote { QuoteId = quoteId });
        _dbContextMock.Setup(db => db.MotorRisks.Add(It.IsAny<MotorRisk>())).Verifiable();
        _dbContextMock.Setup(db => db.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _motorRiskController.AddMotorRisk(motorRiskDto, quoteId);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task AddMotorRisk_ReturnsNotFound_WhenQuoteDoesNotExist()
    {
        // Arrange
        var motorRiskDto = new MotorRiskDto
        {
            VehicleMake = "Audi",
            VehicleModel = "A3 SPORTBACK 2.0TFSI",
            YearOfManufacture = 2022,
            SumInsured = 25000,
            Rate = 0.02m
        };
        var quoteId = 1;

        _dbContextMock.Setup(db => db.Quotes.FindAsync(quoteId)).ReturnsAsync((Quote)null);

        // Act
        var result = await _motorRiskController.AddMotorRisk(motorRiskDto, quoteId);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>()
              .Which.Value.Should().Be($"Quote with ID {quoteId} not found.");
    }
}

