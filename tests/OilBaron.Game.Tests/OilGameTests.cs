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
                Assert.Equal(a.OilFields[x, y].RemainingReserves, b.OilFields[x, y].RemainingReserves);
                Assert.Equal(a.OilFields[x, y].EstimatedReserves, b.OilFields[x, y].EstimatedReserves);
            }
        }

        Assert.Equal(a.Seed, b.Seed);
        Assert.Equal(EconomyConstants.StartingCash, a.Cash);
        Assert.Equal(EconomyConstants.StartingOilPrice, a.OilPrice);
        Assert.Equal(EconomyConstants.StartingMonth, a.Month);
        Assert.Equal((1972, 1, 1), GameCalendar.FromMonthIndex(a.Month));
    }

    [Theory]
    [InlineData(0, 1972, 1)]
    [InlineData(3, 1972, 4)]
    [InlineData(11, 1972, 12)]
    [InlineData(12, 1973, 1)]
    [InlineData(21, 1973, 10)]
    public void Calendar_maps_month_index_from_january_1972(int monthIndex, int year, int calendarMonth)
    {
        var (y, m, d) = GameCalendar.FromMonthIndex(monthIndex);
        Assert.Equal(year, y);
        Assert.Equal(calendarMonth, m);
        Assert.Equal(EconomyConstants.StartingCalendarDay, d);
    }

    [Fact]
    public void Advance_month_moves_calendar_forward()
    {
        var state = OilGame.Create("TestCo", seed: 1);
        Assert.Equal((1972, 1, 1), GameCalendar.FromMonthIndex(state.Month));

        Assert.True(OilGame.AdvanceMonth(state).Success);
        Assert.Equal((1972, 2, 1), GameCalendar.FromMonthIndex(state.Month));

        for (var i = 0; i < 2; i++)
        {
            Assert.True(OilGame.AdvanceMonth(state).Success);
        }

        Assert.Equal((1972, 4, 1), GameCalendar.FromMonthIndex(state.Month));
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
            Assert.True(OilGame.AdvanceMonth(a).Success);
            Assert.True(OilGame.AdvanceMonth(b).Success);
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
        var field = FindFirstWithReserve(state, minReserve: 100);
        var startCash = state.Cash;

        Assert.True(OilGame.BuyOilField(state, field.X, field.Y).Success);
        Assert.Equal(startCash - field.PurchasePrice, state.Cash);
        Assert.True(field.Owned);

        Assert.True(OilGame.DrillOilField(state, field.X, field.Y).Success);
        Assert.True(field.Drilled);
        Assert.True(field.Producing);

        var reserveBefore = field.RemainingReserves;
        var cashBeforeAdvance = state.Cash;
        Assert.True(OilGame.AdvanceMonth(state).Success);
        Assert.Equal(1, state.Month);
        Assert.Equal(EconomyConstants.DefaultMonthlyProduction, state.OilBarrels);
        Assert.Equal(reserveBefore - state.OilBarrels, field.RemainingReserves);
        Assert.Equal(cashBeforeAdvance - field.OperatingCostPerMonth, state.Cash);

        var barrels = state.OilBarrels;
        var price = state.OilPrice;
        var cashBeforeSell = state.Cash;
        Assert.True(OilGame.SellOil(state).Success);
        Assert.Equal(0, state.OilBarrels);
        Assert.Equal(cashBeforeSell + barrels * price, state.Cash);
    }

    [Fact]
    public void Buy_owned_oil_field_fails()
    {
        var state = OilGame.Create("TestCo", seed: 1);
        Assert.True(OilGame.BuyOilField(state, 0, 0).Success);
        var cash = state.Cash;

        var result = OilGame.BuyOilField(state, 0, 0);
        Assert.False(result.Success);
        Assert.Equal(cash, state.Cash);
    }

    [Fact]
    public void Drill_unowned_oil_field_fails()
    {
        var state = OilGame.Create("TestCo", seed: 1);
        var cash = state.Cash;

        var result = OilGame.DrillOilField(state, 0, 0);
        Assert.False(result.Success);
        Assert.Contains("own", result.Error, StringComparison.OrdinalIgnoreCase);
        Assert.Equal(cash, state.Cash);
        Assert.False(state.OilFields[0, 0].Drilled);
    }

    [Fact]
    public void Insufficient_funds_rejects_buy()
    {
        var state = OilGame.Create("TestCo", seed: 1);
        state.Cash = state.OilFields[0, 0].PurchasePrice - 1;

        var result = OilGame.BuyOilField(state, 0, 0);
        Assert.False(result.Success);
        Assert.False(state.OilFields[0, 0].Owned);
    }

    [Fact]
    public void Insufficient_funds_rejects_drill()
    {
        var state = OilGame.Create("TestCo", seed: 1);
        Assert.True(OilGame.BuyOilField(state, 0, 0).Success);
        state.Cash = EconomyConstants.DrillCost - 1;

        var result = OilGame.DrillOilField(state, 0, 0);
        Assert.False(result.Success);
        Assert.False(state.OilFields[0, 0].Drilled);
    }

    [Fact]
    public void Depletion_stops_production()
    {
        var state = OilGame.Create("TestCo", seed: 1);
        var field = state.OilFields[0, 0];
        field.Owned = true;
        field.Drilled = true;
        field.RemainingReserves = 25;

        while (field.Producing)
        {
            Assert.True(OilGame.AdvanceMonth(state).Success);
        }

        Assert.Equal(0, field.RemainingReserves);
        Assert.Equal(25, state.OilBarrels);
        Assert.False(field.Producing);

        var barrels = state.OilBarrels;
        var cash = state.Cash;
        Assert.True(OilGame.AdvanceMonth(state).Success);
        Assert.Equal(barrels, state.OilBarrels);
        Assert.Equal(cash, state.Cash);
    }

    [Fact]
    public void Out_of_range_coordinates_fail()
    {
        var state = OilGame.Create("TestCo", seed: 1);
        Assert.False(OilGame.BuyOilField(state, -1, 0).Success);
        Assert.False(OilGame.BuyOilField(state, 3, 0).Success);
        Assert.False(OilGame.DrillOilField(state, 0, 3).Success);
    }

    private static OilField FindFirstWithReserve(GameState state, int minReserve)
    {
        for (var y = 0; y < state.GridSize; y++)
        {
            for (var x = 0; x < state.GridSize; x++)
            {
                if (state.OilFields[x, y].RemainingReserves >= minReserve)
                {
                    return state.OilFields[x, y];
                }
            }
        }

        throw new InvalidOperationException("No oil field with sufficient reserve for seed under test.");
    }
}
