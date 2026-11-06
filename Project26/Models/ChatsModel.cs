using Plugin.CloudFirestore;
using Project26.EventsArgs;
using Project26.ModelsLogic;
using System.Collections.ObjectModel;

namespace Project26.Models
{
    internal abstract class ChatsModel
    {
        protected bool isBusy = false;
        protected Chat? newChat;
        protected FbData fbd = FbData.Instance;
        protected User user = User.Instance;
        public event EventHandler? StatusChanged;
        protected void OnStatusChanged() => StatusChanged?.Invoke(this, EventArgs.Empty);
        public event EventHandler? ChatsChanged;
        protected void OnChatsChanged() => ChatsChanged?.Invoke(this, EventArgs.Empty);
        public event EventHandler<ChatArgs>? ChatAdded;
        protected void OnChatAdded(ChatArgs args) => ChatAdded?.Invoke(this, args);
        public ObservableCollection<Chat> ChatsList { get; set; } = [];
        public bool CanAddChat => !isBusy;
        public abstract void AddChat();
        public abstract void AddSnapshotListener();
        public abstract void RemoveSnapshotListener();
        protected abstract void OnComplete(IQuerySnapshot snapshot);
        protected abstract void OnComplete(Action<Task> task);
    }
}