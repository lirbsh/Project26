using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Project26.General;
using Project26.Models;
using Project26.ModelsLogic;
using Project26.Views;
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
            string msg = e.Created ? Strings.ChatCreated : Strings.ChatCreationFailed;
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                Toast.Make(msg + Strings.NewLine + e.Chat.Id, ToastDuration.Long, 14).Show();
                Shell.Current.Navigation.PushAsync(new ChatPage(e.Chat), true);
            });
           
          
        }

        private void OnChatsChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(ChatsList));
        }
    }
}
