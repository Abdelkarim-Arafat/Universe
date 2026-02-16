using System;
using System.Collections.Generic;
using System.Text;
using Universe.Application.CourseServices.Dtos;

namespace Universe.Application.CourseServices.Query.GetAllCourses;

public class GetAllCoursesCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllCoursesCommand, Result<List<CourseResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<CourseResponse>>> Handle(GetAllCoursesCommand request, CancellationToken cancellationToken)
    {
        var courses = await _unitOfWork.CourseRepository.GetAllAsync(request.CollegeId, cancellationToken);

        return Result.Success(courses.Adapt<List<CourseResponse>>());
    }
}
