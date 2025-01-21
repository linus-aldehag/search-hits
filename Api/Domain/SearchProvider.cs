namespace SearchHitCount.Domain;

public record SearchProvider(
    string Url,
    string Path,
    string Pattern,
    List<CookieInfo> Cookies);