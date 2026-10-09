using Project26.Models;
using Project26.ModelsLogic;
using Project26.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Project26.ViewModels
{
    internal partial class MainPageVM :Models.ObservableObject
    {
        private Chat? _selectedChat;
        private readonly Chats chats = new();
        public ICommand AddChatCommand => new Command(AddChat);
        public ObservableCollection<Chat> ChatsList => chats.ChatsList;
        public Chat? SelectedChat 
        {  
            get => _selectedChat;
            set
            {
                _selectedChat = value;
                _selectedChat!.IsMeHost = false;
                OpenChatPage(_selectedChat);
            }
        }

        private static void OpenChatPage(Chat? chat)
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                Shell.Current.Navigation.PushAsync(new ChatPage(chat!), true);
            });
        }

        private void AddChat()
        {
            chats.AddChat();
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
            chats.ChatAdded += OnChatAdded;
        }

        private void OnChatAdded(object? sender, ChatArgs e)
        {
            OpenChatPage(e.Chat);
        }

        private void OnChatsChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(ChatsList));
        }
    }
}
