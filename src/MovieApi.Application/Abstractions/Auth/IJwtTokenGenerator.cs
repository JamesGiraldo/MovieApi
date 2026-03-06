using MovieApi.Domain.Entities;

namespace MovieApi.Application.Abstractions.Auth;

public interface IJwtTokenGenerator
{
    string Generate(User user, IReadOnlyCollection<string> roles, IReadOnlyCollection<string> permissions);
}