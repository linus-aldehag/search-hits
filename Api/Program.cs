using SearchHitCount.Business;
using SearchHitCount.Domain.Contracts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IResultScraper, ResultCountScraper>();

var app = builder.Build();

app.MapGet("{input}", (IResultScraper scraper, string input) =>
{
    var result = scraper.GetCount(input).Result;
    
    return result != null ?
        Results.Ok(result) :
        Results.NotFound();
});

app.Run();
