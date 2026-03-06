using Microsoft.EntityFrameworkCore;
using MovieApi.Application.Abstractions.Persistence;
using MovieApi.Domain.Entities;
using MovieApi.Infrastructure.Persistence;

namespace MovieApi.Infrastructure.Repositories;

public sealed class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _context;

    public RoleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _context.Roles.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
    }
}