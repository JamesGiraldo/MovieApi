using Microsoft.EntityFrameworkCore;
using MovieApi.Domain.Entities;

namespace MovieApi.Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!await context.Roles.AnyAsync())
        {
            context.Roles.AddRange(
                new Role("Admin"),
                new Role("Editor"),
                new Role("User")
            );

            await context.SaveChangesAsync();
        }

        if (!await context.Permissions.AnyAsync())
        {
            context.Permissions.AddRange(
                new Permission("movies.create"),
                new Permission("movies.update"),
                new Permission("movies.delete"),
                new Permission("categories.manage"),
                new Permission("users.manage"),
                new Permission("reviews.create")
            );

            await context.SaveChangesAsync();
        }
    }
}