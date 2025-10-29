using GamePlatform.API.DTOs;
using GamePlatform.Core.Entities;
using GamePlatform.Data;
using Microsoft.EntityFrameworkCore;

namespace GamePlatform.API.Services;

public interface IStatisticsService
{
    Task TrackGamePlayAsync(int gameId, int? userId, TrackPlayDto trackData, string? country, string? city);
    Task<StatisticsDto> GetPlatformStatisticsAsync();
    Task<List<CountryStatDto>> GetTopCountriesAsync(int limit = 10);
    Task<List<GameStatDto>> GetTopGamesAsync(int limit = 10);
}

public class StatisticsService : IStatisticsService
{
    private readonly GamePlatformDbContext _context;

    public StatisticsService(GamePlatformDbContext context)
    {
        _context = context;
    }

    public async Task TrackGamePlayAsync(int gameId, int? userId, TrackPlayDto trackData, string? country, string? city)
    {
        var statistic = new GameStatistic
        {
            GameId = gameId,
            UserId = userId,
            Country = country,
            City = city,
            DeviceType = trackData.DeviceType,
            Browser = trackData.Browser,
            Os = trackData.Os,
            AgeGroup = trackData.AgeGroup,
            Gender = trackData.Gender,
            ReferrerUrl = trackData.ReferrerUrl,
            SessionDuration = 0,
            CreatedAt = DateTime.UtcNow
        };

        _context.GameStatistics.Add(statistic);
        await _context.SaveChangesAsync();
    }

    public async Task<StatisticsDto> GetPlatformStatisticsAsync()
    {
        var totalGames = await _context.Games.CountAsync(g => !g.IsDeleted);
        var totalUsers = await _context.Users.CountAsync(u => !u.IsDeleted);
        var totalPlays = await _context.Games.SumAsync(g => g.PlayCount);
        var totalViews = await _context.Games.SumAsync(g => g.ViewCount);

        var topCountries = await GetTopCountriesAsync(10);
        var topGames = await GetTopGamesAsync(10);
        var categoryStats = await GetCategoryStatisticsAsync();

        return new StatisticsDto
        {
            TotalGames = totalGames,
            TotalUsers = totalUsers,
            TotalPlays = totalPlays,
            TotalViews = totalViews,
            TopCountries = topCountries,
            TopGames = topGames,
            CategoryStats = categoryStats
        };
    }

    public async Task<List<CountryStatDto>> GetTopCountriesAsync(int limit = 10)
    {
        var stats = await _context.GameStatistics
            .Where(s => !string.IsNullOrEmpty(s.Country))
            .GroupBy(s => s.Country)
            .Select(g => new CountryStatDto
            {
                Country = g.Key!,
                Count = g.Count()
            })
            .OrderByDescending(s => s.Count)
            .Take(limit)
            .ToListAsync();

        return stats;
    }

    public async Task<List<GameStatDto>> GetTopGamesAsync(int limit = 10)
    {
        var games = await _context.Games
            .Where(g => !g.IsDeleted)
            .OrderByDescending(g => g.PlayCount)
            .Take(limit)
            .Select(g => new GameStatDto
            {
                GameId = g.Id,
                GameTitle = g.Title,
                PlayCount = g.PlayCount,
                ViewCount = g.ViewCount
            })
            .ToListAsync();

        return games;
    }

    private async Task<List<CategoryStatDto>> GetCategoryStatisticsAsync()
    {
        var categoryStats = await _context.Categories
            .Include(c => c.Games)
            .Where(c => !c.IsDeleted)
            .Select(c => new CategoryStatDto
            {
                CategoryId = c.Id,
                CategoryName = c.Name,
                GamesCount = c.Games.Count(g => !g.IsDeleted),
                TotalPlays = c.Games.Sum(g => g.PlayCount)
            })
            .OrderByDescending(s => s.TotalPlays)
            .ToListAsync();

        return categoryStats;
    }
}
