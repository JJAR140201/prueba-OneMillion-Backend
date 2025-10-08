namespace RealEstate.Application.Queries;

public sealed record PropertySearchQuery(
    string? Name = null,
    string? Address = null,
    decimal? MinPrice = null,
    decimal? MaxPrice = null,
    string? IdOwner = null,
    string? CodeInternal = null,
    int? Year = null,
    int? MinYear = null,
    int? MaxYear = null,
    string? SortBy = "Price", // Price, Name, Address, Year
    string? SortOrder = "desc", // asc, desc
    int Page = 1,
    int PageSize = 20
)
{
    public int Skip => Math.Max(0, (Page - 1) * PageSize);
    public int Take => Math.Clamp(PageSize, 1, 200);
}
