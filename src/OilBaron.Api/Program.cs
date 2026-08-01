using Newtonsoft.Json.Serialization;
using OilBaron.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<InMemoryGameStore>();
builder.Services.AddSingleton<IGameSessionService, GameSessionService>();

builder.Services
    .AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;
    });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors();
app.MapControllers();

app.Run();
