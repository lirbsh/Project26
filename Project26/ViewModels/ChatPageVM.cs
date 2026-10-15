using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project26.General;
using Project26.Models;

namespace Project26.ViewModels
{
    internal partial class ChatPageVM : ObservableObject
    {
        private readonly ModelsLogic.Chat chat;
        [RelayCommand]
        private static void SendMessage()
        {
               Toast.Make("MessageSent", ToastDuration.Short, 14).Show();
        }

        internal void AddSnapshotListener()
        {
            chat.AddSnapshotListener();
        }

        internal void RemoveSnapshotListener()
        {
            chat.RemoveSnapshotListener();
        }

        public string OpponentName => chat.IsMeHost ? chat.GuestName! : chat.HostName!;
        public string ReceivedMessage => chat.Message!;
        public string Message
        {
            get => chat.Message!;
            set => chat.Message = value;
        }
        public ChatPageVM(ModelsLogic.Chat chat)
        {
            this.chat = chat;
            chat.ChatDeleted += OnChatDeleted;
            chat.ChatChanged += OnChatChanged;
        }

        private void OnChatChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(OpponentName));
        }

        private void OnChatDeleted(object? sender, DeleteArgs e)
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                if(e.IsMyDelete)
                    Toast.Make(Strings.ChatDeleted, ToastDuration.Long, 14).Show();
                else
                    Shell.Current.Navigation.PopAsync();
            });
        }
    }
}
