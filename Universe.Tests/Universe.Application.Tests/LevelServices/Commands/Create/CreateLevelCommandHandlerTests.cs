using FluentAssertions;
using Moq;
using Universe.Application.LevelServices.Commands.Create;
using Universe.Application.LevelServices.Commands.CreateLevel;
using Universe.Core.Entities;
using Universe.Core.Errors;
using Universe.Core.Interfaces;
using Universe.Core.Interfaces.Repositories;

namespace Universe.Tests.Universe.Application.Tests.LevelServices.Commands.Create;

public class CreateLevelCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ICacheService> _cacheServiceMock;

    public CreateLevelCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _cacheServiceMock = new Mock<ICacheService>();
    }

    [Fact]
    public async Task Handle_WhenAcademicProgramDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var command = new CreateLevelCommand(Guid.NewGuid(), "Level 1", 10, 20);

        _unitOfWorkMock
            .Setup(x => x.AcademicProgramRepository
                 .IsExistAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())
            )
            .ReturnsAsync(false);

        var handler = new CreateLevelCommandHandler(
            _unitOfWorkMock.Object,
            _cacheServiceMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(AcademicProgramErrors.NotFound);
    }

    [Fact]
    public async Task Handle_WhenHoursAreInvalid_ShouldReturnInvalidHours()
    {
        // Arrange
        var command = new CreateLevelCommand(Guid.NewGuid(), "Level 1", 10, 20);

        _unitOfWorkMock
            .Setup(x => x.AcademicProgramRepository
                 .IsExistAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>())
            )
            .ReturnsAsync(true);

        _unitOfWorkMock
            .Setup(x => x.LevelRepository
                .CheckOverLabedHoursAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>())
            )
            .ReturnsAsync(true);

        var handler = new CreateLevelCommandHandler(
            _unitOfWorkMock.Object,
            _cacheServiceMock.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(LevelErrors.InvalidHours);
    }

    [Fact]
    public async Task Handle_WhenCompleteAsyncThrows_ShouldThrowException()
    {
        // Arrange
        var levelRepositoryMock = new Mock<IGenericRepository<Level>>();

        _unitOfWorkMock
            .Setup(x => x.AcademicProgramRepository
                .IsExistAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _unitOfWorkMock
            .Setup(x => x.LevelRepository
                .CheckOverLabedHoursAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var command = new CreateLevelCommand(
            Guid.NewGuid(),
            "Level 1",
            10,
            20
        );

        _unitOfWorkMock
            .Setup(x => x.Repository<Level>())
            .Returns(levelRepositoryMock.Object);

        levelRepositoryMock
            .Setup(x => x.AddAsync(
                It.IsAny<Level>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock
            .Setup(x => x.CompleteAsync(
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        var handler = new CreateLevelCommandHandler(
            _unitOfWorkMock.Object,
            _cacheServiceMock.Object);

        // Act & Assert
        Func<Task> act = async () =>
            await handler.Handle(command, CancellationToken.None);

        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Database error");
    }

    [Fact]
    public async Task Handle_WhenRequestIsValid_ShouldCreateLevel()
    {
        // Arrange
        var command = new CreateLevelCommand(
            Guid.NewGuid(),
            "Level 1",
            10,
            20);

        _unitOfWorkMock
            .Setup(x => x.AcademicProgramRepository
                .IsExistAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _unitOfWorkMock
            .Setup(x => x.LevelRepository
                .CheckOverLabedHoursAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _unitOfWorkMock
            .Setup(x => x.CompleteAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var levelRepositoryMock = new Mock<IGenericRepository<Level>>();

        levelRepositoryMock
            .Setup(x => x.AddAsync(
                It.IsAny<Level>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock
            .Setup(x => x.Repository<Level>())
            .Returns(levelRepositoryMock.Object);

        _cacheServiceMock
            .Setup(x => x.RemoveByTagAsync(
                It.IsAny<string[]>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new CreateLevelCommandHandler(
            _unitOfWorkMock.Object,
            _cacheServiceMock.Object);

        // Act
        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().NotBe(Guid.Empty);

        levelRepositoryMock.Verify(
            x => x.AddAsync(
                It.IsAny<Level>(),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _unitOfWorkMock.Verify(
            x => x.CompleteAsync(
                It.IsAny<CancellationToken>()),
            Times.Once);

        _cacheServiceMock.Verify(
            x => x.RemoveByTagAsync(
                It.IsAny<string[]>(),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}