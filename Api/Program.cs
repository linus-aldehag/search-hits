using SearchHitCount.Business;
using SearchHitCount.Domain.Contracts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ISearchHitService, SearchHitService>();
builder.Services.AddScoped<IResultCountScraper, ResultCountScraper>();

var app = builder.Build();

app.MapGet("{inputString}", async (string inputString, ISearchHitService service) =>
{
    var result = await service.PerformSearchAsync(inputString);
    
    return Results.Ok(result);
});

app.Run();
