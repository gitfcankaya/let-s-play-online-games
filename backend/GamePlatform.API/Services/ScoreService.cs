using GamePlatform.API.DTOs;
using GamePlatform.Core.Entities;
using GamePlatform.Data;
using Microsoft.EntityFrameworkCore;

namespace GamePlatform.API.Services;

public interface IScoreService
{
    Task<List<GameScoreDto>> GetGameLeaderboardAsync(int gameId, int limit = 100);
    Task<GameScoreDto> SubmitScoreAsync(int userId, CreateGameScoreDto scoreDto, string? country);
    Task<int?> GetUserBestScoreAsync(int gameId, int userId);
}

public class ScoreService : IScoreService
{
    private readonly GamePlatformDbContext _context;

    public ScoreService(GamePlatformDbContext context)
    {
        _context = context;
    }

    public async Task<List<GameScoreDto>> GetGameLeaderboardAsync(int gameId, int limit = 100)
    {
        var scores = await _context.GameScores
            .Where(s => s.GameId == gameId && !s.IsDeleted)
            .OrderByDescending(s => s.Score)
            .Take(limit)
            .ToListAsync();

        return scores.Select(s => new GameScoreDto
        {
            Id = s.Id,
            GameId = s.GameId,
            UserId = s.UserId,
            PlayerName = s.PlayerName,
            Score = s.Score,
            Country = s.Country,
            CreatedAt = s.CreatedAt
        }).ToList();
    }

    public async Task<GameScoreDto> SubmitScoreAsync(int userId, CreateGameScoreDto scoreDto, string? country)
    {
        var score = new GameScore
        {
            GameId = scoreDto.GameId,
            UserId = userId,
            Score = scoreDto.Score,
            PlayerName = scoreDto.PlayerName,
            Country = country,
            CreatedAt = DateTime.UtcNow
        };

        _context.GameScores.Add(score);
        await _context.SaveChangesAsync();

        return new GameScoreDto
        {
            Id = score.Id,
            GameId = score.GameId,
            UserId = score.UserId,
            PlayerName = score.PlayerName,
            Score = score.Score,
            Country = score.Country,
            CreatedAt = score.CreatedAt
        };
    }

    public async Task<int?> GetUserBestScoreAsync(int gameId, int userId)
    {
        var bestScore = await _context.GameScores
            .Where(s => s.GameId == gameId && s.UserId == userId && !s.IsDeleted)
            .MaxAsync(s => (int?)s.Score);

        return bestScore;
    }
}
