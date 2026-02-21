using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.Commands.UpdateFamilyData;

public class UpdateFamilyDataCommandValidator : AbstractValidator<UpdateFamilyDataCommand>
{
    public UpdateFamilyDataCommandValidator()
    {
        RuleFor(x => x.GuardianName)
            .MaximumLength(100).WithMessage("Guardian name cannot exceed 100 characters.");

        RuleFor(x => x.MotherName)
            .MaximumLength(100).WithMessage("Mother name cannot exceed 100 characters.");

        RuleFor(x => x.RelationshipDegree)
            .MaximumLength(50).WithMessage("Relationship degree cannot exceed 50 characters.");

        RuleFor(x => x.PhoneNumber)
            .Matches(RegexPatterns.PhoneNumber).WithMessage("Invalid phone number format.");

        RuleFor(x => x.Address)
            .MaximumLength(300).WithMessage("Address cannot exceed 300 characters.");

        RuleFor(x => x.City)
            .MaximumLength(100).WithMessage("City cannot exceed 100 characters.");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

        RuleFor(x => x.Job)
            .MaximumLength(100).WithMessage("Job cannot exceed 100 characters.");
    }
}
