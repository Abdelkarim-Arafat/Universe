using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.Commands.UpdatePreviousQualification;

public class UpdatePreviousQualificationCommandValidator : AbstractValidator<UpdatePreviousQualificationCommand>
{
    public UpdatePreviousQualificationCommandValidator()
    {
        RuleFor(x => x.SchoolName)
            .MaximumLength(100).WithMessage("School name cannot exceed 100 characters.");

        RuleFor(x => x.Qualification)
            .MaximumLength(100).WithMessage("Qualification cannot exceed 100 characters.");

        RuleFor(x => x.GraduationYear)
            .MaximumLength(10).WithMessage("Graduation year cannot exceed 10 characters.");

        RuleFor(x => x.GraduationYear)
            .MaximumLength(10).WithMessage("Graduation year cannot exceed 10 characters.");

        RuleFor(x => x.EnrollmentYear)
            .MaximumLength(10).WithMessage("Graduation year cannot exceed 10 characters.");

        RuleFor(x => x.AdmissionType)
            .NotNull().NotEmpty()
            .IsInEnum().WithMessage("Invalid admission type value.");

        RuleFor(x => x.TotalGrade)
            .InclusiveBetween(0, 9999.99m);
    }
}
