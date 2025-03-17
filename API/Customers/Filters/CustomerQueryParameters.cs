using MonApi.Shared.Pagination;

namespace MonApi.API.Customers.Filters
{
    public class CustomerQueryParameters : PagedQueryParameters
    {
        public string? deleted { get; set; }

        public string? first_name { get; set; }

        public string? last_name { get; set; }

        public string? email { get; set; }

        public string? phone { get; set; }

        public string? active { get; set; }
    }
}
