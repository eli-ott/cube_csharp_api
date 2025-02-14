using MonApi.Shared.Pagination;

namespace MonApi.API.Suppliers.Filters
{
    public class SupplierQueryParameters : PagedQueryParameters
    {
        public string? deleted { get; set; }
        public string? siret { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }

    }
}
