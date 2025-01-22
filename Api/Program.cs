using SearchHitCount.Business;
using SearchHitCount.Domain;
using SearchHitCount.Domain.Contracts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IResultScraper, ResultCountScraper>();

var app = builder.Build();

app.MapGet("{inputString}", (IResultScraper scraper, ILogger<Program> logger, string inputString) =>
{
    var inputs = inputString.Split(" ");
    var result = new List<SearchResult>();
    
    foreach (var input in inputs)
    {
        try
        {
            var count = scraper.GetCount(input).Result;
            if (count.HasValue)
                result.Add(new SearchResult(input, count.Value));
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
        }
    }
    
    Results.Ok(result);
});

app.Run();
