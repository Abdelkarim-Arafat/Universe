using Org.BouncyCastle.Asn1.IsisMtt.X509;
using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.StudyLoadRuleServices.Dtos;

namespace Universe.Application.StudyLoadRuleServices.Commands.UpdateStudyLoadRule;

public class UpdateStudyLoadRuleCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateStudyLoadRuleCommand, Result<StudyLoadRuleResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<StudyLoadRuleResponse>> Handle(UpdateStudyLoadRuleCommand request, CancellationToken cancellationToken)
    {
        if ((await _unitOfWork.StudyLoadRuleRepository
            .CheckOverLabedGpaAsync(request.AcademicProgramId, request.Id, request.GpaFrom, request.GpaTo, cancellationToken)))
            return Result.Failure<StudyLoadRuleResponse>(StudyLoadRuleErrors.OverLabedExist);

        if(await _unitOfWork.StudyLoadRuleRepository.GetByIdAsync(request.Id, cancellationToken) is not { } studyLoadRule)
            return Result.Failure<StudyLoadRuleResponse>(StudyLoadRuleErrors.NotFound);

        request.Adapt(studyLoadRule);

        _unitOfWork.Repository<StudyLoadRule>().Update(studyLoadRule);

        await _unitOfWork.CompleteAsync(cancellationToken);
        return Result.Success(studyLoadRule.Adapt<StudyLoadRuleResponse>());
    }
}
