using OilBaron.Game;

namespace OilBaron.Game.Tests;

public class OilGameTests
{
    [Fact]
    public void Same_seed_produces_identical_initial_reserves()
    {
        var a = OilGame.Create("A", seed: 42);
        var b = OilGame.Create("B", seed: 42);

        for (var y = 0; y < a.GridSize; y++)
        {
            for (var x = 0; x < a.GridSize; x++)
            {
                Assert.Equal(a.Plots[x, y].RemainingReserve, b.Plots[x, y].RemainingReserve);
            }
        }

        Assert.Equal(a.Seed, b.Seed);
        Assert.Equal(EconomyConstants.StartingCash, a.Cash);
        Assert.Equal(EconomyConstants.StartingOilPrice, a.OilPrice);
    }

    [Fact]
    public void Same_seed_and_commands_produce_identical_price_sequence()
    {
        var a = OilGame.Create("A", seed: 99);
        var b = OilGame.Create("B", seed: 99);

        var pricesA = new List<int> { a.OilPrice };
        var pricesB = new List<int> { b.OilPrice };

        for (var i = 0; i < 20; i++)
        {
            Assert.True(OilGame.AdvanceDay(a).Success);
            Assert.True(OilGame.AdvanceDay(b).Success);
            pricesA.Add(a.OilPrice);
            pricesB.Add(b.OilPrice);
        }

        Assert.Equal(pricesA, pricesB);
        Assert.All(pricesA, p => Assert.InRange(p, EconomyConstants.OilPriceMin, EconomyConstants.OilPriceMax));
    }

    [Fact]
    public void Buy_drill_advance_sell_updates_economy()
    {
        var state = OilGame.Create("TestCo", seed: 7);
        var plot = FindFirstWithReserve(state, minReserve: 100);
        var startCash = state.Cash;

        Assert.True(OilGame.BuyPlot(state, plot.X, plot.Y).Success);
        Assert.Equal(startCash - EconomyConstants.PlotBuyCost, state.Cash);
        Assert.True(plot.Owned);

        Assert.True(OilGame.DrillPlot(state, plot.X, plot.Y).Success);
        Assert.True(plot.Drilled);
        Assert.True(plot.Producing);

        var reserveBefore = plot.RemainingReserve;
        Assert.True(OilGame.AdvanceDay(state).Success);
        Assert.Equal(1, state.Day);
        Assert.True(state.OilBarrels > 0);
        Assert.Equal(reserveBefore - state.OilBarrels, plot.RemainingReserve);

        var barrels = state.OilBarrels;
        var price = state.OilPrice;
        var cashBeforeSell = state.Cash;
        Assert.True(OilGame.SellOil(state).Success);
        Assert.Equal(0, state.OilBarrels);
        Assert.Equal(cashBeforeSell + barrels * price, state.Cash);
    }

    [Fact]
    public void Buy_owned_plot_fails()
    {
        var state = OilGame.Create("TestCo", seed: 1);
        Assert.True(OilGame.BuyPlot(state, 0, 0).Success);
        var cash = state.Cash;

        var result = OilGame.BuyPlot(state, 0, 0);
        Assert.False(result.Success);
        Assert.Equal(cash, state.Cash);
    }

    [Fact]
    public void Drill_unowned_plot_fails()
    {
        var state = OilGame.Create("TestCo", seed: 1);
        var cash = state.Cash;

        var result = OilGame.DrillPlot(state, 0, 0);
        Assert.False(result.Success);
        Assert.Contains("own", result.Error, StringComparison.OrdinalIgnoreCase);
        Assert.Equal(cash, state.Cash);
        Assert.False(state.Plots[0, 0].Drilled);
    }

    [Fact]
    public void Insufficient_funds_rejects_buy()
    {
        var state = OilGame.Create("TestCo", seed: 1);
        state.Cash = EconomyConstants.PlotBuyCost - 1;

        var result = OilGame.BuyPlot(state, 0, 0);
        Assert.False(result.Success);
        Assert.False(state.Plots[0, 0].Owned);
    }

    [Fact]
    public void Insufficient_funds_rejects_drill()
    {
        var state = OilGame.Create("TestCo", seed: 1);
        Assert.True(OilGame.BuyPlot(state, 0, 0).Success);
        state.Cash = EconomyConstants.DrillCost - 1;

        var result = OilGame.DrillPlot(state, 0, 0);
        Assert.False(result.Success);
        Assert.False(state.Plots[0, 0].Drilled);
    }

    [Fact]
    public void Depletion_stops_production()
    {
        var state = OilGame.Create("TestCo", seed: 1);
        var plot = state.Plots[0, 0];
        plot.Owned = true;
        plot.Drilled = true;
        plot.RemainingReserve = 50;

        while (plot.Producing)
        {
            Assert.True(OilGame.AdvanceDay(state).Success);
        }

        Assert.Equal(0, plot.RemainingReserve);
        Assert.Equal(50, state.OilBarrels);
        Assert.False(plot.Producing);

        var barrels = state.OilBarrels;
        Assert.True(OilGame.AdvanceDay(state).Success);
        Assert.Equal(barrels, state.OilBarrels);
    }

    [Fact]
    public void Out_of_range_coordinates_fail()
    {
        var state = OilGame.Create("TestCo", seed: 1);
        Assert.False(OilGame.BuyPlot(state, -1, 0).Success);
        Assert.False(OilGame.BuyPlot(state, 3, 0).Success);
        Assert.False(OilGame.DrillPlot(state, 0, 3).Success);
    }

    private static Plot FindFirstWithReserve(GameState state, int minReserve)
    {
        for (var y = 0; y < state.GridSize; y++)
        {
            for (var x = 0; x < state.GridSize; x++)
            {
                if (state.Plots[x, y].RemainingReserve >= minReserve)
                {
                    return state.Plots[x, y];
                }
            }
        }

        throw new InvalidOperationException("No plot with sufficient reserve for seed under test.");
    }
}
