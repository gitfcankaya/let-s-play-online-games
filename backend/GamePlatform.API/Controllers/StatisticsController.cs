using GamePlatform.API.DTOs;
using GamePlatform.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamePlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController : ControllerBase
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsController(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    [HttpGet]
    public async Task<ActionResult<StatisticsDto>> GetStatistics()
    {
        var statistics = await _statisticsService.GetPlatformStatisticsAsync();
        return Ok(statistics);
    }

    [HttpGet("countries")]
    public async Task<ActionResult<List<CountryStatDto>>> GetTopCountries([FromQuery] int limit = 10)
    {
        var countries = await _statisticsService.GetTopCountriesAsync(limit);
        return Ok(countries);
    }

    [HttpGet("top-games")]
    public async Task<ActionResult<List<GameStatDto>>> GetTopGames([FromQuery] int limit = 10)
    {
        var games = await _statisticsService.GetTopGamesAsync(limit);
        return Ok(games);
    }
}
