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
        [RelayCommand(CanExecute = nameof(CanSendMessage))]
        private void SendMessage()
        {
               chat.SendMessage(OnSendComplete);
               SendMessageCommand.NotifyCanExecuteChanged();
        }

        private void OnSendComplete(Task task)
        {
            if(task.IsFaulted) 
                Toast.Make(Strings.MessageNotSent, ToastDuration.Long, 14).Show();
            SendMessageCommand.NotifyCanExecuteChanged();
        }

        private bool CanSendMessage()
        {
            return chat.IsMyTurn;
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
        public string ReceivedMessage => chat.ReceivedMessage!;
        public string Message
        {
            get => chat.Message!;
            set => chat.Message = value;
        }
        public bool SendBoldMessage
        {
            get => chat.SendBoldMessage;
            set => chat.SendBoldMessage = value;
        }
        public bool ReceivedMessageBold => chat.ReceivedMessageBold;
        public ChatPageVM(ModelsLogic.Chat chat)
        {
            this.chat = chat;
            chat.ChatStarted += OnChatStarted;
            chat.ChatChanged += OnChatChanged;
            chat.ChatDeleted += OnChatDeleted;
        }

        private void OnChatStarted(object? sender, EventArgs e)
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                Toast.Make(Strings.ChatStarted, ToastDuration.Long, 14).Show();
            });
        }

        private void OnChatChanged(object? sender, ChatChangedArgs e)
        {
            OnPropertyChanged(nameof(OpponentName));
            OnPropertyChanged(nameof(ReceivedMessage));
            OnPropertyChanged(nameof(ReceivedMessageBold));
            SendMessageCommand.NotifyCanExecuteChanged();
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                Toast.Make(e.Message, ToastDuration.Long, 14).Show();
            });
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
