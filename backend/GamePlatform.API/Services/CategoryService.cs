using GamePlatform.API.DTOs;
using GamePlatform.Data;
using Microsoft.EntityFrameworkCore;

namespace GamePlatform.API.Services;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllCategoriesAsync();
    Task<CategoryDto?> GetCategoryByIdAsync(int id);
    Task<CategoryDto?> GetCategoryBySlugAsync(string slug);
}

public class CategoryService : ICategoryService
{
    private readonly GamePlatformDbContext _context;

    public CategoryService(GamePlatformDbContext context)
    {
        _context = context;
    }

    public async Task<List<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await _context.Categories
            .Where(c => !c.IsDeleted)
            .OrderBy(c => c.DisplayOrder)
            .ToListAsync();

        return categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name,
            Slug = c.Slug,
            Description = c.Description,
            IconUrl = c.IconUrl,
            DisplayOrder = c.DisplayOrder,
            GamesCount = c.Games.Count(g => !g.IsDeleted && g.IsActive)
        }).ToList();
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

        if (category == null) return null;

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            Description = category.Description,
            IconUrl = category.IconUrl,
            DisplayOrder = category.DisplayOrder,
            GamesCount = category.Games.Count(g => !g.IsDeleted && g.IsActive)
        };
    }

    public async Task<CategoryDto?> GetCategoryBySlugAsync(string slug)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Slug == slug && !c.IsDeleted);

        if (category == null) return null;

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
            Description = category.Description,
            IconUrl = category.IconUrl,
            DisplayOrder = category.DisplayOrder,
            GamesCount = category.Games.Count(g => !g.IsDeleted && g.IsActive)
        };
    }
}
