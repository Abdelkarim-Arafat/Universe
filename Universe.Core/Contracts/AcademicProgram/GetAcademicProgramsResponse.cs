namespace Universe.Core.Contracts.AcademicProgram;

public record GetAcademicProgramsResponse(
    Guid Id,
    string Name,
    string Code
);
