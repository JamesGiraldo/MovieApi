using MediatR;
using MovieApi.Application.Features.Auth.Common;

namespace MovieApi.Application.Features.Auth.Commands.RegisterUser;

public sealed record RegisterUserCommand(
    string FullName,
    string Email,
    string Password
) : IRequest<AuthResponse>;