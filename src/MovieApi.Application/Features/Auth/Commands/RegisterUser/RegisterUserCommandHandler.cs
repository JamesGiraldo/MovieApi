using MediatR;
using MovieApi.Application.Abstractions.Auth;
using MovieApi.Application.Abstractions.Persistence;
using MovieApi.Application.Features.Auth.Common;
using MovieApi.Domain.Entities;

namespace MovieApi.Application.Features.Auth.Commands.RegisterUser;

public sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterUserCommandHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email.Trim().ToLowerInvariant();

        var exists = await _userRepository.ExistsByEmailAsync(email, cancellationToken);
        if (exists)
            throw new ApplicationException("A user with this email already exists.");

        var passwordHash = _passwordHasher.Hash(request.Password);

        var user = new User(request.FullName.Trim(), email, passwordHash);

        var userRole = await _roleRepository.GetByNameAsync("User", cancellationToken);
        if (userRole is null)
            throw new ApplicationException("Default role 'User' was not found.");

        user.AddRole(userRole);

        await _userRepository.AddAsync(user, cancellationToken);

        var roles = new List<string> { userRole.Name };
        var permissions = Array.Empty<string>();

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