using FluentAssertions;
using Moq;
using Universe.Application.Common;
using Universe.Application.LevelServices.Commands.Remove;
using Universe.Core.Entities;
using Universe.Core.Errors;
using Universe.Core.Interfaces;
using Universe.Core.Interfaces.Repositories;

namespace Universe.Tests.Universe.Application.Tests.LevelServices.Commands.Remove;

public class RemoveLevelCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ILevelRepository> _levelRepositoryMock;
    private readonly Mock<IGenericRepository<Level>> _genericLevelRepositoryMock;
    private readonly Mock<ICacheService> _cacheServiceMock;
    public RemoveLevelCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _levelRepositoryMock = new Mock<ILevelRepository>();
        _genericLevelRepositoryMock = new Mock<IGenericRepository<Level>>();
        _cacheServiceMock = new Mock<ICacheService>();

        _unitOfWorkMock
            .Setup(x => x.LevelRepository)
            .Returns(_levelRepositoryMock.Object);

        _unitOfWorkMock
            .Setup(x => x.Repository<Level>())
            .Returns(_genericLevelRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenLevelDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var command = new RemoveLevelCommand(
            Guid.NewGuid(),
            Guid.NewGuid());

        _levelRepositoryMock
            .Setup(x => x.GetByIdAsync(
                command.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Level?)null);

        var handler = new RemoveLevelCommandHandler(
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

        _genericLevelRepositoryMock.Verify(
            x => x.DeletePermanently(
                It.IsAny<Level>()),
            Times.Never);

        _unitOfWorkMock.Verify(
            x => x.CompleteAsync(
                It.IsAny<CancellationToken>()),
            Times.Never);

        _cacheServiceMock.Verify(
            x => x.RemoveAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()),
            Times.Never);

        _cacheServiceMock.Verify(
            x => x.RemoveByTagAsync(
                It.IsAny<string[]>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_WhenLevelExists_ShouldReturnSuccess()
    {
        // Arrange
        var levelId = Guid.NewGuid();
        var programId = Guid.NewGuid();

        var command = new RemoveLevelCommand(
            programId,
            levelId);

        var level = new Level
        {
            Id = levelId,
            AcademicProgramId = programId
        };

        _levelRepositoryMock
            .Setup(x => x.GetByIdAsync(
                command.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(level);

        _unitOfWorkMock
            .Setup(x => x.CompleteAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        _cacheServiceMock
            .Setup(x => x.RemoveAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _cacheServiceMock
            .Setup(x => x.RemoveByTagAsync(
                It.IsAny<string[]>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new RemoveLevelCommandHandler(
            _unitOfWorkMock.Object,
            _cacheServiceMock.Object);

        // Act
        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        _levelRepositoryMock.Verify(
            x => x.GetByIdAsync(
                command.Id,
                It.IsAny<CancellationToken>()),
            Times.Once);

        _genericLevelRepositoryMock.Verify(
            x => x.DeletePermanently(level),
            Times.Once);

        _unitOfWorkMock.Verify(
            x => x.CompleteAsync(
                It.IsAny<CancellationToken>()),
            Times.Once);

        _cacheServiceMock.Verify(
            x => x.RemoveAsync(
                LevelCacheKeys.ById(command.Id),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _cacheServiceMock.Verify(
            x => x.RemoveByTagAsync(
                LevelCacheKeys.Tags(command.ProgramId),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}