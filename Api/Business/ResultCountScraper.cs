using System.Net;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using SearchHitCount.Domain;
using SearchHitCount.Domain.Contracts;

namespace SearchHitCount.Business;

public class ResultCountScraper(IConfiguration config, ILogger<ResultCountScraper> logger) : IResultScraper
{
    public async Task<int?> GetCount(string input)
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
                var client = SetupHttpClient(searchProvider.Cookies);

                var result = await client.GetAsync(url);

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

    private static HttpClient SetupHttpClient(IList<CookieInfo>? cookies)
    {
        var handler = new HttpClientHandler();
        if (cookies is { Count: > 0 })
        {
            handler.CookieContainer = new CookieContainer();
            foreach (var cookie in cookies)
            {
                handler.CookieContainer.Add(
                    new Uri(cookie.Domain),
                    new Cookie(cookie.Name, cookie.Value));
            }
        }
                
        var client = new HttpClient(handler);
        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
        return client;
    }
}