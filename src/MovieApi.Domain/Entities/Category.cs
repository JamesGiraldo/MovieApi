using MovieApi.Domain.Common;

namespace MovieApi.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }

    private Category() { }

    public Category(string name, string? description)
    {
        Name = name;
        Description = description;
    }

    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
        MarkAsUpdated();
    }
}