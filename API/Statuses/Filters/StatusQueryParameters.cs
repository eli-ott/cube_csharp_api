using MonApi.Shared.Pagination;

namespace MonApi.API.Statuses.Filters
{
    public class StatusQueryParameters : PagedQueryParameters
    {
        public string? deleted { get; set; }
    }
}
