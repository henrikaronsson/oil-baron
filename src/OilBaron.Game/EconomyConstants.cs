namespace OilBaron.Game;

/// <summary>
/// Single source of truth for MVP economy numbers. Keep docs/economy.md in sync.
/// </summary>
public static class EconomyConstants
{
    public const int StartingCash = 100_000;
    public const int StartingDay = 0;
    public const int StartingOilBarrels = 0;
    public const int StartingOilPrice = 50;

    public const int PlotBuyCost = 15_000;
    public const int DrillCost = 25_000;

    public const int GridSize = 3;

    public const int OilPriceMin = 20;
    public const int OilPriceMax = 100;
    public const int OilPriceDeltaMin = -3;
    public const int OilPriceDeltaMaxInclusive = 3;

    /// <summary>Reserve steps: 0, 100, ..., 2000.</summary>
    public const int ReserveStep = 100;
    public const int ReserveMaxSteps = 20;
}
