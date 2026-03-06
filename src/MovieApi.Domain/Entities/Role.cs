using MovieApi.Domain.Common;

namespace MovieApi.Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; private set; } = default!;

    private Role() { }

    public Role(string name)
    {
        Name = name;
    }
}