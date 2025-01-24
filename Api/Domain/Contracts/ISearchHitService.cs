namespace SearchHitCount.Domain.Contracts;

public interface ISearchHitService
{
    Task<List<SearchResult>> PerformSearchAsync(string inputString);
}