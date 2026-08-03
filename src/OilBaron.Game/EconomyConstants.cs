namespace OilBaron.Game;

/// <summary>
/// Single source of truth for MVP economy numbers. Keep docs/economy.md in sync.
/// </summary>
public static class EconomyConstants
{
    public const int StartingCash = 100_000;
    /// <summary>Turn index at create (0 = first playable month).</summary>
    public const int StartingMonth = 0;
    /// <summary>Calendar year for turn 0. Game opens before the Oct 1973 oil crisis.</summary>
    public const int StartingYear = 1972;
    /// <summary>Calendar month for turn 0 (1 = January … 12 = December).</summary>
    public const int StartingCalendarMonth = 1;
    /// <summary>Day of month shown on the calendar (turns advance by month).</summary>
    public const int StartingCalendarDay = 1;
    public const int StartingOilBarrels = 0;
    public const int StartingOilPrice = 50;

    public const int DefaultPurchasePrice = 15_000;
    public const int DrillCost = 25_000;
    public const int DefaultMonthlyProduction = 10;
    public const int DefaultOperatingCostPerMonth = 400;

    public const int GridSize = 3;

    public const int OilPriceMin = 20;
    public const int OilPriceMax = 100;
    public const int OilPriceDeltaMin = -3;
    public const int OilPriceDeltaMaxInclusive = 3;

    /// <summary>Reserve steps: 0, 100, ..., 2000.</summary>
    public const int ReserveStep = 100;
    public const int ReserveMaxSteps = 20;
}
