namespace OilBaron.Game;

/// <summary>
/// Deterministic oil company simulation. All business rules live here.
/// </summary>
public static class OilGame
{
    public static GameState Create(string companyName, int? seed = null, Guid? id = null)
    {
        if (string.IsNullOrWhiteSpace(companyName))
        {
            throw new ArgumentException("Company name is required.", nameof(companyName));
        }

        var actualSeed = seed ?? Random.Shared.Next();
        var rng = new Random(actualSeed);
        var size = EconomyConstants.GridSize;
        var plots = new Plot[size, size];

        for (var y = 0; y < size; y++)
        {
            for (var x = 0; x < size; x++)
            {
                var steps = rng.Next(0, EconomyConstants.ReserveMaxSteps + 1);
                var reserve = steps * EconomyConstants.ReserveStep;
                plots[x, y] = new Plot(x, y, reserve);
            }
        }

        return new GameState(
            id ?? Guid.NewGuid(),
            companyName.Trim(),
            actualSeed,
            EconomyConstants.StartingDay,
            EconomyConstants.StartingCash,
            EconomyConstants.StartingOilBarrels,
            EconomyConstants.StartingOilPrice,
            plots,
            rng);
    }

    public static GameCommandResult BuyPlot(GameState state, int x, int y)
    {
        if (!TryGetPlot(state, x, y, out var plot))
        {
            return GameCommandResult.Fail("Plot coordinates are out of range.");
        }

        if (plot.Owned)
        {
            return GameCommandResult.Fail("Plot is already owned.");
        }

        if (state.Cash < EconomyConstants.PlotBuyCost)
        {
            return GameCommandResult.Fail("Insufficient cash to buy plot.");
        }

        state.Cash -= EconomyConstants.PlotBuyCost;
        plot.Owned = true;
        return GameCommandResult.Ok();
    }

    public static GameCommandResult DrillPlot(GameState state, int x, int y)
    {
        if (!TryGetPlot(state, x, y, out var plot))
        {
            return GameCommandResult.Fail("Plot coordinates are out of range.");
        }

        if (!plot.Owned)
        {
            return GameCommandResult.Fail("Cannot drill a plot you do not own.");
        }

        if (plot.Drilled)
        {
            return GameCommandResult.Fail("Plot is already drilled.");
        }

        if (state.Cash < EconomyConstants.DrillCost)
        {
            return GameCommandResult.Fail("Insufficient cash to drill.");
        }

        state.Cash -= EconomyConstants.DrillCost;
        plot.Drilled = true;
        return GameCommandResult.Ok();
    }

    public static GameCommandResult AdvanceDay(GameState state)
    {
        var size = state.GridSize;
        for (var y = 0; y < size; y++)
        {
            for (var x = 0; x < size; x++)
            {
                var plot = state.Plots[x, y];
                if (!plot.Producing)
                {
                    continue;
                }

                var output = Math.Max(1, plot.RemainingReserve / 100);
                var produced = Math.Min(output, plot.RemainingReserve);
                plot.RemainingReserve -= produced;
                state.OilBarrels += produced;
            }
        }

        var delta = state.Rng.Next(
            EconomyConstants.OilPriceDeltaMin,
            EconomyConstants.OilPriceDeltaMaxInclusive + 1);
        state.OilPrice = Math.Clamp(
            state.OilPrice + delta,
            EconomyConstants.OilPriceMin,
            EconomyConstants.OilPriceMax);

        state.Day += 1;
        return GameCommandResult.Ok();
    }

    public static GameCommandResult SellOil(GameState state)
    {
        if (state.OilBarrels <= 0)
        {
            return GameCommandResult.Ok();
        }

        var revenue = state.OilBarrels * state.OilPrice;
        state.Cash += revenue;
        state.OilBarrels = 0;
        return GameCommandResult.Ok();
    }

    private static bool TryGetPlot(GameState state, int x, int y, out Plot plot)
    {
        var size = state.GridSize;
        if (x < 0 || y < 0 || x >= size || y >= size)
        {
            plot = null!;
            return false;
        }

        plot = state.Plots[x, y];
        return true;
    }
}
