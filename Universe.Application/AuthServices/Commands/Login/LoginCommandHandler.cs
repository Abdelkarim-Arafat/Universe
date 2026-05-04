using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Universe.Application.AuthServices.AuthDtos;
using Universe.Core.Abstractions;
using Universe.Core.Entities;
using Universe.Core.Errors;
using Universe.Core.Interfaces;

namespace Universe.Application.AuthServices.Commands.Login;

public class LoginCommandHandler(
    UserManager<ApplicationUser> userManager,
    IJwtProvider jwtProvider,
    SignInManager<ApplicationUser> signInManager,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<LoginCommand, Result<AuthResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IJwtProvider _jwtProvider = jwtProvider;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<AuthResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user is null || user.IsDeleted) return Result.Failure<AuthResponse>(StudentErrors.InvalidCredentials);

        var result = await _signInManager.PasswordSignInAsync(user , request.Password , request.RememberMe , true);

        if(result.Succeeded)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            var userPermissions = await _unitOfWork.RoleRepository.GetUserPermissionsAsync(userRoles , cancellationToken);

            var (accesstoken , ExpiryIn) = _jwtProvider.GenerateToken(user , userRoles , userPermissions);
            var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            var refreshTokenExpiration = DateTime.UtcNow.AddDays(request.RememberMe ? 7 : 1);

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                ExpiresOn = refreshTokenExpiration,
            });
            await _userManager.UpdateAsync(user);

            var response = new AuthResponse(
                user.Id.ToString(),
                user.CollegeId.ToString(),
                user.Name,
                user.ImageUrl,
                user.Email!,
                userRoles,
                userPermissions,
                accesstoken,
                ExpiryIn,
                refreshToken,
                refreshTokenExpiration);

            return Result.Success(response);
        }

        var error = result.IsNotAllowed
            ? StudentErrors.EmailNotConfirmed
            : result.IsLockedOut
            ? StudentErrors.LockedUser
            : StudentErrors.InvalidCredentials;

        return Result.Failure<AuthResponse>(error);
    }
}
