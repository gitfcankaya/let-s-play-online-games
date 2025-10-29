namespace GamePlatform.API.DTOs;

public class CommentDto
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; }
    public int? ParentCommentId { get; set; }
    public int LikesCount { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<CommentDto> Replies { get; set; } = new();
}

public class CreateCommentDto
{
    public int GameId { get; set; }
    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; } = 0;
    public int? ParentCommentId { get; set; }
}

public class GameScoreDto
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int UserId { get; set; }
    public string? PlayerName { get; set; }
    public int Score { get; set; }
    public string? Country { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateGameScoreDto
{
    public int GameId { get; set; }
    public int Score { get; set; }
    public string? PlayerName { get; set; }
}
