namespace LMS.Application.DTOs.Common
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    }

    public class PaginationParams
    {
        private const int MaxPageSize = 50;
        private int _pageSize = 20;

        public int Page { get; set; } = 1;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
        public string? Search { get; set; }
        public string? SortBy { get; set; }
        public string SortDirection { get; set; } = "asc"; // asc | desc
    }
}
