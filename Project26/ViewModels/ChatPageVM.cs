using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Project26.General;
using Project26.Views;
using System.Windows.Input;

namespace Project26.ViewModels
{
    internal class ChatPageVM
    {
        private readonly ModelsLogic.Chat chat;
        public ICommand SendMessageCommand { get; } = new Command(SendMessage);

        private static void SendMessage()
        {

        }

        internal void AddSnapshotListener()
        {
            chat.AddSnapshotListener();
        }

        internal void RemoveSnapshotListener()
        {
            chat.RemoveSnapshotListener();
        }

        public string GuestName => chat.GuestName!;
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
        }

        private void OnChatDeleted(object? sender, EventArgs e)
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                Toast.Make( Strings.ChatDeleted, ToastDuration.Long, 14).Show();
            });
        }
    }
}
