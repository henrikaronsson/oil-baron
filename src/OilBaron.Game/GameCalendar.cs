namespace OilBaron.Game;

/// <summary>
/// Maps the simulation month index to the in-world calendar (epoch: January 1972).
/// </summary>
public static class GameCalendar
{
    public static (int Year, int Month, int Day) FromMonthIndex(int monthIndex)
    {
        var absolute = EconomyConstants.StartingCalendarMonth - 1 + monthIndex;
        var year = EconomyConstants.StartingYear + Math.DivRem(absolute, 12, out var month0);
        return (year, month0 + 1, EconomyConstants.StartingCalendarDay);
    }
}
