using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Project26.ModelsLogic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Project26.ViewModels
{
    internal partial class MainPageVM :Models.ObservableObject
    {
        private readonly Chats chats = new();
        public ICommand AddChatCommand => new Command(AddChat);
        public ObservableCollection<Chat> ChatsList => chats.ChatsList;
        private void AddChat()
        {
            chats.AddChat(OnComplete);
        }
        private void OnComplete(Task task)
        {
            if (task.IsFaulted)
                Toast.Make(task.Exception.Message, ToastDuration.Long).Show();
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
