using Plugin.CloudFirestore;
using Project26.General;

namespace Project26.ModelsLogic
{
    internal class Chats : Models.ChatsModel
    {
        public override void AddChat(Action<Task> OnComplete)
        {
            Chat c = new(); 
        }
        public override void AddSnapshotListener()
        {
            fbd.ilr = fbd.AddSnapshotListener(Keys.CollectionKey, OnChange);
        }

        private void OnChange(IQuerySnapshot? snapshot, Exception? error)
        {
            fbd.GetDocumentsWhereEqualTo(Keys.CollectionKey,nameof(Chat.IsFull), false, OnComplete);
        }
        protected override void OnComplete(IQuerySnapshot qs)
        {
            ChatsList.Clear();
            foreach (IDocumentSnapshot ds in qs.Documents)
            {
                Chat? chat = ds.ToObject<Chat>();
                if (chat != null)
                {
                    chat.Id = ds.Id;
                    ChatsList.Add(chat);
                }
            }
            ChatsChanged?.Invoke(this, EventArgs.Empty);    
        }
        public override void RemoveSnapshotListener()
        {
            fbd.RemoveSnapshotListener();
        }
    }
}
