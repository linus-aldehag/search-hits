namespace SearchHitCount.Domain;

public record SearchResult(
    string Input,
    int Hits);