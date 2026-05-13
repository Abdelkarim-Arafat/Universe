using Universe.Core.Enums;

<<<<<<<< HEAD:Universe.Core/Contracts/ExamTerm/ExamTermResponse.cs
namespace Universe.Core.Contracts.ExamTerm;
========
namespace Universe.Application.ExamTermServices.Dtos;
>>>>>>>> 4af299b699488d181e33aa6b8cb24bc5218cbf57:Universe.Application/ExamTermServices/Dtos/ExamTermResponse.cs

public record ExamTermResponse
(
    Guid Id,
    ExamType ExamType,
    DateOnly StartDate,
    DateOnly EndDate,
    bool IsPublished
);