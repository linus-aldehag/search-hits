namespace SearchHitCount.Domain.Contracts;

public interface IResultScraper
{
    Task<int?> GetCount(string input);
}