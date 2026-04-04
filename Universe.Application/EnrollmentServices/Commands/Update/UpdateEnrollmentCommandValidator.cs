namespace Universe.Application.EnrollmentServices.Commands.Update;

public class UpdateEnrollmentCommandValidator : AbstractValidator<UpdateEnrollmentCommand>
{
    public UpdateEnrollmentCommandValidator()
    {
        RuleFor(x => x.Hours.RegisterdHours)
           .GreaterThanOrEqualTo(x => x.Hours.MinAllowedHours)
           .WithMessage("register hours should be greater than min hours");
        RuleFor(x => x.Hours.RegisterdHours)
          .LessThanOrEqualTo(x => x.Hours.MaxAllowedHours)
          .WithMessage("register hours should be less than max hours");
    }
}
