namespace GamePlatform.Core.Entities;

public class GameScore : BaseEntity
{
    public int GameId { get; set; }
    public int UserId { get; set; }
    public int Score { get; set; }
    public string? PlayerName { get; set; }
    public string? Country { get; set; }

    // Navigation properties
    public virtual Game Game { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
