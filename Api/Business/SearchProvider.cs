using System.Net;
using SearchHitCount.Domain;

namespace SearchHitCount.Business;

public class SearchProvider(
    string url,
    string path,
    string pattern,
    List<CookieInfo>? cookies = null)
{
    public string Url { get; } = url;
    public string Path { get; } = path;
    public string Pattern { get; } = pattern;
    public List<CookieInfo>? Cookies { get; } = cookies;

    private HttpClient? client;

    internal HttpClient GetClient()
    {
        if (client == null)
        {
            var handler = new HttpClientHandler();
            if (Cookies is { Count: > 0 })
            {
                handler.CookieContainer = new CookieContainer();
                foreach (var cookie in Cookies)
                {
                    handler.CookieContainer.Add(
                        new Uri(cookie.Domain),
                        new Cookie(cookie.Name, cookie.Value));
                }
            }

            client = new HttpClient(handler);
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
        }
        
        return client;
    }
}