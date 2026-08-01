namespace OilBaron.Api.Models;

public sealed class ErrorResponse
{
    public ErrorResponse(string error)
    {
        Error = error;
    }

    public string Error { get; }
}
