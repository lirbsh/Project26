using Plugin.CloudFirestore;
using Plugin.CloudFirestore.Attributes;
using Project26.EventsArgs;
using Project26.General;
using Project26.ModelsLogic;

namespace Project26.Models
{
    internal abstract class ChatModel
    {
        protected bool _isMeHost = true;
        protected FbData fbd = FbData.Instance;
        protected User user = User.Instance;
        [Ignored]
        public EventHandler? ChatStarted { get; set; }
        [Ignored]
        public EventHandler<ChatChangedArgs>? ChatChanged { get; set; }
        [Ignored]
        public EventHandler<DeleteArgs>? ChatDeleted { get; set; }
        [Ignored]
        public string? Id { get; set; }
        [Ignored]
        public abstract bool IsMeHost { get; set; }
        [Ignored]
        public bool IsMyTurn => IsMeHost ? IsHostTurn : !IsHostTurn;
        [Ignored]
        public string? OpponentName => IsMeHost ? GuestName : HostName;
        [Ignored]
        public string? ReceivedMessage { get; protected set; }
        [Ignored]
        public bool ReceivedMessageBold { get; protected set; }
        [Ignored]
        public Color ReceivedMessageColor { get; protected set; } = Colors.Black;
        public Message Message { get; set; } = new();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? HostName { get; set; }
        public string? GuestName { get; set; } = Strings.PleaseWait;
        public bool IsFull { get; set; }
        public bool IsHostTurn { get; set; }
        public abstract void SendMessage(Action<Task> OnComplete);
        public abstract string SetFbDocument(Action<Task> OnComplete);
        protected abstract void OnChange(IDocumentSnapshot? snapshot, Exception? error);
        protected abstract void OnDeleteComplete(Task task);
        protected abstract void OnStartChatComplete(Task task);
        public abstract void AddSnapshotListener();
        public abstract void RemoveSnapshotListener();
    }
}