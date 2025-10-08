using RealEstate.Application.Queries;
using RealEstate.Domain.Queries;

namespace RealEstate.Application.Mapping;

public static class QueryMapping
{
    public static PropertySearchCriteria ToCriteria(this PropertySearchQuery query) =>
        new(query.Name, query.Address, query.MinPrice, query.MaxPrice, 
            query.IdOwner, query.CodeInternal, query.Year, query.MinYear, query.MaxYear,
            query.SortBy, query.SortOrder, query.Page, query.PageSize);
}