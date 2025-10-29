using GamePlatform.Core.Enums;

namespace GamePlatform.API.DTOs;

public class GameDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public string? FullImageUrl { get; set; }
    public string GameUrl { get; set; } = string.Empty;
    public GameType GameType { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool IsFeatured { get; set; }
    public int ViewCount { get; set; }
    public int PlayCount { get; set; }
    public double AverageRating { get; set; }
    public int RatingCount { get; set; }
    public string? Developer { get; set; }
    public string? Tags { get; set; }
    public AgeRating AgeRating { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public bool IsMobileCompatible { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateGameDto
{
    public string Title { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public string? FullImageUrl { get; set; }
    public string GameUrl { get; set; } = string.Empty;
    public GameType GameType { get; set; }
    public int CategoryId { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsFeatured { get; set; } = false;
    public string? Developer { get; set; }
    public string? Tags { get; set; }
    public AgeRating AgeRating { get; set; } = AgeRating.Everyone;
    public int Width { get; set; } = 800;
    public int Height { get; set; } = 600;
    public bool IsMobileCompatible { get; set; } = true;
}

public class UpdateGameDto : CreateGameDto
{
    public int Id { get; set; }
}
