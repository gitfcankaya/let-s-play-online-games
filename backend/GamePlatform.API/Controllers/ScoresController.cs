using GamePlatform.API.DTOs;
using GamePlatform.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GamePlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScoresController : ControllerBase
{
    private readonly IScoreService _scoreService;

    public ScoresController(IScoreService scoreService)
    {
        _scoreService = scoreService;
    }

    [HttpGet("game/{gameId}")]
    public async Task<ActionResult<List<GameScoreDto>>> GetGameLeaderboard(int gameId, [FromQuery] int limit = 100)
    {
        var scores = await _scoreService.GetGameLeaderboardAsync(gameId, limit);
        return Ok(scores);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<GameScoreDto>> SubmitScore([FromBody] CreateGameScoreDto scoreDto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var country = HttpContext.Connection.RemoteIpAddress?.ToString(); // In production, use IP geolocation
        
        var score = await _scoreService.SubmitScoreAsync(userId, scoreDto, country);
        return CreatedAtAction(nameof(GetGameLeaderboard), new { gameId = score.GameId }, score);
    }

    [Authorize]
    [HttpGet("game/{gameId}/my-best")]
    public async Task<ActionResult<int>> GetMyBestScore(int gameId)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var bestScore = await _scoreService.GetUserBestScoreAsync(gameId, userId);
        
        if (bestScore == null)
            return NotFound();

        return Ok(new { score = bestScore });
    }
}
