using SearchHitCount.Domain;
using SearchHitCount.Domain.Contracts;

namespace SearchHitCount.Business;

public class SearchHitService(IResultScraper scraper, ILogger<SearchHitService> logger) : ISearchHitService
{
    public List<SearchResult> PerformSearch(string inputString)
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
        
        return result;
    }
}