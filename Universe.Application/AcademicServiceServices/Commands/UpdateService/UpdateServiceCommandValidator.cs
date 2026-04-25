using System;
using System.Collections.Generic;
using System.Text;

namespace Universe.Application.AcademicServiceServices.Commands.UpdateService;

public class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
{
    public UpdateServiceCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MinimumLength(500).WithMessage("Description must be at least 500 characters long.");
    }
}