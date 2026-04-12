using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.CourseServices.Commands.AddCourse;

public class AddCourseCommandValidator : AbstractValidator<AddCourseCommand>
{
    public AddCourseCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Course name is required.")
            .MaximumLength(100)
            .WithMessage("Course name must not exceed 100 characters.");
        RuleFor(c => c.Code)
            .NotEmpty()
            .WithMessage("Course code is required.")
            .MaximumLength(50)
            .WithMessage("Course code must not exceed 50 characters.");
        RuleFor(c => c.Description)
            .MaximumLength(10000)
            .WithMessage("Course description must not exceed 10,000 characters.");
    }
}
