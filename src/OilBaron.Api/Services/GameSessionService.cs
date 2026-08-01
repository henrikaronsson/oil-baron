using OilBaron.Api.Models;
using OilBaron.Game;

namespace OilBaron.Api.Services;

public sealed class GameSessionService(InMemoryGameStore store) : IGameSessionService
{
    public GameStateDto Create(string companyName, int? seed)
    {
        var state = OilGame.Create(companyName, seed);
        store.Add(state);
        return GameStateMapper.ToDto(state);
    }

    public GameStateDto? Get(Guid id)
    {
        return store.TryGet(id, out var state) ? GameStateMapper.ToDto(state!) : null;
    }

    public (GameStateDto? State, string? Error, bool NotFound) Buy(Guid id, int x, int y)
        => Mutate(id, s => OilGame.BuyPlot(s, x, y));

    public (GameStateDto? State, string? Error, bool NotFound) Drill(Guid id, int x, int y)
        => Mutate(id, s => OilGame.DrillPlot(s, x, y));

    public (GameStateDto? State, string? Error, bool NotFound) AdvanceDay(Guid id)
        => Mutate(id, OilGame.AdvanceDay);

    public (GameStateDto? State, string? Error, bool NotFound) SellOil(Guid id)
        => Mutate(id, OilGame.SellOil);

    private (GameStateDto? State, string? Error, bool NotFound) Mutate(
        Guid id,
        Func<GameState, GameCommandResult> command)
    {
        if (!store.TryGet(id, out var state) || state is null)
        {
            return (null, null, true);
        }

        lock (state)
        {
            var result = command(state);
            if (!result.Success)
            {
                return (null, result.Error, false);
            }

            return (GameStateMapper.ToDto(state), null, false);
        }
    }
}
