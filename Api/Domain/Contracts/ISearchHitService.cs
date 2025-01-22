namespace SearchHitCount.Domain.Contracts;

public interface ISearchHitService
{
    List<SearchResult> PerformSearch(string inputString);
}