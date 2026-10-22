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
        protected FbData fbd = new();
        protected User user = new();
        public EventHandler? StatusChanged { get; set; }
        public EventHandler? ChatsChanged { get; set; }
        public EventHandler<ChatArgs>? ChatAdded { get; set; }
        public ObservableCollection<Chat> ChatsList { get; set; } = [];
        public bool CanAddChat => !isBusy;
        public abstract void AddChat();
        public abstract void AddSnapshotListener();
        public abstract void RemoveSnapshotListener();
        protected abstract void OnComplete(IQuerySnapshot snapshot);
        protected abstract void OnComplete(Action<Task> task);
    }
}