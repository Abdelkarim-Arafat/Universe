using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Universe.Core.Abstractions;
using Universe.Core.Entities;
using Universe.Core.Errors;
using Universe.Core.Interfaces;
using System;
using System.Text;

namespace Universe.Application.AuthServices.Commands.Register;

public class RegisterCommandHandler(
    UserManager<ApplicationUser> userManager,
    ILogger<RegisterCommandHandler> logger,
    IEmailSender emailSender
    ) : IRequestHandler<RegisterCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ILogger<RegisterCommandHandler> _logger = logger;
    private readonly IEmailSender _emailSender = emailSender;

    public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if ((await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken)))
            return Result.Failure(AuthErrors.DuplicatedEmail);

        var user = request.Adapt<ApplicationUser>();

        var result = await _userManager.CreateAsync(user, request.Password);

        if(result.Succeeded)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            _logger.LogInformation("Confirmation code: {code}", code);
            await _emailSender.SendConfirmationEmail(user, code);
            return Result.Success();
        }
        var error = result.Errors.First();

        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }
}

