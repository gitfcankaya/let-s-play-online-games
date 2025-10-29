namespace GamePlatform.API.DTOs;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? IconUrl { get; set; }
    public int DisplayOrder { get; set; }
    public int GamesCount { get; set; }
}

public class StatisticsDto
{
    public int TotalGames { get; set; }
    public int TotalUsers { get; set; }
    public int TotalPlays { get; set; }
    public int TotalViews { get; set; }
    public List<CountryStatDto> TopCountries { get; set; } = new();
    public List<GameStatDto> TopGames { get; set; } = new();
    public List<CategoryStatDto> CategoryStats { get; set; } = new();
}

public class CountryStatDto
{
    public string Country { get; set; } = string.Empty;
    public int Count { get; set; }
}

public class GameStatDto
{
    public int GameId { get; set; }
    public string GameTitle { get; set; } = string.Empty;
    public int PlayCount { get; set; }
    public int ViewCount { get; set; }
}

public class CategoryStatDto
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int GamesCount { get; set; }
    public int TotalPlays { get; set; }
}

public class TrackPlayDto
{
    public int GameId { get; set; }
    public string? DeviceType { get; set; }
    public string? Browser { get; set; }
    public string? Os { get; set; }
    public int? AgeGroup { get; set; }
    public string? Gender { get; set; }
    public string? ReferrerUrl { get; set; }
}
