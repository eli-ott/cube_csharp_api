using MonApi.Shared.Pagination;

namespace MonApi.API.Families.Filters
{
    public class FamilyQueryParameters : PagedQueryParameters
    {
        public string? deleted { get; set; }
    }
}
