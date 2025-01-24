namespace SearchHitCount.Domain.Contracts;

public interface IResultCountScraper
{
    Task<long?> GetCountAsync(string input);
}