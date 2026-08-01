namespace OilBaron.Game;

public sealed class GameState
{
    public GameState(
        Guid id,
        string companyName,
        int seed,
        int day,
        int cash,
        int oilBarrels,
        int oilPrice,
        Plot[,] plots,
        Random rng)
    {
        Id = id;
        CompanyName = companyName;
        Seed = seed;
        Day = day;
        Cash = cash;
        OilBarrels = oilBarrels;
        OilPrice = oilPrice;
        Plots = plots;
        Rng = rng;
    }

    public Guid Id { get; }
    public string CompanyName { get; }
    public int Seed { get; }
    public int Day { get; set; }
    public int Cash { get; set; }
    public int OilBarrels { get; set; }
    public int OilPrice { get; set; }
    public Plot[,] Plots { get; }
    public int GridSize => EconomyConstants.GridSize;

    /// <summary>Seeded PRNG for reserves (at create) and daily price walks.</summary>
    internal Random Rng { get; }
}
