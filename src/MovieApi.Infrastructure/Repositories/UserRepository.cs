using Microsoft.EntityFrameworkCore;
using MovieApi.Application.Abstractions.Persistence;
using MovieApi.Domain.Entities;
using MovieApi.Infrastructure.Persistence;

namespace MovieApi.Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                    .ThenInclude(x => x.RolePermissions)
                        .ThenInclude(x => x.Permission)
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Users.AnyAsync(x => x.Email == email, cancellationToken);
    }
}