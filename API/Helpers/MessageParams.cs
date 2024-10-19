namespace API.Helpers
{
    public class MessageParams : PaginationParams
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Container { get; set; } = "Unread";
    }
}