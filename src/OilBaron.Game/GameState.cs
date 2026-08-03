namespace OilBaron.Game;

public sealed class GameState
{
    public GameState(
        Guid id,
        string companyName,
        int seed,
        int month,
        int cash,
        int oilBarrels,
        int oilPrice,
        OilField[,] oilFields,
        Random rng)
    {
        Id = id;
        CompanyName = companyName;
        Seed = seed;
        Month = month;
        Cash = cash;
        OilBarrels = oilBarrels;
        OilPrice = oilPrice;
        OilFields = oilFields;
        Rng = rng;
    }

    public Guid Id { get; }
    public string CompanyName { get; }
    public int Seed { get; }
    public int Month { get; set; }
    public int Cash { get; set; }
    public int OilBarrels { get; set; }
    public int OilPrice { get; set; }
    public OilField[,] OilFields { get; }
    public int GridSize => EconomyConstants.GridSize;

    /// <summary>Seeded PRNG for reserves (at create) and monthly price walks.</summary>
    internal Random Rng { get; }
}
