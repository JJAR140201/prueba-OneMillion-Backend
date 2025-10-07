namespace RealEstate.Domain.Queries;

public sealed record PropertySearchCriteria(
    string? Name = null,
    string? Address = null,
    decimal? MinPrice = null,
    decimal? MaxPrice = null,
    int Page = 1,
    int PageSize = 20
)
{
    public int Skip => Math.Max(0, (Page - 1) * PageSize);
    public int Take => Math.Clamp(PageSize, 1, 200);
}