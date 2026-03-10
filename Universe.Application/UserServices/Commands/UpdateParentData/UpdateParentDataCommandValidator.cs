using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.UserServices.Commands.UpdateFamilyData;

public class UpdateParentDataCommandValidator : AbstractValidator<UpdateParentDataCommand>
{
    public UpdateParentDataCommandValidator()
    {
        RuleFor(x => x.GuardianName)
            .MaximumLength(100).WithMessage("Guardian name cannot exceed 100 characters.");

        RuleFor(x => x.MotherName)
            .MaximumLength(100).WithMessage("Mother name cannot exceed 100 characters.");

        RuleFor(x => x.RelationshipDegree)
            .MaximumLength(50).WithMessage("Relationship degree cannot exceed 50 characters.");

        RuleFor(x => x.GuardianPhoneNumber)
            .Matches(RegexPatterns.PhoneNumber).WithMessage("Invalid phone number format.");

        RuleFor(x => x.GuardianAddress)
            .MaximumLength(300).WithMessage("Address cannot exceed 300 characters.");

        RuleFor(x => x.GuardianCity)
            .MaximumLength(100).WithMessage("City cannot exceed 100 characters.");

        RuleFor(x => x.GuardianEmail)
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

        RuleFor(x => x.Job)
            .MaximumLength(100).WithMessage("Job cannot exceed 100 characters.");
    }
}
