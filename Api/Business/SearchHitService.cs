using SearchHitCount.Domain;
using SearchHitCount.Domain.Contracts;

namespace SearchHitCount.Business;

public class SearchHitService(IResultCountScraper scraper, ILogger<SearchHitService> logger) : ISearchHitService
{
    public async Task<List<SearchResult>> PerformSearchAsync(string inputString)
    {
        var inputs = inputString.Split(" ");
        var result = new List<SearchResult>();
    
        foreach (var input in inputs)
        {
            try
            {
                var count = await scraper.GetCountAsync(input);
                if (count.HasValue)
                    result.Add(new SearchResult(input, count.Value));
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
            }
        }
        
        return result;
    }
}