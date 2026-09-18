using FluentAssertions;
using Moq;
using Universe.Application.Common;
using Universe.Application.LevelServices.Commands.Update;
using Universe.Core.Entities;
using Universe.Core.Errors;
using Universe.Core.Interfaces;
using Universe.Core.Interfaces.Repositories;

namespace Universe.Tests.Universe.Application.Tests.LevelServices.Commands.Update;

public class UpdateLevelCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ILevelRepository> _levelRepositoryMock;
    private readonly Mock<IGenericRepository<Level>> _genericLevelRepositoryMock;
    private readonly Mock<ICacheService> _cacheServiceMock;

    public UpdateLevelCommandHandlerTests()
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
        var command = new UpdateLevelCommand(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Level 1",
            10,
            20);

        _levelRepositoryMock
            .Setup(x => x.GetByIdAsync(
                command.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((Level?)null);

        var handler = new UpdateLevelCommandHandler(
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

        _levelRepositoryMock.Verify(
            x => x.CheckOverLabedHoursAsync(
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<Guid>(),
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()),
            Times.Never);

        _genericLevelRepositoryMock.Verify(
            x => x.Update(It.IsAny<Level>()),
            Times.Never);

        _unitOfWorkMock.Verify(
            x => x.CompleteAsync(
                It.IsAny<CancellationToken>()),
            Times.Never);

        _cacheServiceMock.Verify(
            x => x.RemoveByTagAsync(
                It.IsAny<string[]>(),
                It.IsAny<CancellationToken>()),
            Times.Never);

        _cacheServiceMock.Verify(
            x => x.RemoveAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_WhenHoursAreInvalid_ShouldReturnInvalidHours()
    {
        // Arrange
        var levelId = Guid.NewGuid();
        var academicProgramId = Guid.NewGuid();

        var command = new UpdateLevelCommand(
            academicProgramId,
            levelId,
            "Level 1",
            10,
            20);

        var level = new Level
        {
            Id = levelId,
            AcademicProgramId = academicProgramId,
            Name = "Level 1",
            MinHours = 10,
            MaxHours = 20
        };

        _levelRepositoryMock
            .Setup(x => x.GetByIdAsync(
                command.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(level);

        _levelRepositoryMock
            .Setup(x => x.CheckOverLabedHoursAsync(
                command.MinHours,
                command.MaxHours,
                level.Id,
                level.AcademicProgramId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var handler = new UpdateLevelCommandHandler(
            _unitOfWorkMock.Object,
            _cacheServiceMock.Object);

        // Act
        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(LevelErrors.InvalidHours);

        _levelRepositoryMock.Verify(
            x => x.GetByIdAsync(
                command.Id,
                It.IsAny<CancellationToken>()),
            Times.Once);

        _levelRepositoryMock.Verify(
            x => x.CheckOverLabedHoursAsync(
                command.MinHours,
                command.MaxHours,
                level.Id,
                level.AcademicProgramId,
                It.IsAny<CancellationToken>()),
            Times.Once);

        _genericLevelRepositoryMock.Verify(
            x => x.Update(It.IsAny<Level>()),
            Times.Never);

        _unitOfWorkMock.Verify(
            x => x.CompleteAsync(
                It.IsAny<CancellationToken>()),
            Times.Never);

        _cacheServiceMock.Verify(
            x => x.RemoveByTagAsync(
                It.IsAny<string[]>(),
                It.IsAny<CancellationToken>()),
            Times.Never);

        _cacheServiceMock.Verify(
            x => x.RemoveAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_WhenCompleteAsyncThrowsException_ShouldThrowException()
    {
        // Arrange
        var levelId = Guid.NewGuid();
        var academicProgramId = Guid.NewGuid();

        var command = new UpdateLevelCommand(
            academicProgramId,
            levelId,
            "Updated Level",
            15,
            25);

        var level = new Level
        {
            Id = levelId,
            AcademicProgramId = academicProgramId,
            Name = "Level 1",
            MinHours = 10,
            MaxHours = 20
        };

        _levelRepositoryMock
            .Setup(x => x.GetByIdAsync(
                command.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(level);

        _levelRepositoryMock
            .Setup(x => x.CheckOverLabedHoursAsync(
                command.MinHours,
                command.MaxHours,
                level.Id,
                level.AcademicProgramId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _genericLevelRepositoryMock
            .Setup(x => x.Update(It.IsAny<Level>()));

        _unitOfWorkMock
            .Setup(x => x.CompleteAsync(
                It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database error"));

        var handler = new UpdateLevelCommandHandler(
            _unitOfWorkMock.Object,
            _cacheServiceMock.Object);

        // Act
        Func<Task> act = async () =>
            await handler.Handle(
                command,
                CancellationToken.None);

        // Assert
        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Database error");

        _levelRepositoryMock.Verify(
            x => x.GetByIdAsync(
                command.Id,
                It.IsAny<CancellationToken>()),
            Times.Once);

        _levelRepositoryMock.Verify(
            x => x.CheckOverLabedHoursAsync(
                command.MinHours,
                command.MaxHours,
                level.Id,
                level.AcademicProgramId,
                It.IsAny<CancellationToken>()),
            Times.Once);

        _genericLevelRepositoryMock.Verify(
            x => x.Update(It.IsAny<Level>()),
            Times.Once);

        _unitOfWorkMock.Verify(
            x => x.CompleteAsync(
                It.IsAny<CancellationToken>()),
            Times.Once);

        _cacheServiceMock.Verify(
            x => x.RemoveByTagAsync(
                It.IsAny<string[]>(),
                It.IsAny<CancellationToken>()),
            Times.Never);

        _cacheServiceMock.Verify(
            x => x.RemoveAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_WhenRequestIsValid_ShouldUpdateLevelAndReturnSuccess()
    {
        // Arrange
        var levelId = Guid.NewGuid();
        var academicProgramId = Guid.NewGuid();

        var command = new UpdateLevelCommand(
            academicProgramId,
            levelId,
            "Updated Level",
            15,
            25);

        var level = new Level
        {
            Id = levelId,
            AcademicProgramId = academicProgramId,
            Name = "Level 1",
            MinHours = 10,
            MaxHours = 20
        };

        _levelRepositoryMock
            .Setup(x => x.GetByIdAsync(
                command.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(level);

        _levelRepositoryMock
            .Setup(x => x.CheckOverLabedHoursAsync(
                command.MinHours,
                command.MaxHours,
                level.Id,
                level.AcademicProgramId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _genericLevelRepositoryMock
            .Setup(x => x.Update(It.IsAny<Level>()));

        _unitOfWorkMock
            .Setup(x => x.CompleteAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        _cacheServiceMock
            .Setup(x => x.RemoveByTagAsync(
                It.IsAny<string[]>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _cacheServiceMock
            .Setup(x => x.RemoveAsync(
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var handler = new UpdateLevelCommandHandler(
            _unitOfWorkMock.Object,
            _cacheServiceMock.Object);

        // Act
        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.Name.Should().Be(command.Name);
        result.Value.MinHours.Should().Be(command.MinHours);
        result.Value.MaxHours.Should().Be(command.MaxHours);

        _levelRepositoryMock.Verify(
            x => x.GetByIdAsync(
                command.Id,
                It.IsAny<CancellationToken>()),
            Times.Once);

        _levelRepositoryMock.Verify(
            x => x.CheckOverLabedHoursAsync(
                command.MinHours,
                command.MaxHours,
                level.Id,
                level.AcademicProgramId,
                It.IsAny<CancellationToken>()),
            Times.Once);

        _genericLevelRepositoryMock.Verify(
            x => x.Update(
                It.Is<Level>(x =>
                    x.Id == command.Id &&
                    x.Name == command.Name &&
                    x.MinHours == command.MinHours &&
                    x.MaxHours == command.MaxHours &&
                    x.AcademicProgramId == level.AcademicProgramId)),
            Times.Once);

        _unitOfWorkMock.Verify(
            x => x.CompleteAsync(
                It.IsAny<CancellationToken>()),
            Times.Once);

        _cacheServiceMock.Verify(
            x => x.RemoveByTagAsync(
                LevelCacheKeys.Tags(command.ProgramId),
                It.IsAny<CancellationToken>()),
            Times.Once);

        _cacheServiceMock.Verify(
            x => x.RemoveAsync(
                LevelCacheKeys.ById(level.Id),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}