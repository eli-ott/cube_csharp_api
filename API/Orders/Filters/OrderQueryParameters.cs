using MonApi.Shared.Pagination;

namespace MonApi.API.Orders.Filters;

public class OrderQueryParameters : PagedQueryParameters
{
    public string? deleted { get; set; }
    public string? status_id { get; set; }
    public string? customer_id { get; set; }
}