
namespace Core.Specification.Parameters.Driver
{
    public class DriverSpecificationParameters
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;

        private int _pagSize = 6;

        public int PageSize
        {
            get => _pagSize;
            set => _pagSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? DriverId { get; set; }
        public string? Sort { get; set; }

        private string _search;

        public string? Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}
