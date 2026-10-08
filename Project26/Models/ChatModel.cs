using Plugin.CloudFirestore.Attributes;
using Project26.ModelsLogic;
using Project26.General;

namespace Project26.Models
{
    internal abstract class ChatModel
    {
        protected FbData fbd = new();
        public EventHandler? ChatDeleted { get; set; }
        [Ignored]
        public string? Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? HostName { get; set; }
        public string? GuestName { get; set; } = Strings.PleaseWait;
        public string? Message { get; set; }
        public bool IsFull { get; set; }
        public bool IsHostTurn { get; set; }
        public abstract void SendMessage(Action<Task> OnComplete);
        public abstract string SetFbDocument(Action<Task> OnComplete);
        public abstract void AddSnapshotListener();
        public abstract void RemoveSnapshotListener();
    }
}
