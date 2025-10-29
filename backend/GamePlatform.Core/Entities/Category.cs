namespace GamePlatform.Core.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? IconUrl { get; set; }
    public int DisplayOrder { get; set; }

    // Navigation properties
    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}
