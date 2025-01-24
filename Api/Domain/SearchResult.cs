namespace SearchHitCount.Domain;

public record SearchResult(
    string Input,
    long Hits);