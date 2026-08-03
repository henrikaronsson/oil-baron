using OilBaron.Game;

namespace OilBaron.Api.Models;

public static class GameStateMapper
{
    public static GameStateDto ToDto(GameState state)
    {
        var oilFields = new List<OilFieldDto>();
        for (var y = 0; y < state.GridSize; y++)
        {
            for (var x = 0; x < state.GridSize; x++)
            {
                var field = state.OilFields[x, y];
                oilFields.Add(new OilFieldDto
                {
                    X = field.X,
                    Y = field.Y,
                    Owned = field.Owned,
                    Drilled = field.Drilled,
                    Producing = field.Producing,
                    PurchasePrice = field.PurchasePrice,
                    MonthlyProduction = field.MonthlyProduction,
                    OperatingCostPerMonth = field.OperatingCostPerMonth,
                    EstimatedReserves = field.EstimatedReserves,
                    RemainingReserves = field.Drilled ? field.RemainingReserves : null
                });
            }
        }

        var (year, calendarMonth, day) = GameCalendar.FromMonthIndex(state.Month);

        return new GameStateDto
        {
            Id = state.Id,
            CompanyName = state.CompanyName,
            Seed = state.Seed,
            Month = state.Month,
            CalendarYear = year,
            CalendarMonth = calendarMonth,
            CalendarDay = day,
            Cash = state.Cash,
            OilBarrels = state.OilBarrels,
            OilPrice = state.OilPrice,
            GridSize = state.GridSize,
            OilFields = oilFields
        };
    }
}
