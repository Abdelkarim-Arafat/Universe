using Universe.Application.ControlServices.Dtos;
using Universe.Core.Enums;
 
namespace Universe.Application.ControlServices.Queries.GetStudents;

public class GetStudentsCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetStudentsCommand, Result<StudentsDegreesResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<StudentsDegreesResponse>> Handle(GetStudentsCommand command, CancellationToken cancellationToken)
    {

        var isProgramExist = await _unitOfWork.AcademicProgramRepository.IsExistAsync(command.AcademicProgramId, cancellationToken);
        if (!isProgramExist)
            return Result.Failure<StudentsDegreesResponse>(AcademicProgramErrors.AcademicProgramNotFound);

        var courseOffering = await _unitOfWork.CourseOfferingRepository
            .GetByIdAsync(command.CourseOfferingId, cancellationToken);

        if (courseOffering is null)
            return Result.Failure<StudentsDegreesResponse>(CourseOfferingErrors.NotFound);

        // get data
        var studentsInfos = await _unitOfWork.UserRepository
            .GetStudentsByCourseOfferingAndGroupNumberAsync(command.CourseOfferingId, command.GroupNumber, cancellationToken);

        var assessmentHeadersRaw = await _unitOfWork.CourseOfferingRepository
            .GetCourseOfferingAssessments(command.CourseOfferingId, cancellationToken);

        var letterGrades = await _unitOfWork.GradeRepository
            .GetProgramGradesAsync(command.AcademicProgramId, cancellationToken);

        // get current degrees for students
        var studentIds = studentsInfos.Select(s => s.Student.Id).ToList();

        var StudentsAssessments = await _unitOfWork.UserRepository
            .GetStudentsAssessmentsAsync(studentIds, cancellationToken);

        var headers = assessmentHeadersRaw.Select(ah => new AssessmentHeader(
            ah.Id,
            ah.Type.ToString(),
            ah.MaxScore
        )).ToList();

        var studentList = studentsInfos.Select(s =>
        {
            var currentStudentDegrees = StudentsAssessments.Where(ass => ass.StudentId == s.Student.Id).ToList();

            var totalDegree = currentStudentDegrees.Sum(ass => ass.degree.HasValue ? ass.degree : 0);

            var letter = letterGrades
                .FirstOrDefault(grade => totalDegree >= grade.MinScore && totalDegree <= grade.MaxScore)
                ?.Code ?? "-";

            return new StudentInformation(
                s.Student.Id,
                s.Student.Name,
                s.Student.StudentCode,
                s.StudentLevelName,
                s.Student.Enrollments.Count(e => e.Status == EnrollmentStatus.Failed),
                totalDegree!.Value,
                letter,
                currentStudentDegrees
                .Select(ass => new StudentDegreeValue(
                   ass.CourseOfferingAssessmentId,
                   ass.degree
                )).ToList()
            );
        }).ToList();

        var filteredList = studentList.AsQueryable();
        var filter = command.Filter;

        if (!string.IsNullOrEmpty(filter.SearchValue))
            filteredList = filteredList
                .Where(x => x.Name.Contains(filter.SearchValue) || x.Code.Contains(filter.SearchValue));

        if (!string.IsNullOrEmpty(filter.SortColumn))
            filteredList = filteredList
                .OrderBy($"{filter.SortColumn} {filter.SortDirection}");

        var totalCount = filteredList.Count();

        var pagedData = filteredList
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();

        var response = new StudentsDegreesResponse(
            headers,
            courseOffering.TotalGrade,
            new PaginationList<StudentInformation>(pagedData, totalCount, filter.PageNumber, filter.PageSize)
        );

        return Result.Success(response);
    }
}

