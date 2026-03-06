using MovieApi.Domain.Entities;

namespace MovieApi.Application.Abstractions.Persistence;

public interface IRoleRepository
{
    Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken);
}