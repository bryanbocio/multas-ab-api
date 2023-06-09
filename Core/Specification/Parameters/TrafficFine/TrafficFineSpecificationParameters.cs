﻿
namespace Core.Specification.Parameters.TrafficFine
{
    public class TrafficFineSpecificationParameters
    {
        private const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;

        private int _pagSize = 6;

        public int PageSize
        {
            get => _pagSize;
            set => _pagSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public int? AgentId { get; set; }
        public int? DriverId { get; set; }
        public int? TrafficFineId { get; set; }

        public string? IdentityDriver { get; set; }
        public string? IdentityAgent{ get; set; }
        public string? Sort { get; set; }

        private string _search;

        public string? Search
        {
            get => _search;
            set => _search = value.ToLower();
        }
    }
}
