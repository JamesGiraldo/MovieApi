using MovieApi.Domain.Common;

namespace MovieApi.Domain.Entities;

public class Movie : BaseEntity
{
    public string Title { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public int ReleaseYear { get; private set; }
    public int DurationMinutes { get; private set; }
    public Guid CategoryId { get; private set; }
    public decimal RatingAverage { get; private set; }

    public Category Category { get; private set; } = default!;

    private Movie() { }

    public Movie(
        string title,
        string description,
        int releaseYear,
        int durationMinutes,
        Guid categoryId)
    {
        Title = title;
        Description = description;
        ReleaseYear = releaseYear;
        DurationMinutes = durationMinutes;
        CategoryId = categoryId;
        RatingAverage = 0;
    }

    public void Update(
        string title,
        string description,
        int releaseYear,
        int durationMinutes,
        Guid categoryId)
    {
        Title = title;
        Description = description;
        ReleaseYear = releaseYear;
        DurationMinutes = durationMinutes;
        CategoryId = categoryId;
        MarkAsUpdated();
    }

    public void UpdateRatingAverage(decimal average)
    {
        RatingAverage = average;
        MarkAsUpdated();
    }
}