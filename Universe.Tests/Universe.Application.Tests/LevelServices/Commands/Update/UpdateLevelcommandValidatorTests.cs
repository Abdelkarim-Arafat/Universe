using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.LevelServices.Commands.Create;
using Universe.Application.LevelServices.Commands.Update;

namespace Universe.Tests.Universe.Application.Tests.LevelServices.Commands.Update;

public class UpdateLevelcommandValidatorTests
{
    [Fact]
    public void Validate_WhenCommandIsValid_ShouldBeValid()
    {
        // Arrange
        var command = new UpdateLevelCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Level 1",
            10,
            20
        );

        var validator = new UpdateLevelCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Validate_WhenNameIsEmpty_ShouldBeInValid(string name)
    {
        // Arrange
        var command = new UpdateLevelCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            name,
            10,
            20
        );

        var validator = new UpdateLevelCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("This is a level name that contains more than fifty characters")]
    public void Validate_WhenNameExceedsMaximumLength_ShouldBeInvalid(string name)
    {
        // Arrange
        var command = new UpdateLevelCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            name,
            10,
            20
        );

        var validator = new UpdateLevelCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
    }

    [Fact]
    public void Validate_WhenMinHoursIsNegative_ShouldBeInvalid()
    {
        // Arrange
        var command = new UpdateLevelCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Level 1",
            -1,
            20
        );

        var validator = new UpdateLevelCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData(10, 20)]
    [InlineData(19, 20)]
    [InlineData(0, 20)]
    public void Validate_WhenMinHoursIsLessThanMaxHours_ShouldBeValid(int minHours, int maxHours)
    {
        // Arrange
        var command = new UpdateLevelCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Level 1",
            minHours,
            maxHours
        );

        var validator = new UpdateLevelCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData(20, 20)]
    [InlineData(25, 20)]
    [InlineData(100, 50)]
    public void Validate_WhenMinHoursIsGreaterThanOrEqualToMaxHours_ShouldBeInvalid(int minHours, int maxHours)
    {
        // Arrange
        var command = new UpdateLevelCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Level 1",
            minHours,
            maxHours
        );

        var validator = new UpdateLevelCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        Assert.False(result.IsValid);
    }
}
