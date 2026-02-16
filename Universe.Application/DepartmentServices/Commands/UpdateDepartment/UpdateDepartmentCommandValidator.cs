using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.DepartmentServices.Commands.UpdateDepartment;

internal class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
{
    public UpdateDepartmentCommandValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty()
            .WithMessage("Department name is required.")
            .MaximumLength(100)
            .WithMessage("Department name cannot exceed 100 characters.");

        RuleFor(d => d.Code)
            .NotEmpty()
            .WithMessage("Department code is required.")
            .MaximumLength(50)
            .WithMessage("Department code cannot exceed 50 characters.");
        RuleFor(d => d.Description)
            .MaximumLength(10000)
            .WithMessage("Department description cannot exceed 10,000 characters.");

        RuleFor(d => d.RequiredCreditHours)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Required credit hours.");
    }
}
