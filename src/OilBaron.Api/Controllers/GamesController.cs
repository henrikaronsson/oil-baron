using Microsoft.AspNetCore.Mvc;
using OilBaron.Api.Models;
using OilBaron.Api.Services;

namespace OilBaron.Api.Controllers;

[ApiController]
[Route("api/games")]
public sealed class GamesController(IGameSessionService games) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(GameStateDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public ActionResult<GameStateDto> Create([FromBody] CreateGameRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.CompanyName))
        {
            return BadRequest(new ErrorResponse("Company name is required."));
        }

        var state = games.Create(request.CompanyName, request.Seed);
        return CreatedAtAction(nameof(Get), new { id = state.Id }, state);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GameStateDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<GameStateDto> Get(Guid id)
    {
        var state = games.Get(id);
        return state is null ? NotFound() : state;
    }

    [HttpPost("{id:guid}/fields/{x:int}/{y:int}/buy")]
    [ProducesResponseType(typeof(GameStateDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<GameStateDto> Buy(Guid id, int x, int y)
        => FromMutation(games.Buy(id, x, y));

    [HttpPost("{id:guid}/fields/{x:int}/{y:int}/drill")]
    [ProducesResponseType(typeof(GameStateDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<GameStateDto> Drill(Guid id, int x, int y)
        => FromMutation(games.Drill(id, x, y));

    [HttpPost("{id:guid}/advance-month")]
    [ProducesResponseType(typeof(GameStateDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<GameStateDto> AdvanceMonth(Guid id)
        => FromMutation(games.AdvanceMonth(id));

    [HttpPost("{id:guid}/sell-oil")]
    [ProducesResponseType(typeof(GameStateDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<GameStateDto> SellOil(Guid id)
        => FromMutation(games.SellOil(id));

    private ActionResult<GameStateDto> FromMutation(
        (GameStateDto? State, string? Error, bool NotFound) result)
    {
        if (result.NotFound)
        {
            return NotFound();
        }

        if (result.Error is not null)
        {
            return BadRequest(new ErrorResponse(result.Error));
        }

        return result.State!;
    }
}
