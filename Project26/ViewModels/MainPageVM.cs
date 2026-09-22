using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Project26.ViewModels
{
    internal partial class MainPageVM :Models.ObservableObject
    {
        private readonly ModelsLogic.Chats chats = new();
        public ICommand AddChatCommand => new Command(AddChat);
        public ObservableCollection<ModelsLogic.Chat> ChatsList => chats.ChatsList;
        private void AddChat()
        {
            
        }
        public void AddSnapshotListener()
        {
            chats.AddSnapshotListener();
        }
        public void RemoveSnapshotListener()
        {
            chats.RemoveSnapshotListener();
        }
        public MainPageVM()
        {
            chats.ChatsChanged += OnChatsChanged;
        }
        private void OnChatsChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(ChatsList));
        }
    }
}
