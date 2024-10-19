namespace API.Helpers
{
    public class UserParams
    {
        public string CurrentUsername { get; set; }
        public string CurrentEmail { get; set; }
        public string Location { get; set; }
        private const int MaxPageSize = 20;
        public int PageNumber { get; set; } =1;
        public int _pageSize = 10;
        public int PageSize 
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}