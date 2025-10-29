using GamePlatform.API.DTOs;
using GamePlatform.Core.Entities;
using GamePlatform.Data;
using Microsoft.EntityFrameworkCore;

namespace GamePlatform.API.Services;

public interface IGameService
{
    Task<List<GameDto>> GetAllGamesAsync(int? categoryId = null, bool? isFeatured = null, bool activeOnly = true);
    Task<GameDto?> GetGameByIdAsync(int id);
    Task<GameDto?> GetGameBySlugAsync(string slug);
    Task<GameDto> CreateGameAsync(CreateGameDto gameDto);
    Task<GameDto?> UpdateGameAsync(int id, UpdateGameDto gameDto);
    Task<bool> DeleteGameAsync(int id);
    Task<bool> IncrementViewCountAsync(int gameId);
    Task<bool> IncrementPlayCountAsync(int gameId);
    Task<List<GameDto>> SearchGamesAsync(string query);
}

public class GameService : IGameService
{
    private readonly GamePlatformDbContext _context;

    public GameService(GamePlatformDbContext context)
    {
        _context = context;
    }

    public async Task<List<GameDto>> GetAllGamesAsync(int? categoryId = null, bool? isFeatured = null, bool activeOnly = true)
    {
        var query = _context.Games.Include(g => g.Category).AsQueryable();

        if (activeOnly)
            query = query.Where(g => g.IsActive && !g.IsDeleted);

        if (categoryId.HasValue)
            query = query.Where(g => g.CategoryId == categoryId.Value);

        if (isFeatured.HasValue)
            query = query.Where(g => g.IsFeatured == isFeatured.Value);

        var games = await query.OrderByDescending(g => g.IsFeatured)
                              .ThenByDescending(g => g.PlayCount)
                              .ToListAsync();

        return games.Select(MapToGameDto).ToList();
    }

    public async Task<GameDto?> GetGameByIdAsync(int id)
    {
        var game = await _context.Games.Include(g => g.Category)
                                      .FirstOrDefaultAsync(g => g.Id == id && !g.IsDeleted);
        return game == null ? null : MapToGameDto(game);
    }

    public async Task<GameDto?> GetGameBySlugAsync(string slug)
    {
        var game = await _context.Games.Include(g => g.Category)
                                      .FirstOrDefaultAsync(g => g.Slug == slug && !g.IsDeleted);
        return game == null ? null : MapToGameDto(game);
    }

    public async Task<GameDto> CreateGameAsync(CreateGameDto gameDto)
    {
        var game = new Game
        {
            Title = gameDto.Title,
            Slug = gameDto.Slug,
            Description = gameDto.Description,
            ThumbnailUrl = gameDto.ThumbnailUrl,
            FullImageUrl = gameDto.FullImageUrl,
            GameUrl = gameDto.GameUrl,
            GameType = gameDto.GameType,
            CategoryId = gameDto.CategoryId,
            IsActive = gameDto.IsActive,
            IsFeatured = gameDto.IsFeatured,
            Developer = gameDto.Developer,
            Tags = gameDto.Tags,
            AgeRating = gameDto.AgeRating,
            Width = gameDto.Width,
            Height = gameDto.Height,
            IsMobileCompatible = gameDto.IsMobileCompatible,
            CreatedAt = DateTime.UtcNow
        };

        _context.Games.Add(game);
        await _context.SaveChangesAsync();

        return (await GetGameByIdAsync(game.Id))!;
    }

    public async Task<GameDto?> UpdateGameAsync(int id, UpdateGameDto gameDto)
    {
        var game = await _context.Games.FindAsync(id);
        if (game == null || game.IsDeleted)
            return null;

        game.Title = gameDto.Title;
        game.Slug = gameDto.Slug;
        game.Description = gameDto.Description;
        game.ThumbnailUrl = gameDto.ThumbnailUrl;
        game.FullImageUrl = gameDto.FullImageUrl;
        game.GameUrl = gameDto.GameUrl;
        game.GameType = gameDto.GameType;
        game.CategoryId = gameDto.CategoryId;
        game.IsActive = gameDto.IsActive;
        game.IsFeatured = gameDto.IsFeatured;
        game.Developer = gameDto.Developer;
        game.Tags = gameDto.Tags;
        game.AgeRating = gameDto.AgeRating;
        game.Width = gameDto.Width;
        game.Height = gameDto.Height;
        game.IsMobileCompatible = gameDto.IsMobileCompatible;
        game.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return await GetGameByIdAsync(id);
    }

    public async Task<bool> DeleteGameAsync(int id)
    {
        var game = await _context.Games.FindAsync(id);
        if (game == null)
            return false;

        game.IsDeleted = true;
        game.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> IncrementViewCountAsync(int gameId)
    {
        var game = await _context.Games.FindAsync(gameId);
        if (game == null || game.IsDeleted)
            return false;

        game.ViewCount++;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> IncrementPlayCountAsync(int gameId)
    {
        var game = await _context.Games.FindAsync(gameId);
        if (game == null || game.IsDeleted)
            return false;

        game.PlayCount++;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<GameDto>> SearchGamesAsync(string query)
    {
        var games = await _context.Games.Include(g => g.Category)
                                       .Where(g => !g.IsDeleted && g.IsActive &&
                                                  (g.Title.Contains(query) ||
                                                   g.Description.Contains(query) ||
                                                   (g.Tags != null && g.Tags.Contains(query))))
                                       .OrderByDescending(g => g.PlayCount)
                                       .Take(20)
                                       .ToListAsync();

        return games.Select(MapToGameDto).ToList();
    }

    private static GameDto MapToGameDto(Game game)
    {
        return new GameDto
        {
            Id = game.Id,
            Title = game.Title,
            Slug = game.Slug,
            Description = game.Description,
            ThumbnailUrl = game.ThumbnailUrl,
            FullImageUrl = game.FullImageUrl,
            GameUrl = game.GameUrl,
            GameType = game.GameType,
            CategoryId = game.CategoryId,
            CategoryName = game.Category.Name,
            IsActive = game.IsActive,
            IsFeatured = game.IsFeatured,
            ViewCount = game.ViewCount,
            PlayCount = game.PlayCount,
            AverageRating = game.AverageRating,
            RatingCount = game.RatingCount,
            Developer = game.Developer,
            Tags = game.Tags,
            AgeRating = game.AgeRating,
            Width = game.Width,
            Height = game.Height,
            IsMobileCompatible = game.IsMobileCompatible,
            CreatedAt = game.CreatedAt
        };
    }
}
