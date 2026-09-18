using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using Universe.Application.Common;
using Universe.Application.LevelServices.Queries.GetAcademicProgramLevels;
using Universe.Core.Contracts.Level;
using Universe.Core.Entities;
using Universe.Core.Errors;
using Universe.Core.Interfaces;
using Universe.Core.Interfaces.Repositories;
using Universe.Infrastructure.Persistence;

namespace Universe.Tests.Universe.Application.Tests.LevelServices.Queries.GetAcademicProgramLevels;

public class GetAcademicProgramLevelsQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IAcademicProgramRepository> _academicProgramRepositoryMock;
    private readonly Mock<IGenericRepository<Level>> _genericLevelRepositoryMock;
    private readonly Mock<ICacheService> _cacheServiceMock;

    public GetAcademicProgramLevelsQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _academicProgramRepositoryMock = new Mock<IAcademicProgramRepository>();
        _genericLevelRepositoryMock = new Mock<IGenericRepository<Level>>();
        _cacheServiceMock = new Mock<ICacheService>();

        _unitOfWorkMock
            .Setup(x => x.AcademicProgramRepository)
            .Returns(_academicProgramRepositoryMock.Object);

        _unitOfWorkMock
            .Setup(x => x.Repository<Level>())
            .Returns(_genericLevelRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenAcademicProgramDoesNotExist_ShouldReturnNotFound()
    {
        // Arrange
        var command = new GetAcademicProgramLevelsQuery(
            Guid.NewGuid(),
            new FilterRequest()
        );

        _academicProgramRepositoryMock
            .Setup(x => x.IsExistAsync(
                command.ProgramId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var handler = new GetAcademicProgramLevelsQueryHandler(
            _unitOfWorkMock.Object,
            _cacheServiceMock.Object);

        // Act
        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be(AcademicProgramErrors.NotFound);

        _academicProgramRepositoryMock.Verify(
            x => x.IsExistAsync(
                command.ProgramId,
                It.IsAny<CancellationToken>()),
            Times.Once);

        _cacheServiceMock.Verify(
            x => x.GetOrCreateAsync(
                It.IsAny<string>(),
                It.IsAny<Func<Task<PaginationList<LevelResponse>>>>(),
                It.IsAny<CancellationToken>(),
                It.IsAny<string[]?>()),
            Times.Never);

        _genericLevelRepositoryMock.Verify(
            x => x.GetQueryable(),
            Times.Never);
    }

    [Fact]
    public async Task Handle_WhenRequestIsValid_ShouldReturnLevels()
    {
        // Arrange
        var programId = Guid.NewGuid();

        var level1 = new Level
        {
            Id = Guid.NewGuid(),
            AcademicProgramId = programId,
            Name = "Level 1",
            MinHours = 10,
            MaxHours = 20,
            IsDeleted = false
        };

        var level2 = new Level
        {
            Id = Guid.NewGuid(),
            AcademicProgramId = programId,
            Name = "Level 2",
            MinHours = 20,
            MaxHours = 30,
            IsDeleted = false
        };

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

        await using var context = new ApplicationDbContext(
            options,
            httpContextAccessorMock.Object);

        context.Set<Level>().AddRange(level1, level2);

        await context.SaveChangesAsync();

        _academicProgramRepositoryMock
            .Setup(x => x.IsExistAsync(
                programId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _genericLevelRepositoryMock
            .Setup(x => x.GetQueryable())
            .Returns(context.Set<Level>());

        _cacheServiceMock
            .Setup(x => x.GetOrCreateAsync(
                It.IsAny<string>(),
                It.IsAny<Func<Task<PaginationList<LevelResponse>>>>(),
                It.IsAny<CancellationToken>(),
                It.IsAny<string[]?>()))
            .Returns(
                (
                    string key,
                    Func<Task<PaginationList<LevelResponse>>> factory,
                    CancellationToken cancellationToken,
                    string[]? tags
                ) => factory());

        var command = new GetAcademicProgramLevelsQuery(
            programId,
            new FilterRequest
            {
                PageNumber = 1,
                PageSize = 10
            });

        var handler = new GetAcademicProgramLevelsQueryHandler(
            _unitOfWorkMock.Object,
            _cacheServiceMock.Object);

        // Act
        var result = await handler.Handle(
            command,
            CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();

        result.Value.Should().NotBeNull();
        result.Value.Items.Should().HaveCount(2);
        result.Value.TotalCount.Should().Be(2);
        result.Value.PageNumber.Should().Be(1);

        result.Value.Items.Should().Contain(x =>
            x.Id == level1.Id &&
            x.Name == level1.Name &&
            x.MinHours == level1.MinHours &&
            x.MaxHours == level1.MaxHours);

        result.Value.Items.Should().Contain(x =>
            x.Id == level2.Id &&
            x.Name == level2.Name &&
            x.MinHours == level2.MinHours &&
            x.MaxHours == level2.MaxHours);

        _academicProgramRepositoryMock.Verify(
            x => x.IsExistAsync(
                programId,
                It.IsAny<CancellationToken>()),
            Times.Once);

        _genericLevelRepositoryMock.Verify(
            x => x.GetQueryable(),
            Times.Once);

        _cacheServiceMock.Verify(
            x => x.GetOrCreateAsync(
                It.IsAny<string>(),
                It.IsAny<Func<Task<PaginationList<LevelResponse>>>>(),
                It.IsAny<CancellationToken>(),
                It.IsAny<string[]?>()),
            Times.Once);
    }
}