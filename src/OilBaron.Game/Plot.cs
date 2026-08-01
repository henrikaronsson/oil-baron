namespace OilBaron.Game;

public sealed class Plot
{
    public Plot(int x, int y, int remainingReserve)
    {
        X = x;
        Y = y;
        RemainingReserve = remainingReserve;
    }

    public int X { get; }
    public int Y { get; }
    public bool Owned { get; set; }
    public bool Drilled { get; set; }
    public int RemainingReserve { get; set; }

    public bool Producing => Drilled && RemainingReserve > 0;
}
