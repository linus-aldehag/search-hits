using SearchHitCount.Business;
using SearchHitCount.Domain.Contracts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ISearchHitService, SearchHitService>();
builder.Services.AddScoped<IResultScraper, ResultCountScraper>();

var app = builder.Build();

app.MapGet("{inputString}", (string inputString, ISearchHitService service) =>
{
    var result = service.PerformSearch(inputString);
    
    return Results.Ok(result);
});

app.Run();
