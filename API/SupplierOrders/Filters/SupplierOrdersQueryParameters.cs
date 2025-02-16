using MonApi.Shared.Pagination;

namespace MonApi.API.SupplierOrders.Filters;

public class SupplierOrdersQueryParameters : PagedQueryParameters
{
    public string? deleted { get; set; }
    public string? status_id { get; set; }
    public string? employee_id { get; set; }
}