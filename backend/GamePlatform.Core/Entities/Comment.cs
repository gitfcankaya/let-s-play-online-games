namespace GamePlatform.Core.Entities;

public class Comment : BaseEntity
{
    public int GameId { get; set; }
    public int UserId { get; set; }
    public string Content { get; set; } = string.Empty;
    public int Rating { get; set; } = 0; // 1-5 stars
    public int? ParentCommentId { get; set; }
    public int LikesCount { get; set; } = 0;

    // Navigation properties
    public virtual Game Game { get; set; } = null!;
    public virtual User User { get; set; } = null!;
    public virtual Comment? ParentComment { get; set; }
    public virtual ICollection<Comment> Replies { get; set; } = new List<Comment>();
}
