using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project26.EventsArgs;
using Project26.ModelsLogic;
using Project26.Views;
using System.Collections.ObjectModel;

namespace Project26.ViewModels
{
    internal partial class MainPageVM :ObservableObject
    {
        private Chat? _selectedChat;
        private readonly Chats chats = new();
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

        [RelayCommand(CanExecute = nameof(CanAddChat))]
        private void AddChat()
        {
            chats.AddChat();
        }
        private bool CanAddChat()
        {
            return chats.CanAddChat;
        }
        public void AddSnapshotListener()
        {
            chats.AddSnapshotListener();
            AddChatCommand.NotifyCanExecuteChanged();
        }
        public void RemoveSnapshotListener()
        {
            chats.RemoveSnapshotListener();
        }
        public MainPageVM()
        {
            chats.ChatsChanged += OnChatsChanged;
            chats.ChatAdded += OnChatAdded;
            chats.StatusChanged += OnStatusChanged;
        }
        private void OnStatusChanged(object? sender, EventArgs e)
        {
            AddChatCommand.NotifyCanExecuteChanged();
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