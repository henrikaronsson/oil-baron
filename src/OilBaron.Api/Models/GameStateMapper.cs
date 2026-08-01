using OilBaron.Game;

namespace OilBaron.Api.Models;

public static class GameStateMapper
{
    public static GameStateDto ToDto(GameState state)
    {
        var plots = new List<PlotDto>();
        for (var y = 0; y < state.GridSize; y++)
        {
            for (var x = 0; x < state.GridSize; x++)
            {
                var plot = state.Plots[x, y];
                plots.Add(new PlotDto
                {
                    X = plot.X,
                    Y = plot.Y,
                    Owned = plot.Owned,
                    Drilled = plot.Drilled,
                    Producing = plot.Producing,
                    RemainingReserve = plot.Drilled ? plot.RemainingReserve : null
                });
            }
        }

        return new GameStateDto
        {
            Id = state.Id,
            CompanyName = state.CompanyName,
            Seed = state.Seed,
            Day = state.Day,
            Cash = state.Cash,
            OilBarrels = state.OilBarrels,
            OilPrice = state.OilPrice,
            GridSize = state.GridSize,
            Plots = plots
        };
    }
}
