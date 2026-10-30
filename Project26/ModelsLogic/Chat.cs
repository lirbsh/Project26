using Plugin.CloudFirestore;
using Project26.EventsArgs;
using Project26.General;
using Project26.Models;

namespace Project26.ModelsLogic
{
    internal class Chat : ChatModel
    {
        public override bool IsMeHost 
        {
            get => _isMeHost;
            set
            { 
                _isMeHost = value;
                if (!IsMeHost)
                {
                    GuestName = user.Name;
                    IsFull = true;
                    Message.IsHostMessage = false;
                    Dictionary<string, object> d = new()
                    {
                        { nameof(GuestName), GuestName! },
                        { nameof(IsFull), IsFull }
                    };
                    fbd.UpdateFields(Keys.CollectionKey, Id!, d, OnStartChatComplete);
                }
            } 
        }
        protected override void OnStartChatComplete(Task task)
        {
            if (task.IsCompletedSuccessfully)
                ChatStarted?.Invoke(this, EventArgs.Empty);
        }
        public override void SendMessage(Action<Task> OnComplete)
        {
            IsHostTurn = !IsHostTurn;
            Dictionary<string, object> d = new() 
            { 
                { nameof(Message), Message },
                { nameof(IsHostTurn), IsHostTurn }
            };
            fbd.UpdateFields(Keys.CollectionKey, Id!, d, OnComplete);
        }
        public override string SetFbDocument(Action<Task> OnComplete)
        {
            return fbd.SetDocument(this, Keys.CollectionKey, Id, OnComplete);
        }
        public override void AddSnapshotListener()
        {
            fbd.ilr = fbd.AddSnapshotListener(Keys.CollectionKey, Id!, OnChange);
        }
        protected override void OnChange(IDocumentSnapshot? snapshot, Exception? error)
        {
            if (snapshot != null && snapshot.Data != null)
            {
                Chat c = snapshot?.ToObject<Chat>()!;
                GuestName = c.GuestName;
                IsHostTurn = c.IsHostTurn;
                if (IsMeHost && !c.Message.IsHostMessage || !IsMeHost && c.Message.IsHostMessage)
                {
                    ReceivedMessage = c.Message.Text;
                    ReceivedMessageBold = c.Message.IsBold;
                    ReceivedMessageColor = c.Message.Color;
                }
                string? status = IsMyTurn? Strings.PleaseSend: Strings.PleaseWait;
                ChatChanged?.Invoke(this, new ChatChangedArgs(status));
            } 
            else 
                ChatDeleted?.Invoke(this, new DeleteArgs(isMyDelete: false));
        }
        public override void RemoveSnapshotListener()
        {
            fbd.RemoveSnapshotListener();
            fbd.DeleteDocument(Keys.CollectionKey, Id!, OnDeleteComplete);
        }
        protected override void OnDeleteComplete(Task task)
        {
            if(task.IsCompletedSuccessfully)
                ChatDeleted?.Invoke(this, new DeleteArgs(isMyDelete: true));
        }
    }
}