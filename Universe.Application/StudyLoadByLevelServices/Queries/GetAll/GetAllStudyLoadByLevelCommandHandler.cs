using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.StudyLoadByLevelServices.StudyLoadByLevelDtos;
using Universe.Application.StudyLoadRuleServices.Query.GetAllStudyLoadRule;

namespace Universe.Application.StudyLoadByLevelServices.Queries.GetAll;

public class GetAllStudyLoadByLevelCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetAllStudyLoadByLevelCommand , Result<List<StudyLoadByLevelResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<StudyLoadByLevelResponse>>> Handle(GetAllStudyLoadByLevelCommand request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.Repository<StudyLoadByLevel>()
            .GetQueryable()
            .AsNoTracking()
            .Where(x => x.AcademicProgramId == request.ProgramId)
            .Select(x => new StudyLoadByLevelResponse(
                x.Id.ToString(),
                x.LevelId.ToString(),
                x.Level.Name,
                x.Sememester.Name,
                x.MinHours,
                x.MaxHours
            ))
            .ToListAsync(cancellationToken);

        return Result.Success(response);
            
    }
}
