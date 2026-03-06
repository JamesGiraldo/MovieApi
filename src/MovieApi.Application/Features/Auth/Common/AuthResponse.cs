namespace MovieApi.Application.Features.Auth.Common;

public sealed record AuthResponse(
    Guid UserId,
    string FullName,
    string Email,
    string Token,
    IReadOnlyCollection<string> Roles,
    IReadOnlyCollection<string> Permissions
);