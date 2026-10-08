using Plugin.CloudFirestore;
using Project26.General;

namespace Project26.ModelsLogic
{
    internal class Chat : Models.ChatModel
    {
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
            
        }

        public override void RemoveSnapshotListener()
        {
            fbd.RemoveSnapshotListener();
            fbd.DeleteDocument(Keys.CollectionKey, Id!, OnComplete);
        }

        private void OnComplete(Task task)
        {
            if(task.IsCompletedSuccessfully)
                ChatDeleted?.Invoke(this, EventArgs.Empty);
        }
    }
}
