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

        if (await _unitOfWork.AcademicYearRepository
            .GetSemesterByTypeAsync(request.AcademicYearId, request.SemesterType, cancellationToken) is not { } semester
            ) return Result.Failure<CourseOfferingWithDetailsResponse>(AcademicYearErrors.NotFound);

        if (await _unitOfWork.CourseOfferingRepository
            .IsExistAsync(request.AcademicProgramId, semester.Id,
                request.LevelId, request.CourseId, cancellationToken)
            ) return Result.Failure<CourseOfferingWithDetailsResponse>(CourseOfferingErrors.AlreadyExist);

        // ??
        if(await _unitOfWork.CourseRepository
            .GetByIdAsync(request.CourseId , cancellationToken) is null) 
            return Result.Failure<CourseOfferingWithDetailsResponse>(CourseErrors.CourseNotFound);

        // ??
        if (await _unitOfWork.LevelRepository
            .GetByIdAsync(request.LevelId, cancellationToken) is null)
            return Result.Failure<CourseOfferingWithDetailsResponse>(LevelErrors.NotFound);

        if (await _unitOfWork.AcademicYearRepository
            .IsExistSemesterAsync(semester.Id , cancellationToken) is false)
            return Result.Failure<CourseOfferingWithDetailsResponse>(SemesterErrors.NotFound);

        var courseOffering = new CourseOffering
        {
            AcademicProgramId = request.AcademicProgramId,
            SemesterId = semester.Id,
            LevelId = request.LevelId,
            CourseId = request.CourseId,
            TotalGrade = request.TotalGrade,
            NumberOfGroups = request.NumberOfGroups,
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
        // حذف الريكويست
        var response = (courseOffering, request).Adapt<CourseOfferingWithDetailsResponse>();
        
        return Result.Success(response);
    }
}
