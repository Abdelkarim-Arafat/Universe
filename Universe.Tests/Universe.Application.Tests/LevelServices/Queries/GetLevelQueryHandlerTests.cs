using FluentAssertions;
using Moq;
using Universe.Application.Common;
using Universe.Application.LevelServices.Queries.GetLevel;
using Universe.Core.Contracts.Level;
using Universe.Core.Entities;
using Universe.Core.Errors;
using Universe.Core.Interfaces;
using Universe.Core.Interfaces.Repositories;

namespace Universe.Tests.Universe.Application.Tests.LevelServices.Queries.GetLevel;

public class GetLevelQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ILevelRepository> _levelRepositoryMock;
    private readonly Mock<ICacheService> _cacheServiceMock;

    public GetLevelQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _levelRepositoryMock = new Mock<ILevelRepository>();
        _cacheServiceMock = new Mock<ICacheService>();

        _unitOfWorkMock
            .Setup(x => x.LevelRepository)
            .Returns(_levelRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenLevelDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var command = new GetLevelQuery(
            Guid.NewGuid(),
            Guid.NewGuid());

        _levelRepositoryMock
            .Setup(x => x.GetByIdAsync(
                command.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Level?)null);

        _cacheServiceMock
            .Setup(x => x.GetOrCreateAsync(
                It.IsAny<string>(),
                It.IsAny<Func<Task<LevelResponse>>>(),
                It.IsAny<CancellationToken>(),
                It.IsAny<string[]?>()))
            .Returns(
                (
                    string key,
                    Func<Task<LevelResponse>> factory,
                    CancellationToken cancellationToken,
                    string[]? tags
                ) => factory());

        var handler = new GetLevelQueryHandler(
            _unitOfWorkMock.Object,
            _cacheServiceMock.Object);

        // Act
        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(LevelErrors.NotFound);

        _levelRepositoryMock.Verify(
            x => x.GetByIdAsync(
                command.Id,
                It.IsAny<CancellationToken>()),
            Times.Once);

        _cacheServiceMock.Verify(
            x => x.GetOrCreateAsync(
                LevelCacheKeys.ById(command.Id),
                It.IsAny<Func<Task<LevelResponse>>>(),
                It.IsAny<CancellationToken>(),
                It.IsAny<string[]?>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_WhenLevelExists_ShouldReturnLevel()
    {
        // Arrange
        var command = new GetLevelQuery(
            Guid.NewGuid(),
            Guid.NewGuid());

        var level = new Level
        {
            Id = command.Id,
            AcademicProgramId = command.ProgramId,
            Name = "Level 1",
            MinHours = 10,
            MaxHours = 20
        };

        _levelRepositoryMock
            .Setup(x => x.GetByIdAsync(
                command.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(level);

        _cacheServiceMock
            .Setup(x => x.GetOrCreateAsync(
                It.IsAny<string>(),
                It.IsAny<Func<Task<LevelResponse>>>(),
                It.IsAny<CancellationToken>(),
                It.IsAny<string[]?>()))
            .Returns(
                (
                    string key,
                    Func<Task<LevelResponse>> factory,
                    CancellationToken cancellationToken,
                    string[]? tags
                ) => factory());

        var handler = new GetLevelQueryHandler(
            _unitOfWorkMock.Object,
            _cacheServiceMock.Object);

        // Act
        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeNull();
        result.Value.Id.Should().Be(level.Id);
        result.Value.Name.Should().Be(level.Name);
        result.Value.MinHours.Should().Be(level.MinHours);
        result.Value.MaxHours.Should().Be(level.MaxHours);

        _levelRepositoryMock.Verify(
            x => x.GetByIdAsync(
                command.Id,
                It.IsAny<CancellationToken>()),
            Times.Once);

        _cacheServiceMock.Verify(
            x => x.GetOrCreateAsync(
                LevelCacheKeys.ById(command.Id),
                It.IsAny<Func<Task<LevelResponse>>>(),
                It.IsAny<CancellationToken>(),
                It.IsAny<string[]?>()),
            Times.Once);
    }
}