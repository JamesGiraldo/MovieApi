using MovieApi.Domain.Common;

namespace MovieApi.Domain.Entities;

public class Permission : BaseEntity
{
    public string Name { get; private set; } = default!;

    private Permission() { }

    public Permission(string name)
    {
        Name = name;
    }
}