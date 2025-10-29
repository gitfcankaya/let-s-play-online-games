namespace GamePlatform.Core.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? FullName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Country { get; set; }
    public bool IsAdmin { get; set; } = false;
    public DateTime? LastLoginAt { get; set; }

    // Navigation properties
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public virtual ICollection<GameScore> GameScores { get; set; } = new List<GameScore>();
    public virtual ICollection<GameStatistic> GameStatistics { get; set; } = new List<GameStatistic>();
}
