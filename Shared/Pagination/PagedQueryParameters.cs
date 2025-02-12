namespace MonApi.Shared.Pagination
{
    public class PagedQueryParameters
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 10;

        // Pagination properties
        public int page { get; set; } = 1;
        public int size
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
