using System.Collections.Concurrent;
using OilBaron.Game;

namespace OilBaron.Api.Services;

public sealed class InMemoryGameStore
{
    private readonly ConcurrentDictionary<Guid, GameState> _games = new();

    public void Add(GameState state) => _games[state.Id] = state;

    public bool TryGet(Guid id, out GameState? state) => _games.TryGetValue(id, out state);
}
