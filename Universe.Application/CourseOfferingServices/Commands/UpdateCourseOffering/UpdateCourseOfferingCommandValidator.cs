using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.CourseOfferingServices.Commands.UpdateCourseOffering;
public class UpdateCourseOfferingCommandValidator : AbstractValidator<UpdateCourseOfferingCommand>
{
    public UpdateCourseOfferingCommandValidator()
    {
        RuleFor(x => x.OptionalGroupCode)
            .NotEmpty()
            .When(x => x.IsOptional == true);

        RuleFor(x => x.CreditHours)
            .GreaterThan(0)
            .WithMessage("Credit hours must be greater than 0.");

        RuleFor(x => x.TotalGrade)
            .GreaterThan(0)
            .WithMessage("Total grade must be greater than 0.");

        RuleFor(x => x.SuccessPercentage)
            .GreaterThan(0)
            .WithMessage("Success percentage must be greater than 0.");

        RuleFor(x => x.CourseId)
            .NotEmpty();

        RuleFor(x => x.SemesterId)
            .NotEmpty();

        RuleFor(x => x.LevelId)
            .NotEmpty();

        RuleFor(x => x.Assessments)
            .NotNull()
            .NotEmpty()
            .WithMessage("At least one assessment is required.");

        RuleForEach(x => x.Assessments)
            .SetValidator(new CourseOfferingAssessmentDtoValidator());

        RuleFor(x => x)
            .Must(x =>
                x.Assessments.Sum(a => a.MaxScore) == x.TotalGrade)
            .WithMessage("Sum of assessment scores must equal total grade.");
    }
}

public class CourseOfferingAssessmentDtoValidator : AbstractValidator<CourseOfferingAssessmentCommand>
{
    public CourseOfferingAssessmentDtoValidator()
    {
        RuleFor(x => x.MaxScore)
            .GreaterThan(0)
            .WithMessage("Assessment max score must be greater than 0.");

        RuleFor(x => x.Type)
            .NotNull()
            .IsInEnum();
    }
}

