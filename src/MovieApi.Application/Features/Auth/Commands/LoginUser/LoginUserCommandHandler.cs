using MediatR;
using MovieApi.Application.Abstractions.Auth;
using MovieApi.Application.Abstractions.Persistence;
using MovieApi.Application.Features.Auth.Common;

namespace MovieApi.Application.Features.Auth.Commands.LoginUser;

public sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginUserCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var user = await _userRepository.GetByEmailAsync(email, cancellationToken);
        if (user is null)
            throw new UnauthorizedAccessException("Invalid credentials.");

        var validPassword = _passwordHasher.Verify(request.Password, user.PasswordHash);
        if (!validPassword)
            throw new UnauthorizedAccessException("Invalid credentials.");

        if (!user.IsActive)
            throw new UnauthorizedAccessException("User is inactive.");

        var roles = user.UserRoles.Select(x => x.Role.Name).Distinct().ToList();
        var permissions = user.UserRoles
            .SelectMany(x => x.Role.RolePermissions.Select(rp => rp.Permission.Name))
            .Distinct()
            .ToList();

        var token = _jwtTokenGenerator.Generate(user, roles, permissions);

        return new AuthResponse(
            user.Id,
            user.FullName,
            user.Email,
            token,
            roles,
            permissions
        );
    }
}