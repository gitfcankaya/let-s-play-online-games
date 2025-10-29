using GamePlatform.Core.Enums;

namespace GamePlatform.Core.Entities;

public class Game : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public string? FullImageUrl { get; set; }
    public string GameUrl { get; set; } = string.Empty;
    public GameType GameType { get; set; } = GameType.Html5;
    public int CategoryId { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsFeatured { get; set; } = false;
    public int ViewCount { get; set; } = 0;
    public int PlayCount { get; set; } = 0;
    public double AverageRating { get; set; } = 0;
    public int RatingCount { get; set; } = 0;
    public string? Developer { get; set; }
    public string? Tags { get; set; }
    public AgeRating AgeRating { get; set; } = AgeRating.Everyone;
    public int Width { get; set; } = 800;
    public int Height { get; set; } = 600;
    public bool IsMobileCompatible { get; set; } = true;

    // Navigation properties
    public virtual Category Category { get; set; } = null!;
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public virtual ICollection<GameScore> GameScores { get; set; } = new List<GameScore>();
    public virtual ICollection<GameStatistic> GameStatistics { get; set; } = new List<GameStatistic>();
}
