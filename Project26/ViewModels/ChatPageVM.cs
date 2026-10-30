using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Project26.EventsArgs;
using Project26.General;
using Project26.ModelsLogic;

namespace Project26.ViewModels
{
    internal partial class ChatPageVM : ObservableObject
    {
        private readonly ColorsGrid colorsGrid;
        private readonly ModelsLogic.Chat chat;
        [RelayCommand(CanExecute = nameof(CanSendMessage))]
        private void SendMessage()
        {
               chat.SendMessage(OnSendComplete);
               SendMessageCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand]
        private static void QuitChat()
        {
            Shell.Current.Navigation.PopAsync();
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
        public Color SendMessageColor => colorsGrid.SelectedColor;
        public string Message
        {
            get => chat.Message.Text;
            set => chat.Message.Text = value;
        }
        public bool SendBoldMessage
        {
            get => chat.Message.IsBold;
            set
            {
                chat.Message.IsBold = value;
                OnPropertyChanged(nameof(SendBoldMessage));
            }
        }
        public bool ReceivedMessageBold => chat.ReceivedMessageBold;
        public Color ReceivedMessageColor => chat.ReceivedMessageColor;
        public ChatPageVM(ModelsLogic.Chat chat, Grid grdColors)
        {
            colorsGrid = new ColorsGrid(grdColors);
            colorsGrid.ColorSelected += OnColorSelected;
            this.chat = chat;
            chat.ChatStarted += OnChatStarted;
            chat.ChatChanged += OnChatChanged;
            chat.ChatDeleted += OnChatDeleted;
        }

        private void OnColorSelected(object? sender, EventArgs e)
        {
            chat.Message.Color = colorsGrid.SelectedColor;
            OnPropertyChanged(nameof(SendMessageColor));
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
            OnPropertyChanged(nameof(ReceivedMessageColor));
            SendMessageCommand.NotifyCanExecuteChanged();
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                Toast.Make(e.Status, ToastDuration.Long, 14).Show();
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