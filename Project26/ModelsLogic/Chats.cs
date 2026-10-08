using Plugin.CloudFirestore;
using Project26.General;
using Project26.Models;

namespace Project26.ModelsLogic
{
    internal class Chats : Models.ChatsModel
    {
        public override void AddChat()
        {
            newChat = new() { HostName = user.Name };
            newChat.Id = newChat.SetFbDocument(OnComplete);
        }

        private void OnComplete(Task task)
        {
            ChatAdded?.Invoke(this, new ChatArgs(newChat!, task.IsCompletedSuccessfully));
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

        protected override void OnComplete(Action<Task> task)
        {
            throw new NotImplementedException();
        }
    }
}
