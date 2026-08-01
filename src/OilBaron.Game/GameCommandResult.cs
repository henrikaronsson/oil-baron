namespace OilBaron.Game;

public sealed class GameCommandResult
{
    private GameCommandResult(bool success, string? error)
    {
        Success = success;
        Error = error;
    }

    public bool Success { get; }
    public string? Error { get; }

    public static GameCommandResult Ok() => new(true, null);

    public static GameCommandResult Fail(string error) => new(false, error);
}
