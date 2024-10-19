namespace API.DTOs
{
    public class NotificationDto
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime DateSent { get; set; }
        public bool IsRead { get; set; }
    }
}
