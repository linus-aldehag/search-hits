using System.Text.RegularExpressions;
using HtmlAgilityPack;
using SearchHitCount.Domain.Contracts;

namespace SearchHitCount.Business;

public class ResultCountScraper(IConfiguration config, ILogger<ResultCountScraper> logger) : IResultCountScraper
{
    public async Task<long?> GetCountAsync(string input)
    {
        var searchProviders = config.GetSection("SearchProviders").Get<SearchProvider[]>();
        if (searchProviders == null)
            return 0;
        
        var totalSearchHitCount = 0;
        
        foreach (var searchProvider in searchProviders)
        {
            try
            {
                var url = string.Format(searchProvider.Url, input);

                var result = await searchProvider.GetClient().GetAsync(url);

                if (!result.IsSuccessStatusCode)
                {
                    logger.LogError($"Failed to get search hits count for {url}");
                    continue;
                }

                var content = await result.Content.ReadAsStringAsync();
                var numericValue = ScrapeValue(content, searchProvider);

                logger.LogTrace($"Found {numericValue} search hits count for {input} using {url}");

                totalSearchHitCount += numericValue;
            }
            catch (Exception e)
            {
                logger.LogError("Exception caught when scraping {}:\n\n{}\n\n\n{}", searchProvider.Url, e.Message, e.StackTrace);
            }
        }

        return totalSearchHitCount;
    }

    private static int ScrapeValue(string content, SearchProvider searchProvider)
    {
        var document = new HtmlDocument();
        document.LoadHtml(content);
                
        var node = document.DocumentNode.SelectSingleNode(searchProvider.Path);
        
        if (node?.InnerText == null)
            return 0;
        
        var resultsString = node.InnerText;
        var match = Regex.Match(resultsString, searchProvider.Pattern);
        var value = match.Value;
            
        value = Regex.Replace(value, "[^0-9]", "");
        var numericValue = int.Parse(value);
        return numericValue;
    }
}