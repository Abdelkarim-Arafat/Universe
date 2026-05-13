<<<<<<<< HEAD:Universe.Application/ExamServices/ExamCommitteeServices/Queries/GetExamTermCommittees/GetExamTermCommitteesQuery.cs
namespace Universe.Application.ExamCommitteeServices.Queries.GetExamTermCommittees;
========
using Universe.Application.ExamCommitteeServices.Dtos;
namespace Universe.Application.ExamCommitteeServices.Queries.GetAll;
>>>>>>>> 4af299b699488d181e33aa6b8cb24bc5218cbf57:Universe.Application/ExamCommitteeServices/Queries/GetAll/GetAllExamCommitteesQuery.cs

public record GetExamTermCommitteesQuery
(
    FilterRequest Filter,
    [Required] Guid ExamTermId
) : IRequest<Result<PaginationList<ExamCommitteeResponse>>>;