using MovieApi.Domain.Common;

namespace MovieApi.Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; private set; } = default!;

    private readonly List<RolePermission> _rolePermissions = new();
    public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions.AsReadOnly();

    private Role() { }

    public Role(string name)
    {
        Name = name;
    }

    public void AddPermission(Permission permission)
    {
        if (_rolePermissions.Any(x => x.PermissionId == permission.Id))
            return;

        _rolePermissions.Add(new RolePermission(Id, permission.Id));
    }
}