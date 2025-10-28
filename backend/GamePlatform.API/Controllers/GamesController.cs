using GamePlatform.API.DTOs;
using GamePlatform.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GamePlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly IGameService _gameService;
    private readonly IStatisticsService _statisticsService;

    public GamesController(IGameService gameService, IStatisticsService statisticsService)
    {
        _gameService = gameService;
        _statisticsService = statisticsService;
    }

    [HttpGet]
    public async Task<ActionResult<List<GameDto>>> GetGames(
        [FromQuery] int? categoryId = null,
        [FromQuery] bool? isFeatured = null)
    {
        var games = await _gameService.GetAllGamesAsync(categoryId, isFeatured);
        return Ok(games);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GameDto>> GetGame(int id)
    {
        var game = await _gameService.GetGameByIdAsync(id);
        if (game == null)
            return NotFound();

        await _gameService.IncrementViewCountAsync(id);
        return Ok(game);
    }

    [HttpGet("slug/{slug}")]
    public async Task<ActionResult<GameDto>> GetGameBySlug(string slug)
    {
        var game = await _gameService.GetGameBySlugAsync(slug);
        if (game == null)
            return NotFound();

        await _gameService.IncrementViewCountAsync(game.Id);
        return Ok(game);
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<GameDto>>> SearchGames([FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return BadRequest("Search query is required");

        var games = await _gameService.SearchGamesAsync(query);
        return Ok(games);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<GameDto>> CreateGame([FromBody] CreateGameDto gameDto)
    {
        var game = await _gameService.CreateGameAsync(gameDto);
        return CreatedAtAction(nameof(GetGame), new { id = game.Id }, game);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<GameDto>> UpdateGame(int id, [FromBody] UpdateGameDto gameDto)
    {
        if (id != gameDto.Id)
            return BadRequest();

        var game = await _gameService.UpdateGameAsync(id, gameDto);
        if (game == null)
            return NotFound();

        return Ok(game);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteGame(int id)
    {
        var result = await _gameService.DeleteGameAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpPost("{id}/play")]
    public async Task<ActionResult> TrackPlay(int id, [FromBody] TrackPlayDto trackData)
    {
        await _gameService.IncrementPlayCountAsync(id);

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var country = HttpContext.Connection.RemoteIpAddress?.ToString(); // In production, use IP geolocation service
        
        await _statisticsService.TrackGamePlayAsync(
            id,
            userId != null ? int.Parse(userId) : null,
            trackData,
            country,
            null
        );

        return Ok();
    }
}
