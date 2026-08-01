namespace OilBaron.Api.Models;

public sealed class CreateGameRequest
{
    public string CompanyName { get; set; } = "";
    public int? Seed { get; set; }
}
