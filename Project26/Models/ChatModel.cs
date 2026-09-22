namespace Project26.Models
{
    internal abstract class ChatModel
    {
        public string? Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? HostName { get; set; }
        public string? GuestName { get; set; }
        public string? Message { get; set; }
        public bool IsFull { get; set; }
        public bool IsHostTurn { get; set; }
        public abstract void SendMessage(Action<Task> OnComplete);
    }
}
