using MonApi.Shared.Pagination;

namespace MonApi.API.Roles.Filters
{
    public class RoleQueryParameters : PagedQueryParameters
    {
        public string? deleted { get; set; }
    }
}
