using MovieApi.Domain.Entities;

namespace MovieApi.Application.Abstractions.Persistence;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);
}