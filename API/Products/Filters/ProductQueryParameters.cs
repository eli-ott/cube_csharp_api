using MonApi.Shared.Pagination;

namespace MonApi.API.Products.Filters
{
    public class ProductQueryParameters : PagedQueryParameters
    {
        public string? deleted { get; set; }
        public int? year { get; set; }
        public bool? is_bio { get; set; }
        public int? family_id { get; set; }
        public int? supplier_id { get; set; }
        public float? price_min { get; set; }
        public float? price_max { get; set; }
    }
}
