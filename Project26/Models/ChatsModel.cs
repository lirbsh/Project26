using Plugin.CloudFirestore;
using Project26.ModelsLogic;
using System.Collections.ObjectModel;

namespace Project26.Models
{
    internal abstract class ChatsModel
    {
        protected FbData fbd = new();
        public EventHandler? ChatsChanged { get; set; }
        public ObservableCollection<Chat> ChatsList { get; set; } = [] ;
        public abstract void AddChat(Action<Task> OnComplete);
        public abstract void AddSnapshotListener();
        public abstract void RemoveSnapshotListener();
        protected abstract void OnComplete(IQuerySnapshot snapshot);
    }
}
