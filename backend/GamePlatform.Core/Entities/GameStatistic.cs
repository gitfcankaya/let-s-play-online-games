namespace GamePlatform.Core.Entities;

public class GameStatistic : BaseEntity
{
    public int GameId { get; set; }
    public int? UserId { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? DeviceType { get; set; }
    public string? Browser { get; set; }
    public string? Os { get; set; }
    public int? AgeGroup { get; set; } // Age ranges: 0-12, 13-17, 18-24, 25-34, 35-44, 45-54, 55+
    public string? Gender { get; set; }
    public int SessionDuration { get; set; } // in seconds
    public bool CompletedGame { get; set; } = false;
    public string? ReferrerUrl { get; set; }

    // Navigation properties
    public virtual Game Game { get; set; } = null!;
    public virtual User? User { get; set; }
}
