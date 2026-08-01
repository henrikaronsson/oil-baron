namespace OilBaron.Api.Models;

public sealed class GameStateDto
{
    public Guid Id { get; set; }
    public string CompanyName { get; set; } = "";
    public int Seed { get; set; }
    public int Day { get; set; }
    public int Cash { get; set; }
    public int OilBarrels { get; set; }
    public int OilPrice { get; set; }
    public int GridSize { get; set; }
    public List<PlotDto> Plots { get; set; } = [];
}

public sealed class PlotDto
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool Owned { get; set; }
    public bool Drilled { get; set; }
    public bool Producing { get; set; }
    public int? RemainingReserve { get; set; }
}
