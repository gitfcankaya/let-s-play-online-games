using GamePlatform.API.DTOs;
using GamePlatform.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GamePlatform.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet("game/{gameId}")]
    public async Task<ActionResult<List<CommentDto>>> GetGameComments(int gameId)
    {
        var comments = await _commentService.GetGameCommentsAsync(gameId);
        return Ok(comments);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<CommentDto>> CreateComment([FromBody] CreateCommentDto commentDto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var comment = await _commentService.CreateCommentAsync(userId, commentDto);
        return CreatedAtAction(nameof(GetGameComments), new { gameId = comment.GameId }, comment);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteComment(int id)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var isAdmin = User.IsInRole("Admin");
        
        var result = await _commentService.DeleteCommentAsync(id, userId, isAdmin);
        if (!result)
            return NotFound();

        return NoContent();
    }
}
