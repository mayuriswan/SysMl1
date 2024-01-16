using System.Text;

namespace SysML_Sync.Services
{
    public class FilterParameters
    {
        // page size with default value and setter to set the value to 1 if it's less than 1
        private const int MaxPageSize = 50;
        private int _pageSize = 6;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
        public int PageNumber { get; set; } = 1;
        public OrderBy OrderBy { get; set; } = OrderBy.Modeltype;
        public bool IsSortDesc { get; set; } = false;

        public string SearchTerm { get; set; } = string.Empty;
        //public CommandFilterBy FilterBy { get; set; } = CommandFilterBy.None;

        public string ToQueryString()
        {
            var builder = new StringBuilder();

            builder.Append($"pageNumber={PageNumber}&");
            builder.Append($"pageSize={PageSize}&");
            builder.Append($"orderBy={((int)OrderBy)}&");
            builder.Append($"isSortDesc={IsSortDesc}&");
            if (!string.IsNullOrEmpty(SearchTerm))
                builder.Append($"searchTerm={SearchTerm}&");
            //builder.Append($"filterBy={FilterBy}&");

            return builder.ToString();
        }
    }
}