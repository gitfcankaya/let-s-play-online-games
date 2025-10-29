using GamePlatform.API.DTOs;
using GamePlatform.Core.Entities;
using GamePlatform.Data;
using Microsoft.EntityFrameworkCore;

namespace GamePlatform.API.Services;

public interface ICommentService
{
    Task<List<CommentDto>> GetGameCommentsAsync(int gameId);
    Task<CommentDto> CreateCommentAsync(int userId, CreateCommentDto commentDto);
    Task<bool> DeleteCommentAsync(int commentId, int userId, bool isAdmin);
}

public class CommentService : ICommentService
{
    private readonly GamePlatformDbContext _context;

    public CommentService(GamePlatformDbContext context)
    {
        _context = context;
    }

    public async Task<List<CommentDto>> GetGameCommentsAsync(int gameId)
    {
        var comments = await _context.Comments
            .Include(c => c.User)
            .Include(c => c.Replies)
                .ThenInclude(r => r.User)
            .Where(c => c.GameId == gameId && c.ParentCommentId == null && !c.IsDeleted)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();

        return comments.Select(MapToCommentDto).ToList();
    }

    public async Task<CommentDto> CreateCommentAsync(int userId, CreateCommentDto commentDto)
    {
        var comment = new Comment
        {
            GameId = commentDto.GameId,
            UserId = userId,
            Content = commentDto.Content,
            Rating = commentDto.Rating,
            ParentCommentId = commentDto.ParentCommentId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        // Update game rating
        if (commentDto.Rating > 0 && commentDto.ParentCommentId == null)
        {
            await UpdateGameRatingAsync(commentDto.GameId);
        }

        var savedComment = await _context.Comments
            .Include(c => c.User)
            .FirstAsync(c => c.Id == comment.Id);

        return MapToCommentDto(savedComment);
    }

    public async Task<bool> DeleteCommentAsync(int commentId, int userId, bool isAdmin)
    {
        var comment = await _context.Comments.FindAsync(commentId);
        if (comment == null)
            return false;

        if (comment.UserId != userId && !isAdmin)
            return false;

        comment.IsDeleted = true;
        comment.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    private async Task UpdateGameRatingAsync(int gameId)
    {
        var game = await _context.Games.FindAsync(gameId);
        if (game == null) return;

        var ratings = await _context.Comments
            .Where(c => c.GameId == gameId && c.Rating > 0 && c.ParentCommentId == null && !c.IsDeleted)
            .Select(c => c.Rating)
            .ToListAsync();

        if (ratings.Any())
        {
            game.AverageRating = ratings.Average();
            game.RatingCount = ratings.Count;
            await _context.SaveChangesAsync();
        }
    }

    private static CommentDto MapToCommentDto(Comment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            GameId = comment.GameId,
            UserId = comment.UserId,
            Username = comment.User.Username,
            Content = comment.Content,
            Rating = comment.Rating,
            ParentCommentId = comment.ParentCommentId,
            LikesCount = comment.LikesCount,
            CreatedAt = comment.CreatedAt,
            Replies = comment.Replies
                .Where(r => !r.IsDeleted)
                .Select(MapToCommentDto)
                .ToList()
        };
    }
}
