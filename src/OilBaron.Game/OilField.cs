namespace OilBaron.Game;

public sealed class OilField
{
    public OilField(
        int x,
        int y,
        int purchasePrice,
        int monthlyProduction,
        int operatingCostPerMonth,
        int estimatedReserves,
        int remainingReserves)
    {
        X = x;
        Y = y;
        PurchasePrice = purchasePrice;
        MonthlyProduction = monthlyProduction;
        OperatingCostPerMonth = operatingCostPerMonth;
        EstimatedReserves = estimatedReserves;
        RemainingReserves = remainingReserves;
    }

    public int X { get; }
    public int Y { get; }
    public int PurchasePrice { get; }
    public int MonthlyProduction { get; }
    public int OperatingCostPerMonth { get; }
    public int EstimatedReserves { get; }
    public int RemainingReserves { get; set; }
    public bool Owned { get; set; }
    public bool Drilled { get; set; }

    public bool Producing => Drilled && RemainingReserves > 0;
}
