using Plugin.CloudFirestore.Attributes;
using Project26.ModelsLogic;
using Project26.General;

namespace Project26.Models
{
    internal abstract class ChatModel
    {
        protected bool _isMeHost = true;
        protected FbData fbd = new();
        protected User user = new();
        [Ignored]
        public EventHandler<DeleteArgs>? ChatDeleted { get; set; }
        [Ignored]
        public string? Id { get; set; }
        [Ignored]
        public abstract bool IsMeHost { get; set; } 
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
