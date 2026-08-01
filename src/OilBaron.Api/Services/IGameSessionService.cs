using OilBaron.Api.Models;
using OilBaron.Game;

namespace OilBaron.Api.Services;

public interface IGameSessionService
{
    GameStateDto Create(string companyName, int? seed);
    GameStateDto? Get(Guid id);
    (GameStateDto? State, string? Error, bool NotFound) Buy(Guid id, int x, int y);
    (GameStateDto? State, string? Error, bool NotFound) Drill(Guid id, int x, int y);
    (GameStateDto? State, string? Error, bool NotFound) AdvanceDay(Guid id);
    (GameStateDto? State, string? Error, bool NotFound) SellOil(Guid id);
}
