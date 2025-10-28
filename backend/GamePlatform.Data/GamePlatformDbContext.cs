using GamePlatform.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamePlatform.Data;

public class GamePlatformDbContext : DbContext
{
    public GamePlatformDbContext(DbContextOptions<GamePlatformDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<GameScore> GameScores { get; set; }
    public DbSet<GameStatistic> GameStatistics { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.Username).IsUnique();
            entity.Property(e => e.Username).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
            entity.Property(e => e.PasswordHash).HasMaxLength(500).IsRequired();
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
        });

        // Category configuration
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Slug).IsUnique();
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Slug).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IconUrl).HasMaxLength(500);
        });

        // Game configuration
        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Slug).IsUnique();
            entity.HasIndex(e => e.CategoryId);
            entity.Property(e => e.Title).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Slug).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(2000).IsRequired();
            entity.Property(e => e.ThumbnailUrl).HasMaxLength(500).IsRequired();
            entity.Property(e => e.FullImageUrl).HasMaxLength(500);
            entity.Property(e => e.GameUrl).HasMaxLength(1000).IsRequired();
            entity.Property(e => e.Developer).HasMaxLength(200);
            entity.Property(e => e.Tags).HasMaxLength(500);

            entity.HasOne(e => e.Category)
                .WithMany(c => c.Games)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Comment configuration
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.GameId);
            entity.HasIndex(e => e.UserId);
            entity.Property(e => e.Content).HasMaxLength(2000).IsRequired();

            entity.HasOne(e => e.Game)
                .WithMany(g => g.Comments)
                .HasForeignKey(e => e.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.ParentComment)
                .WithMany(c => c.Replies)
                .HasForeignKey(e => e.ParentCommentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // GameScore configuration
        modelBuilder.Entity<GameScore>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.GameId);
            entity.HasIndex(e => e.UserId);
            entity.Property(e => e.PlayerName).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);

            entity.HasOne(e => e.Game)
                .WithMany(g => g.GameScores)
                .HasForeignKey(e => e.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.User)
                .WithMany(u => u.GameScores)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // GameStatistic configuration
        modelBuilder.Entity<GameStatistic>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.GameId);
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.Country);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.DeviceType).HasMaxLength(50);
            entity.Property(e => e.Browser).HasMaxLength(100);
            entity.Property(e => e.Os).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.ReferrerUrl).HasMaxLength(1000);

            entity.HasOne(e => e.Game)
                .WithMany(g => g.GameStatistics)
                .HasForeignKey(e => e.GameId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.User)
                .WithMany(u => u.GameStatistics)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        // Seed data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Action", Slug = "action", Description = "Fast-paced action games", DisplayOrder = 1, CreatedAt = DateTime.UtcNow },
            new Category { Id = 2, Name = "Puzzle", Slug = "puzzle", Description = "Brain-teasing puzzle games", DisplayOrder = 2, CreatedAt = DateTime.UtcNow },
            new Category { Id = 3, Name = "Racing", Slug = "racing", Description = "Exciting racing games", DisplayOrder = 3, CreatedAt = DateTime.UtcNow },
            new Category { Id = 4, Name = "Sports", Slug = "sports", Description = "Sports and athletics games", DisplayOrder = 4, CreatedAt = DateTime.UtcNow },
            new Category { Id = 5, Name = "Adventure", Slug = "adventure", Description = "Explore new worlds", DisplayOrder = 5, CreatedAt = DateTime.UtcNow },
            new Category { Id = 6, Name = "Arcade", Slug = "arcade", Description = "Classic arcade games", DisplayOrder = 6, CreatedAt = DateTime.UtcNow },
            new Category { Id = 7, Name = "Strategy", Slug = "strategy", Description = "Strategic thinking games", DisplayOrder = 7, CreatedAt = DateTime.UtcNow },
            new Category { Id = 8, Name = "Shooting", Slug = "shooting", Description = "Shooter games", DisplayOrder = 8, CreatedAt = DateTime.UtcNow }
        );

        // Seed default admin user
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "admin",
                Email = "admin@gameplatform.com",
                PasswordHash = "$2a$11$1234567890123456789012uO1234567890123456789012345678901234", // Placeholder - should be properly hashed
                FullName = "Admin User",
                IsAdmin = true,
                CreatedAt = DateTime.UtcNow
            }
        );

        // Seed sample games
        modelBuilder.Entity<Game>().HasData(
            new Game
            {
                Id = 1,
                Title = "2048",
                Slug = "2048",
                Description = "Join the tiles with the same numbers and get to the 2048 tile!",
                ThumbnailUrl = "/images/games/2048-thumb.jpg",
                GameUrl = "https://play2048.co/",
                GameType = Core.Enums.GameType.Iframe,
                CategoryId = 2,
                IsActive = true,
                IsFeatured = true,
                AgeRating = Core.Enums.AgeRating.Everyone,
                IsMobileCompatible = true,
                Developer = "Gabriele Cirulli",
                Tags = "puzzle,numbers,strategy",
                CreatedAt = DateTime.UtcNow
            },
            new Game
            {
                Id = 2,
                Title = "Tetris",
                Slug = "tetris",
                Description = "The classic block-stacking puzzle game!",
                ThumbnailUrl = "/images/games/tetris-thumb.jpg",
                GameUrl = "https://tetris.com/play-tetris",
                GameType = Core.Enums.GameType.Html5,
                CategoryId = 2,
                IsActive = true,
                IsFeatured = true,
                AgeRating = Core.Enums.AgeRating.Everyone,
                IsMobileCompatible = true,
                Tags = "puzzle,classic,arcade",
                CreatedAt = DateTime.UtcNow
            },
            new Game
            {
                Id = 3,
                Title = "Subway Surfers",
                Slug = "subway-surfers",
                Description = "Dash as fast as you can and dodge the oncoming trains!",
                ThumbnailUrl = "/images/games/subway-surfers-thumb.jpg",
                GameUrl = "https://poki.com/en/g/subway-surfers",
                GameType = Core.Enums.GameType.Html5,
                CategoryId = 1,
                IsActive = true,
                IsFeatured = true,
                AgeRating = Core.Enums.AgeRating.Everyone,
                IsMobileCompatible = true,
                Developer = "SYBO Games",
                Tags = "running,action,endless",
                CreatedAt = DateTime.UtcNow
            },
            new Game
            {
                Id = 4,
                Title = "Temple Run",
                Slug = "temple-run",
                Description = "Run for your life in this exciting endless runner!",
                ThumbnailUrl = "/images/games/temple-run-thumb.jpg",
                GameUrl = "https://poki.com/en/g/temple-run-2",
                GameType = Core.Enums.GameType.Html5,
                CategoryId = 1,
                IsActive = true,
                IsFeatured = false,
                AgeRating = Core.Enums.AgeRating.Everyone,
                IsMobileCompatible = true,
                Developer = "Imangi Studios",
                Tags = "running,action,adventure",
                CreatedAt = DateTime.UtcNow
            },
            new Game
            {
                Id = 5,
                Title = "8 Ball Pool",
                Slug = "8-ball-pool",
                Description = "Play pool against players from around the world!",
                ThumbnailUrl = "/images/games/8ball-thumb.jpg",
                GameUrl = "https://www.miniclip.com/games/8-ball-pool-multiplayer/en/",
                GameType = Core.Enums.GameType.Html5,
                CategoryId = 4,
                IsActive = true,
                IsFeatured = true,
                AgeRating = Core.Enums.AgeRating.Everyone,
                IsMobileCompatible = true,
                Developer = "Miniclip",
                Tags = "sports,pool,multiplayer",
                CreatedAt = DateTime.UtcNow
            }
        );
    }
}
