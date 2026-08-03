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
        var oilFields = new OilField[size, size];

        for (var y = 0; y < size; y++)
        {
            for (var x = 0; x < size; x++)
            {
                var steps = rng.Next(0, EconomyConstants.ReserveMaxSteps + 1);
                var reserve = steps * EconomyConstants.ReserveStep;
                oilFields[x, y] = new OilField(
                    x,
                    y,
                    EconomyConstants.DefaultPurchasePrice,
                    EconomyConstants.DefaultMonthlyProduction,
                    EconomyConstants.DefaultOperatingCostPerMonth,
                    estimatedReserves: reserve,
                    remainingReserves: reserve);
            }
        }

        return new GameState(
            id ?? Guid.NewGuid(),
            companyName.Trim(),
            actualSeed,
            EconomyConstants.StartingMonth,
            EconomyConstants.StartingCash,
            EconomyConstants.StartingOilBarrels,
            EconomyConstants.StartingOilPrice,
            oilFields,
            rng);
    }

    public static GameCommandResult BuyOilField(GameState state, int x, int y)
    {
        if (!TryGetOilField(state, x, y, out var field))
        {
            return GameCommandResult.Fail("Oil field coordinates are out of range.");
        }

        if (field.Owned)
        {
            return GameCommandResult.Fail("Oil field is already owned.");
        }

        if (state.Cash < field.PurchasePrice)
        {
            return GameCommandResult.Fail("Insufficient cash to buy oil field.");
        }

        state.Cash -= field.PurchasePrice;
        field.Owned = true;
        return GameCommandResult.Ok();
    }

    public static GameCommandResult DrillOilField(GameState state, int x, int y)
    {
        if (!TryGetOilField(state, x, y, out var field))
        {
            return GameCommandResult.Fail("Oil field coordinates are out of range.");
        }

        if (!field.Owned)
        {
            return GameCommandResult.Fail("Cannot drill an oil field you do not own.");
        }

        if (field.Drilled)
        {
            return GameCommandResult.Fail("Oil field is already drilled.");
        }

        if (state.Cash < EconomyConstants.DrillCost)
        {
            return GameCommandResult.Fail("Insufficient cash to drill.");
        }

        state.Cash -= EconomyConstants.DrillCost;
        field.Drilled = true;
        return GameCommandResult.Ok();
    }

    public static GameCommandResult AdvanceMonth(GameState state)
    {
        var size = state.GridSize;
        for (var y = 0; y < size; y++)
        {
            for (var x = 0; x < size; x++)
            {
                var field = state.OilFields[x, y];
                if (!field.Producing)
                {
                    continue;
                }

                state.Cash -= field.OperatingCostPerMonth;

                var produced = Math.Min(field.MonthlyProduction, field.RemainingReserves);
                field.RemainingReserves -= produced;
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

        state.Month += 1;
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

    private static bool TryGetOilField(GameState state, int x, int y, out OilField field)
    {
        var size = state.GridSize;
        if (x < 0 || y < 0 || x >= size || y >= size)
        {
            field = null!;
            return false;
        }

        field = state.OilFields[x, y];
        return true;
    }
}
