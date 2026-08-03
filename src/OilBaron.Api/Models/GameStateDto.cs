namespace OilBaron.Api.Models;

public sealed class GameStateDto
{
    public Guid Id { get; set; }
    public string CompanyName { get; set; } = "";
    public int Seed { get; set; }
    /// <summary>Turn index from game start (0 = first month).</summary>
    public int Month { get; set; }
    /// <summary>In-world calendar year (epoch January 1972).</summary>
    public int CalendarYear { get; set; }
    /// <summary>In-world calendar month 1–12.</summary>
    public int CalendarMonth { get; set; }
    /// <summary>In-world calendar day (always 1 while turns are monthly).</summary>
    public int CalendarDay { get; set; }
    public int Cash { get; set; }
    public int OilBarrels { get; set; }
    public int OilPrice { get; set; }
    public int GridSize { get; set; }
    public List<OilFieldDto> OilFields { get; set; } = [];
}

public sealed class OilFieldDto
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool Owned { get; set; }
    public bool Drilled { get; set; }
    public bool Producing { get; set; }
    public int PurchasePrice { get; set; }
    public int MonthlyProduction { get; set; }
    public int OperatingCostPerMonth { get; set; }
    public int EstimatedReserves { get; set; }
    public int? RemainingReserves { get; set; }
}
