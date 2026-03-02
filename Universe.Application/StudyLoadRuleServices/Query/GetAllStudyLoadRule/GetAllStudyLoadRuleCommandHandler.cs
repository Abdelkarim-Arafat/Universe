using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.LevelServices.LevelDtos;
using Universe.Application.StudyLoadRuleServices.Dtos;

namespace Universe.Application.StudyLoadRuleServices.Query.GetAllStudyLoadRule;

public class GetAllStudyLoadRuleCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<GetAllStudyLoadRuleCommand, Result<List<StudyLoadRuleResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<StudyLoadRuleResponse>>> Handle(GetAllStudyLoadRuleCommand request, CancellationToken cancellationToken)
    {
        var isCollegeExist = await _unitOfWork.CollegeRepository
            .CheckCollegeIsExistAsync(request.CollegeId, cancellationToken);

        if (!isCollegeExist) return Result.Failure<List<StudyLoadRuleResponse>>(CollegeErrors.NotFound);

        var studyLoadRules = await _unitOfWork.Repository<StudyLoadRule>()
            .GetQueryable().ToListAsync(cancellationToken);

        return Result.Success(studyLoadRules.Adapt<List<StudyLoadRuleResponse>>());
    }
}
