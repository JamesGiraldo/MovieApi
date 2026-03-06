using MovieApi.Domain.Common;

namespace MovieApi.Domain.Entities;

public class User : BaseEntity
{
    public string FullName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public bool IsActive { get; private set; } = true;

    private User() { }

    public User(string fullName, string email, string passwordHash)
    {
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
    }

    public void Deactivate()
    {
        IsActive = false;
        MarkAsUpdated();
    }
}