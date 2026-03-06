using MediatR;
using MovieApi.Application.Features.Auth.Common;

namespace MovieApi.Application.Features.Auth.Commands.LoginUser;

public sealed record LoginUserCommand(
    string Email,
    string Password
) : IRequest<AuthResponse>;