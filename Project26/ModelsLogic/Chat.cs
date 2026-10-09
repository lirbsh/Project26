using Plugin.CloudFirestore;
using Project26.General;
using Project26.Models;

namespace Project26.ModelsLogic
{
    internal class Chat : Models.ChatModel
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
                    Dictionary<string, object> d = new()
                    {
                        { nameof(GuestName), GuestName! },
                        { nameof(IsFull), IsFull }
                    };
                    fbd.UpdateFields(Keys.CollectionKey, Id!, d, OnUpdateComplete);

                }
            } 
        }

        private void OnUpdateComplete(Task task)
        {
            
        }

        public override void SendMessage(Action<Task> OnComplete)
        {
            
        }

        public override string SetFbDocument(Action<Task> OnComplete)
        {
            return fbd.SetDocument(this, Keys.CollectionKey, Id, OnComplete);
        }
        public override void AddSnapshotListener()
        {
            fbd.ilr = fbd.AddSnapshotListener(Keys.CollectionKey, Id!, OnChange);
        }

        private void OnChange(IDocumentSnapshot? snapshot, Exception? error)
        {
            if(snapshot!= null && snapshot.Data != null) 
            {
                // Handle the updated data
            } else 
            {
                ChatDeleted?.Invoke(this, new DeleteArgs(isMyDelete: false));
            }
        }

        public override void RemoveSnapshotListener()
        {
            fbd.RemoveSnapshotListener();
            fbd.DeleteDocument(Keys.CollectionKey, Id!, OnComplete);
        }

        private void OnComplete(Task task)
        {
            if(task.IsCompletedSuccessfully)
                ChatDeleted?.Invoke(this, new DeleteArgs(isMyDelete: true));
        }
    }
}
