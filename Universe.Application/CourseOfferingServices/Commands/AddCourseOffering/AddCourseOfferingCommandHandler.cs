using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.CourseOfferingServices.Dtos;

namespace Universe.Application.CourseOfferingServices.Commands.AddCourseOffering;

internal class AddCourseOfferingCommandHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<AddCourseOfferingCommand, Result<CourseOfferingWithDetailsResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<CourseOfferingWithDetailsResponse>> Handle(AddCourseOfferingCommand request, CancellationToken cancellationToken)
    {
        if (!(await _unitOfWork.AcademicProgramRepository
            .IsExistAsync(request.AcademicProgramId, cancellationToken))
            ) return Result.Failure<CourseOfferingWithDetailsResponse>(AcademicProgramErrors.AcademicProgramNotFound);

        if (await _unitOfWork.CourseOfferingRepository
            .IsExistAsync(request.AcademicProgramId, request.SemesterId,
                request.LevelId, request.CourseId, cancellationToken)
            ) return Result.Failure<CourseOfferingWithDetailsResponse>(CourseOfferingErrors.AlreadyExist);

        var courseOffering = new CourseOffering
        {
            AcademicProgramId = request.AcademicProgramId,
            SemesterId = request.SemesterId,
            LevelId = request.LevelId,
            CourseId = request.CourseId,
            TotalGrade = request.TotalGrade,
            SuccessPercentage = request.SuccessPercentage,
            IsOptional = request.IsOptional,
            OptionalGroupCode = request.OptionalGroupCode!,
            Type = request.Type,
            IsIncludedInGpa = request.IsIncludedInGpa,
            CreditHours = request.CreditHours,
            Assessments = request.Assessments.Select(static x => new CourseOfferingAssessment
            {
                Type = x.Type,
                MaxScore = x.MaxScore,
            }).ToList()
        };

        await _unitOfWork.Repository<CourseOffering>().AddAsync(courseOffering, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);

        return Result.Success(courseOffering.Adapt<CourseOfferingWithDetailsResponse>());
    }
}
