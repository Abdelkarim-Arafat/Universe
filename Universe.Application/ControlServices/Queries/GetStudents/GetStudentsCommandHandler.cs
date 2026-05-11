using Universe.Application.Extensions;
using Universe.Core.Enums;
 
namespace Universe.Application.ControlServices.Queries.GetStudents;

public class GetStudentsCommandHandler(IUnitOfWork unitOfWork,ICacheService cacheService) 
    : IRequestHandler<GetStudentsCommand, Result<PaginationList<StudentInformationResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<PaginationList<StudentInformationResponse>>> Handle(GetStudentsCommand command, CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.AcademicProgramRepository.IsExistAsync(command.AcademicProgramId, cancellationToken))
            return Result.Failure<PaginationList<StudentInformationResponse>>(AcademicProgramErrors.NotFound);

        if (! await _unitOfWork.CourseOfferingRepository.IsExistAsync(command.CourseOfferingId, cancellationToken))
            return Result.Failure<PaginationList<StudentInformationResponse>>(CourseOfferingErrors.NotFound);

       
        var query = _unitOfWork.Repository<Student>()
            .GetQueryable()
            .AsNoTracking()
            .Where(student => !student.IsDeleted &&
                        student.Enrollments.Any(e => !e.IsDeleted
                                                  && e.CourseOfferingId == command.CourseOfferingId
                                                  && (command.GroupNumber == null || e.GroupNumber == command.GroupNumber)));

        var filter = command.Filter;

        if (!string.IsNullOrEmpty(filter.SearchValue))
            query = query.ApplySearch(filter.SearchValue, s => s.Name, s => s.StudentCode);

        if (!string.IsNullOrEmpty(filter.SortColumn))
            query = query.OrderBy($"{filter.SortColumn} {filter.SortDirection}");

        var totalCount = await query.CountAsync(cancellationToken);

        var pagedStudents = await query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(s => new
            {
                s.Id,
                s.Name,
                s.StudentCode,
                NumberOfFailed = s.Enrollments.Count(e => !e.IsDeleted
                                                  && e.CourseOfferingId == command.CourseOfferingId
                                                  && e.Status == EnrollmentStatus.Failed)
            })
            .ToListAsync(cancellationToken);

        var pagedStudentIds = pagedStudents.Select(s => s.Id).ToList();

        var studentsAssessmentsLookUp = await _unitOfWork.UserRepository
            .GetStudentsAssessmentsLookupAsync(pagedStudentIds, command.CourseOfferingId, cancellationToken);

        var studentsLevelNameDictionary = await _unitOfWork.UserRepository
            .GetStudentsLevelNameDictionaryAsync(pagedStudentIds, command.AcademicProgramId, cancellationToken);

        var letterGrades = await _unitOfWork.GradeRepository
                .GetProgramGradesAsync(command.AcademicProgramId, cancellationToken);

        var responseItems = pagedStudents.Select(student =>
        {
            var studentAssessments = studentsAssessmentsLookUp[student.Id].ToList();

            var totalDegree = studentAssessments.Sum(a => a.DegreeValue ?? 0);

            var letterGrade = letterGrades
                .FirstOrDefault(g => totalDegree >= g.MinScore && totalDegree <= g.MaxScore)?.Code ?? "N/A";

            var levelName = studentsLevelNameDictionary.GetValueOrDefault(student.Id, "No Level");

            return new StudentInformationResponse(
                student.Id,
                student.Name,
                student.StudentCode,
                levelName,
                student.NumberOfFailed,
                totalDegree,
                letterGrade,
                studentAssessments
            );
        }).ToList(); 

        var response = new PaginationList<StudentInformationResponse>(
            responseItems,
            totalCount,
            filter.PageNumber,
            filter.PageSize
        );

        return Result.Success(response);
    }
}

