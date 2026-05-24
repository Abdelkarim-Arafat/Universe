namespace Universe.Application.StaffServices.Commands.RegisterStaff;



internal class RegisterStaffCommandValidator : AbstractValidator<RegisterStaffCommand>
{
    public RegisterStaffCommandValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty()
           .MaximumLength(100);

        RuleFor(x => x.UserName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Password)
            .NotEmpty()
            .Length(8, 15);
    }
}
