using MovieApi.Domain.Common;

namespace MovieApi.Domain.Entities;

public class User : BaseEntity
{
    public string FullName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public bool IsActive { get; private set; } = true;

    private readonly List<UserRole> _userRoles = new();
    public IReadOnlyCollection<UserRole> UserRoles => _userRoles.AsReadOnly();

    private User() { }

    public User(string fullName, string email, string passwordHash)
    {
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
    }

    public void AddRole(Role role)
    {
        if (_userRoles.Any(x => x.RoleId == role.Id))
            return;

        _userRoles.Add(new UserRole(Id, role.Id));
        MarkAsUpdated();
    }

    public void Deactivate()
    {
        IsActive = false;
        MarkAsUpdated();
    }
}